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
                else if (e.ColumnIndex == dgvCatalogs.Columns.Count - 2)
                {
                    //显示链接提示框
                    try
                    {
                        if (MainConfig.Config.Dict.ContainsKey("论证报告解压目录"))
                        {
                            string decompressDir = MainConfig.Config.Dict["论证报告解压目录"];
                            string catalogNumber = ConnectionManager.Context.table("Catalog").where("CatalogID='" + catalogId + "'").select("CatalogNumber").getValue<string>("");
                            if (File.Exists(Path.Combine(decompressDir, Path.Combine(catalogNumber, "论证报告.doc"))))
                            {
                                Process.Start(Path.Combine(decompressDir, Path.Combine(catalogNumber, "论证报告.doc")));
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
                else if (e.ColumnIndex == dgvCatalogs.Columns.Count - 3)
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
                    rtb.Dock = DockStyle.Fill;
                    f.Controls.Add(rtb);

                    sb.Append("研究目标：").AppendLine();
                    sb.Append(proj.StudyDest).AppendLine();
                    sb.Append("研究内容：").AppendLine();
                    if (proj.StudyContent != null)
                    {
                        string[] cList = proj.StudyContent.Split(new string[] { BaseModuleController.rowFlag }, StringSplitOptions.None);
                        if (cList != null && cList.Length >= 1)
                        {
                            foreach (string s in cList)
                            {
                                sb.Append(s).AppendLine();
                            }
                        }
                    }

                    rtb.Text = sb.ToString().Trim();
                    f.ShowDialog();
                }
            }
        }
    }
}