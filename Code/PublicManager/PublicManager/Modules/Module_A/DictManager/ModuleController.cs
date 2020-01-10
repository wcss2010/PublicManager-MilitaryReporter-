using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB.Entitys;
using System.IO;
using PublicManager.DB;

namespace PublicManager.Modules.Module_A.DictManager
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

        private void btnLoadData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = string.Empty;
            ofd.Filter = "*.json|*.json";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //删除所有
                    ConnectionManager.Context.table("Professions").delete();

                    //添加专业类别
                    List<Professions> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Professions>>(File.ReadAllText(ofd.FileName));
                    foreach (Professions pfo in list)
                    {
                        pfo.copyTo(ConnectionManager.Context.table("Professions")).insert();
                    }

                    //更新列表
                    tc.updateCatalogs();

                    MessageBox.Show("导入完成！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导入失败！Ex:" + ex.ToString());
                }
            }
        }
    }
}