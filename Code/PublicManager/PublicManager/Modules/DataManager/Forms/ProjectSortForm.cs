using PublicManager.DB;
using PublicManager.DB.Entitys;
using PublicManager.Modules.Manager.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PublicManager.Modules.DataManager.Forms
{
    public partial class ProjectSortForm : Form
    {
        List<ComboBoxObject<Professions>> professionList = new List<ComboBoxObject<Professions>>();

        private string defaultProfessionSort = "其他";

        public ProjectSortForm()
        {
            InitializeComponent();

            PublicManager.Modules.DictManager.MainView.initDicts();
            loadComboboxItems();
            updateCatalogs();
        }

        public void loadComboboxItems()
        {
            professionList = new List<ComboBoxObject<Professions>>();

            ((DataGridViewComboBoxColumn)dgvCatalogs.Columns[3]).Items.Clear();
            List<Professions> list = ConnectionManager.Context.table("Professions").select("*").getList<Professions>(new Professions());
            foreach (Professions prf in list)
            {
                var objj = new ComboBoxObject<Professions>(prf.ProfessionName, prf);
                ((DataGridViewComboBoxColumn)dgvCatalogs.Columns[3]).Items.Add(objj);
                professionList.Add(objj);
            }
        }

        public void updateCatalogs()
        {
            dgvCatalogs.Rows.Clear();

            List<Project> projList = ConnectionManager.Context.table("Project").orderBy("ProfessionID,ProfessionSort").select("*").getList<Project>(new Project());
            int indexx = 0;
            foreach (Project proj in projList)
            {
                indexx++;

                List<object> cells = new List<object>();
                cells.Add(indexx);
                cells.Add(getProjectType(proj));
                cells.Add(proj.ProjectName);
                cells.Add(getProfessionObj(proj));
                cells.Add(proj.ProfessionSort);

                int rowIndex = dgvCatalogs.Rows.Add(cells.ToArray());
                dgvCatalogs.Rows[rowIndex].Tag = proj;
            }

            dgvCatalogs.checkCellSize();
        }

        /// <summary>
        /// 获得专业类别
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        private ComboBoxObject<Professions> getProfessionObj(Project proj)
        {
            string professionName = ConnectionManager.Context.table("Professions").where("ProfessionID='" + proj.ProfessionID + "'").select("ProfessionName").getValue<string>(defaultProfessionSort);
            ComboBoxObject<Professions> result = professionList[professionList.Count - 1];
            foreach (ComboBoxObject<Professions> prf in professionList)
            {
                if (prf.Tag.ProfessionName == professionName)
                {
                    result = prf;
                    break;
                }
            }
            return result;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 获得项目类型
        /// </summary>
        /// <param name="catalogID"></param>
        /// <returns></returns>
        public string getProjectType(Project proj)
        {
            return proj.IsPrivateProject == "true" ? "专项项目" : "从填报工具导入";
        }

        private void dgvCatalogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //检查是否点击的是删除的那一列
            if (e.RowIndex >= 0 && dgvCatalogs.Rows.Count > e.RowIndex)
            {
                //获得要删除的项目ID
                Project proj = ((Project)dgvCatalogs.Rows[e.RowIndex].Tag);

                //筛选出适合排序的列表
                List<Project> projList = new List<Project>();
                foreach (DataGridViewRow dgvRow in dgvCatalogs.Rows)
                {
                    Project subP = ((Project)dgvRow.Tag);
                    if (subP.ProfessionID == proj.ProfessionID)
                    {
                        projList.Add(subP);
                    }
                }

                //检查当前项目在哪个位置
                int projIndex = projList.IndexOf(proj);

                if (e.ColumnIndex == dgvCatalogs.Columns.Count - 1)
                {
                    //向下
                    if (projIndex == projList.Count - 1)
                    {
                        return;
                    }

                    projList.Remove(proj);
                    projList.Insert(projIndex + 1, proj);
                }
                else if (e.ColumnIndex == dgvCatalogs.Columns.Count - 2)
                {
                    //向上
                    if (projIndex == 0)
                    {
                        return;
                    }

                    projList.Remove(proj);
                    projList.Insert(projIndex - 1, proj);
                }

                //更新数据
                foreach (Project projjjj in projList)
                {
                    projjjj.copyTo(ConnectionManager.Context.table("Project")).where("ProjectID='" + projjjj.ProjectID + "'").update();
                }

                //刷新显示
                updateCatalogs();
            }
        }

        private void dgvCatalogs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCatalogs.Rows.Count > e.RowIndex)
            {
                //获得要删除的项目ID
                Project proj = ((Project)dgvCatalogs.Rows[e.RowIndex].Tag);

                ComboBoxObject<Professions> currentProfession = (ComboBoxObject<Professions>)((DataGridViewComboBoxCell)dgvCatalogs.Rows[e.RowIndex].Cells[3]).Value;
                proj.ProfessionID = currentProfession.Tag.ProfessionID;
                proj.copyTo(ConnectionManager.Context.table("Project")).where("ProjectID='" + proj.ProjectID + "'").update();                
            }
        }
    }
}