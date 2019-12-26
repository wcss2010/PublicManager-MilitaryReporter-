using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PublicManager
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void btnToA_Click(object sender, EventArgs e)
        {
            Top = Screen.PrimaryScreen.Bounds.Height * 5;
            Modules.Module_A.ModuleMainForm form = new Modules.Module_A.ModuleMainForm();
            form.FormClosing += form_FormClosing;
            form.Show();
        }

        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnToB_Click(object sender, EventArgs e)
        {
            Top = Screen.PrimaryScreen.Bounds.Height * 5;
            Modules.Module_B.ModuleMainForm form = new Modules.Module_B.ModuleMainForm();
            form.FormClosing += form_FormClosing;
            form.Show();
        }
    }
}