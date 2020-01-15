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
            string zipFile = string.Empty;

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
                string pluginDir = Path.Combine(Application.StartupPath, Path.Combine("ReportPlugins", "ProjectMilitaryTechnologPlanPlugin"));

                //判断插件是否存在
                if (Directory.Exists(pluginDir) && File.Exists(Path.Combine(pluginDir, "ProjectMilitaryTechnologPlanPlugin.dll")))
                {
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
                    PublicReporterLib.Utility.ZipUtil zu = new PublicReporterLib.Utility.ZipUtil();
                    zu.UnZipFile(zipFile, currentPath, string.Empty, true);

                    #endregion

                    #region 显示填报插件窗体
                    if (IsHandleCreated)
                    {
                        Invoke(new MethodInvoker(delegate()
                        {
                            try
                            {
                                //创建并显示窗体
                                PublicReporter.DisplayForm df = new PublicReporter.DisplayForm();
                                df.FormClosed += df_FormClosed;
                                df.loadPlugin(pluginDir);
                                df.WindowState = FormWindowState.Maximized;
                                df.Show();
                                df.WindowState = FormWindowState.Maximized;

                                tc.updateCatalogs();
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
                }
                else
                {
                    MessageBox.Show("对不起，没有找到填报插件！");
                }
            }));
        }

        void df_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}