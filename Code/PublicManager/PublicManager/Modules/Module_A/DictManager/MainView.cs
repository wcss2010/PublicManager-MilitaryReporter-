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

namespace PublicManager.Modules.Module_A.DictManager
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

            initDicts();

            updateCatalogs();
        }

        public static void initDicts()
        {
            int rowCount = 0;
            object obj = ConnectionManager.Context.table("Professions").select("count(*)").getValue();
            try
            {
                rowCount = int.Parse(obj.ToString());
            }
            catch (Exception ex) { }

            if (rowCount == 0)
            {
                Professions prf = new Professions();
                prf.ProfessionID = Guid.NewGuid().ToString();
                prf.ProfessionNum = "1";
                prf.ProfessionCategory = "军事思想";
                prf.ProfessionName = "军事思想";
                prf.IsAcceptModify = "false";
                prf.copyTo(ConnectionManager.Context.table("Professions")).insert();

                prf = new Professions();
                prf.ProfessionID = Guid.NewGuid().ToString();
                prf.ProfessionNum = "2";
                prf.ProfessionCategory = "强敌研究";
                prf.ProfessionName = "强敌研究";
                prf.IsAcceptModify = "false";
                prf.copyTo(ConnectionManager.Context.table("Professions")).insert();

                prf = new Professions();
                prf.ProfessionID = Guid.NewGuid().ToString();
                prf.ProfessionNum = "3";
                prf.ProfessionCategory = "军事战略";
                prf.ProfessionName = "军事战略";
                prf.IsAcceptModify = "false";
                prf.copyTo(ConnectionManager.Context.table("Professions")).insert();

                prf = new Professions();
                prf.ProfessionID = Guid.NewGuid().ToString();
                prf.ProfessionNum = "4";
                prf.ProfessionCategory = "战略管理";
                prf.ProfessionName = "战略管理";
                prf.IsAcceptModify = "false";
                prf.copyTo(ConnectionManager.Context.table("Professions")).insert();

                prf = new Professions();
                prf.ProfessionID = Guid.NewGuid().ToString();
                prf.ProfessionNum = "5";
                prf.ProfessionCategory = "xx作战";
                prf.ProfessionName = "xx作战";
                prf.IsAcceptModify = "true";
                prf.copyTo(ConnectionManager.Context.table("Professions")).insert();

                prf = new Professions();
                prf.ProfessionID = Guid.NewGuid().ToString();
                prf.ProfessionNum = "6";
                prf.ProfessionCategory = "xx应用";
                prf.ProfessionName = "xx应用";
                prf.IsAcceptModify = "true";
                prf.copyTo(ConnectionManager.Context.table("Professions")).insert();

                prf = new Professions();
                prf.ProfessionID = Guid.NewGuid().ToString();
                prf.ProfessionNum = "7";
                prf.ProfessionCategory = "xx建设";
                prf.ProfessionName = "xx建设";
                prf.IsAcceptModify = "true";
                prf.copyTo(ConnectionManager.Context.table("Professions")).insert();

                prf = new Professions();
                prf.ProfessionID = Guid.NewGuid().ToString();
                prf.ProfessionNum = "8";
                prf.ProfessionCategory = "其他";
                prf.ProfessionName = "其他";
                prf.IsAcceptModify = "false";
                prf.copyTo(ConnectionManager.Context.table("Professions")).insert();
            }
        }

        public void updateCatalogs()
        {
            dgvCatalogs.Rows.Clear();

            List<Professions> list = ConnectionManager.Context.table("Professions").orderBy("ProfessionNum").select("*").getList<Professions>(new Professions());
            foreach (Professions pfo in list)
            {
                List<object> cells = new List<object>();
                cells.Add(pfo.ProfessionNum);
                cells.Add(pfo.ProfessionCategory);
                cells.Add(pfo.ProfessionName);
                cells.Add(pfo.IsAcceptModify == "true" ? "允许" : "不允许");

                int rowIndex = dgvCatalogs.Rows.Add(cells.ToArray());
                dgvCatalogs.Rows[rowIndex].Tag = pfo;
            }

            dgvCatalogs.checkCellSize();
        }

        private void dgvCatalogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //检查是否点击的是删除的那一列
            if (e.RowIndex >= 0 && dgvCatalogs.Rows.Count > e.RowIndex)
            {
                //获得要删除的项目ID
                Professions prf = ((Professions)dgvCatalogs.Rows[e.RowIndex].Tag);

                if (prf != null)
                {
                    if (e.ColumnIndex == dgvCatalogs.Columns.Count - 1)
                    {
                        //编辑
                        if (prf.IsAcceptModify == "true")
                        {
                            Forms.DictEditForm def = new Forms.DictEditForm();
                            def.ProfessionName = prf.ProfessionName;
                            if (def.ShowDialog() == DialogResult.OK)
                            {
                                prf.ProfessionName = def.ProfessionName;
                                prf.copyTo(ConnectionManager.Context.table("Professions")).where("ProfessionID='" + prf.ProfessionID + "'").update();

                                updateCatalogs();
                            }
                        }
                        else
                        {
                            MessageBox.Show("对不起，这个专业类别不能编辑！");
                        }
                    }
                }
            }
        }
    }
}