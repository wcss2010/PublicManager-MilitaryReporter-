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
using SuperCodeFactoryUILib.Forms;
using System.IO;
using Noear.Weed;
using NPOI.SS.UserModel;
using PublicManager.Modules.DataManager.Forms;

namespace PublicManager.Modules.Module_B.Manager
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
            AddOrUpdateProjectForm ff = new AddOrUpdateProjectForm();
            if (ff.ShowDialog() == DialogResult.OK)
            {
                Catalog catalog = new Catalog();
                catalog.CatalogID = Guid.NewGuid().ToString();
                catalog.CatalogNumber = "XXXXXXXXX专项项目XXXXXXXXX";
                catalog.CatalogName = ff.ProjectName;
                catalog.CatalogType = "论证报告书";
                catalog.CatalogVersion = "v1.0";
                catalog.ImportTime = DateTime.Now;
                catalog.copyTo(ConnectionManager.Context.table("Catalog")).insert();

                Project newProj = new Project();
                newProj.ProjectID = catalog.CatalogID;
                newProj.CatalogID = catalog.CatalogID;
                newProj.ProjectName = catalog.CatalogName;
                newProj.StudyTime = ff.StudyTime != null ? ff.StudyTime.Tag : 0;
                newProj.StudyMoney = decimal.Parse(ff.StudyMoney);
                newProj.DutyUnit = ff.DutyUnit;
                newProj.StudyDest = "";
                newProj.StudyContent = "";
                newProj.WillResult = "";
                newProj.ProjectSort = "";
                newProj.NextUnit = "";
                newProj.Memo = "";
                newProj.Worker = "";
                newProj.WorkerCardID = "";
                newProj.WorkerSex = "男";
                newProj.WorkerNation = "";
                newProj.WorkerBirthday = DateTime.Now;
                newProj.WorkerTelephone = "";
                newProj.WorkerMobilephone = "";
                newProj.SectionJobCateGory = "";
                newProj.AllStudyUnit = "";
                newProj.RequestMoney = 0;
                newProj.TaskCompleteTime = DateTime.Now;
                newProj.IsPrivateProject = "true";
                newProj.ProfessionSort = 0;
                newProj.copyTo(ConnectionManager.Context.table("Project")).insert();

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

                if (tc.getProjectType(proj).Contains("专项"))
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
                        catalog.ImportTime = DateTime.Now;
                        catalog.copyTo(ConnectionManager.Context.table("Catalog")).insert();

                        Project newProj = new Project();
                        newProj.ProjectID = catalog.CatalogID;
                        newProj.CatalogID = catalog.CatalogID;
                        newProj.ProjectName = catalog.CatalogName;
                        newProj.StudyTime = ff.StudyTime != null ? ff.StudyTime.Tag : 0;
                        newProj.StudyMoney = decimal.Parse(ff.StudyMoney);
                        newProj.DutyUnit = ff.DutyUnit;
                        newProj.StudyDest = "";
                        newProj.StudyContent = "";
                        newProj.WillResult = "";
                        newProj.ProjectSort = "";
                        newProj.NextUnit = "";
                        newProj.Memo = "";
                        newProj.Worker = "";
                        newProj.WorkerCardID = "";
                        newProj.WorkerSex = "男";
                        newProj.WorkerNation = "";
                        newProj.WorkerBirthday = DateTime.Now;
                        newProj.WorkerTelephone = "";
                        newProj.WorkerMobilephone = "";
                        newProj.SectionJobCateGory = "";
                        newProj.AllStudyUnit = "";
                        newProj.RequestMoney = 0;
                        newProj.TaskCompleteTime = DateTime.Now;
                        newProj.IsPrivateProject = "true";
                        newProj.ProfessionSort = 0;
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
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = string.Empty;
            sfd.Filter = "*.doc|*.doc";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DutyUnitForm duf = new DutyUnitForm();
                if (duf.ShowDialog() == DialogResult.OK)
                {
                    int tableIndex = 0;
                    if (duf.DutyUnitToProfessonLinks.Contains(duf.SelectedItem))
                    {
                        //有类别
                        tableIndex = 1;
                    }
                    else
                    {
                        //无类别
                        tableIndex = 0;
                    }

                    ProgressForm pf = new ProgressForm();
                    pf.Show();
                    pf.run(100, 0, new EventHandler(delegate(object senders, EventArgs ee)
                    {
                        ProgressForm senderForm = (ProgressForm)senders;

                        try
                        {
                            //输出的Excel路径
                            string excelFile = sfd.FileName;

                            //筛选责任单位
                            List<DataRow> delList = new List<DataRow>();
                            DataTable dtSource = tc.exportToDataTable();
                            foreach (DataRow dr in dtSource.Rows)
                            {
                                string dutyUnitStr = dr["责任单位"] != null ? dr["责任单位"].ToString() : string.Empty;
                                if (dutyUnitStr != duf.SelectedItem)
                                {
                                    delList.Add(dr);
                                }
                            }
                            foreach (DataRow drr in delList)
                            {
                                dtSource.Rows.Remove(drr);
                            }
                            WordPrinter.wordOutput(senderForm, dtSource, tableIndex, sfd.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("对不起，导出失败！Ex:" + ex.ToString());
                        }
                        finally
                        {
                            //检查是否已创建句柄，并调用委托执行UI方法
                            if (pf.IsHandleCreated)
                            {
                                pf.Invoke(new MethodInvoker(delegate()
                                {
                                    try
                                    {
                                        //关闭进度窗口
                                        pf.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        BaseModuleMainForm.writeLog(ex.ToString());
                                    }
                                }));
                            }
                        }
                    }));
                }
            }
        }

        private string getExportName(Project proj)
        {
            return proj.ProjectName + "-" + "专项项目" + ".zip";
        }

        private void btnSorts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProjectSortForm form = new ProjectSortForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                tc.updateCatalogs();
            }
        }
    }
}