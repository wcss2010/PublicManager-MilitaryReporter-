using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PublicManager.DB;
using System.IO;
using PublicManager.DB.Entitys;
using NPOI.SS.Util;

namespace PublicManager.Modules.Module_A.PackageEditor
{
    public partial class ModuleController : BaseModuleController
    {
        private List<string> dutyUnitToProfessonLinks;
        /// <summary>
        /// 责任单位(有类别的)
        /// </summary>
        public List<string> DutyUnitToProfessonLinks
        {
            get { return dutyUnitToProfessonLinks; }
        }

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
    }
}