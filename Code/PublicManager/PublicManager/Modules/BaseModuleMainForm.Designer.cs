namespace PublicManager.Modules
{
    partial class BaseModuleMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseModuleMainForm));
            this.rcTopBar = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.skinRibbonGalleryBarItem1 = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.ribbonGalleryBarItem1 = new DevExpress.XtraBars.RibbonGalleryBarItem();
            this.btnSkinColorModify = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.bsiBottomText = new DevExpress.XtraBars.BarStaticItem();
            this.rpPageB = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgGroupA = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rsbStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.nbcLeftTree = new DevExpress.XtraNavBar.NavBarControl();
            this.plRightContent = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.rcTopBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbcLeftTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plRightContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rcTopBar
            // 
            this.rcTopBar.AutoSaveLayoutToXml = true;
            this.rcTopBar.ExpandCollapseItem.Id = 0;
            this.rcTopBar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rcTopBar.ExpandCollapseItem,
            this.barCheckItem1,
            this.skinRibbonGalleryBarItem1,
            this.ribbonGalleryBarItem1,
            this.btnSkinColorModify,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.bsiBottomText});
            this.rcTopBar.Location = new System.Drawing.Point(0, 0);
            this.rcTopBar.MaxItemId = 13;
            this.rcTopBar.Name = "rcTopBar";
            this.rcTopBar.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpPageB});
            this.rcTopBar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.rcTopBar.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcTopBar.ShowToolbarCustomizeItem = false;
            this.rcTopBar.Size = new System.Drawing.Size(1359, 147);
            this.rcTopBar.StatusBar = this.rsbStatusBar;
            this.rcTopBar.Toolbar.ShowCustomizeItem = false;
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "演示选择";
            this.barCheckItem1.Id = 1;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // skinRibbonGalleryBarItem1
            // 
            this.skinRibbonGalleryBarItem1.Caption = "skinRibbonGalleryBarItem1";
            this.skinRibbonGalleryBarItem1.Id = 3;
            this.skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
            this.skinRibbonGalleryBarItem1.GalleryItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(this.skinRibbonGalleryBarItem1_GalleryItemClick);
            // 
            // ribbonGalleryBarItem1
            // 
            this.ribbonGalleryBarItem1.Caption = "ribbonGalleryBarItem1";
            this.ribbonGalleryBarItem1.Id = 4;
            this.ribbonGalleryBarItem1.Name = "ribbonGalleryBarItem1";
            // 
            // btnSkinColorModify
            // 
            this.btnSkinColorModify.Caption = "自定义";
            this.btnSkinColorModify.Id = 5;
            this.btnSkinColorModify.LargeGlyph = global::PublicManager.Properties.Resources.ColorMixer_32x32;
            this.btnSkinColorModify.Name = "btnSkinColorModify";
            this.btnSkinColorModify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSkinColorModify_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 6;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 7;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 8;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "barButtonItem4";
            this.barButtonItem4.Id = 9;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "barButtonItem5";
            this.barButtonItem5.Id = 10;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "barButtonItem6";
            this.barButtonItem6.Id = 11;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // bsiBottomText
            // 
            this.bsiBottomText.Caption = "...";
            this.bsiBottomText.Id = 12;
            this.bsiBottomText.Name = "bsiBottomText";
            this.bsiBottomText.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // rpPageB
            // 
            this.rpPageB.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgGroupA});
            this.rpPageB.Name = "rpPageB";
            this.rpPageB.Text = "皮肤";
            // 
            // rpgGroupA
            // 
            this.rpgGroupA.ItemLinks.Add(this.skinRibbonGalleryBarItem1);
            this.rpgGroupA.ItemLinks.Add(this.btnSkinColorModify);
            this.rpgGroupA.Name = "rpgGroupA";
            this.rpgGroupA.Text = "皮肤";
            // 
            // rsbStatusBar
            // 
            this.rsbStatusBar.ItemLinks.Add(this.bsiBottomText);
            this.rsbStatusBar.Location = new System.Drawing.Point(0, 670);
            this.rsbStatusBar.Name = "rsbStatusBar";
            this.rsbStatusBar.Ribbon = this.rcTopBar;
            this.rsbStatusBar.Size = new System.Drawing.Size(1359, 31);
            // 
            // nbcLeftTree
            // 
            this.nbcLeftTree.ActiveGroup = null;
            this.nbcLeftTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nbcLeftTree.Location = new System.Drawing.Point(0, 0);
            this.nbcLeftTree.Name = "nbcLeftTree";
            this.nbcLeftTree.OptionsNavPane.ExpandedWidth = 176;
            this.nbcLeftTree.Size = new System.Drawing.Size(176, 523);
            this.nbcLeftTree.TabIndex = 2;
            this.nbcLeftTree.Text = "navBarControl1";
            this.nbcLeftTree.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            this.nbcLeftTree.NavPaneStateChanged += new System.EventHandler(this.nbcLeftTree_NavPaneStateChanged);
            this.nbcLeftTree.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.nbcLeftTree_ActiveGroupChanged);
            // 
            // plRightContent
            // 
            this.plRightContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plRightContent.Location = new System.Drawing.Point(0, 0);
            this.plRightContent.Name = "plRightContent";
            this.plRightContent.Size = new System.Drawing.Size(1178, 523);
            this.plRightContent.TabIndex = 3;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 147);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.nbcLeftTree);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.plRightContent);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1359, 523);
            this.splitContainerControl1.SplitterPosition = 176;
            this.splitContainerControl1.TabIndex = 6;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // BaseModuleMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 701);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.rsbStatusBar);
            this.Controls.Add(this.rcTopBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseModuleMainForm";
            this.Ribbon = this.rcTopBar;
            this.StatusBar = this.rsbStatusBar;
            this.Text = "XXXX系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.rcTopBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbcLeftTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plRightContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl rcTopBar;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpPageB;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar rsbStatusBar;
        private DevExpress.XtraNavBar.NavBarControl nbcLeftTree;
        private DevExpress.XtraEditors.PanelControl plRightContent;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgGroupA;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem skinRibbonGalleryBarItem1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.RibbonGalleryBarItem ribbonGalleryBarItem1;
        private DevExpress.XtraBars.BarButtonItem btnSkinColorModify;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarStaticItem bsiBottomText;

    }
}

