using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB;
using PublicManager.Modules.Teacher.TeacherManager.Forms;

namespace PublicManager.Modules.Teacher.TeacherManager.Forms
{
    public partial class AddOrUpdateTeacherForm : Form
    {
        public DB.Entitys.Teacher TeacherObj { get; set; }

        public string teacherID = string.Empty;

        public AddOrUpdateTeacherForm(DB.Entitys.Teacher teacher)
        {
            InitializeComponent();

            TeacherObj = teacher;            
        }

        private void updateTeacherComments()
        {
            if (TeacherObj != null)
            {
                dgvDetail.Rows.Clear();
                List<DB.Entitys.TeacherComment> list = ConnectionManager.Context.table("TeacherComment").where("TeacherID='" + teacherID + "'").select("*").getList<DB.Entitys.TeacherComment>(new DB.Entitys.TeacherComment());
                foreach (DB.Entitys.TeacherComment tc in list)
                {
                    List<object> cells = new List<object>();
                    cells.Add(tc.CommentDate.ToString("yyyy-MM-dd"));
                    cells.Add(tc.CommentText);

                    int rowIndex = dgvDetail.Rows.Add(cells.ToArray());
                    dgvDetail.Rows[rowIndex].Tag = tc;
                }

                dgvDetail.checkCellSize();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtTName.Text == string.Empty)
            {
                MessageBox.Show("对不起，请输入专家名称!");
                return;
            }
            if (txtTSex.Text == string.Empty)
            {
                MessageBox.Show("对不起，请输入性别!");
                return;
            }
            if (txtIDCard.Text == string.Empty)
            {
                MessageBox.Show("对不起，请输入身份证!");
                return;
            }
            if (txtTPhone.Text == string.Empty)
            {
                MessageBox.Show("对不起，请输入电话!");
                return;
            }
            if (txtTJob.Text == string.Empty)
            {
                MessageBox.Show("对不起，请输入职务!");
                return;
            }
            if (txtTUnit.Text == string.Empty)
            {
                MessageBox.Show("对不起，请输入单位!");
                return;
            }
            if (txtTRange.Text == string.Empty)
            {
                MessageBox.Show("对不起，请输入领域!");
                return;
            }

            TeacherObj.TName = txtTName.Text;
            TeacherObj.TSex = txtTSex.Text;
            TeacherObj.TIDCard = txtIDCard.Text;
            TeacherObj.TPhone = txtTPhone.Text;
            TeacherObj.TJob = txtTJob.Text;
            TeacherObj.TUnit = txtTUnit.Text;
            TeacherObj.TRange = txtTRange.Text;

            if (string.IsNullOrEmpty(TeacherObj.TeacherID))
            {
                TeacherObj.TeacherID = teacherID;
                TeacherObj.copyTo(ConnectionManager.Context.table("Teacher")).insert();
            }
            else
            {
                TeacherObj.copyTo(ConnectionManager.Context.table("Teacher")).where("TeacherID='" + TeacherObj.TeacherID + "'").update();
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            AddOrUpdateTeacherCommentForm form = new AddOrUpdateTeacherCommentForm(null);
            form.CommentObj.TeacherID = teacherID;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                updateTeacherComments();
            }
        }
        
        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DB.Entitys.TeacherComment commentObj = ((DB.Entitys.TeacherComment)dgvDetail.Rows[e.RowIndex].Tag);

                if (e.ColumnIndex == dgvDetail.Columns.Count - 1)
                {
                    //删除
                    if (MessageBox.Show("真的要删除吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ConnectionManager.Context.table("TeacherComment").where("TeacherCommentID='" + commentObj.TeacherCommentID + "'").delete();
                        updateTeacherComments();
                    }
                }
                else if (e.ColumnIndex == dgvDetail.Columns.Count - 2)
                {
                    //编辑
                    if (new AddOrUpdateTeacherCommentForm(commentObj).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        updateTeacherComments();
                    }
                }
            }
        }

        private void dgvDetail_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dgvDetail.Rows.Count <= 0) return;
            dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null ? dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() : string.Empty).ToString();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(teacherID))
            {
                if (TeacherObj != null)
                {
                    teacherID = TeacherObj.TeacherID;

                    txtTName.Text = TeacherObj.TName;
                    txtTSex.Text = TeacherObj.TSex;
                    txtIDCard.Text = TeacherObj.TIDCard;
                    txtTPhone.Text = TeacherObj.TPhone;
                    txtTJob.Text = TeacherObj.TJob;
                    txtTUnit.Text = TeacherObj.TUnit;
                    txtTRange.Text = TeacherObj.TRange;

                    updateTeacherComments();
                }
                else
                {
                    teacherID = Guid.NewGuid().ToString();
                    TeacherObj = new DB.Entitys.Teacher();
                }
            }
        }
    }
}