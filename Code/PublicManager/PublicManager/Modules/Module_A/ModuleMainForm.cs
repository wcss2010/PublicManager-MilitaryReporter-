using DevExpress.XtraTreeList;
using PublicManager.DB;
using PublicManager.DB.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PublicManager.Modules.Module_A
{
    public class ModuleMainForm : BaseModuleMainForm
    {
        private List<string> dutyUnitToProfessonLinks;
        /// <summary>
        /// 责任单位(有类别的)
        /// </summary>
        public List<string> DutyUnitToProfessonLinks
        {
            get { return dutyUnitToProfessonLinks; }
        }

        private DevExpress.XtraTreeList.TreeList treeListObj;
        private DevExpress.XtraNavBar.NavBarGroup firstPage;

        protected override void initMenus()
        {
            base.initMenus();

            //menuController = new MainMenuController();
            //menuController.MenuControl.Width = 0;
            //menuController.MenuControl.Height = 0;
            //Controls.Add(menuController.MenuControl);
            //rcTopBar.ApplicationButtonDropDownControl = menuController.MenuControl;
        }

        protected override void initModules()
        {
            base.initModules();

            ModuleDict["数据包汇总"] = new PkgImporter.ReporterModuleController();
            ModuleDict["数据编辑"] = new DataManager.ModuleController();
            ModuleDict["专业类别维护"] = new DictManager.ModuleController();
            ModuleDict["数据导出"] = new DataExport.ModuleController();
        }

        protected override void initUI()
        {
            base.initUI();

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

            LocalUnit lu = ConnectionManager.Context.table("LocalUnit").select("*").getItem<LocalUnit>(new LocalUnit());
            Text = "其它机关(" + lu.LocalUnitName + ")汇总系统";
            
            if (string.IsNullOrEmpty(lu.LocalUnitID))
            {
                treeListObj = buildTreeControl(new string[] { string.Empty }, new string[] { "数据包汇总", "专业类别维护", "数据编辑", "数据导出" });
            }
            else
            {
                if (DutyUnitToProfessonLinks.Contains(lu.LocalUnitName))
                {
                    treeListObj = buildTreeControl(new string[] { string.Empty }, new string[] { "数据包汇总", "专业类别维护", "数据编辑", "数据导出" });
                }
                else
                {
                    treeListObj = buildTreeControl(new string[] { string.Empty }, new string[] { "数据包汇总", "数据编辑", "数据导出" });
                }
            }

            treeListObj.MouseClick += treeListObj_MouseClick;
            firstPage = appendPage("数据汇总", PublicManager.Properties.Resources.Mail_32x32);
            firstPage.ControlContainer.Controls.Add(treeListObj);
        }

        void treeListObj_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList tree = ((DevExpress.XtraTreeList.TreeList)sender);
            Point p = new Point(e.X, e.Y);　　//获取到鼠标点击的坐标位置
            TreeListHitInfo hitInfo = tree.CalcHitInfo(e.Location);
            if (hitInfo.HitInfoType == HitInfoType.Cell)
            {
                tree.SetFocusedNode(hitInfo.Node);         //这句话就是关键，用于选中节点　　

                //显示模块
                showModule(hitInfo.Node.GetDisplayText(0), true);
            }
        }
    }
}