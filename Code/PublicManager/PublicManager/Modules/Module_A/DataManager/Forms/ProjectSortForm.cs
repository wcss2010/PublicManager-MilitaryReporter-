using PublicManager.DB;
using PublicManager.DB.Entitys;
using SuperCodeFactoryLib.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PublicManager.Modules.Module_A.DataManager.Forms
{
    public partial class ProjectSortForm : Form
    {
        private List<string> dutyUnitToProfessonLinks;
        /// <summary>
        /// 责任单位(有类别的)
        /// </summary>
        public List<string> DutyUnitToProfessonLinks
        {
            get { return dutyUnitToProfessonLinks; }
        }

        KeyedList<string, ComboBoxObject<Professions>> professionMap = new KeyedList<string, ComboBoxObject<Professions>>();
        private string selectedProjectID;

        public ProjectSortForm()
        {
            InitializeComponent();

            //加载责任单位与专业类型映射选项
            if (MainConfig.Config.ObjectDict.ContainsKey("责任单位与专业类别映射"))
            {
                try
                {
                    dutyUnitToProfessonLinks = new List<string>();
                    Newtonsoft.Json.Linq.JArray teams = (Newtonsoft.Json.Linq.JArray)MainConfig.Config.ObjectDict["责任单位与专业类别映射"];
                    foreach (string s in teams)
                    {
                        dutyUnitToProfessonLinks.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }

            LocalUnit lu = ConnectionManager.Context.table("LocalUnit").select("*").getItem<LocalUnit>(new LocalUnit());
            if (string.IsNullOrEmpty(lu.LocalUnitID))
            {
                dgvCatalogs.Columns[3].ReadOnly = false;
            }
            else
            {
                if (DutyUnitToProfessonLinks.Contains(lu.LocalUnitName))
                {
                    dgvCatalogs.Columns[3].ReadOnly = false;
                }
                else
                {
                    dgvCatalogs.Columns[3].ReadOnly = true;
                }
            }

            PublicManager.Modules.Module_A.DictManager.MainView.initDicts();
            loadComboboxItems();
            updateCatalogs();
        }

        public ProjectSortForm(string projID)
        {
            InitializeComponent();

            //加载责任单位与专业类型映射选项
            if (MainConfig.Config.ObjectDict.ContainsKey("责任单位与专业类别映射"))
            {
                try
                {
                    dutyUnitToProfessonLinks = new List<string>();
                    Newtonsoft.Json.Linq.JArray teams = (Newtonsoft.Json.Linq.JArray)MainConfig.Config.ObjectDict["责任单位与专业类别映射"];
                    foreach (string s in teams)
                    {
                        dutyUnitToProfessonLinks.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }

            LocalUnit lu = ConnectionManager.Context.table("LocalUnit").select("*").getItem<LocalUnit>(new LocalUnit());
            if (string.IsNullOrEmpty(lu.LocalUnitID))
            {
                dgvCatalogs.Columns[3].ReadOnly = false;
            }
            else
            {
                if (DutyUnitToProfessonLinks.Contains(lu.LocalUnitName))
                {
                    dgvCatalogs.Columns[3].ReadOnly = false;
                }
                else
                {
                    dgvCatalogs.Columns[3].ReadOnly = true;
                }
            }

            PublicManager.Modules.Module_A.DictManager.MainView.initDicts();
            loadComboboxItems();

            selectedProjectID = projID;
            updateCatalogs();
        }

        public void loadComboboxItems()
        {
            professionMap = new KeyedList<string, ComboBoxObject<Professions>>();

            ((DataGridViewComboBoxColumn)dgvCatalogs.Columns[3]).Items.Clear();
            List<Professions> list = ConnectionManager.Context.table("Professions").select("*").getList<Professions>(new Professions());
            foreach (Professions prf in list)
            {
                var objj = new ComboBoxObject<Professions>(prf.ProfessionName, prf);
                ((DataGridViewComboBoxColumn)dgvCatalogs.Columns[3]).Items.Add(objj.Text);
                professionMap.Add(objj.Text, objj);
            }

            ((DataGridViewComboBoxColumn)dgvCatalogs.Columns[3]).Items.Add(string.Empty);
        }

        public void updateCatalogs()
        {
            int selectedIndex = -1;
            dgvCatalogs.Rows.Clear();

            #region 排序
            List<Project> allProjects = ConnectionManager.Context.table("Project").orderBy("ProfessionID,ProfessionSort").select("*").getList<Project>(new Project());
            List<Project> tempProjectList = new List<Project>();
            List<Professions> pfList = ConnectionManager.Context.table("Professions").select("*").getList<Professions>(new Professions());
            foreach (Professions prf in pfList)
            {
                List<Project> projList = ConnectionManager.Context.table("Project").where("ProfessionID='" + prf.ProfessionID + "'").orderBy("ProfessionSort").select("*").getList<Project>(new Project());

                if (projList != null)
                {
                    tempProjectList.AddRange(projList);
                }
            }
            if (tempProjectList.Count == 0)
            {
                tempProjectList = allProjects;
            }
            if (allProjects.Count > tempProjectList.Count)
            {
                foreach (Project projjjj in allProjects)
                {
                    bool needAdd = true;
                    foreach (Project curProj in tempProjectList)
                    {
                        if (projjjj.ProjectID == curProj.ProjectID)
                        {
                            needAdd = false;
                        }
                    }
                    if (needAdd)
                    {
                        tempProjectList.Add(projjjj);
                    }
                }
            }
            #endregion

            int indexx = 0;
            foreach (Project proj in tempProjectList)
            {
                indexx++;

                List<object> cells = new List<object>();
                cells.Add(indexx);
                cells.Add(getProjectType(proj));
                cells.Add(proj.ProjectName);
                cells.Add(getProfessionObj(proj).Text);
                cells.Add((proj.ProfessionSort + 1));

                int rowIndex = dgvCatalogs.Rows.Add(cells.ToArray());
                dgvCatalogs.Rows[rowIndex].Tag = proj;

                if (proj.ProjectID == selectedProjectID)
                {
                    selectedIndex = rowIndex;
                }
            }

            dgvCatalogs.checkCellSize();

            if (selectedIndex >= 0 && dgvCatalogs.Rows.Count > selectedIndex)
            {
                dgvCatalogs.Rows[selectedIndex].Selected = true;
            }
        }

        /// <summary>
        /// 获得专业类别
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        private ComboBoxObject<Professions> getProfessionObj(Project proj)
        {
            string professionName = ConnectionManager.Context.table("Professions").where("ProfessionID='" + proj.ProfessionID + "'").select("ProfessionName").getValue<string>(string.Empty);
            ComboBoxObject<Professions> result = new ComboBoxObject<Professions>(string.Empty, new Professions());
            foreach (ComboBoxObject<Professions> prf in professionMap.Values)
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

                //保存当前选项
                selectedProjectID = proj.ProjectID; 

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
                int newOrder = 0;
                foreach (Project projjjj in projList)
                {
                    projjjj.ProfessionSort = newOrder;
                    projjjj.copyTo(ConnectionManager.Context.table("Project")).where("ProjectID='" + projjjj.ProjectID + "'").update();
                    newOrder++;
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

                ComboBoxObject<Professions> currentProfession = null;
                if (dgvCatalogs.Rows[e.RowIndex].Cells[3].Value != null)
                {
                    currentProfession = professionMap[dgvCatalogs.Rows[e.RowIndex].Cells[3].Value.ToString().Trim()];
                    if (currentProfession != null)
                    {
                        proj.ProfessionID = currentProfession.Tag.ProfessionID;
                        proj.copyTo(ConnectionManager.Context.table("Project")).where("ProjectID='" + proj.ProjectID + "'").update();
                        updateCatalogs();
                    }
                }
            }
        }
    }
}