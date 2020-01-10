using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB;
using PublicManager.DB.Entitys;
using System.IO;

namespace PublicManager.Modules.Module_B.DictManager
{
    public partial class ModuleController : BaseModuleController
    {
        private MainView tc;

        public ModuleController()
        {
            InitializeComponent();
        }

        public override DevExpress.XtraBars.Ribbon.RibbonPage[] getTopBarPages()
        {
            return new DevExpress.XtraBars.Ribbon.RibbonPage[] { rpMaster };
        }

        public override void start()
        {
            //显示详细页
            showDetailPage();
        }

        /// <summary>
        /// 显示详细页
        /// </summary>
        private void showDetailPage()
        {
            DisplayControl.Controls.Clear();
            tc = new MainView();
            tc.Dock = DockStyle.Fill;
            DisplayControl.Controls.Add(tc);

            tc.updateCatalogs();
        }

        public override void stop()
        {
            base.stop();
        }

        private void btnAddProfession_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PublicManager.Modules.Module_B.DictManager.Forms.DictEditForm def = new PublicManager.Modules.Module_B.DictManager.Forms.DictEditForm();
            if (def.ShowDialog() == DialogResult.OK)
            {
                Professions pfoObj = new Professions();
                pfoObj.ProfessionID = Guid.NewGuid().ToString();
                pfoObj.ProfessionName = def.ProfessionName;
                pfoObj.IsAcceptModify = def.IsAcceptModify ? "true" : "false";
                pfoObj.ProfessionCategory = def.ProfessionName;
                pfoObj.copyTo(ConnectionManager.Context.table("Professions")).insert();

                List<Professions> list = ConnectionManager.Context.table("Professions").select("*").getList<Professions>(new Professions());
                int proIndex = 0;
                foreach (Professions prf in list)
                {
                    proIndex++;
                    prf.ProfessionNum = proIndex.ToString();
                    prf.copyTo(ConnectionManager.Context.table("Professions")).where("ProfessionID='" + prf.ProfessionID + "'").update();
                }

                tc.updateCatalogs();
            }
        }

        private void btnExportToXml_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = string.Empty;
            sfd.Filter = "*.json|*.json";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<Professions> list = ConnectionManager.Context.table("Professions").select("*").getList<Professions>(new Professions());
                    File.WriteAllText(sfd.FileName, Newtonsoft.Json.JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented));

                    MessageBox.Show("生成完成！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("生成失败！Ex:" + ex.ToString());
                }
            }
        }
    }
}