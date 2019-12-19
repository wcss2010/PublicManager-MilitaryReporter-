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
            tc.Dock = DockStyle.Fill;
            DisplayControl.Controls.Add(tc);

            tc.updateCatalogs();
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
            if (tc.getCurrentProject() != null)
            {
                Project proj = tc.getCurrentProject();

                if (tc.getProjectType(proj.CatalogID).Contains("专项"))
                {
                    AddOrUpdateProjectForm ff = new AddOrUpdateProjectForm();
                    ff.ProjectName = proj.ProjectName;

                    foreach (object obj in ff.StudyTimeItems)
                    {
                        if (obj is ComboBoxObject<int>)
                        {
                            ComboBoxObject<int> itemObj = ((ComboBoxObject<int>)obj);
                            if (itemObj.Tag == proj.StudyTime)
                            {
                                ff.StudyTime = itemObj;
                                break;
                            }
                        }
                    }
                    ff.StudyMoney = proj.StudyMoney + "";
                    ff.DutyUnit = proj.DutyUnit;

                    if (ff.ShowDialog() == DialogResult.OK)
                    {
                        //删除旧的工程
                        new PublicManager.Modules.Reporter.DBImporter().deleteProject(proj.CatalogID);

                        Catalog catalog = new Catalog();
                        catalog.CatalogID = Guid.NewGuid().ToString();
                        catalog.CatalogNumber = "XXXXXXXXX专项项目XXXXXXXXX";
                        catalog.CatalogName = ff.ProjectName;
                        catalog.CatalogType = "论证报告书";
                        catalog.CatalogVersion = "v1.0";
                        catalog.copyTo(ConnectionManager.Context.table("Catalog")).insert();

                        Project newProj = new Project();
                        newProj.ProjectID = catalog.CatalogID;
                        newProj.CatalogID = catalog.CatalogID;
                        newProj.ProjectName = catalog.CatalogName;
                        newProj.StudyTime = ff.StudyTime != null ? ff.StudyTime.Tag : 0;
                        newProj.StudyMoney = decimal.Parse(ff.StudyMoney);
                        newProj.DutyUnit = ff.DutyUnit;
                        newProj.copyTo(ConnectionManager.Context.table("Project")).insert();

                        tc.updateCatalogs();
                    }
                }
                else
                {
                    MessageBox.Show("对不起，只能编辑专项项目！");
                }
            }
        }

        private void btnExportToExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExcelHelper.ExportToExcel(tc.exportToDataTable());
        }

        private void btnExportToPkg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}