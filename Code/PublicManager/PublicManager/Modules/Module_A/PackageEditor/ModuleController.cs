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
            tc.Dock = DockStyle.Fill;
            DisplayControl.Controls.Add(tc);

            tc.updateCatalogs();
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
                                        df.FormClosed += df_FormClosed;
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

        /// <summary>
        /// 修改插件显示界面
        /// </summary>
        /// <param name="df"></param>
        private void modifyPluginDisplayForm(PublicReporter.DisplayForm df)
        {
            if (PublicReporterLib.PluginLoader.CurrentPlugin != null)
            {
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
            }
        }

        void df_FormClosed(object sender, FormClosedEventArgs e)
        {
            tc.updateCatalogs();
        }
    }
}