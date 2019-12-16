namespace PublicManager.Modules
{
    partial class MainMenuController
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
            this.bvcMenus = new DevExpress.XtraBars.Ribbon.BackstageViewControl();
            this.backstageViewClientControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.backstageViewTabItem1 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.backstageViewClientControl2 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.backstageViewTabItem2 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.backstageViewClientControl3 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.backstageViewTabItem3 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            ((System.ComponentModel.ISupportInitialize)(this.bvcMenus)).BeginInit();
            this.bvcMenus.SuspendLayout();
            this.SuspendLayout();
            // 
            // bvcMenus
            // 
            this.bvcMenus.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Yellow;
            this.bvcMenus.Controls.Add(this.backstageViewClientControl1);
            this.bvcMenus.Controls.Add(this.backstageViewClientControl2);
            this.bvcMenus.Controls.Add(this.backstageViewClientControl3);
            this.bvcMenus.Items.Add(this.backstageViewTabItem1);
            this.bvcMenus.Items.Add(this.backstageViewTabItem2);
            this.bvcMenus.Items.Add(this.backstageViewTabItem3);
            this.bvcMenus.Location = new System.Drawing.Point(3, 3);
            this.bvcMenus.Name = "bvcMenus";
            this.bvcMenus.SelectedTab = this.backstageViewTabItem3;
            this.bvcMenus.SelectedTabIndex = 2;
            this.bvcMenus.Size = new System.Drawing.Size(1068, 427);
            this.bvcMenus.TabIndex = 1;
            this.bvcMenus.Text = "backstageViewControl1";
            // 
            // backstageViewClientControl1
            // 
            this.backstageViewClientControl1.Location = new System.Drawing.Point(192, 0);
            this.backstageViewClientControl1.Name = "backstageViewClientControl1";
            this.backstageViewClientControl1.Size = new System.Drawing.Size(876, 427);
            this.backstageViewClientControl1.TabIndex = 1;
            // 
            // backstageViewTabItem1
            // 
            this.backstageViewTabItem1.Caption = "演示菜单项1";
            this.backstageViewTabItem1.ContentControl = this.backstageViewClientControl1;
            this.backstageViewTabItem1.Name = "backstageViewTabItem1";
            this.backstageViewTabItem1.Selected = false;
            // 
            // backstageViewClientControl2
            // 
            this.backstageViewClientControl2.Location = new System.Drawing.Point(192, 0);
            this.backstageViewClientControl2.Name = "backstageViewClientControl2";
            this.backstageViewClientControl2.Size = new System.Drawing.Size(876, 427);
            this.backstageViewClientControl2.TabIndex = 2;
            // 
            // backstageViewTabItem2
            // 
            this.backstageViewTabItem2.Caption = "演示菜单项2";
            this.backstageViewTabItem2.ContentControl = this.backstageViewClientControl2;
            this.backstageViewTabItem2.Name = "backstageViewTabItem2";
            this.backstageViewTabItem2.Selected = false;
            // 
            // backstageViewClientControl3
            // 
            this.backstageViewClientControl3.Location = new System.Drawing.Point(132, 0);
            this.backstageViewClientControl3.Name = "backstageViewClientControl3";
            this.backstageViewClientControl3.Size = new System.Drawing.Size(936, 427);
            this.backstageViewClientControl3.TabIndex = 3;
            // 
            // backstageViewTabItem3
            // 
            this.backstageViewTabItem3.Caption = "演示菜单项3";
            this.backstageViewTabItem3.ContentControl = this.backstageViewClientControl3;
            this.backstageViewTabItem3.Name = "backstageViewTabItem3";
            this.backstageViewTabItem3.Selected = true;
            // 
            // MainMenuController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bvcMenus);
            this.Name = "MainMenuController";
            this.Size = new System.Drawing.Size(1075, 435);
            ((System.ComponentModel.ISupportInitialize)(this.bvcMenus)).EndInit();
            this.bvcMenus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.BackstageViewControl bvcMenus;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl2;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl3;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem1;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem2;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem3;
    }
}
