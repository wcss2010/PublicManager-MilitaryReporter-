namespace PublicManager.Modules.Module_B.PkgImporter.Forms
{
    partial class ImporterWithSelectedForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tlTestA = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFilteKeys = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gcIgnoreList = new DevExpress.XtraEditors.GroupControl();
            this.lvErrorList = new System.Windows.Forms.ListView();
            this.chID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.cbIsKeepProfessionConfig = new System.Windows.Forms.CheckBox();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcIgnoreList)).BeginInit();
            this.gcIgnoreList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.tlTestA);
            this.splitContainerControl1.Panel1.Controls.Add(this.panel1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcIgnoreList);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(854, 463);
            this.splitContainerControl1.SplitterPosition = 295;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // tlTestA
            // 
            this.tlTestA.CheckBoxes = true;
            this.tlTestA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlTestA.FullRowSelect = true;
            this.tlTestA.HideSelection = false;
            this.tlTestA.Location = new System.Drawing.Point(0, 0);
            this.tlTestA.Name = "tlTestA";
            this.tlTestA.Size = new System.Drawing.Size(295, 441);
            this.tlTestA.TabIndex = 0;
            this.tlTestA.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tlTestA_AfterCheck);
            this.tlTestA.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tlTestA_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtFilteKeys);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 441);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 22);
            this.panel1.TabIndex = 1;
            // 
            // txtFilteKeys
            // 
            this.txtFilteKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFilteKeys.Location = new System.Drawing.Point(48, 0);
            this.txtFilteKeys.Name = "txtFilteKeys";
            this.txtFilteKeys.Size = new System.Drawing.Size(247, 22);
            this.txtFilteKeys.TabIndex = 1;
            this.txtFilteKeys.TextChanged += new System.EventHandler(this.txtFilteKeys_TextChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "关键字:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gcIgnoreList
            // 
            this.gcIgnoreList.Controls.Add(this.lvErrorList);
            this.gcIgnoreList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcIgnoreList.Location = new System.Drawing.Point(0, 0);
            this.gcIgnoreList.Name = "gcIgnoreList";
            this.gcIgnoreList.Size = new System.Drawing.Size(554, 463);
            this.gcIgnoreList.TabIndex = 0;
            this.gcIgnoreList.Text = "是否需要覆盖已存在数据？";
            // 
            // lvErrorList
            // 
            this.lvErrorList.CheckBoxes = true;
            this.lvErrorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID});
            this.lvErrorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvErrorList.Location = new System.Drawing.Point(2, 21);
            this.lvErrorList.Name = "lvErrorList";
            this.lvErrorList.Size = new System.Drawing.Size(550, 440);
            this.lvErrorList.TabIndex = 1;
            this.lvErrorList.UseCompatibleStateImageBehavior = false;
            this.lvErrorList.View = System.Windows.Forms.View.Details;
            this.lvErrorList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvErrorList_ItemChecked);
            // 
            // chID
            // 
            this.chID.Text = "";
            this.chID.Width = 300;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.cbIsKeepProfessionConfig);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 463);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(854, 38);
            this.panelControl2.TabIndex = 5;
            // 
            // cbIsKeepProfessionConfig
            // 
            this.cbIsKeepProfessionConfig.AutoSize = true;
            this.cbIsKeepProfessionConfig.Checked = true;
            this.cbIsKeepProfessionConfig.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsKeepProfessionConfig.Location = new System.Drawing.Point(471, 11);
            this.cbIsKeepProfessionConfig.Name = "cbIsKeepProfessionConfig";
            this.cbIsKeepProfessionConfig.Size = new System.Drawing.Size(288, 18);
            this.cbIsKeepProfessionConfig.TabIndex = 1;
            this.cbIsKeepProfessionConfig.Text = "是否保留之前设置的专业类别信息(如果存在的话)";
            this.cbIsKeepProfessionConfig.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(780, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 34);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "导入";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ImporterWithSelectedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 501);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl2);
            this.MaximizeBox = false;
            this.Name = "ImporterWithSelectedForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据包更新";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcIgnoreList)).EndInit();
            this.gcIgnoreList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl gcIgnoreList;
        private System.Windows.Forms.TreeView tlTestA;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.ListView lvErrorList;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.CheckBox cbIsKeepProfessionConfig;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilteKeys;
    }
}