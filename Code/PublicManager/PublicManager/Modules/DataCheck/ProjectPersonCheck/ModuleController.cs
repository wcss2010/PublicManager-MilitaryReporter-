using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB;
using PublicManager.DB.Entitys;

namespace PublicManager.Modules.DataCheck.ProjectPersonCheck
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
        }

        public override void start()
        {
            base.start();

            this.DisplayControl.Controls.Clear();
            this.Dock = DockStyle.Fill;
            this.DisplayControl.Controls.Add(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvDetail.Rows.Clear();
            List<Project> projList = ConnectionManager.Context.table("Project").where("ProjectName like '%" + txtKey.Text + "%'" + strCatalogIDFilterString).select("*").getList<Project>(new Project());
            foreach (Project proj in projList)
            {
                //显示仅为项目负责人
                Person masterPerson = ConnectionManager.Context.table("Person").where("CatalogID = '" + proj.CatalogID + "' and SubjectID = '' and JobInProject = '负责人' and IsProjectMaster = 'true'").select("*").getItem<Person>(new Person());
                if (masterPerson != null && masterPerson.PersonID != null && masterPerson.PersonID.Length >= 1)
                {
                    //存在仅为负责人的记录
                    List<object> cells = new List<object>();
                    cells.Add(ConnectionManager.Context.table("Catalog").where("CatalogID='" + proj.CatalogID + "'").select("CatalogVersion").getValue<string>("未知"));
                    cells.Add(ConnectionManager.Context.table("Catalog").where("CatalogID='" + proj.CatalogID + "'").select("CatalogType").getValue<string>("未知"));
                    cells.Add(proj.ProjectName);
                    cells.Add("");
                    cells.Add(masterPerson.PersonName);
                    cells.Add(masterPerson.PersonIDCard);
                    cells.Add(masterPerson.PersonSex);
                    cells.Add(masterPerson.WorkUnit);
                    cells.Add(masterPerson.PersonJob);
                    cells.Add(masterPerson.PersonSpecialty);
                    cells.Add(masterPerson.TotalTime);
                    cells.Add(masterPerson.TaskContent);

                    cells.Add("项目负责人");

                    dgvDetail.Rows.Add(cells.ToArray());
                }

                //显示课题成员
                List<Subject> subList = ConnectionManager.Context.table("Subject").where("CatalogID = '" + proj.CatalogID + "' and ProjectID = '" + proj.ProjectID + "'").select("*").getList<Subject>(new Subject());
                foreach (Subject sub in subList)
                {
                    List<Person> perList = ConnectionManager.Context.table("Person").where("CatalogID = '" + proj.CatalogID + "' and SubjectID = '" + sub.SubjectID + "'").select("*").getList<Person>(new Person());
                    foreach (Person p in perList)
                    {
                        List<object> cells = new List<object>();
                        cells.Add(ConnectionManager.Context.table("Catalog").where("CatalogID='" + proj.CatalogID + "'").select("CatalogVersion").getValue<string>("未知"));
                        cells.Add(ConnectionManager.Context.table("Catalog").where("CatalogID='" + proj.CatalogID + "'").select("CatalogType").getValue<string>("未知"));
                        cells.Add(proj.ProjectName);
                        cells.Add(sub.SubjectName);
                        cells.Add(p.PersonName);
                        cells.Add(p.PersonIDCard);
                        cells.Add(p.PersonSex);
                        cells.Add(p.WorkUnit);
                        cells.Add(p.PersonJob);
                        cells.Add(p.PersonSpecialty);
                        cells.Add(p.TotalTime);
                        cells.Add(p.TaskContent);

                        string roleStr = "未知";
                        if (p.IsProjectMaster == "true")
                        {
                            roleStr = "项目负责人兼" + sub.SubjectName + "的" + p.JobInProject;
                        }
                        else
                        {
                            roleStr = sub.SubjectName + "的" + p.JobInProject;
                        }

                        cells.Add(roleStr);

                        dgvDetail.Rows.Add(cells.ToArray());
                    }
                }
            }

            dgvDetail.checkCellSize();
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