﻿using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PublicManager.Modules.Module_A
{
    public class ModuleMainForm : BaseModuleMainForm
    {
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
            ModuleDict["数据管理"] = new DataManager.ModuleController();
            ModuleDict["字典维护"] = new DictManager.ModuleController();
            ModuleDict["数据导出"] = new DataExport.ModuleController();
        }

        protected override void initUI()
        {
            base.initUI();

            treeListObj = buildTreeControl(new string[] { string.Empty }, new string[] { "数据包汇总", "字典维护", "数据管理", "数据导出" });
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