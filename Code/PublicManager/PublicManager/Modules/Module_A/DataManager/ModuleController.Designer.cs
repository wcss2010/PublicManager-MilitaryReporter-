namespace PublicManager.Modules.Module_A.DataManager
{
    partial class ModuleController
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rcTopBar = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnAddProject = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteProject = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditProject = new DevExpress.XtraBars.BarButtonItem();
            this.btnSorts = new DevExpress.XtraBars.BarButtonItem();
            this.btnSetUnitA = new DevExpress.XtraBars.BarButtonItem();
            this.rpMaster = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgPrivateProject = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgElse = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.rcTopBar)).BeginInit();
            this.SuspendLayout();
            // 
            // rcTopBar
            // 
            this.rcTopBar.ExpandCollapseItem.Id = 0;
            this.rcTopBar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rcTopBar.ExpandCollapseItem,
            this.btnAddProject,
            this.btnDeleteProject,
            this.btnEditProject,
            this.btnSorts,
            this.btnSetUnitA});
            this.rcTopBar.Location = new System.Drawing.Point(0, 0);
            this.rcTopBar.MaxItemId = 13;
            this.rcTopBar.Name = "rcTopBar";
            this.rcTopBar.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpMaster});
            this.rcTopBar.Size = new System.Drawing.Size(1106, 145);
            // 
            // btnAddProject
            // 
            this.btnAddProject.Caption = "新增专项项目";
            this.btnAddProject.Id = 7;
            this.btnAddProject.LargeGlyph = global::PublicManager.Properties.Resources.add;
            this.btnAddProject.Name = "btnAddProject";
            this.btnAddProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddProject_ItemClick);
            // 
            // btnDeleteProject
            // 
            this.btnDeleteProject.Caption = "删除专项项目";
            this.btnDeleteProject.Id = 8;
            this.btnDeleteProject.LargeGlyph = global::PublicManager.Properties.Resources.delete;
            this.btnDeleteProject.Name = "btnDeleteProject";
            this.btnDeleteProject.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnDeleteProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteProject_ItemClick);
            // 
            // btnEditProject
            // 
            this.btnEditProject.Caption = "编辑专项项目";
            this.btnEditProject.Id = 9;
            this.btnEditProject.LargeGlyph = global::PublicManager.Properties.Resources.edit;
            this.btnEditProject.Name = "btnEditProject";
            this.btnEditProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditProject_ItemClick);
            // 
            // btnSorts
            // 
            this.btnSorts.Caption = "项目编辑";
            this.btnSorts.Id = 11;
            this.btnSorts.LargeGlyph = global::PublicManager.Properties.Resources.export2;
            this.btnSorts.Name = "btnSorts";
            this.btnSorts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSorts_ItemClick);
            // 
            // btnSetUnitA
            // 
            this.btnSetUnitA.Caption = "设置所属单位";
            this.btnSetUnitA.Id = 12;
            this.btnSetUnitA.LargeGlyph = global::PublicManager.Properties.Resources.Contact_32x32;
            this.btnSetUnitA.Name = "btnSetUnitA";
            this.btnSetUnitA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSetUnitA_ItemClick);
            // 
            // rpMaster
            // 
            this.rpMaster.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgPrivateProject,
            this.rpgElse});
            this.rpMaster.Name = "rpMaster";
            this.rpMaster.Text = "论证报告书";
            // 
            // rpgPrivateProject
            // 
            this.rpgPrivateProject.ItemLinks.Add(this.btnAddProject);
            this.rpgPrivateProject.ItemLinks.Add(this.btnDeleteProject);
            this.rpgPrivateProject.ItemLinks.Add(this.btnEditProject);
            this.rpgPrivateProject.Name = "rpgPrivateProject";
            this.rpgPrivateProject.Text = "专项项目";
            // 
            // rpgElse
            // 
            this.rpgElse.ItemLinks.Add(this.btnSorts);
            this.rpgElse.ItemLinks.Add(this.btnSetUnitA);
            this.rpgElse.Name = "rpgElse";
            this.rpgElse.Text = "其它";
            // 
            // ModuleController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rcTopBar);
            this.Name = "ModuleController";
            this.Size = new System.Drawing.Size(1106, 147);
            ((System.ComponentModel.ISupportInitialize)(this.rcTopBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl rcTopBar;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpMaster;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgPrivateProject;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgElse;
        private DevExpress.XtraBars.BarButtonItem btnAddProject;
        private DevExpress.XtraBars.BarButtonItem btnDeleteProject;
        private DevExpress.XtraBars.BarButtonItem btnEditProject;
        private DevExpress.XtraBars.BarButtonItem btnSorts;
        private DevExpress.XtraBars.BarButtonItem btnSetUnitA;
    }
}
