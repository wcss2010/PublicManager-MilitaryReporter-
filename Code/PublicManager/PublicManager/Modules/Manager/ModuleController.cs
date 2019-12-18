using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.Modules.Manager.Forms;
using PublicManager.DB.Entitys;
using PublicManager.DB;

namespace PublicManager.Modules.Manager
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
            tc.updateCatalogs();
            tc.Dock = DockStyle.Fill;
            DisplayControl.Controls.Add(tc);
        }

        public override void stop()
        {
            base.stop();
        }

        private void btnAddProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddOrUpdateProjectForm form = new AddOrUpdateProjectForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Catalog catalog = new Catalog();
                catalog.CatalogID = Guid.NewGuid().ToString();
                catalog.CatalogNumber = "XXXXXXXXX专项项目XXXXXXXXX";
                catalog.CatalogName = form.ProjectName;
                catalog.CatalogType = "论证报告书";
                catalog.CatalogVersion = "v1.0";
                catalog.copyTo(ConnectionManager.Context.table("Catalog")).insert();

                Project proj = new Project();
                proj.ProjectID = catalog.CatalogID;
                proj.CatalogID = catalog.CatalogID;
                proj.ProjectName = catalog.CatalogName;
                proj.StudyTime = form.StudyTime != null ? form.StudyTime.Tag : 0;
                proj.StudyMoney = decimal.Parse(form.StudyMoney);
                proj.DutyUnit = form.DutyUnit;
                proj.copyTo(ConnectionManager.Context.table("Project")).insert();

                tc.updateCatalogs();
            }
        }

        private void btnDeleteProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnEditProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnExportToExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExcelHelper.ExportToExcel(tc.exportToDataTable());
        }
    }
}