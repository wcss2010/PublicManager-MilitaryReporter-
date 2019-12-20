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
        public static void wordOutput(CircleProgressBarDialog progressDialog, DataTable dtBase, int tableTempleteIndex, string destDocFile)
        {
            Report(progressDialog, 10, "准备Word...", 1000);

            if (dtBase == null)
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
                Table curTable = (Table)ncTables[tableTempleteIndex];
                wd.WordDoc.AppendChild(curTable);

                DataTable dtData = new DataTable();
                if (curTable.GetText().StartsWith("类别"))
                {
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
                    dtData.Columns.Add("备注", typeof(string));
                }
                else
                {
                    //前头不带类别（专业类别）的表
                    dtData.Columns.Add("序号", typeof(string));
                    dtData.Columns.Add("项目名称", typeof(string));
                    dtData.Columns.Add("目标与内容", typeof(string));
                    dtData.Columns.Add("预期成果", typeof(string));
                    dtData.Columns.Add("周期", typeof(string));
                    dtData.Columns.Add("经费概算", typeof(string));
                    dtData.Columns.Add("项目类别", typeof(string));
                    dtData.Columns.Add("责任单位", typeof(string));
                    dtData.Columns.Add("备注", typeof(string));

                }

                Report(progressDialog, 20, "准备数据...", 1000);
                
                Report(progressDialog, 30, "写入基本信息...", 1000);
                
                Report(progressDialog, 40, "写入文档文件...", 1000);
                
                Report(progressDialog, 60, "写入表格数据...", 1000);
                
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
        private static void Report(CircleProgressBarDialog progressDialog, int progress, string txt, int sleepTime)
        {
            progressDialog.ReportProgress(progress, 100);
            progressDialog.ReportInfo(txt);
            try
            {
                Thread.Sleep(sleepTime);
            }
            catch (Exception ex) { }
        }
    }
}