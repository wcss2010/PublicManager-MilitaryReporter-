﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PublicManager.DB.Entitys;
using PublicManager.DB;
using NPOI.SS.Util;

namespace PublicManager.Modules.Module_B.DataExport
{
    public partial class ModuleController : BaseModuleController
    {
        List<string> allUnitList = new List<string>();

        private MainView tc;

        public ModuleController()
        {
            InitializeComponent();

            //加载责任单位选项
            if (MainConfig.Config.ObjectDict.ContainsKey("责任单位"))
            {
                try
                {
                    Newtonsoft.Json.Linq.JArray teams = (Newtonsoft.Json.Linq.JArray)MainConfig.Config.ObjectDict["责任单位"];
                    foreach (string s in teams)
                    {
                        allUnitList.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }
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

        private void btnExportToExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //数据源
            DataTable dtSource = new DataTable();

            #region 排序
            List<Project> tempProjectList = new List<Project>();
            foreach (string unit in allUnitList)
            {
                List<Project> unitWithAllProjectList = ConnectionManager.Context.table("Project").where("DutyUnit = '" + unit + "'").orderBy("ProfessionSort").select("*").getList<Project>(new Project());

                List<Professions> pfList = ConnectionManager.Context.table("Professions").select("*").getList<Professions>(new Professions());
                foreach (Professions prf in pfList)
                {
                    List<Project> projList = ConnectionManager.Context.table("Project").where("ProfessionID='" + prf.ProfessionID + "' and DutyUnit = '" + unit + "'").orderBy("ProfessionSort").select("*").getList<Project>(new Project());
                    if (projList != null && projList.Count >= 1)
                    {
                        tempProjectList.AddRange(projList);
                    }
                }

                foreach (Project projjjj in unitWithAllProjectList)
                {
                    bool needAdd = true;
                    foreach (Project curProj in tempProjectList)
                    {
                        if (projjjj.ProjectID == curProj.ProjectID)
                        {
                            needAdd = false;
                        }
                    }
                    if (needAdd)
                    {
                        tempProjectList.Add(projjjj);
                    }
                }
            }
            #endregion

            #region 创建数据源
            dtSource.Columns.Add("所属单位", typeof(string));
            dtSource.Columns.Add("类别", typeof(string));
            dtSource.Columns.Add("序号", typeof(string));
            dtSource.Columns.Add("项目名称", typeof(string));
            dtSource.Columns.Add("目标与内容", typeof(string));
            dtSource.Columns.Add("预期成果", typeof(string));
            dtSource.Columns.Add("周期", typeof(string));
            dtSource.Columns.Add("经费概算", typeof(string));
            dtSource.Columns.Add("项目类别", typeof(string));
            dtSource.Columns.Add("责任单位", typeof(string));
            dtSource.Columns.Add("备  注", typeof(string));

            string lastProfessionName = string.Empty;
            int aSort = 0;
            int bSort = 0;
            foreach (Project proj in tempProjectList)
            {
                string indexString = string.Empty;
                List<object> cells = new List<object>();

                //所属单位
                cells.Add(proj.DutyUnit);

                //类别
                string professionNameStr = ConnectionManager.Context.table("Professions").where("ProfessionID='" + proj.ProfessionID + "'").select("ProfessionName").getValue<string>(string.Empty);
                if (professionNameStr == lastProfessionName)
                {
                    bSort++;
                }
                else
                {
                    aSort++;
                    bSort = 1;
                    lastProfessionName = professionNameStr;
                }
                cells.Add(professionNameStr);

                //序号
                indexString = aSort + "." + bSort;
                cells.Add(indexString);

                //项目名称
                cells.Add(proj.ProjectName);

                //目标与内容
                if (proj.IsPrivateProject == "true")
                {
                    cells.Add("***合并这行***");
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("研究目标：").Append(proj.StudyDest).AppendLine();
                    sb.Append("研究内容：").Append(proj.StudyContent).AppendLine();
                    cells.Add(sb.ToString());
                }

                //预期成果
                cells.Add(proj.WillResult);

                //周期
                cells.Add(proj.StudyTime);

                //经费概算
                cells.Add(proj.StudyMoney);

                //项目类别
                cells.Add(proj.ProjectSort);

                //负责单位
                cells.Add(proj.DutyUnit);

                //备注
                cells.Add(proj.Memo);

                dtSource.Rows.Add(cells.ToArray());
            }

            #endregion

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = string.Empty;
            sfd.Filter = "*.xls|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
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

                    //写入基本数据
                    writeSheet(workbook, cellStyleA, cellStyleB, dtSource);

                    #region 合并专项项目列
                    //Excel数据
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);
                    for (int kk = 0; kk < sheet.PhysicalNumberOfRows; kk++)
                    {
                        NPOI.SS.UserModel.IRow rowObj = sheet.GetRow(kk);

                        if ((rowObj.GetCell(4).StringCellValue != null && rowObj.GetCell(4).StringCellValue.Contains("***合并这行***")))
                        {
                            rowObj.GetCell(1).SetCellValue(rowObj.GetCell(3).StringCellValue);
                            rowObj.GetCell(2).SetCellValue(string.Empty);
                            rowObj.GetCell(3).SetCellValue(string.Empty);
                            rowObj.GetCell(4).SetCellValue(string.Empty);
                            CellRangeAddress region = new CellRangeAddress(kk, kk, 1, 5);
                            sheet.AddMergedRegion(region);
                        }
                    }
                    #endregion

                    #region 检查合并第一列
                    //Excel数据
                    int lastSameRow = -1;
                    string lastSameName = "";
                    for (int kk = 0; kk < sheet.PhysicalNumberOfRows; kk++)
                    {
                        NPOI.SS.UserModel.IRow rowObj = sheet.GetRow(kk);

                        string curValue = rowObj.GetCell(0).StringCellValue;

                        if (curValue != lastSameName)
                        {
                            //合并
                            if (kk == 0)
                            {
                                lastSameName = curValue;
                                lastSameRow = 0;
                            }
                            else
                            {
                                CellRangeAddress regionss = new CellRangeAddress(lastSameRow, kk - 1, 0, 0);
                                sheet.AddMergedRegion(regionss);

                                lastSameName = curValue;
                                lastSameRow = kk;
                            }
                        }
                    }

                    CellRangeAddress regionbb = new CellRangeAddress(lastSameRow, sheet.PhysicalNumberOfRows - 1, 0, 0);
                    sheet.AddMergedRegion(regionbb);
                    #endregion

                    #region 检查合并第二列
                    //Excel数据
                    lastSameRow = -1;
                    lastSameName = "";
                    for (int kk = 0; kk < sheet.PhysicalNumberOfRows; kk++)
                    {
                        NPOI.SS.UserModel.IRow rowObj = sheet.GetRow(kk);

                        string curValue = rowObj.GetCell(1).StringCellValue;

                        if (curValue != lastSameName)
                        {
                            //合并
                            if (kk == 0)
                            {
                                lastSameName = curValue;
                                lastSameRow = 0;
                            }
                            else
                            {
                                CellRangeAddress regioncc = new CellRangeAddress(lastSameRow, kk - 1, 1, 1);
                                sheet.AddMergedRegion(regioncc);

                                lastSameName = curValue;
                                lastSameRow = kk;
                            }
                        }
                    }

                    CellRangeAddress regionsss = new CellRangeAddress(lastSameRow, sheet.PhysicalNumberOfRows - 1, 1, 1);
                    sheet.AddMergedRegion(regionsss);
                    #endregion

                    #region 输出文件
                    //输出到流
                    workbook.Write(memoryStream);

                    //写Excel文件
                    File.WriteAllBytes(sfd.FileName, memoryStream.ToArray());
                    #endregion

                    MessageBox.Show("生成完成！");
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("生成失败！Ex:" + ex.ToString());
                }
            }

        }

        private void btnExportToWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}