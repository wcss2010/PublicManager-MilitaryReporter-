using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB;
using System.IO;
using PublicManager.DB.Entitys;
using NPOI.SS.Util;
using SuperCodeFactoryUILib.Forms;
using PublicManager.Modules.Module_A.PkgImporter;

namespace PublicManager.Modules.Module_A.PackageEditor
{
    public partial class ModuleController : BaseModuleController
    {
        private List<string> dutyUnitToProfessonLinks;
        /// <summary>
        /// 责任单位(有类别的)
        /// </summary>
        public List<string> DutyUnitToProfessonLinks
        {
            get { return dutyUnitToProfessonLinks; }
        }

        private MainView tc;
        private string decompressDir;

        public ModuleController()
        {
            InitializeComponent();
        }

        public override DevExpress.XtraBars.Ribbon.RibbonPage[] getTopBarPages()
        {
            return new DevExpress.XtraBars.Ribbon.RibbonPage[] { rpMaster };
        }

        public override void start()
        {
            //显示详细页
            showDetailPage();

            //加载责任单位与专业类型映射选项
            if (MainConfig.Config.ObjectDict.ContainsKey("责任单位与专业类别映射"))
            {
                try
                {
                    dutyUnitToProfessonLinks = new List<string>();
                    Newtonsoft.Json.Linq.JArray teams = (Newtonsoft.Json.Linq.JArray)MainConfig.Config.ObjectDict["责任单位与专业类别映射"];
                    foreach (string s in teams)
                    {
                        dutyUnitToProfessonLinks.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 显示详细页
        /// </summary>
        private void showDetailPage()
        {
            DisplayControl.Controls.Clear();
            tc = new MainView();
            tc.OnViewProject += tc_OnViewProject;
            tc.Dock = DockStyle.Fill;
            DisplayControl.Controls.Add(tc);

            tc.updateCatalogs();
        }

        void tc_OnViewProject(object sender, ViewProjectEventArgs args)
        {
            btnReportEdit.PerformClick();
        }

        public override void stop()
        {
            base.stop();
        }

        private void btnReportEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Project projectObj = tc.getCurrentProject();
            if (projectObj != null)
            {
                Catalog catalogObj = ConnectionManager.Context.table("Catalog").where("CatalogID='" + projectObj.CatalogID + "'").select("*").getItem<Catalog>(new Catalog());
                if (catalogObj != null && File.Exists(catalogObj.ZipPath))
                {
                    string zipFile = catalogObj.ZipPath;

                    btnReportEdit.Enabled = false;

                    CircleProgressBarDialog dialogb = new CircleProgressBarDialog();
                    dialogb.TransparencyKey = dialogb.BackColor;
                    dialogb.ProgressBar.ForeColor = Color.Red;
                    dialogb.MessageLabel.ForeColor = Color.Blue;
                    dialogb.FormBorderStyle = FormBorderStyle.None;
                    dialogb.Start(new EventHandler<CircleProgressBarEventArgs>(delegate(object thisObject, CircleProgressBarEventArgs argss)
                    {
                        CircleProgressBarDialog senderForm = ((CircleProgressBarDialog)thisObject);

                        //插件目录
                        string pluginDir = Path.Combine(PublicReporter.DisplayForm.PluginWorkDir, "ProjectMilitaryTechnologPlanPlugin");

                        //判断插件是否存在
                        if (Directory.Exists(pluginDir) && File.Exists(Path.Combine(pluginDir, "ProjectMilitaryTechnologPlanPlugin.dll")))
                        {
                            senderForm.ReportProgress(30, 100);

                            #region 尝试关闭Sqlite数据库连接
                            try
                            {
                                dynamic script = CSScriptLibrary.CSScript.LoadCode(
                                       @"using System.Windows.Forms;
                             public class Script
                             {
                                 public void CloseDB()
                                 {
                                     ProjectMilitaryTechnologPlanPlugin.DB.ConnectionManager.Close();
                                 }
                             }")
                                         .CreateObject("*");
                                script.CloseDB();
                            }
                            catch (Exception ex) { }
                            #endregion

                            senderForm.ReportProgress(60, 100);

                            #region 导入数据
                            string currentPath = Path.Combine(Path.Combine(pluginDir, "Data"), "Current");
                            try
                            {
                                if (Directory.Exists(currentPath))
                                {
                                    Directory.Delete(currentPath, true);
                                }
                            }
                            catch (Exception ex) { }
                            try
                            {
                                Directory.CreateDirectory(currentPath);
                            }
                            catch (Exception ex) { }
                            try
                            {
                                new PublicReporterLib.Utility.ZipUtil().UnZipFile(zipFile, currentPath, string.Empty, true);
                            }
                            catch (Exception ex) { }
                            #endregion

                            senderForm.ReportProgress(90, 100);

                            #region 显示填报插件窗体
                            if (senderForm.IsHandleCreated)
                            {
                                senderForm.Invoke(new MethodInvoker(delegate()
                                {
                                    try
                                    {
                                        //创建插件界面
                                        PublicReporter.DisplayForm df = new PublicReporter.DisplayForm();
                                        df.DestZipPath = zipFile;
                                        df.FormClosed += df_FormClosed;
                                        df.OnExportComplete += df_OnExportComplete;
                                        df.loadPlugin(pluginDir);

                                        //修改显示界面
                                        modifyPluginDisplayForm(df);

                                        //显示
                                        df.Show();
                                        df.BringToFront();
                                        df.WindowState = FormWindowState.Maximized;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("对不起，填报系统启动失败！Ex:" + ex.ToString());
                                    }

                                    btnReportEdit.Enabled = true;
                                }));
                            }
                            else
                            {
                                btnReportEdit.Enabled = true;
                            }
                            #endregion

                            senderForm.ReportProgress(99, 100);
                        }
                        else
                        {
                            btnReportEdit.Enabled = true;
                            MessageBox.Show("对不起，没有找到填报插件！");
                        }
                    }));
                }
            }
        }

        void df_OnExportComplete(object sender, PublicReporter.ExportCompleteEventArgs args)
        {
            if (MainConfig.Config.StringDict.ContainsKey("论证报告解压目录"))
            {
                decompressDir = MainConfig.Config.StringDict["论证报告解压目录"];

                if (File.Exists(args.DestZipFile))
                {
                    try
                    {
                        string zipFile = args.DestZipFile;
                        string zipName = Path.GetFileNameWithoutExtension(args.DestZipFile);

                        ProgressForm pf = new ProgressForm();
                        pf.Show();
                        pf.run(5, 0, new EventHandler(delegate(object senders, EventArgs ee)
                        {
                            pf.reportProgress(1, zipName + "_开始解压");
                            bool returnContent = unZipFile(zipFile, zipName);
                            if (returnContent)
                            {
                                //报告进度
                                pf.reportProgress(2, zipName + "_开始导入");
                                BaseModuleMainForm.writeLog("开始导入__" + zipName);

                                //导入数据库
                                new DBImporter().addOrReplaceProject(zipFile, zipName, Path.Combine(Path.Combine(decompressDir, zipName), "static.db"));

                                //报告进度
                                pf.reportProgress(3, zipName + "_结束导入");
                                BaseModuleMainForm.writeLog("结束导入__" + zipName);
                            }
                            pf.reportProgress(4, zipName + "_结束解压");

                            //检查是否已创建句柄，并调用委托执行UI方法
                            if (pf.IsHandleCreated)
                            {
                                pf.Invoke(new MethodInvoker(delegate()
                                {
                                    try
                                    {
                                        //刷新Catalog列表
                                        tc.updateCatalogs();

                                        //关闭进度窗口
                                        pf.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        BaseModuleMainForm.writeLog(ex.ToString());
                                    }
                                }));
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导入失败！Ex:" + ex.ToString());
                    }
                }
            }
        }

        private bool unZipFile(string pkgZipFile, string zipName)
        {
            List<string> outList = new List<string>();
            //生成路径字段
            string unZipDir = System.IO.Path.Combine(decompressDir, zipName);
            //删除旧的目录
            try
            {
                Directory.Delete(unZipDir, true);
            }
            catch (Exception ex) { }
            //申报主文件夹创建
            Directory.CreateDirectory(unZipDir);

            BaseModuleMainForm.writeLog("开始解析__" + zipName);

            //判断是否存在申报包
            if (pkgZipFile != null && pkgZipFile.EndsWith(".zip"))
            {
                BaseModuleMainForm.writeLog("项目" + zipName + "的解包操作，开始ZIP文件解压");

                //解压这个包
                new ZipTool().UnZipFile(pkgZipFile, unZipDir, string.Empty, true);
                //校验文件信息
                string[] foldersValidata = MainConfig.Config.StringDict["报告验证_目录"].Split(',');
                int foldersLen = foldersValidata.Length;
                string[] filesValidata = MainConfig.Config.StringDict["报告验证_文件"].Split(',');
                int filesLen = filesValidata.Length;
                for (int i = 0; i < foldersLen; i++)
                {
                    if (!System.IO.Directory.Exists(Path.Combine(unZipDir, foldersValidata[i])))
                    {
                        BaseModuleMainForm.writeLog("项目" + zipName + "的解包操作，" + foldersValidata[i] + "文件夹不存在");
                        outList.Add(foldersValidata[i] + "文件夹 不存在");
                    }
                }
                for (int i = 0; i < filesLen; i++)
                {
                    if (!File.Exists(Path.Combine(unZipDir, filesValidata[i])))
                    {
                        BaseModuleMainForm.writeLog("项目" + zipName + "的解包操作，" + filesValidata[i] + "不存在");
                        outList.Add(filesValidata[i] + " 不存在");
                    }
                }
                BaseModuleMainForm.writeLog("项目" + zipName + "的解包操作，结束ZIP文件解压");
            }
            else
            {
                BaseModuleMainForm.writeLog("项目" + zipName + "没有找到ZIP文件");
                outList.Add("没有找到ZIP文件");
            }

            BaseModuleMainForm.writeLog("结束解析__" + zipName);
            if (outList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改插件显示界面
        /// </summary>
        /// <param name="df"></param>
        private void modifyPluginDisplayForm(PublicReporter.DisplayForm df)
        {
            if (PublicReporterLib.PluginLoader.CurrentPlugin != null)
            {
                //将用不到的按钮变成灰色
                foreach (ToolStripItem tsi in PublicReporterLib.PluginLoader.CurrentPlugin.Parent_TopToolStrip.Items)
                {
                    switch (tsi.Text)
                    {
                        case "项目管理":
                            tsi.Enabled = false;
                            break;
                        case "新建项目":
                            tsi.Enabled = false;
                            break;
                        case "导入数据包":
                            tsi.Enabled = false;
                            break;
                        case "导出数据包":
                            tsi.Enabled = false;
                            break;
                    }
                }

                //退出时不提示
                dynamic script = CSScriptLibrary.CSScript.LoadCode(
                           @"using System.Windows.Forms;
                             using System;
                             using System.Data;
                             using System.IO;
                             using System.Text;
                             using PublicReporterLib;
                             
                             public class Script
                             {
                                 public void check()
                                 {
                                     ProjectMilitaryTechnologPlanPlugin.PluginRoot rootObj = (ProjectMilitaryTechnologPlanPlugin.PluginRoot)PublicReporterLib.PluginLoader.CurrentPlugin;
                                     rootObj.enabledShowExitHint = false;
                                 }
                             }")
                             .CreateObject("*");
                script.check();
            }
        }

        void df_FormClosed(object sender, FormClosedEventArgs e)
        {
            tc.updateCatalogs();
        }
    }
}