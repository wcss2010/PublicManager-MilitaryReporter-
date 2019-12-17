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

            List<Project> projList = ConnectionManager.Context.table("Project").select("*").getList<Project>(new Project());
            int indexx = 0;
            foreach (Project proj in projList)
            {
                indexx++;

                List<object> cells = new List<object>();
                cells.Add(indexx);
                cells.Add(proj.ProjectName);

                StringBuilder sbWillResult = new StringBuilder();
                if (proj.WillResult != null && proj.WillResult.Contains(BaseModuleController.rowFlag))
                {
                    string[] tttt = proj.WillResult.Split(new string[] { BaseModuleController.rowFlag }, StringSplitOptions.None);
                    if (tttt != null)
                    {
                        foreach (string ss in tttt)
                        {
                            string[] vvvv = ss.Split(new string[] { BaseModuleController.cellFlag }, StringSplitOptions.None);
                            if (vvvv != null && vvvv.Length >= 2)
                            {
                                if (string.IsNullOrEmpty(vvvv[0])) { continue; }

                                sbWillResult.Append(vvvv[0].Insert(vvvv[0].IndexOf("("), vvvv[1]).Replace("(", string.Empty).Replace(")", string.Empty)).AppendLine();
                            }
                        }
                    }
                }
                cells.Add(sbWillResult.ToString());

                cells.Add(proj.StudyTime);
                cells.Add(proj.StudyMoney);
                cells.Add(proj.ProjectSort);
                cells.Add(proj.DutyUnit + "(" + proj.NextUnit + ")");
                cells.Add(proj.Memo != null && proj.Memo.Contains(BaseModuleController.rowFlag) ? proj.Memo.Replace(BaseModuleController.rowFlag, ":") : proj.Memo);

                int rowIndex = dgvCatalogs.Rows.Add(cells.ToArray());
                dgvCatalogs.Rows[rowIndex].Tag = proj;
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