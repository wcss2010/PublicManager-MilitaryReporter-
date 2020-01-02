using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB;
using PublicManager.DB.Entitys;
using System.IO;

namespace PublicManager.Modules.Module_B.PkgImporter
{
    public partial class ReporterModuleController : BaseModuleController
    {
        private string totalDir = "(未设置)";

        private string decompressDir = "(未设置)";
        private MainView tc;

        public ReporterModuleController()
        {
            InitializeComponent();

            PublicManager.Modules.Module_B.DictManager.MainView.initDicts();
        }

        public override DevExpress.XtraBars.Ribbon.RibbonPage[] getTopBarPages()
        {
            return new DevExpress.XtraBars.Ribbon.RibbonPage[] { rpMaster };
        }

        public override void start()
        {
            //显示详细页
            showDetailPage();

            //更新目录提示
            updateDirectoryHint();
        }

        /// <summary>
        /// 更新目录提示
        /// </summary>
        private void updateDirectoryHint()
        {
            if (MainConfig.Config.StringDict.ContainsKey("论证报告总目录"))
            {
                totalDir = MainConfig.Config.StringDict["论证报告总目录"];
            }

            if (MainConfig.Config.StringDict.ContainsKey("论证报告解压目录"))
            {
                decompressDir = MainConfig.Config.StringDict["论证报告解压目录"];
            }

            StatusLabelControl.Caption = "主目录:" + totalDir + ",解压目录:" + decompressDir;
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

        }

        private void btnSetSourceDir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fbdFolderSelect.SelectedPath = totalDir == "(未设置)" ? string.Empty : totalDir;
            if (fbdFolderSelect.ShowDialog() == DialogResult.OK)
            {
                if (fbdFolderSelect.SelectedPath == MainConfig.Config.StringDict["论证报告解压目录"])
                {
                    MessageBox.Show("对不起，总目录和解压目录不能是一个目录！");
                }
                else
                {
                    totalDir = fbdFolderSelect.SelectedPath;

                    MainConfig.Config.StringDict["论证报告总目录"] = totalDir;
                    MainConfig.saveConfig();
                }

                updateDirectoryHint();
            }
        }

        private void btnSetDestDir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fbdFolderSelect.SelectedPath = decompressDir == "(未设置)" ? string.Empty : decompressDir;
            if (fbdFolderSelect.ShowDialog() == DialogResult.OK)
            {
                if (fbdFolderSelect.SelectedPath == MainConfig.Config.StringDict["论证报告总目录"])
                {
                    MessageBox.Show("对不起，总目录和解压目录不能是一个目录！");
                }
                else
                {
                    decompressDir = fbdFolderSelect.SelectedPath;

                    MainConfig.Config.StringDict["论证报告解压目录"] = decompressDir;
                    MainConfig.saveConfig();
                }

                updateDirectoryHint();
            }
        }

        private void btnImportAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.ImporterWithAllForm ifm = new Forms.ImporterWithAllForm(tc, true, totalDir, decompressDir);
            ifm.ShowDialog();
        }

        private void btnOpenMasterDir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(MainConfig.Config.StringDict["论证报告总目录"]);
            }
            catch (Exception ex) { }
        }

        private void btnOpenDecompressDir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(MainConfig.Config.StringDict["论证报告解压目录"]);
            }
            catch (Exception ex) { }
        }

        private void btnImportSelected_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Forms.ImporterWithSelectedForm ifm = new Forms.ImporterWithSelectedForm(tc, false, totalDir, decompressDir);
            ifm.ShowDialog();
        }
    }
}