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
                proj.StudyDest = "";
                proj.StudyContent = "";
                proj.WillResult = "";
                proj.ProjectSort = "";
                proj.NextUnit = "";
                proj.Memo = "";
                proj.Worker = "";
                proj.WorkerCardID = "";
                proj.WorkerSex = "男";
                proj.WorkerNation = "";
                proj.WorkerBirthday = DateTime.Now;
                proj.WorkerTelephone = "";
                proj.WorkerMobilephone = "";
                proj.SectionJobCateGory = "";
                proj.AllStudyUnit = "";
                proj.RequestMoney = 0;
                proj.TaskCompleteTime = DateTime.Now;
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
                                        MainForm.writeLog(ex.ToString());
                                    }
                                }));
                            }
                        }
                    }));
                }
            }
        }

        private void btnExportToPkg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tc.getCurrentProject() != null)
            {
                Project proj = tc.getCurrentProject();

                if (tc.getProjectType(proj.CatalogID).Contains("专项"))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "ZIP数据包|*.zip";
                    sfd.FileName = getExportName(proj);
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (MessageBox.Show("真的要导出吗?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            CircleProgressBarDialog dialoga = new CircleProgressBarDialog();
                            dialoga.TransparencyKey = dialoga.BackColor;
                            dialoga.ProgressBar.ForeColor = Color.Red;
                            dialoga.MessageLabel.ForeColor = Color.Blue;
                            dialoga.FormBorderStyle = FormBorderStyle.None;
                            dialoga.Start(new EventHandler<CircleProgressBarEventArgs>(delegate(object thisObject, CircleProgressBarEventArgs argss)
                            {
                                //当前窗体
                                CircleProgressBarDialog senderForm = ((CircleProgressBarDialog)thisObject);

                                senderForm.ReportProgress(10, 100);
                                senderForm.ReportInfo("准备导出...");
                                try { System.Threading.Thread.Sleep(1000); }
                                catch (Exception ex) { }

                                #region 准备环境
                                //数据文件
                                string tempDBFile = Path.Combine(Application.StartupPath, "mtp-static.db");

                                //目标目录
                                string destDir = Path.Combine(Application.StartupPath, Path.Combine("Temps", Guid.NewGuid().ToString()));
                                try
                                {
                                    Directory.CreateDirectory(destDir);
                                }
                                catch (Exception ex) { }
                                try
                                {
                                    Directory.CreateDirectory(Path.Combine(destDir, "Files"));
                                }
                                catch (Exception ex) { }
                                File.WriteAllText(Path.Combine(Path.Combine(destDir, "Files"), "temp.txt"), string.Empty);

                                //目标文件
                                string destDBFile = Path.Combine(destDir, "static.db");

                                //复制
                                File.Copy(tempDBFile, destDBFile, true);

                                #endregion

                                senderForm.ReportProgress(40, 100);
                                senderForm.ReportInfo("准备写入数据...");
                                try { System.Threading.Thread.Sleep(1000); }
                                catch (Exception ex) { }

                                #region 往临时DB中写入数据
                                //SQLite数据库工厂
                                System.Data.SQLite.SQLiteFactory factory = new System.Data.SQLite.SQLiteFactory();

                                //NDEY数据库连接
                                Noear.Weed.DbContext context = new Noear.Weed.DbContext("main", "Data Source = " + destDBFile, factory);
                                //是否在执入后执行查询（主要针对Sqlite）
                                context.IsSupportSelectIdentityAfterInsert = false;
                                //是否在Dispose后执行GC用于解决Dispose后无法删除的问题（主要针对Sqlite）
                                context.IsSupportGCAfterDispose = true;

                                try
                                {
                                    var tableTemp = context.table("JiBenXinXiBiao");
                                    tableTemp.set("BianHao", Guid.NewGuid().ToString());
                                    tableTemp.set("XiangMuMingCheng", proj.ProjectName);
                                    tableTemp.set("YanJiuMuBiao", "");
                                    tableTemp.set("YanJiuNeiRong", "");
                                    tableTemp.set("YuQiChengGuo", "");
                                    tableTemp.set("YanJiuZhouQi", proj.StudyTime);
                                    tableTemp.set("JingFeiYuSuan", proj.StudyMoney);
                                    tableTemp.set("XiangMuLeiBie", "");
                                    tableTemp.set("ZeRenDanWei", proj.DutyUnit);
                                    tableTemp.set("XiaJiDanWei", "");
                                    tableTemp.set("BeiZhu", "");
                                    tableTemp.set("QianTouRen", "");
                                    tableTemp.set("QianTouRenShenFenZheng", "");
                                    tableTemp.set("QianTouRenXingBie", "男");
                                    tableTemp.set("QianTouRenMinZu", "");
                                    tableTemp.set("QianTouRenShengRi", DateTime.Now);
                                    tableTemp.set("QianTouRenDianHua", "");
                                    tableTemp.set("QianTouRenShouJi", "");
                                    tableTemp.set("BuZhiBie", "");
                                    tableTemp.set("LianHeYanJiuDanWei", "");
                                    tableTemp.set("ShenQingJingFei", 0);
                                    tableTemp.set("JiHuaWanChengShiJian", DateTime.Now);
                                    tableTemp.set("ZhuangTai", 0);
                                    tableTemp.set("ModifyTime", DateTime.Now);
                                    tableTemp.insert();
                                }
                                catch (Exception ex)
                                {
                                    System.Console.WriteLine(ex.ToString());
                                }
                                finally
                                {
                                    factory.Dispose();
                                    context.Dispose();
                                }
                                #endregion

                                senderForm.ReportProgress(80, 100);
                                senderForm.ReportInfo("准备打包数据包...");
                                try { System.Threading.Thread.Sleep(1000); }
                                catch (Exception ex) { }

                                #region 打包
                                new ZipTool().ZipFileDirectory(destDir, sfd.FileName);
                                #endregion

                                senderForm.ReportProgress(99, 100);
                                senderForm.ReportInfo("导出完成...");
                                try { System.Threading.Thread.Sleep(1000); }
                                catch (Exception ex) { }
                            }));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("对不起，只能导出专项项目！");
                }
            }
        }

        private string getExportName(Project proj)
        {
            return proj.ProjectName + "-" + "专项项目" + ".zip";
        }
    }
}