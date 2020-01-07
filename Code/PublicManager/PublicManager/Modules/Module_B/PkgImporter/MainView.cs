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
using System.Diagnostics;
using System.IO;

namespace PublicManager.Modules.Module_B.PkgImporter
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

            List<Catalog> projList = ConnectionManager.Context.table("Catalog").orderBy("ImportTime").select("*").getList<Catalog>(new Catalog());
            int indexx = 0;
            foreach (Catalog catalog in projList)
            {
                indexx++;

                //项目信息
                Project proj = ConnectionManager.Context.table("Project").where("CatalogID='" + catalog.CatalogID + "'").select("*").getItem<Project>(new Project());
                if (proj == null)
                {
                    continue;
                }

                List<object> cells = new List<object>();
                cells.Add(indexx);
                cells.Add(getProjectType(proj));
                cells.Add(proj.ProjectName);
                cells.Add(proj.WillResult);
                cells.Add(proj.StudyTime);
                cells.Add(proj.StudyMoney);
                cells.Add(proj.ProjectSort);

                string professionNameStr = ConnectionManager.Context.table("Professions").where("ProfessionID='" + proj.ProfessionID + "'").select("ProfessionName").getValue<string>("其他");
                cells.Add(professionNameStr + "(" + proj.ProfessionSort + ")");

                cells.Add(proj.DutyUnit + "(" + proj.NextUnit + ")");
                cells.Add(proj.Memo);

                string importTimes = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
                importTimes = ConnectionManager.Context.table("Catalog").where("CatalogID='" + proj.CatalogID + "'").select("ImportTime").getValue<DateTime>(DateTime.Now).ToString("yyyy年MM月dd日 HH:mm:ss");
                cells.Add(importTimes);

                int rowIndex = dgvCatalogs.Rows.Add(cells.ToArray());
                dgvCatalogs.Rows[rowIndex].Tag = proj;
            }
            
            dgvCatalogs.checkCellSize();
        }

        private void dgvCatalogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //检查是否点击的是删除的那一列
            if (e.RowIndex >= 0 && dgvCatalogs.Rows.Count > e.RowIndex)
            {
                //获得要删除的项目ID
                Project proj = ((Project)dgvCatalogs.Rows[e.RowIndex].Tag);
                string catalogId = proj.CatalogID;
                
                if (e.ColumnIndex == dgvCatalogs.Columns.Count - 1)
                {
                    //显示删除提示框
                    if (MessageBox.Show("真的要删除吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //删除项目数据
                        new DBImporter().deleteProject(catalogId);

                        //刷新GridView
                        updateCatalogs();
                    }
                }
                else if (e.ColumnIndex == dgvCatalogs.Columns.Count - 2 && !getProjectType(proj).Contains("专项"))
                {
                    //显示链接提示框
                    try
                    {
                        if (MainConfig.Config.StringDict.ContainsKey("论证报告解压目录"))
                        {
                            string decompressDir = MainConfig.Config.StringDict["论证报告解压目录"];
                            string catalogNumber = ConnectionManager.Context.table("Catalog").where("CatalogID='" + catalogId + "'").select("CatalogNumber").getValue<string>("");
                            if (File.Exists(Path.Combine(decompressDir, Path.Combine(catalogNumber, "论证报告.doc"))))
                            {
                                Process.Start(Path.Combine(decompressDir, Path.Combine(catalogNumber, "论证报告.doc")));
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
                else if (e.ColumnIndex == dgvCatalogs.Columns.Count - 3 && !getProjectType(proj).Contains("专项"))
                {
                    //显示详细提示框
                    StringBuilder sb = new StringBuilder();
                    Form f = new Form();
                    f.Text = "详细";
                    f.MaximizeBox = false;
                    f.MinimizeBox = false;
                    f.ShowInTaskbar = false;
                    f.ShowIcon = false;
                    f.Size = new System.Drawing.Size(600, 600);
                    RichTextBox rtb = new RichTextBox();
                    rtb.Font = new System.Drawing.Font("宋体", 14);
                    rtb.ReadOnly = true;
                    rtb.Dock = DockStyle.Fill;
                    f.Controls.Add(rtb);

                    sb.Append("研究目标：").AppendLine();
                    sb.Append(proj.StudyDest).AppendLine();
                    sb.Append("研究内容：").AppendLine();
                    sb.Append(proj.StudyContent).AppendLine();

                    rtb.Text = sb.ToString().Trim();
                    f.ShowDialog();
                }
            }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RadioButton radioButton = null;
            #region 查找已选择项
            foreach (Control c in plRules.Controls)
            {
                if (c is RadioButton)
                {
                    if (((RadioButton)c).Checked)
                    {
                        radioButton = ((RadioButton)c);
                        break;
                    }
                }
            }
            #endregion

            if (radioButton != null)
            {
                switch (radioButton.Text.Replace("按", string.Empty).Replace("查询", string.Empty))
                {
                    case "项目名称":
                        foreach (DataGridViewRow dgvRow in dgvCatalogs.Rows)
                        {
                            Project proj = (Project)dgvRow.Tag;
                            if (proj.ProjectName.Contains(cbxKeys.Text.Trim()))
                            {
                                dgvRow.Visible = true;
                            }
                            else
                            {
                                dgvRow.Visible = false;
                            }
                        }
                        break;
                    case "责任单位":
                        foreach (DataGridViewRow dgvRow in dgvCatalogs.Rows)
                        {
                            Project proj = (Project)dgvRow.Tag;
                            if (proj.DutyUnit == cbxKeys.Text.Trim())
                            {
                                dgvRow.Visible = true;
                            }
                            else
                            {
                                dgvRow.Visible = false;
                            }
                        }
                        break;
                    case "项目类型":
                        foreach (DataGridViewRow dgvRow in dgvCatalogs.Rows)
                        {
                            Project proj = (Project)dgvRow.Tag;
                            if (proj.ProjectSort == cbxKeys.Text.Trim())
                            {
                                dgvRow.Visible = true;
                            }
                            else
                            {
                                dgvRow.Visible = false;
                            }
                        }
                        break;
                }
            }
        }

        private void rbProjectType_CheckedChanged(object sender, EventArgs e)
        {
            cbxKeys.DropDownStyle = ComboBoxStyle.DropDown;
            cbxKeys.Items.Clear();
            cbxKeys.Text = string.Empty;

            RadioButton radioButton = (RadioButton)sender;
            switch (radioButton.Text.Replace("按", string.Empty).Replace("查询", string.Empty))
            {
                case "项目名称":
                    cbxKeys.Text = "";
                    break;
                case "责任单位":
                    cbxKeys.DropDownStyle = ComboBoxStyle.DropDownList;

                    #region 加载责任单位选项
                    if (MainConfig.Config.ObjectDict.ContainsKey("责任单位"))
                    {
                        try
                        {
                            cbxKeys.Items.Clear();
                            Newtonsoft.Json.Linq.JArray teams = (Newtonsoft.Json.Linq.JArray)MainConfig.Config.ObjectDict["责任单位"];
                            foreach (string s in teams)
                            {
                                cbxKeys.Items.Add(s);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.ToString());
                        }
                    }
                    #endregion

                    if (cbxKeys.Items.Count >= 1)
                    {
                        cbxKeys.SelectedIndex = 0;
                    }
                    break;
                case "项目类型":
                    cbxKeys.DropDownStyle = ComboBoxStyle.DropDownList;

                    #region 加载项目类别选项
                    if (MainConfig.Config.ObjectDict.ContainsKey("项目类别"))
                    {
                        try
                        {
                            cbxKeys.Items.Clear();
                            Newtonsoft.Json.Linq.JArray teams = (Newtonsoft.Json.Linq.JArray)MainConfig.Config.ObjectDict["项目类别"];
                            foreach (string sss in teams)
                            {
                                string[] ttt = sss.Split(new string[] { MainConfig.rowFlag }, StringSplitOptions.None);
                                if (ttt != null && ttt.Length >= 4)
                                {
                                    ProjectSortObject pso = new ProjectSortObject();
                                    pso.Text = ttt[0];

                                    string[] vvv = ttt[1].Replace("[", string.Empty).Replace("]", string.Empty).Split(new string[] { "," }, StringSplitOptions.None);
                                    if (vvv != null && vvv.Length >= 2)
                                    {
                                        pso.InfoMin = int.Parse(vvv[0]);
                                        pso.InfoMax = int.Parse(vvv[1]);
                                    }

                                    vvv = ttt[2].Replace("[", string.Empty).Replace("]", string.Empty).Split(new string[] { "," }, StringSplitOptions.None);
                                    if (vvv != null && vvv.Length >= 2)
                                    {
                                        pso.TableMin = int.Parse(vvv[0]);
                                        pso.TableMax = int.Parse(vvv[1]);
                                    }

                                    pso.Money = decimal.Parse(ttt[3]);

                                    cbxKeys.Items.Add(pso);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.ToString());
                        }
                    }
                    #endregion

                    if (cbxKeys.Items.Count >= 1)
                    {
                        cbxKeys.SelectedIndex = 0;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 项目类别
    /// </summary>
    public class ProjectSortObject
    {
        public ProjectSortObject()
        {
            Text = string.Empty;
            InfoMin = 80;
            InfoMax = 150;
            TableMin = 5;
            TableMax = 10;
            Money = 100;
        }

        public string Text { get; set; }

        public int InfoMin { get; set; }

        public int InfoMax { get; set; }

        public int TableMin { get; set; }

        public int TableMax { get; set; }

        public decimal Money { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}