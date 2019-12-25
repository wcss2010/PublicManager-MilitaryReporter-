using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ColorWheel;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using PublicManager.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PublicManager.Modules
{
    public partial class BaseModuleMainForm : RibbonForm
    {
        private Dictionary<string, DevExpress.XtraNavBar.NavBarGroup> pageDict = new Dictionary<string, NavBarGroup>();
        /// <summary>
        /// 页字典
        /// </summary>
        protected Dictionary<string, DevExpress.XtraNavBar.NavBarGroup> PageDict
        {
            get { return pageDict; }
        }

        private static Dictionary<string, BaseModuleController> moduleDict = new Dictionary<string, BaseModuleController>();
        /// <summary>
        /// 模块字典(Key=名称,Value=模块控制器)
        /// </summary>
        public static Dictionary<string, BaseModuleController> ModuleDict
        {
            get { return BaseModuleMainForm.moduleDict; }
        }

        /// <summary>
        /// 菜单控制器
        /// </summary>
        protected MainMenuController menuController = null;

        public BaseModuleMainForm()
        {
            InitializeComponent();

            //加载菜单
            initMenus();

            //加载所有模块
            initModules();

            //显示第一页
            initUI();
        }

        protected virtual void initUI() { }

        protected virtual void initMenus() { }

        protected virtual void initModules() { }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="pageIco"></param>
        /// <returns></returns>
        public virtual DevExpress.XtraNavBar.NavBarGroup appendPage(string pageName, Image pageIco)
        {
            DevExpress.XtraNavBar.NavBarGroupControlContainer tempContainers = new NavBarGroupControlContainer();
            tempContainers.Appearance.BackColor = System.Drawing.SystemColors.Control;
            tempContainers.Appearance.Options.UseBackColor = true;
            tempContainers.Name = Guid.NewGuid().ToString();
            tempContainers.Size = new System.Drawing.Size(176, 402);
            tempContainers.TabIndex = 0;

            DevExpress.XtraNavBar.NavBarGroup tempGroup = new NavBarGroup();
            tempGroup.Caption = pageName;
            tempGroup.ControlContainer = tempContainers;
            tempGroup.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
            tempGroup.Expanded = true;
            tempGroup.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            tempGroup.GroupClientHeight = 264;
            tempGroup.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            tempGroup.LargeImage = pageIco;
            tempGroup.Name = Guid.NewGuid().ToString();

            this.nbcLeftTree.Controls.Add(tempContainers);
            this.nbcLeftTree.Groups.Add(tempGroup);

            pageDict[pageName] = tempGroup;

            return tempGroup;
        }

        /// <summary>
        /// 创建树控件
        /// </summary>
        public virtual DevExpress.XtraTreeList.TreeList buildTreeControl(string[] cols,string[] nodes)
        {
            List<DevExpress.XtraTreeList.Columns.TreeListColumn> colList = new List<DevExpress.XtraTreeList.Columns.TreeListColumn>();
            foreach (string c in cols)
            {
                DevExpress.XtraTreeList.Columns.TreeListColumn colObj = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                colObj.MinWidth = 52;
                colObj.Name = Guid.NewGuid().ToString();
                colObj.Caption = c;
                colObj.OptionsColumn.AllowFocus = false;
                colObj.Visible = true;
                colObj.VisibleIndex = 0;
                colObj.Width = 100;
                colList.Add(colObj);
            }

            DevExpress.XtraTreeList.TreeList resultControl = new TreeList();
            resultControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            resultControl.Columns.AddRange(colList.ToArray());
            resultControl.Dock = System.Windows.Forms.DockStyle.Fill;
            resultControl.Location = new System.Drawing.Point(0, 0);
            resultControl.Name = Guid.NewGuid().ToString();
            resultControl.BeginUnboundLoad();

            foreach (string n in nodes)
            {
                resultControl.AppendNode(new object[] { n }, -1);
            }

            resultControl.EndUnboundLoad();
            resultControl.OptionsBehavior.Editable = false;
            resultControl.OptionsView.ShowColumns = false;
            resultControl.OptionsView.ShowHorzLines = false;
            resultControl.OptionsView.ShowIndentAsRowStyle = true;
            resultControl.OptionsView.ShowIndicator = false;
            resultControl.OptionsView.ShowVertLines = false;
            resultControl.Size = new System.Drawing.Size(176, 402);
            resultControl.TabIndex = 0;

            return resultControl;
        }

        /// <summary>
        /// 显示模块
        /// </summary>
        /// <param name="name">模块名称</param>
        /// <param name="isDisableAllModules">是否屏蔽其它模块</param>
        public void showModule(string name, bool isDisableAllModules)
        {
            //检查是否需要屏蔽其它的模块
            if (isDisableAllModules)
            {
                foreach (BaseModuleController bmc in ModuleDict.Values)
                {
                    //停止
                    bmc.stop();
                }

                //清除顶部工具条
                int clearCount = rcTopBar.Pages.Count - 1;
                if (clearCount >= 1)
                {
                    for (int kk = 0; kk < clearCount; kk++)
                    {
                        rcTopBar.Pages.RemoveAt(0);
                    }
                }
            }

            //按名称搜索并显示模块
            BaseModuleController currentModule = null;
            if (ModuleDict.ContainsKey(name))
            {
                //设置当前模块引用
                currentModule = ModuleDict[name];

                //设置内容显示控件
                currentModule.DisplayControl = plRightContent;

                //设置底部提示文本控件
                currentModule.StatusLabelControl = bsiBottomText;

                //插入顶部工具条
                RibbonPage[] pages = currentModule.getTopBarPages();
                if (pages != null && pages.Length >= 1)
                {
                    //显示顶部工具条
                    foreach (RibbonPage rp in pages) 
                    {
                        if (rcTopBar.Pages.Contains(rp))
                        {
                            continue;
                        }
                        else
                        {
                            rcTopBar.Pages.Insert(rcTopBar.Pages.Count - 1, rp);
                        }
                    }
                    
                    //显示新添加的页面
                    rcTopBar.SelectedPage = pages[0];
                }

                //启动
                currentModule.start();
            }
        }

        private void nbcLeftTree_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            plRightContent.Controls.Clear();

            if (e.Group.ControlContainer.Controls.Count >= 1)
            {
                if (e.Group.ControlContainer.Controls[0] is TreeList)
                {
                    TreeList tl = ((TreeList)e.Group.ControlContainer.Controls[0]);
                    if (tl.Nodes.Count >= 1)
                    {
                        tl.SetFocusedNode(tl.Nodes[0]);
                        showModule(tl.Nodes[0].GetDisplayText(0), true);
                    }
                }
            }
        }

        private void nbcLeftTree_NavPaneStateChanged(object sender, EventArgs e)
        {

        }
        
        private void btnSkinColorModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ColorWheelForm form = new ColorWheelForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.SkinMaskColor = UserLookAndFeel.Default.SkinMaskColor;
            form.SkinMaskColor2 = UserLookAndFeel.Default.SkinMaskColor2;
            form.ShowDialog(this);

            MainConfig.Config.StringDict["皮肤颜色1"] = UserLookAndFeel.Default.SkinMaskColor != null ? UserLookAndFeel.Default.SkinMaskColor.ToArgb().ToString() : "-1";
            MainConfig.Config.StringDict["皮肤颜色2"] = UserLookAndFeel.Default.SkinMaskColor2 != null ? UserLookAndFeel.Default.SkinMaskColor2.ToArgb().ToString() : "-1";

            MainConfig.Config.StringDict["当前皮肤"] = UserLookAndFeel.Default.ActiveSkinName;
            MainConfig.saveConfig();
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="text"></param>
        public static void writeLog(string text)
        {
            //日志目录
            string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            //判断是否需要创建目录
            if (!System.IO.Directory.Exists(logPath))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(logPath);
                }
                catch (Exception ex) { }
            }

            //生成日志文件名称
            string fileFullName = System.IO.Path.Combine(logPath, string.Format("{0}.txt", DateTime.Now.ToString("yyyyMMdd")));

            //写日志
            try
            {
                File.AppendAllText(fileFullName, DateTime.Now.ToString() + ":" + text + Environment.NewLine);
            }
            catch (Exception ex) { }

            //错误计数
            ProgressForm.errorCount++;
        }
            
        private void skinRibbonGalleryBarItem1_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
        {
            MainConfig.Config.StringDict["当前皮肤"] = string.Concat(e.Item.Tag);
            MainConfig.saveConfig();
        }
    }
}