namespace PublicManager.Modules.Module_B.PkgImporter
{
    partial class MainView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvCatalogs = new PublicManager.Modules.DataGridViewEx();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.plTopBar = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbxKeys = new System.Windows.Forms.ComboBox();
            this.plRules = new System.Windows.Forms.Panel();
            this.rbProjectType = new System.Windows.Forms.RadioButton();
            this.rbDutyUnit = new System.Windows.Forms.RadioButton();
            this.rbProjectName = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogs)).BeginInit();
            this.plTopBar.SuspendLayout();
            this.plRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCatalogs
            // 
            this.dgvCatalogs.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCatalogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCatalogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCatalogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column12,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column13,
            this.Column7,
            this.Column8,
            this.Column14,
            this.Column9,
            this.Column10,
            this.Column11});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCatalogs.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCatalogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCatalogs.Location = new System.Drawing.Point(0, 38);
            this.dgvCatalogs.MultiSelect = false;
            this.dgvCatalogs.Name = "dgvCatalogs";
            this.dgvCatalogs.ReadOnly = true;
            this.dgvCatalogs.RowHeadersVisible = false;
            this.dgvCatalogs.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 10.5F);
            this.dgvCatalogs.RowTemplate.Height = 23;
            this.dgvCatalogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCatalogs.Size = new System.Drawing.Size(1276, 646);
            this.dgvCatalogs.TabIndex = 1;
            this.dgvCatalogs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCatalogs_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.MinimumWidth = 60;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "项目类型";
            this.Column12.MinimumWidth = 120;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Visible = false;
            this.Column12.Width = 120;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "项目名称";
            this.Column2.MinimumWidth = 120;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "预期成果";
            this.Column3.MinimumWidth = 120;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 120;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "周期(月)";
            this.Column4.MinimumWidth = 90;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 90;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "经费概算(万元)";
            this.Column5.MinimumWidth = 130;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 130;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "项目类别";
            this.Column6.MinimumWidth = 120;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 120;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "专业类别(类别内序号)";
            this.Column13.MinimumWidth = 180;
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Visible = false;
            this.Column13.Width = 180;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "责任单位(下级单位)";
            this.Column7.MinimumWidth = 160;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 160;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "备注";
            this.Column8.MinimumWidth = 120;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 120;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "导入时间";
            this.Column14.MinimumWidth = 120;
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 120;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "";
            this.Column9.MinimumWidth = 60;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column9.Text = "详情";
            this.Column9.UseColumnTextForLinkValue = true;
            this.Column9.Width = 60;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "";
            this.Column10.MinimumWidth = 60;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column10.Text = "链接";
            this.Column10.UseColumnTextForLinkValue = true;
            this.Column10.Width = 60;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "";
            this.Column11.MinimumWidth = 60;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Text = "删除";
            this.Column11.UseColumnTextForButtonValue = true;
            this.Column11.Width = 60;
            // 
            // plTopBar
            // 
            this.plTopBar.Controls.Add(this.btnSearch);
            this.plTopBar.Controls.Add(this.cbxKeys);
            this.plTopBar.Controls.Add(this.plRules);
            this.plTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTopBar.Location = new System.Drawing.Point(0, 0);
            this.plTopBar.Name = "plTopBar";
            this.plTopBar.Padding = new System.Windows.Forms.Padding(6);
            this.plTopBar.Size = new System.Drawing.Size(1276, 38);
            this.plTopBar.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSearch.Location = new System.Drawing.Point(701, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 26);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbxKeys
            // 
            this.cbxKeys.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbxKeys.ItemHeight = 14;
            this.cbxKeys.Location = new System.Drawing.Point(357, 6);
            this.cbxKeys.Name = "cbxKeys";
            this.cbxKeys.Size = new System.Drawing.Size(344, 22);
            this.cbxKeys.TabIndex = 8;
            // 
            // plRules
            // 
            this.plRules.Controls.Add(this.rbProjectType);
            this.plRules.Controls.Add(this.rbDutyUnit);
            this.plRules.Controls.Add(this.rbProjectName);
            this.plRules.Dock = System.Windows.Forms.DockStyle.Left;
            this.plRules.Location = new System.Drawing.Point(6, 6);
            this.plRules.Name = "plRules";
            this.plRules.Padding = new System.Windows.Forms.Padding(4);
            this.plRules.Size = new System.Drawing.Size(351, 26);
            this.plRules.TabIndex = 7;
            // 
            // rbProjectType
            // 
            this.rbProjectType.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbProjectType.Location = new System.Drawing.Point(235, 4);
            this.rbProjectType.Name = "rbProjectType";
            this.rbProjectType.Size = new System.Drawing.Size(113, 18);
            this.rbProjectType.TabIndex = 1;
            this.rbProjectType.TabStop = true;
            this.rbProjectType.Text = "按项目类型查询";
            this.rbProjectType.UseVisualStyleBackColor = true;
            this.rbProjectType.CheckedChanged += new System.EventHandler(this.rbProjectType_CheckedChanged);
            // 
            // rbDutyUnit
            // 
            this.rbDutyUnit.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbDutyUnit.Location = new System.Drawing.Point(120, 4);
            this.rbDutyUnit.Name = "rbDutyUnit";
            this.rbDutyUnit.Size = new System.Drawing.Size(115, 18);
            this.rbDutyUnit.TabIndex = 3;
            this.rbDutyUnit.TabStop = true;
            this.rbDutyUnit.Text = "按责任单位查询";
            this.rbDutyUnit.UseVisualStyleBackColor = true;
            this.rbDutyUnit.CheckedChanged += new System.EventHandler(this.rbProjectType_CheckedChanged);
            // 
            // rbProjectName
            // 
            this.rbProjectName.Checked = true;
            this.rbProjectName.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbProjectName.Location = new System.Drawing.Point(4, 4);
            this.rbProjectName.Name = "rbProjectName";
            this.rbProjectName.Size = new System.Drawing.Size(116, 18);
            this.rbProjectName.TabIndex = 2;
            this.rbProjectName.TabStop = true;
            this.rbProjectName.Text = "按项目名称查询";
            this.rbProjectName.UseVisualStyleBackColor = true;
            this.rbProjectName.CheckedChanged += new System.EventHandler(this.rbProjectType_CheckedChanged);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvCatalogs);
            this.Controls.Add(this.plTopBar);
            this.Name = "MainView";
            this.Size = new System.Drawing.Size(1276, 684);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogs)).EndInit();
            this.plTopBar.ResumeLayout(false);
            this.plRules.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PublicManager.Modules.DataGridViewEx dgvCatalogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewLinkColumn Column9;
        private System.Windows.Forms.DataGridViewLinkColumn Column10;
        private System.Windows.Forms.DataGridViewButtonColumn Column11;
        private System.Windows.Forms.Panel plTopBar;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbxKeys;
        private System.Windows.Forms.Panel plRules;
        private System.Windows.Forms.RadioButton rbProjectType;
        private System.Windows.Forms.RadioButton rbDutyUnit;
        private System.Windows.Forms.RadioButton rbProjectName;


    }
}
