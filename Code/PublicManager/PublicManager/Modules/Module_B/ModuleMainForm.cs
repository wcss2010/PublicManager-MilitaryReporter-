using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PublicManager.Modules.Module_B
{
    public class ModuleMainForm : BaseModuleMainForm
    {
        public ModuleMainForm()
            : base()
        {
            Text = "军队主管部门汇总系统";
        }

        private DevExpress.XtraTreeList.TreeList treeListObj;
        private DevExpress.XtraNavBar.NavBarGroup firstPage;
        private TreeList treeListObj2;
        private DevExpress.XtraNavBar.NavBarGroup secondPage;
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
            ModuleDict["专业类别查看"] = new DictManager.ModuleController();
            ModuleDict["数据导出"] = new DataExport.ModuleController();
            ModuleDict["项目审核"] = new ProjectState.ModuleController();
        }

        protected override void initUI()
        {
            base.initUI();

            treeListObj = buildTreeControl(new string[] { string.Empty }, new string[] { "数据包汇总", "专业类别查看", "数据编辑", "数据导出" });
            treeListObj.MouseClick += treeListObj_MouseClick;
            firstPage = appendPage("数据汇总", PublicManager.Properties.Resources.Mail_32x32);
            firstPage.ControlContainer.Controls.Add(treeListObj);

            treeListObj2 = buildTreeControl(new string[] { string.Empty }, new string[] { "项目审核" });
            treeListObj2.MouseClick += treeListObj2_MouseClick;
            secondPage = appendPage("数据管理", PublicManager.Properties.Resources.Contact_32x32);
            secondPage.ControlContainer.Controls.Add(treeListObj2);
        }

        void treeListObj2_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
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