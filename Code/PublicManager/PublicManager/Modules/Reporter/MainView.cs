using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Localization;
using PublicManager.DB;
using PublicManager.DB.Entitys;

namespace PublicManager.Modules.Reporter
{
    public partial class MainView : XtraUserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            updateCatalogs();
        }

        public void updateCatalogs()
        {
            dgvCatalogs.Rows.Clear();

            List<Catalog> list = ConnectionManager.Context.table("Catalog").where("CatalogType='建议书'").select("*").getList<Catalog>(new Catalog());
            int indexx = 0;
            foreach (Catalog catalog in list)
            {
                indexx++;

                List<object> cells = new List<object>();
                cells.Add(indexx);
                cells.Add(catalog.CatalogVersion);
                cells.Add(catalog.CatalogNumber);
                cells.Add(catalog.CatalogName);

                Person p = ConnectionManager.Context.table("Person").where("IsProjectMaster='true' and CatalogID = '" + catalog.CatalogID + "'").select("*").getItem<Person>(new Person());
                if (p != null)
                {
                    cells.Add(p.PersonName);
                    cells.Add(p.WorkUnit);
                }

                int rowIndex = dgvCatalogs.Rows.Add(cells.ToArray());
                dgvCatalogs.Rows[rowIndex].Tag = catalog;
            }

            dgvCatalogs.checkCellSize();
        }

        private void dgvCatalogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //检查是否点击的是删除的那一列
            if (e.ColumnIndex == dgvCatalogs.Columns.Count - 1 && e.RowIndex >= 0)
            {
                //获得要删除的项目ID,项目编号
                string projectId = ((Catalog)dgvCatalogs.Rows[e.RowIndex].Tag).CatalogID;
                string projectNumber = ((Catalog)dgvCatalogs.Rows[e.RowIndex].Tag).CatalogNumber;

                //显示删除提示框
                if (MessageBox.Show("真的要删除吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    //删除项目数据
                    new DBImporter().deleteProject(projectId);

                    //删除申报包缓存
                    //try
                    //{
                    //    System.IO.Directory.Delete(System.IO.Path.Combine(PackageDir, projectNumber), true);
                    //}
                    //catch (Exception ex) { MainForm.writeLog(ex.ToString()); }

                    //刷新GridView
                    updateCatalogs();
                }
            }
        }
    }
}