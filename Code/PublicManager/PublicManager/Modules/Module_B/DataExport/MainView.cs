using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PublicManager.DB.Entitys;
using PublicManager.DB;

namespace PublicManager.Modules.Module_B.DataExport
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