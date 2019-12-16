using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB;
using PublicManager.Modules.Teacher.TeacherManager.Forms;
using System.IO;
using System.Diagnostics;

namespace PublicManager.Modules.Teacher.TeacherManager
{
    public partial class ModuleController : BaseModuleController
    {
        public ModuleController()
        {
            InitializeComponent();
        }

        public override void start()
        {
            base.start();

            this.DisplayControl.Controls.Clear();
            this.Dock = DockStyle.Fill;
            this.DisplayControl.Controls.Add(this);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (new AddOrUpdateTeacherForm(null).ShowDialog() == DialogResult.OK)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvDetail.Rows.Clear();
            List<DB.Entitys.Teacher> list = ConnectionManager.Context.table("Teacher").where("TName like '%" + txtKey.Text + "%'").select("*").getList<DB.Entitys.Teacher>(new DB.Entitys.Teacher());
            foreach (DB.Entitys.Teacher tr in list)
            {
                List<object> cells = new List<object>();
                cells.Add(tr.TName);
                cells.Add(tr.TSex);
                cells.Add(tr.TIDCard);
                cells.Add(tr.TPhone);
                cells.Add(tr.TJob);
                cells.Add(tr.TUnit);
                cells.Add(tr.TRange);

                int rowIndex = dgvDetail.Rows.Add(cells.ToArray());
                dgvDetail.Rows[rowIndex].Tag = tr;
            }

            dgvDetail.checkCellSize();
        }

        private void dgvDetail_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dgvDetail.Rows.Count <= 0) return;
            dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null ? dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() : string.Empty).ToString();
        }

        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DB.Entitys.Teacher teacherObj = ((DB.Entitys.Teacher)dgvDetail.Rows[e.RowIndex].Tag);

                if (e.ColumnIndex == dgvDetail.Columns.Count - 1)
                {
                    //删除
                    if (MessageBox.Show("真的要删除吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ConnectionManager.Context.table("Teacher").where("TeacherID='" + teacherObj.TeacherID + "'").delete();
                        ConnectionManager.Context.table("TeacherComment").where("TeacherID='" + teacherObj.TeacherID + "'").delete();
                        btnSearch.PerformClick();
                    }
                }
                else if (e.ColumnIndex == dgvDetail.Columns.Count - 2)
                {
                    //编辑
                    if (new AddOrUpdateTeacherForm(teacherObj).ShowDialog() == DialogResult.OK)
                    {
                        btnSearch.PerformClick();
                    }
                }
            }
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel(2007-2013)|*.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataSet ds = ExcelHelper.ExcelToDataSet(ofd.FileName, true);
                    if (ds != null && ds.Tables.Count >= 1)
                    {
                        DataTable dt = ds.Tables[0];

                        foreach (DataRow drr in dt.Rows)
                        {
                            //检查非空
                            foreach (DataColumn dc in drr.Table.Columns)
                            {
                                if (drr[dc.ColumnName] == null || drr[dc.ColumnName].ToString() == string.Empty)
                                {
                                    throw new Exception("对不起，'" + dc.ColumnName + "'不能为空！");
                                }
                            }

                            string tName = drr["姓名"] != null ? drr["姓名"].ToString().Trim() : string.Empty;
                            string tSex = drr["性别"] != null ? drr["性别"].ToString().Trim() : string.Empty;
                            string tIDCard = drr["身份证"] != null ? drr["身份证"].ToString().Trim() : string.Empty;
                            string tPhone = drr["电话"] != null ? drr["电话"].ToString().Trim() : string.Empty;
                            string tJob = drr["职务"] != null ? drr["职务"].ToString().Trim() : string.Empty;
                            string tUnit = drr["单位"] != null ? drr["单位"].ToString().Trim() : string.Empty;
                            string tRange = drr["领域"] != null ? drr["领域"].ToString().Trim() : string.Empty;

                            DB.Entitys.Teacher teacherObj = new DB.Entitys.Teacher();
                            teacherObj.TeacherID = Guid.NewGuid().ToString();
                            teacherObj.TName = tName;
                            teacherObj.TSex = tSex;
                            teacherObj.TIDCard = tIDCard;
                            teacherObj.TPhone = tPhone;
                            teacherObj.TJob = tJob;
                            teacherObj.TUnit = tUnit;
                            teacherObj.TRange = tRange;

                            object objResult = ConnectionManager.Context.table("Teacher").where("TIDCard='" + tIDCard + "'").select("TeacherID").getValue();
                            if (objResult == null || objResult.ToString().Equals(string.Empty))
                            {
                                teacherObj.copyTo(ConnectionManager.Context.table("Teacher")).insert();
                            }
                            else
                            {
                                teacherObj.TeacherID = objResult.ToString();
                                teacherObj.copyTo(ConnectionManager.Context.table("Teacher")).where("TeacherID='" + teacherObj.TeacherID + "'").update();
                            }
                        }
                    }
                    btnSearch.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("对不起，导入失败！Ex:" + ex.ToString());
                }
            }
        }

        private void llDownloadTemplete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sourcePath = Path.Combine(Application.StartupPath, Path.Combine("Templetes", "teacherList.xlsx"));

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel(2007-2013)|*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(sourcePath, sfd.FileName, true);
                    Process.Start(sfd.FileName);

                    MessageBox.Show("下载完成！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("下载失败！Ex:" + ex.ToString());
                }
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dgvDetail.SelectedRows.Count == 0)
            {
                MessageBox.Show("对不起，请选择要导出的专家！");
                return;
            }

            string sourcePath = Path.Combine(Application.StartupPath, Path.Combine("Templetes", "TeacherColumnExportTempletes.txt"));

            try
            {
                //读取列名称
                string[] colNames = File.ReadAllLines(sourcePath);

                //添加列
                DataTable dt = new DataTable();
                foreach (string col in colNames)
                {
                    if (string.IsNullOrEmpty(col))
                    {
                        continue;
                    }
                    else
                    {
                        dt.Columns.Add(col, typeof(string));
                    }
                }

                //生成数据
                foreach (DataGridViewRow dgvRow in dgvDetail.SelectedRows)
                {
                    DB.Entitys.Teacher teacherObj = ((DB.Entitys.Teacher)dgvRow.Tag);
                    List<object> cells = new List<object>();
                    cells.Add(teacherObj.TName);
                    cells.Add(teacherObj.TName);
                    cells.Add(new Random((int)DateTime.Now.Ticks + dgvRow.Index).Next(100, 999).ToString());
                    cells.Add(teacherObj.TJob);
                    cells.Add(teacherObj.TPhone);
                    cells.Add(string.Empty);
                    dt.Rows.Add(cells.ToArray());
                }

                //导出数据
                ExcelHelper.ExportToExcel(dt, "专家信息");

                MessageBox.Show("导出完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败！Ex:" + ex.ToString());
            }

        }
    }
}