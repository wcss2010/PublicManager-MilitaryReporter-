using PublicManager.DB;
using PublicManager.DB.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PublicManager.Modules.Module_B.ProjectState.Forms
{
    public partial class StateEditForm : Form
    {
        private Project projectObj;
        public StateEditForm(Project proj)
        {
            InitializeComponent();

            projectObj = proj;

            if (projectObj.ProjectCheckState == "通过")
            {
                rbIsYes.Checked = true;
            }
            else
            {
                rbIsNo.Checked = true;
            }

            txtReason.Text = projectObj.ProjectStateReason;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //修改属性
            if (rbIsYes.Checked)
            {
                projectObj.ProjectCheckState = "通过";
                projectObj.ProjectStateReason = string.Empty;
            }
            else
            {
                projectObj.ProjectCheckState = "不通过";
                projectObj.ProjectStateReason = txtReason.Text;
            }

            //更新
            projectObj.copyTo(ConnectionManager.Context.table("Project").where("ProjectID = '" + projectObj.ProjectID + "'")).update();

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void rbIsYes_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtReason.Text = string.Empty;
                txtReason.ReadOnly = true;
            }
        }

        private void rbIsNo_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtReason.Text = string.Empty;
                txtReason.ReadOnly = false;
            }
        }
    }
}