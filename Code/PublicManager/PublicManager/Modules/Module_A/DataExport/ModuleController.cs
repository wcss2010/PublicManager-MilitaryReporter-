using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB;
using System.IO;

namespace PublicManager.Modules.Module_A.DataExport
{
    public partial class ModuleController : BaseModuleController
    {
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

        private void btnExportToPkg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string dbFile = ConnectionManager.ConnectionString.Replace("Data Source=", string.Empty);
            if (sfdDB.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(dbFile, sfdDB.FileName, true);

                    MessageBox.Show("导出完成！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出失败！Ex:" + ex.ToString());
                }
            }
        }
    }
}