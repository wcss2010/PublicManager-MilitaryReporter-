using Aspose.Words;
using SuperCodeFactoryUILib.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using Aspose.Words.Tables;

namespace PublicManager
{
    public class WordPrinter
    {
        /// <summary>
        /// 输出word内容
        /// </summary>
        /// <param name="progressDialog"></param>
        public static void wordOutput(ProgressForm progressDialog, DataTable source, int tableTempleteIndex, string destDocFile)
        {
            Report(progressDialog, 10, "准备Word...", 1000);

            if (source == null)
            {
                return;
            }

            //创建word文档
            WordDocument wd = new WordDocument();
            wd.WordDocBuilder.PageSetup.Orientation = Aspose.Words.Orientation.Landscape;

            //打开表格模板
            WordDocument tableTemplete = new WordDocument(Path.Combine(Application.StartupPath, Path.Combine("Templetes", "tables.docx")));
            NodeCollection ncTables = tableTemplete.WordDoc.GetChildNodes(NodeType.Table, true);

            if (tableTempleteIndex >= ncTables.Count)
            {
                return;
            }

            try
            {
                Report(progressDialog, 20, "准备数据...", 1000);

                #region 准备数据
                Table curTable = (Table)ncTables[tableTempleteIndex];
                DataTable dtData = new DataTable();
                if (curTable.GetText().StartsWith("类别"))
                {
                    #region 填充数据
                    //前头带类别（专业类别）的表
                    dtData.Columns.Add("类别", typeof(string));
                    dtData.Columns.Add("序号", typeof(string));
                    dtData.Columns.Add("项目名称", typeof(string));
                    dtData.Columns.Add("目标与内容", typeof(string));
                    dtData.Columns.Add("预期成果", typeof(string));
                    dtData.Columns.Add("周期", typeof(string));
                    dtData.Columns.Add("经费概算", typeof(string));
                    dtData.Columns.Add("项目类别", typeof(string));
                    dtData.Columns.Add("责任单位", typeof(string));
                    dtData.Columns.Add("备  注", typeof(string));

                    string lastPSort = string.Empty;
                    int dirIndex = 0;
                    int itemIndex = 0;
                    foreach (DataRow dr in source.Rows)
                    {
                        List<object> cells = new List<object>();

                        string indexStr = string.Empty;
                        string professionSortStr = dr["专业类别"] != null ? dr["专业类别"].ToString() : string.Empty;
                        if (string.IsNullOrEmpty(professionSortStr))
                        {
                            professionSortStr = "其他";
                        }
                        if (professionSortStr == lastPSort)
                        {
                            itemIndex++;
                        }
                        else
                        {
                            lastPSort = professionSortStr;
                            dirIndex++;
                            itemIndex = 1;
                        }

                        indexStr = dirIndex + "." + itemIndex;

                        cells.Add(professionSortStr);
                        cells.Add(indexStr);
                        cells.Add(dr["项目名称"] != null ? dr["项目名称"].ToString() : string.Empty);

                        StringBuilder destAndContentString = new StringBuilder();
                        destAndContentString.Append("研究目标：");
                        destAndContentString.AppendLine(dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty);
                        destAndContentString.Append("研究内容：");
                        destAndContentString.Append(dr["研究内容"] != null ? dr["研究内容"].ToString() : string.Empty);
                        cells.Add(destAndContentString.ToString());

                        cells.Add(dr["预期成果"] != null ? dr["预期成果"].ToString() : string.Empty);
                        cells.Add(dr["周期"] != null ? dr["周期"].ToString() : string.Empty);
                        cells.Add(dr["经费概算"] != null ? dr["经费概算"].ToString() : string.Empty);
                        cells.Add(dr["项目类别"] != null ? dr["项目类别"].ToString() : string.Empty);
                        cells.Add(dr["责任单位"] != null ? dr["责任单位"].ToString() : string.Empty);
                        cells.Add((dr["备注"] != null ? dr["备注"].ToString() : string.Empty).Replace("其他:", string.Empty));

                        dtData.Rows.Add(cells.ToArray());
                    }
                    #endregion
                }
                else
                {
                    #region 填充数据
                    //前头不带类别（专业类别）的表
                    dtData.Columns.Add("序号", typeof(string));
                    dtData.Columns.Add("项目名称", typeof(string));
                    dtData.Columns.Add("目标与内容", typeof(string));
                    dtData.Columns.Add("预期成果", typeof(string));
                    dtData.Columns.Add("周期", typeof(string));
                    dtData.Columns.Add("经费概算", typeof(string));
                    dtData.Columns.Add("项目类别", typeof(string));
                    dtData.Columns.Add("责任单位", typeof(string));
                    dtData.Columns.Add("备  注", typeof(string));

                    int indexxx = 0;
                    foreach (DataRow dr in source.Rows)
                    {
                        indexxx++;

                        List<object> cells = new List<object>();

                        cells.Add(indexxx.ToString());
                        cells.Add(dr["项目名称"] != null ? dr["项目名称"].ToString() : string.Empty);

                        StringBuilder destAndContentString = new StringBuilder();
                        destAndContentString.Append("研究目标：");
                        destAndContentString.AppendLine(dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty);
                        destAndContentString.Append("研究内容：");
                        destAndContentString.Append(dr["研究内容"] != null ? dr["研究内容"].ToString() : string.Empty);
                        cells.Add(destAndContentString.ToString());

                        cells.Add(dr["预期成果"] != null ? dr["预期成果"].ToString() : string.Empty);
                        cells.Add(dr["周期"] != null ? dr["周期"].ToString() : string.Empty);
                        cells.Add(dr["经费概算"] != null ? dr["经费概算"].ToString() : string.Empty);
                        cells.Add(dr["项目类别"] != null ? dr["项目类别"].ToString() : string.Empty);
                        cells.Add(dr["责任单位"] != null ? dr["责任单位"].ToString() : string.Empty);
                        cells.Add((dr["备注"] != null ? dr["备注"].ToString() : string.Empty).Replace("其他:", string.Empty));

                        dtData.Rows.Add(cells.ToArray());
                    }
                    #endregion
                }
                #endregion

                Report(progressDialog, 60, "填充数据到表格...", 1000);

                #region 写表格
                wd.fillDataToTable(curTable, dtData);
                wd.WordDoc.FirstSection.Body.AppendChild(new NodeImporter(tableTemplete.WordDoc, wd.WordDoc, ImportFormatMode.UseDestinationStyles).ImportNode(curTable, true));
                #endregion
                
                #region 合并类别单元格
                int lastMergeStartIndex = -1;
                string lastMergeStartSort = string.Empty;

                Table nowTable = (Table)wd.WordDoc.GetChild(NodeType.Table, 0, true);
                if (nowTable.GetText().StartsWith("类别"))
                {
                    #region 带类别的表格
                    int rowIndex = 0;
                    foreach (DataRow dr in dtData.Rows)
                    {
                        if (dr["项目类别"] == null || dr["项目类别"] == "")
                        {
                            //需要合并
                            if ((nowTable.Rows[rowIndex + 1].Cells[2].ChildNodes.Count >= 1))
                            {
                                nowTable.Rows[rowIndex + 1].Cells[0].RemoveAllChildren();
                                foreach (Node node in nowTable.Rows[rowIndex + 1].Cells[2].ChildNodes)
                                {
                                    nowTable.Rows[rowIndex + 1].Cells[0].AppendChild(node.Clone(true));
                                }
                                nowTable.Rows[rowIndex + 1].Cells[2].RemoveAllChildren();
                            }
                            wd.mergeCells(nowTable.Rows[rowIndex + 1].Cells[0], nowTable.Rows[rowIndex + 1].Cells[3], nowTable);
                        }
                        else
                        {
                            string professionStr = dr["类别"] != null ? dr["类别"].ToString() : string.Empty;
                            if (professionStr == lastMergeStartSort)
                            {
                                //合并
                                wd.mergeCells(nowTable.Rows[lastMergeStartIndex].Cells[0], nowTable.Rows[rowIndex + 1].Cells[0], nowTable);
                            }
                            else
                            {
                                //记录开始
                                lastMergeStartSort = professionStr;
                                lastMergeStartIndex = rowIndex + 1;
                            }
                        }

                        rowIndex++;
                    }
                    #endregion
                }
                else
                {
                    #region 不带类别的表格
                    int rowIndex = 0;
                    foreach (DataRow dr in dtData.Rows)
                    {
                        if (dr["项目类别"] == null || dr["项目类别"] == "")
                        {
                            //需要合并
                            if ((nowTable.Rows[rowIndex + 1].Cells[1].ChildNodes.Count >= 1))
                            {
                                nowTable.Rows[rowIndex + 1].Cells[0].RemoveAllChildren();
                                foreach (Node node in nowTable.Rows[rowIndex + 1].Cells[1].ChildNodes)
                                {
                                    nowTable.Rows[rowIndex + 1].Cells[0].AppendChild(node.Clone(true));
                                }
                                nowTable.Rows[rowIndex + 1].Cells[1].RemoveAllChildren();
                            }
                            wd.mergeCells(nowTable.Rows[rowIndex + 1].Cells[0], nowTable.Rows[rowIndex + 1].Cells[2], nowTable);
                        }

                        rowIndex++;
                    }
                    #endregion
                }
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
}