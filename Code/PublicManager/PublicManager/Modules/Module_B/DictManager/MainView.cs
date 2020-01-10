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

namespace PublicManager.Modules.Module_B.DictManager
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

            List<Professions> list = ConnectionManager.Context.table("Professions").select("*").getList<Professions>(new Professions());
            foreach (Professions pfo in list)
            {
                List<object> cells = new List<object>();
                cells.Add(pfo.ProfessionNum);
                cells.Add(pfo.ProfessionCategory);
                cells.Add(pfo.ProfessionName);
                cells.Add(pfo.IsAcceptModify == "true");

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
                Professions prfs = ((Professions)dgvCatalogs.Rows[e.RowIndex].Tag);

                if (prfs != null)
                {
                    if (e.ColumnIndex == dgvCatalogs.Columns.Count - 1)
                    {
                        //删除
                        if (MessageBox.Show("真的要删除吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            ConnectionManager.Context.table("Professions").where("ProfessionID='" + prfs.ProfessionID + "'").delete();

                            List<Professions> list = ConnectionManager.Context.table("Professions").select("*").getList<Professions>(new Professions());
                            int proIndex = 0;
                            foreach (Professions pObj in list)
                            {
                                proIndex++;
                                pObj.ProfessionNum = proIndex.ToString();
                                pObj.copyTo(ConnectionManager.Context.table("Professions")).where("ProfessionID='" + pObj.ProfessionID + "'").update();
                            }

                            updateCatalogs();
                        }
                    }
                    else if (e.ColumnIndex == dgvCatalogs.Columns.Count - 2)
                    {
                        //编辑
                        Forms.DictEditForm def = new Forms.DictEditForm();
                        def.ProfessionName = prfs.ProfessionName;
                        def.IsAcceptModify = prfs.IsAcceptModify == "true";
                        if (def.ShowDialog() == DialogResult.OK)
                        {
                            prfs.ProfessionName = def.ProfessionName;
                            prfs.IsAcceptModify = def.IsAcceptModify ? "true" : "false";
                            prfs.copyTo(ConnectionManager.Context.table("Professions")).where("ProfessionID='" + prfs.ProfessionID + "'").update();

                            updateCatalogs();
                        }
                    }
                }
            }
        }
    }
}