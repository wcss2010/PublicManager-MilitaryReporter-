﻿namespace PublicManager.Modules.Reporter
{
    partial class ReporterModuleController
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
            this.btnSetSourceDir = new DevExpress.XtraBars.BarButtonItem();
            this.btnSetDestDir = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportWithSelected = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportToExcel = new DevExpress.XtraBars.BarButtonItem();
            this.rpMaster = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgDir = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpbLoad = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgExport = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.fbdFolderSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.rcTopBar)).BeginInit();
            this.SuspendLayout();
            // 
            // rcTopBar
            // 
            this.rcTopBar.ExpandCollapseItem.Id = 0;
            this.rcTopBar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rcTopBar.ExpandCollapseItem,
            this.btnSetSourceDir,
            this.btnSetDestDir,
            this.btnImportAll,
            this.btnImportWithSelected,
            this.btnExportToExcel});
            this.rcTopBar.Location = new System.Drawing.Point(0, 0);
            this.rcTopBar.MaxItemId = 6;
            this.rcTopBar.Name = "rcTopBar";
            this.rcTopBar.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpMaster});
            this.rcTopBar.Size = new System.Drawing.Size(1010, 145);
            // 
            // btnSetSourceDir
            // 
            this.btnSetSourceDir.Caption = "设置主目录";
            this.btnSetSourceDir.Id = 1;
            this.btnSetSourceDir.LargeGlyph = global::PublicManager.Properties.Resources.folderA;
            this.btnSetSourceDir.Name = "btnSetSourceDir";
            this.btnSetSourceDir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSetSourceDir_ItemClick);
            // 
            // btnSetDestDir
            // 
            this.btnSetDestDir.Caption = "设置解压目录";
            this.btnSetDestDir.Id = 2;
            this.btnSetDestDir.LargeGlyph = global::PublicManager.Properties.Resources.folderB;
            this.btnSetDestDir.Name = "btnSetDestDir";
            this.btnSetDestDir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSetDestDir_ItemClick);
            // 
            // btnImportAll
            // 
            this.btnImportAll.Caption = "导入所有";
            this.btnImportAll.Id = 3;
            this.btnImportAll.LargeGlyph = global::PublicManager.Properties.Resources.importA;
            this.btnImportAll.Name = "btnImportAll";
            this.btnImportAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportAll_ItemClick);
            // 
            // btnImportWithSelected
            // 
            this.btnImportWithSelected.Caption = "选择性导入";
            this.btnImportWithSelected.Id = 4;
            this.btnImportWithSelected.LargeGlyph = global::PublicManager.Properties.Resources.importB;
            this.btnImportWithSelected.Name = "btnImportWithSelected";
            this.btnImportWithSelected.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportWithSelected_ItemClick);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Caption = "导出到Excel";
            this.btnExportToExcel.Id = 5;
            this.btnExportToExcel.LargeGlyph = global::PublicManager.Properties.Resources.printtoexcel;
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportToExcel_ItemClick);
            // 
            // rpMaster
            // 
            this.rpMaster.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgDir,
            this.rpbLoad,
            this.rpgExport});
            this.rpMaster.Name = "rpMaster";
            this.rpMaster.Text = "建议书";
            // 
            // rpgDir
            // 
            this.rpgDir.ItemLinks.Add(this.btnSetSourceDir);
            this.rpgDir.ItemLinks.Add(this.btnSetDestDir);
            this.rpgDir.Name = "rpgDir";
            this.rpgDir.Text = "目录";
            // 
            // rpbLoad
            // 
            this.rpbLoad.ItemLinks.Add(this.btnImportAll);
            this.rpbLoad.ItemLinks.Add(this.btnImportWithSelected);
            this.rpbLoad.Name = "rpbLoad";
            this.rpbLoad.Text = "导入";
            // 
            // rpgExport
            // 
            this.rpgExport.ItemLinks.Add(this.btnExportToExcel);
            this.rpgExport.Name = "rpgExport";
            this.rpgExport.Text = "导出";
            // 
            // sfdExport
            // 
            this.sfdExport.Filter = "Excel文件(.xlsx)|*.xlsx";
            // 
            // ReporterModuleController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rcTopBar);
            this.Name = "ReporterModuleController";
            this.Size = new System.Drawing.Size(1010, 143);
            ((System.ComponentModel.ISupportInitialize)(this.rcTopBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl rcTopBar;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpMaster;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgDir;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpbLoad;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgExport;
        private DevExpress.XtraBars.BarButtonItem btnSetSourceDir;
        private DevExpress.XtraBars.BarButtonItem btnSetDestDir;
        private DevExpress.XtraBars.BarButtonItem btnImportAll;
        private DevExpress.XtraBars.BarButtonItem btnImportWithSelected;
        private DevExpress.XtraBars.BarButtonItem btnExportToExcel;
        private System.Windows.Forms.FolderBrowserDialog fbdFolderSelect;
        private System.Windows.Forms.SaveFileDialog sfdExport;
    }
}
