using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

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
            //dgvCatalogs.Rows.Clear();

            //List<Project> projList = ConnectionManager.Context.table("Project").orderBy("ProfessionID,ProfessionSort").select("*").getList<Project>(new Project());
            //int indexx = 0;
            //foreach (Project proj in projList)
            //{
            //    indexx++;

            //    List<object> cells = new List<object>();
            //    cells.Add(indexx);
            //    cells.Add(getProjectType(proj));
            //    cells.Add(proj.ProjectName);
            //    cells.Add(proj.WillResult);
            //    cells.Add(proj.StudyTime);
            //    cells.Add(proj.StudyMoney);
            //    cells.Add(proj.ProjectSort);
            //    cells.Add(proj.ProfessionSort);
            //    cells.Add(proj.DutyUnit + "(" + proj.NextUnit + ")");
            //    cells.Add(proj.Memo);

            //    string importTimes = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            //    importTimes = ConnectionManager.Context.table("Catalog").where("CatalogID='" + proj.CatalogID + "'").select("ImportTime").getValue<DateTime>(DateTime.Now).ToString("yyyy年MM月dd日 HH:mm:ss");
            //    cells.Add(importTimes);

            //    int rowIndex = dgvCatalogs.Rows.Add(cells.ToArray());
            //    dgvCatalogs.Rows[rowIndex].Tag = proj;
            //}

            //dgvCatalogs.checkCellSize();
        }
    }
}