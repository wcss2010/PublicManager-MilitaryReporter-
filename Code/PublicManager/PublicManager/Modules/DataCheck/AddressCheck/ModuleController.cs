using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB;
using PublicManager.DB.Entitys;
using System.IO;

namespace PublicManager.Modules.DataCheck.AddressCheck
{
    public partial class ModuleController : BaseModuleController
    {
        /// <summary>
        /// CatalogID筛选条件
        /// </summary>
        string strCatalogIDFilterString = " and CatalogID in (select CatalogID from Catalog)";

        public ModuleController()
        {
            InitializeComponent();
            cbOrgList.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvDetail.Rows.Clear();
            List<Project> projList = ConnectionManager.Context.table("Project").where("ProjectID in (select ProjectID from Subject where " + (string.IsNullOrEmpty(txtKey.Text) ? "1=1" : "DutyUnitAddress like '%" + txtKey.Text + "%'") + (cbOrgList.SelectedItem.ToString() == "全部" ? string.Empty : " and DutyUnitOrg = '" + cbOrgList.SelectedItem.ToString() + "'") + ")" + strCatalogIDFilterString).select("*").getList<Project>(new Project());
            foreach (Project proj in projList)
            {
               List<Subject> subList = ConnectionManager.Context.table("Subject").where("CatalogID = '" + proj.CatalogID + "' and ProjectID = '" + proj.ProjectID + "'").select("*").getList<Subject>(new Subject());
               foreach (Subject sub in subList)
               {
                   if (sub.DutyUnitOrg == cbOrgList.SelectedItem.ToString() || cbOrgList.SelectedItem.ToString() == "全部")
                   {
                       List<object> cells = new List<object>();
                       cells.Add(ConnectionManager.Context.table("Catalog").where("CatalogID='" + proj.CatalogID + "'").select("CatalogVersion").getValue<string>("未知"));
                       cells.Add(ConnectionManager.Context.table("Catalog").where("CatalogID='" + proj.CatalogID + "'").select("CatalogType").getValue<string>("未知"));
                       cells.Add(proj.ProjectName);
                       cells.Add(sub.SubjectName);
                       cells.Add(sub.DutyUnit);
                       cells.Add(sub.DutyUnitOrg);
                       cells.Add(sub.DutyUnitAddress);

                       dgvDetail.Rows.Add(cells.ToArray());
                   }
               }
            }

            dgvDetail.checkCellSize();
        }

        public override void start()
        {
            base.start();

            this.DisplayControl.Controls.Clear();
            this.Dock = DockStyle.Fill;
            this.DisplayControl.Controls.Add(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetail.Rows.Count >= 1 && e.RowIndex >= 0)
            {
                string content = dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null ? dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() : string.Empty;
                if (content != null && content.EndsWith(".doc"))
                {
                    if (File.Exists(content))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(content);
                        }
                        catch (Exception ex) { }
                    }
                }
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dgvDetail.Rows.Count <= 0) return;
            dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = (dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null ? dgvDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() : string.Empty).ToString();
        }

        private void cbDisplayReporter_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDisplayContract.Checked && cbDisplayReporter.Checked)
            {
                strCatalogIDFilterString = " and CatalogID in (select CatalogID from Catalog)";
            }
            else if (cbDisplayContract.Checked)
            {
                strCatalogIDFilterString = " and CatalogID in (select CatalogID from Catalog where CatalogType = '合同书')";
            }
            else if (cbDisplayReporter.Checked)
            {
                strCatalogIDFilterString = " and CatalogID in (select CatalogID from Catalog where CatalogType = '建议书')";
            }
            else
            {
                strCatalogIDFilterString = " and CatalogID in (select CatalogID from Catalog)";
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            BaseModuleController.exportToExcel(dgvDetail);
        }
    }
}