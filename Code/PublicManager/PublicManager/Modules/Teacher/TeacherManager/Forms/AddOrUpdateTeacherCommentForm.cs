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
    public partial class AddOrUpdateTeacherCommentForm : Form
    {
        public AddOrUpdateTeacherCommentForm(DB.Entitys.TeacherComment commentObj)
        {
            InitializeComponent();

            CommentObj = commentObj;
            if (CommentObj != null)
            {
                txtCommentDate.Value = commentObj.CommentDate;
                txtCommentText.Text = commentObj.CommentText;
            }
            else
            {
                CommentObj = new DB.Entitys.TeacherComment();
            }
        }

        public DB.Entitys.TeacherComment CommentObj { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime dd1;
            if (DateTime.TryParse(txtCommentDate.Text, out dd1) == false)
            {
                MessageBox.Show("对不起，请输入评审日期!");
                return;
            }
            if (txtCommentText.Text == string.Empty)
            {
                MessageBox.Show("对不起，请输入评审内容!");
                return;
            }

            CommentObj.CommentDate = txtCommentDate.Value;
            CommentObj.CommentText = txtCommentText.Text;

            if (string.IsNullOrEmpty(CommentObj.TeacherCommentID))
            {
                CommentObj.TeacherCommentID = Guid.NewGuid().ToString();
                CommentObj.copyTo(ConnectionManager.Context.table("TeacherComment")).insert();
            }
            else
            {
                CommentObj.copyTo(ConnectionManager.Context.table("TeacherComment")).where("TeacherCommentID='" + CommentObj.TeacherCommentID + "'").update();
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}