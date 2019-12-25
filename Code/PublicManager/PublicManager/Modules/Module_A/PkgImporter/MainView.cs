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

namespace PublicManager.Modules.Module_A.PkgImporter
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
    }
}