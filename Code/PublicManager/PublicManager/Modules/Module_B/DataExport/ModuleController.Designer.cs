namespace PublicManager.Modules.Module_B.DataExport
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
            this.btnExportToExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddProject = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteProject = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditProject = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportToPkg = new DevExpress.XtraBars.BarButtonItem();
            this.rpMaster = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgElse = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.rcTopBar)).BeginInit();
            this.SuspendLayout();
            // 
            // rcTopBar
            // 
            this.rcTopBar.ExpandCollapseItem.Id = 0;
            this.rcTopBar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rcTopBar.ExpandCollapseItem,
            this.btnExportToExcel,
            this.btnAddProject,
            this.btnDeleteProject,
            this.btnEditProject,
            this.btnExportToPkg});
            this.rcTopBar.Location = new System.Drawing.Point(0, 0);
            this.rcTopBar.MaxItemId = 11;
            this.rcTopBar.Name = "rcTopBar";
            this.rcTopBar.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpMaster});
            this.rcTopBar.Size = new System.Drawing.Size(1182, 145);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Caption = "导出到Word";
            this.btnExportToExcel.Id = 6;
            this.btnExportToExcel.LargeGlyph = global::PublicManager.Properties.Resources.export;
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnAddProject
            // 
            this.btnAddProject.Caption = "新增专项项目";
            this.btnAddProject.Id = 7;
            this.btnAddProject.LargeGlyph = global::PublicManager.Properties.Resources.add;
            this.btnAddProject.Name = "btnAddProject";
            // 
            // btnDeleteProject
            // 
            this.btnDeleteProject.Caption = "删除专项项目";
            this.btnDeleteProject.Id = 8;
            this.btnDeleteProject.LargeGlyph = global::PublicManager.Properties.Resources.delete;
            this.btnDeleteProject.Name = "btnDeleteProject";
            this.btnDeleteProject.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnEditProject
            // 
            this.btnEditProject.Caption = "编辑专项项目";
            this.btnEditProject.Id = 9;
            this.btnEditProject.LargeGlyph = global::PublicManager.Properties.Resources.edit;
            this.btnEditProject.Name = "btnEditProject";
            // 
            // btnExportToPkg
            // 
            this.btnExportToPkg.Caption = "导出数据包";
            this.btnExportToPkg.Id = 10;
            this.btnExportToPkg.LargeGlyph = global::PublicManager.Properties.Resources.export3;
            this.btnExportToPkg.Name = "btnExportToPkg";
            this.btnExportToPkg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // rpMaster
            // 
            this.rpMaster.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgElse});
            this.rpMaster.Name = "rpMaster";
            this.rpMaster.Text = "论证报告书";
            // 
            // rpgElse
            // 
            this.rpgElse.ItemLinks.Add(this.btnExportToExcel);
            this.rpgElse.ItemLinks.Add(this.btnExportToPkg);
            this.rpgElse.Name = "rpgElse";
            this.rpgElse.Text = "其它";
            // 
            // ModuleController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rcTopBar);
            this.Name = "ModuleController";
            this.Size = new System.Drawing.Size(1182, 152);
            ((System.ComponentModel.ISupportInitialize)(this.rcTopBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl rcTopBar;
        private DevExpress.XtraBars.BarButtonItem btnExportToExcel;
        private DevExpress.XtraBars.BarButtonItem btnAddProject;
        private DevExpress.XtraBars.BarButtonItem btnDeleteProject;
        private DevExpress.XtraBars.BarButtonItem btnEditProject;
        private DevExpress.XtraBars.BarButtonItem btnExportToPkg;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpMaster;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgElse;
    }
}
