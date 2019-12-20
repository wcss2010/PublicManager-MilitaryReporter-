using Aspose.Words;
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

namespace PublicManager
{
    public class WordPrinter
    {
        /// <summary>
        /// 输出word内容
        /// </summary>
        /// <param name="progressDialog"></param>
        public static void wordOutput(ProgressForm progressDialog, DataTable dt, int tableTempleteIndex, string destDocFile)
        {
            Report(progressDialog, 10, "准备Word...", 1000);

            if (dt == null)
            {
                return;
            }

            //创建word文档
            WordDocument wd = new WordDocument();

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

                    int indexxx = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        indexxx++;

                        List<object> cells = new List<object>();

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["专业类别"] != null ? dr["专业类别"].ToString() : string.Empty);
                        }

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(indexxx.ToString());
                        }

                        cells.Add(dr["项目名称"] != null ? dr["项目名称"].ToString() : string.Empty);

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
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

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["预期成果"] != null ? dr["预期成果"].ToString() : string.Empty);
                        }

                        cells.Add(dr["周期"] != null ? dr["周期"].ToString() : string.Empty);
                        cells.Add(dr["经费概算"] != null ? dr["经费概算"].ToString() : string.Empty);

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["项目类别"] != null ? dr["项目类别"].ToString() : string.Empty);
                        }

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["责任单位"] != null ? dr["责任单位"].ToString() : string.Empty);
                        }

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add((dr["备注"] != null ? dr["备注"].ToString() : string.Empty).Replace("其他:", string.Empty));
                        }

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
                    foreach (DataRow dr in dt.Rows)
                    {
                        indexxx++;

                        List<object> cells = new List<object>();

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(indexxx.ToString());
                        }

                        cells.Add(dr["项目名称"] != null ? dr["项目名称"].ToString() : string.Empty);

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
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

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["预期成果"] != null ? dr["预期成果"].ToString() : string.Empty);
                        }

                        cells.Add(dr["周期"] != null ? dr["周期"].ToString() : string.Empty);
                        cells.Add(dr["经费概算"] != null ? dr["经费概算"].ToString() : string.Empty);

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["项目类别"] != null ? dr["项目类别"].ToString() : string.Empty);
                        }

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add(dr["责任单位"] != null ? dr["责任单位"].ToString() : string.Empty);
                        }

                        if ((dr["研究目标"] != null ? dr["研究目标"].ToString() : string.Empty).Contains(""))
                        {
                            cells.Add(string.Empty);
                        }
                        else
                        {
                            cells.Add((dr["备注"] != null ? dr["备注"].ToString() : string.Empty).Replace("其他:", string.Empty));
                        }

                        dtData.Rows.Add(cells.ToArray());
                    }
                    #endregion
                }
                                
                Report(progressDialog, 60, "填充数据到表格...", 1000);

                wd.fillDataToTable(curTable, dtData);

                wd.WordDoc.FirstSection.Body.AppendChild(new NodeImporter(tableTemplete.WordDoc, wd.WordDoc, ImportFormatMode.UseDestinationStyles).ImportNode(curTable, true));
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