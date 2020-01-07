﻿using Aspose.Words;
using SuperCodeFactoryUILib.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using Aspose.Words.Tables;
using PublicManager.DB.Entitys;
using PublicManager.DB;

namespace PublicManager.Modules.Module_B.DataExport
{
    public class WordPrinter
    {
        /// <summary>
        /// 输出word内容
        /// </summary>
        /// <param name="progressDialog"></param>
        public static void wordOutput(ProgressForm progressDialog, string destDocFile)
        {
            Report(progressDialog, 10, "准备Word...", 1000);

            //创建word文档
            WordDocument wd = new WordDocument();
            //wd.WordDocBuilder.PageSetup.Orientation = Aspose.Words.Orientation.Landscape;

            //打开表格模板
            WordDocument tableTemplete = new WordDocument(Path.Combine(Application.StartupPath, Path.Combine("Templetes", "tables.docx")));
            NodeCollection ncTables = tableTemplete.WordDoc.GetChildNodes(NodeType.Table, true);

            try
            {
                Report(progressDialog, 20, "准备数据...", 1000);

                List<UnitCountData> countList = new List<UnitCountData>();

                //合计
                UnitCountData countDataObj = new UnitCountData();
                countDataObj.UnitName = "小   计";
                countDataObj.ABCount = 0;
                countDataObj.ABMoney = 0;
                countDataObj.AllMoney = 0;
                countDataObj.PSortA = 0;
                countDataObj.PSortB = 0;

                #region 准备数据
                Table curTable = (Table)ncTables[ncTables.Count - 1];

                //加载责任单位选项
                if (MainConfig.Config.ObjectDict.ContainsKey("责任单位"))
                {
                    try
                    {
                        Newtonsoft.Json.Linq.JArray teams = (Newtonsoft.Json.Linq.JArray)MainConfig.Config.ObjectDict["责任单位"];
                        foreach (string s in teams)
                        {
                            UnitCountData ucd = new UnitCountData();
                            ucd.UnitName = s;
                            ucd.AllMoney = 0;
                            ucd.ABCount = 0;

                            StringBuilder psBuilder = new StringBuilder();
                            int psIndex = 0;

                            List<Project> projList = ConnectionManager.Context.table("Project").where("DutyUnit='" + s + "'").select("*").getList<Project>(new Project());
                                                        
                            foreach (Project proj in projList)
                            {
                                if (proj.IsPrivateProject == "true")
                                {
                                    psIndex++;
                                    psBuilder.Append(psIndex).Append(". ").Append(proj.ProjectName).Append(",").Append(proj.StudyMoney).AppendLine();
                                }

                                ucd.AllMoney += proj.StudyMoney;

                                if (proj.ProjectSort != null)
                                {
                                    if (proj.ProjectSort.Contains("重大"))
                                    {
                                        ucd.PSortA++;
                                        ucd.ABCount++;

                                        ucd.ABMoney += proj.StudyMoney;
                                    }
                                    else if (proj.ProjectSort.Contains("重点"))
                                    {
                                        ucd.PSortB++;
                                        ucd.ABCount++;

                                        ucd.ABMoney += proj.StudyMoney;
                                    }
                                }
                            }

                            ucd.PrivateProjectInfo = psBuilder.ToString();

                            countList.Add(ucd);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                    }
                }

                #endregion

                //合计
                countList.Add(countDataObj);

                Report(progressDialog, 60, "填充数据到表格...", 1000);

                #region 写表格
                foreach (UnitCountData ucd in countList)
                {
                    foreach (Row r in curTable.Rows)
                    {
                        try
                        {
                            string sss = r.Cells[0].GetText() + r.Cells[1].GetText();
                            if (sss.Contains(ucd.UnitName))
                            {
                                if (sss.Contains(countDataObj.UnitName))
                                {
                                    //输出合计
                                    wd.fillCell(true, r.Cells[1], wd.newParagraph(curTable.Document, ucd.ABCount + ""));
                                    wd.setFontInCell(r.Cells[1], "宋体", 9);

                                    wd.fillCell(true, r.Cells[2], wd.newParagraph(curTable.Document, ucd.PSortA + ""));
                                    wd.setFontInCell(r.Cells[2], "宋体", 9);

                                    wd.fillCell(true, r.Cells[3], wd.newParagraph(curTable.Document, ucd.PSortB + ""));
                                    wd.setFontInCell(r.Cells[3], "宋体", 9);

                                    wd.fillCell(true, r.Cells[4], wd.newParagraph(curTable.Document, ucd.ABMoney + ""));
                                    wd.setFontInCell(r.Cells[4], "宋体", 9);

                                    wd.fillCell(true, r.Cells[9], wd.newParagraph(curTable.Document, ucd.AllMoney + ""));
                                    wd.setFontInCell(r.Cells[9], "宋体", 9);
                                }
                                else
                                {
                                    countDataObj.ABCount += ucd.ABCount;
                                    countDataObj.PSortA += ucd.PSortA;
                                    countDataObj.PSortB += ucd.PSortB;
                                    countDataObj.ABMoney += ucd.ABMoney;
                                    countDataObj.AllMoney += ucd.AllMoney;

                                    //输出一般属性
                                    wd.fillCell(true, r.Cells[2], wd.newParagraph(curTable.Document, ucd.ABCount + ""));
                                    wd.setFontInCell(r.Cells[2], "宋体", 9);

                                    wd.fillCell(true, r.Cells[3], wd.newParagraph(curTable.Document, ucd.PSortA + ""));
                                    wd.setFontInCell(r.Cells[3], "宋体", 9);

                                    wd.fillCell(true, r.Cells[4], wd.newParagraph(curTable.Document, ucd.PSortB + ""));
                                    wd.setFontInCell(r.Cells[4], "宋体", 9);

                                    wd.fillCell(true, r.Cells[5], wd.newParagraph(curTable.Document, ucd.ABMoney + ""));
                                    wd.setFontInCell(r.Cells[5], "宋体", 9);

                                    r.Cells[7].CellFormat.FitText = true;
                                    r.Cells[7].CellFormat.WrapText = true;
                                    r.Cells[7].RemoveAllChildren();
                                    string[] psTeams = ucd.PrivateProjectInfo.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                                    foreach (string s in psTeams)
                                    {
                                        if (string.IsNullOrEmpty(s))
                                        {
                                            continue;
                                        }
                                        r.Cells[7].AppendChild(wd.newParagraph(curTable.Document, s));
                                    }                                    
                                    wd.setFontInCell(r.Cells[7], "宋体", 9);
                                    r.Cells[7].CellFormat.FitText = true;
                                    r.Cells[7].CellFormat.WrapText = true;
                                    r.RowFormat.Height = r.RowFormat.Height * r.Cells[7].ChildNodes.Count;

                                    wd.fillCell(true, r.Cells[10], wd.newParagraph(curTable.Document, ucd.AllMoney + ""));
                                    wd.setFontInCell(r.Cells[10], "宋体", 9);
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                curTable.AllowAutoFit = true;

                wd.WordDoc.FirstSection.Body.AppendChild(new NodeImporter(tableTemplete.WordDoc, wd.WordDoc, ImportFormatMode.UseDestinationStyles).ImportNode(curTable, true));
                #endregion
                
                Report(progressDialog, 90, "生成文档...", 1000);

                #region 显示文档或生成文档
                //保存word
                wd.WordDoc.Save(destDocFile, SaveFormat.Doc);

                //打开文件
                Process.Start(destDocFile);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            Report(progressDialog, 95, "", 1000);
        }

        /// <summary>
        /// 显示进度
        /// </summary>
        /// <param name="progressDialog"></param>
        /// <param name="progress"></param>
        /// <param name="txt"></param>
        /// <param name="sleepTime"></param>
        private static void Report(ProgressForm progressDialog, int progress, string txt, int sleepTime)
        {
            progressDialog.reportProgress(progress, txt);

            try
            {
                Thread.Sleep(sleepTime);
            }
            catch (Exception ex) { }
        }
    }

    public class UnitCountData
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public int ABCount { get; set; }

        /// <summary>
        /// "重大"项目数量
        /// </summary>
        public int PSortA { get; set; }

        /// <summary>
        /// "重点"项目数量
        /// </summary>
        public int PSortB { get; set; }

        /// <summary>
        /// 重大和重点项目经费
        /// </summary>
        public decimal ABMoney { get; set; }

        /// <summary>
        /// 安排总经费
        /// </summary>
        public decimal AllMoney { get; set; }

        /// <summary>
        /// 专项项目详细
        /// </summary>
        public string PrivateProjectInfo { get; set; }
    }
}