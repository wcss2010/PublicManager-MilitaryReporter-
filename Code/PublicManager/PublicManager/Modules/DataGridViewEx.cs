using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PublicManager.Modules
{
    public class DataGridViewEx : DataGridView
    {
        public DataGridViewEx() : base()
        {
            ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 10.5f);
            DefaultCellStyle.Font = new System.Drawing.Font("宋体", 10.5f);
            RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 10.5f);
            RowTemplate.Height = 30;
        }

        /// <summary>
        /// 检查单元格高度
        /// </summary>
        public void checkCellSize()
        {
            //原理是找出所有的列中最长的字符串然后计算行高
            if (Width > 2)
            {
                //单字大小
                System.Drawing.SizeF wordSize = SizeF.Empty;
                //Graphics g = CreateGraphics();
                //try
                //{
                //    StringFormat sf = StringFormat.GenericTypographic;
                //    sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                //    SizeF textSize_F = g.MeasureString("蛛", new Font(dgvDetail.RowTemplate.DefaultCellStyle.Font.Name, dgvDetail.RowTemplate.DefaultCellStyle.Font.Size, GraphicsUnit.Pixel), PointF.Empty, sf);
                //    wordSize = new Size((int)Math.Ceiling(textSize_F.Width), (int)Math.Ceiling(textSize_F.Height));
                //}
                //catch (Exception ex)
                //{
                //    wordSize = TextRenderer.MeasureText("蛛", new Font(dgvDetail.RowTemplate.DefaultCellStyle.Font.Name, dgvDetail.RowTemplate.DefaultCellStyle.Font.Size, GraphicsUnit.Pixel));
                //}
                //finally
                //{
                //    g.Dispose();
                //}

                SizeF textSize_F = TextRenderer.MeasureText("蛛", new Font(RowTemplate.DefaultCellStyle.Font.Name, RowTemplate.DefaultCellStyle.Font.Size, RowTemplate.DefaultCellStyle.Font.Style, GraphicsUnit.Pixel, RowTemplate.DefaultCellStyle.Font.GdiCharSet));
                wordSize = new Size((int)Math.Ceiling(textSize_F.Width), (int)Math.Ceiling(textSize_F.Height));

                //单字高度
                int wordHeight = (int)wordSize.Height;
                //单字宽度
                int wordWidth = (int)wordSize.Width;

                //提取每列的最长字符
                string[] cells = new string[Columns.Count];
                foreach (DataGridViewRow dgvRow in Rows)
                {
                    foreach (DataGridViewColumn dgvCol in Columns)
                    {
                        if (dgvRow.Cells[dgvCol.Name].Value != null)
                        {
                            string strVal = dgvRow.Cells[dgvCol.Name].Value.ToString();

                            if (cells[dgvCol.Index] != null && strVal.Length > cells[dgvCol.Index].Length)
                            {
                                cells[dgvCol.Index] = strVal;
                            }
                            else if (string.IsNullOrEmpty(cells[dgvCol.Index]))
                            {
                                cells[dgvCol.Index] = strVal;
                            }
                        }
                    }
                }

                int maxHeight = 0;
                //检查表格中最长的字符，并计算它的高度
                for (int t = 0; t < cells.Length; t++)
                {
                    //字符
                    string str = cells[t];

                    if (string.IsNullOrEmpty(str))
                    {
                        continue;
                    }

                    //单元格大小
                    Rectangle rect = GetColumnDisplayRectangle(t, true);

                    if (rect.Width <= 0)
                    {
                        continue;
                    }

                    //一行可以放几个
                    int lineWordCount = rect.Width / wordWidth;
                    if (lineWordCount == 0) { continue; }
                    int rowCounts = str.Length / lineWordCount;
                    if (str.Length % lineWordCount > 0)
                    {
                        rowCounts++;
                    }
                    int newHeight = rowCounts * (wordHeight + 4);

                    if (newHeight > maxHeight)
                    {
                        maxHeight = newHeight;
                    }
                }

                //如果计算出来的高度小于默认的直接就使用默认的
                if (RowTemplate.Height > maxHeight)
                {
                    maxHeight = RowTemplate.Height;
                }

                //设置高度
                foreach (DataGridViewRow dgvRow in Rows)
                {
                    dgvRow.Height = maxHeight;
                }

                //更新每个单元格的高度
                if (Rows.Count > 0)
                {
                    UpdateRowHeightInfo(0, true);
                }
            }
        }
    }
}