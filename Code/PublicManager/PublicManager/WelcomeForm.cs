using PublicManager.DB;
using PublicManager.DB.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            string dir = Path.Combine(Application.StartupPath, Path.Combine("Data", "M_A"));
            try
            {
                Directory.CreateDirectory(dir);
            }
            catch (Exception ex) { }
            string dbFile = Path.Combine(dir, "static.db");
            if (!File.Exists(dbFile))
            {
                File.Copy(Path.Combine(Application.StartupPath, "static.db"), dbFile, true);
            }

            //初始化数据库
            PublicManager.DB.ConnectionManager.Open("main", "Data Source=" + dbFile);

            if (checkUnitA())
            {
                Top = Screen.PrimaryScreen.Bounds.Height * 5;
                Modules.Module_A.ModuleMainForm form = new Modules.Module_A.ModuleMainForm();
                form.FormClosing += form_FormClosing;
                form.Show();
            }
        }

        public static bool checkUnitA()
        {
            LocalUnit lu = ConnectionManager.Context.table("LocalUnit").select("*").getItem<LocalUnit>(new LocalUnit());
            if (string.IsNullOrEmpty(lu.LocalUnitID))
            {
                return showUnitADialog();
            }
            else
            {
                return true;
            }
        }

        public static bool showUnitADialog()
        {
            LocalUnit lu = ConnectionManager.Context.table("LocalUnit").select("*").getItem<LocalUnit>(new LocalUnit());
            Modules.Module_A.DataManager.Forms.DutyUnitForm form = new Modules.Module_A.DataManager.Forms.DutyUnitForm();
            form.Text = "设置所属单位";

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConnectionManager.Context.table("LocalUnit").delete();

                lu.LocalUnitID = Guid.NewGuid().ToString();
                lu.LocalUnitName = form.SelectedItem;
                lu.copyTo(ConnectionManager.Context.table("LocalUnit")).insert();

                return true;
            }
            else
            {
                return false;
            }
        }

        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnToB_Click(object sender, EventArgs e)
        {
            string dir = Path.Combine(Application.StartupPath, Path.Combine("Data", "M_B"));
            try
            {
                Directory.CreateDirectory(dir);
            }
            catch (Exception ex) { }
            string dbFile = Path.Combine(dir, "static.db");
            if (!File.Exists(dbFile))
            {
                File.Copy(Path.Combine(Application.StartupPath, "static.db"), dbFile, true);
            }

            //初始化数据库
            PublicManager.DB.ConnectionManager.Open("main", "Data Source=" + dbFile);

            if (checkUnitB())
            {
                Top = Screen.PrimaryScreen.Bounds.Height * 5;
                Modules.Module_B.ModuleMainForm form = new Modules.Module_B.ModuleMainForm();
                form.FormClosing += form_FormClosing;
                form.Show();
            }
        }

        public static bool checkUnitB()
        {
            LocalUnit lu = ConnectionManager.Context.table("LocalUnit").select("*").getItem<LocalUnit>(new LocalUnit());
            if (string.IsNullOrEmpty(lu.LocalUnitID))
            {
                lu.LocalUnitID = Guid.NewGuid().ToString();
                lu.LocalUnitName = "军委机构";
                lu.copyTo(ConnectionManager.Context.table("LocalUnit")).insert();
            }

            //Modules.Module_B.DataManager.Forms.DutyUnitForm form = new Modules.Module_B.DataManager.Forms.DutyUnitForm();

            return true;
        }
    }
}