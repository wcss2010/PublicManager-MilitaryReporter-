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
                            ucd.TotalMoney = 0;

                            List<Project> projList = ConnectionManager.Context.table("Project").where("DutyUnit='" + s + "'").select("*").getList<Project>(new Project());
                            ucd.ProjectCount = projList.Count;

                            foreach (Project proj in projList)
                            {
                                ucd.TotalMoney += proj.StudyMoney;
                            }

                            countList.Add(ucd);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                    }
                }

                #endregion

                Report(progressDialog, 60, "填充数据到表格...", 1000);

                #region 写表格
                foreach (UnitCountData ucd in countList)
                {
                    foreach (Row r in curTable.Rows)
                    {
                        if (r.Cells[1].GetText() != null && r.Cells[1].GetText().Contains(ucd.UnitName))
                        {
                            wd.fillCell(true, r.Cells[2], wd.newParagraph(curTable.Document, ucd.ProjectCount + ""));

                            wd.fillCell(true, r.Cells[5], wd.newParagraph(curTable.Document, ucd.TotalMoney + ""));

                            wd.fillCell(true, r.Cells[10], wd.newParagraph(curTable.Document, ucd.TotalMoney + ""));
                        }
                    }
                }

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
        public string UnitName { get; set; }
        public int ProjectCount { get; set; }
        public decimal TotalMoney { get; set; }
    }
}