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
                proj.StudyDest = "*专项项目*";
                proj.StudyContent = "*专项项目*";
                proj.WillResult = "*专项项目*";
                proj.ProjectSort = "专题活动";
                proj.NextUnit = "*专项项目*";
                proj.Memo = "其他" + MainConfig.rowFlag + "*专项项目*";
                proj.Worker = "*专项项目*";
                proj.WorkerCardID = "*专项项目*";
                proj.WorkerSex = "男";
                proj.WorkerNation = "*专项项目*";
                proj.WorkerBirthday = DateTime.Now;
                proj.WorkerTelephone = "*专项项目*";
                proj.WorkerMobilephone = "*专项项目*";
                proj.SectionJobCateGory = "*专项项目*";
                proj.AllStudyUnit = "*专项项目*";
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
                        newProj.StudyDest = "*专项项目*";
                        newProj.StudyContent = "*专项项目*";
                        newProj.WillResult = "*专项项目*";
                        newProj.ProjectSort = "专题活动";
                        newProj.NextUnit = "*专项项目*";
                        newProj.Memo = "其他" + MainConfig.rowFlag + "*专项项目*";
                        newProj.Worker = "*专项项目*";
                        newProj.WorkerCardID = "*专项项目*";
                        newProj.WorkerSex = "男";
                        newProj.WorkerNation = "*专项项目*";
                        newProj.WorkerBirthday = DateTime.Now;
                        newProj.WorkerTelephone = "*专项项目*";
                        newProj.WorkerMobilephone = "*专项项目*";
                        newProj.SectionJobCateGory = "*专项项目*";
                        newProj.AllStudyUnit = "*专项项目*";
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
            sfd.Filter = "*.xlsx|*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //输出的Excel路径
                    string excelFile = sfd.FileName;

                    //Excel数据
                    MemoryStream memoryStream = new MemoryStream();

                    //创建Workbook
                    NPOI.XSSF.UserModel.XSSFWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();

                    #region 设置Excel样式
                    //创建单元格设置对象(普通内容)
                    NPOI.SS.UserModel.ICellStyle cellStyleA = workbook.CreateCellStyle();
                    cellStyleA.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                    cellStyleA.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    cellStyleA.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyleA.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyleA.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyleA.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyleA.WrapText = true;

                    //创建单元格设置对象(普通内容)
                    NPOI.SS.UserModel.ICellStyle cellStyleB = workbook.CreateCellStyle();
                    cellStyleB.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    cellStyleB.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    cellStyleB.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyleB.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyleB.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyleB.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cellStyleB.WrapText = true;

                    //创建设置字体对象(内容字体)
                    NPOI.SS.UserModel.IFont fontA = workbook.CreateFont();
                    fontA.FontHeightInPoints = 16;//设置字体大小
                    fontA.FontName = "宋体";
                    cellStyleA.SetFont(fontA);

                    //创建设置字体对象(标题字体)
                    NPOI.SS.UserModel.IFont fontB = workbook.CreateFont();
                    fontB.FontHeightInPoints = 16;//设置字体大小
                    fontB.FontName = "宋体";
                    fontB.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                    cellStyleB.SetFont(fontB);
                    #endregion

                    //基本数据
                    DataTable dtBase = new DataTable();

                    #region 输出基本数据
                    //生成列
                    dtBase.Columns.Add("序号", typeof(string));
                    dtBase.Columns.Add("项目名称", typeof(string));
                    dtBase.Columns.Add("目标与内容", typeof(string));
                    dtBase.Columns.Add("预期成果", typeof(string));
                    dtBase.Columns.Add("周期", typeof(string));
                    dtBase.Columns.Add("经费概算", typeof(string));
                    dtBase.Columns.Add("项目类别", typeof(string));
                    dtBase.Columns.Add("责任单位", typeof(string));
                    dtBase.Columns.Add("备  注", typeof(string));

                    int indexxx = 0;
                    foreach (DataRow dr in tc.exportToDataTable().Rows)
                    {
                        indexxx++;

                        List<object> cells = new List<object>();

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains("*专项项目*"))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(indexxx.ToString());
                        }

                        cells.Add(dr["项目名称"] != null ? dr["项目名称"].ToString() : string.Empty);

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains("*专项项目*"))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            StringBuilder destAndContentString = new StringBuilder();
                            destAndContentString.Append("研究目标：");
                            destAndContentString.AppendLine(dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty);
                            destAndContentString.Append("研究内容：");
                            destAndContentString.Append(dr["研究内容"] != null ? dr["研究内容"].ToString() : string.Empty);
                            cells.Add(destAndContentString.ToString());
                        }

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains("*专项项目*"))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["预期成果"] != null ? dr["预期成果"].ToString() : string.Empty);
                        }

                        cells.Add(dr["周期"] != null ? dr["周期"].ToString() : string.Empty);
                        cells.Add(dr["经费概算"] != null ? dr["经费概算"].ToString() : string.Empty);

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains("*专项项目*"))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["项目类别"] != null ? dr["项目类别"].ToString() : string.Empty);
                        }

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains("*专项项目*"))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["责任单位"] != null ? dr["责任单位"].ToString() : string.Empty);
                        }

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains("*专项项目*"))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add((dr["备注"] != null ? dr["备注"].ToString() : string.Empty).Replace("其他:", string.Empty));
                        }

                        dtBase.Rows.Add(cells.ToArray());
                    }
                    #endregion

                    //写入基本数据
                    writeSheet(workbook, cellStyleA, cellStyleB, dtBase);

                    ISheet sheetObj = workbook.GetSheetAt(0);

                    for (int ff = 0; ff < 9; ff++)
                    {
                        sheetObj.AutoSizeColumn(ff, true);
                    }

                    //合并
                    for (int kk = 0; kk < sheetObj.PhysicalNumberOfRows; kk++)
                    {
                        IRow rowObj = sheetObj.GetRow(kk);

                        string projName = rowObj.Cells[1].StringCellValue;
                        if (string.IsNullOrEmpty(rowObj.Cells[0].StringCellValue))
                        {
                            sheetObj.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(kk, kk, 0, 2));
                            rowObj.Cells[0].SetCellValue(projName);
                        }
                    }

                    #region 输出文件并打开文件
                    //输出到流
                    workbook.Write(memoryStream);

                    //写Excel文件
                    File.WriteAllBytes(excelFile, memoryStream.ToArray());

                    //显示提示
                    MessageBox.Show("导出完成！路径：" + excelFile, "提示");

                    //打开Excel文件
                    System.Diagnostics.Process.Start(excelFile);
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show("对不起，导出失败！Ex:" + ex.ToString());
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
                                    tableTemp.set("YanJiuMuBiao", "*专项项目*");
                                    tableTemp.set("YanJiuNeiRong", "*专项项目*");
                                    tableTemp.set("YuQiChengGuo", "*专项项目*");
                                    tableTemp.set("YanJiuZhouQi", proj.StudyTime);
                                    tableTemp.set("JingFeiYuSuan", proj.StudyMoney);
                                    tableTemp.set("XiangMuLeiBie", "专题活动");
                                    tableTemp.set("ZeRenDanWei", proj.DutyUnit);
                                    tableTemp.set("XiaJiDanWei", "*专项项目*");
                                    tableTemp.set("BeiZhu", "其他" + MainConfig.rowFlag + "*专项项目*");
                                    tableTemp.set("QianTouRen", "*专项项目*");
                                    tableTemp.set("QianTouRenShenFenZheng", "*专项项目*");
                                    tableTemp.set("QianTouRenXingBie", "男");
                                    tableTemp.set("QianTouRenMinZu", "*专项项目*");
                                    tableTemp.set("QianTouRenShengRi", DateTime.Now);
                                    tableTemp.set("QianTouRenDianHua", "*专项项目*");
                                    tableTemp.set("QianTouRenShouJi", "*专项项目*");
                                    tableTemp.set("BuZhiBie", "*专项项目*");
                                    tableTemp.set("LianHeYanJiuDanWei", "*专项项目*");
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