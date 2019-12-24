namespace PublicManager.Modules.Manager.Forms
{
    partial class AddOrUpdateProjectForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStudyMoney = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStudyTime = new System.Windows.Forms.ComboBox();
            this.txtDutyUnit = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目名称：";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Font = new System.Drawing.Font("宋体", 12F);
            this.txtProjectName.Location = new System.Drawing.Point(99, 16);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(486, 26);
            this.txtProjectName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(21, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "研究周期：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(21, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "研究经费：";
            // 
            // txtStudyMoney
            // 
            this.txtStudyMoney.Font = new System.Drawing.Font("宋体", 12F);
            this.txtStudyMoney.Location = new System.Drawing.Point(99, 80);
            this.txtStudyMoney.Name = "txtStudyMoney";
            this.txtStudyMoney.Size = new System.Drawing.Size(157, 26);
            this.txtStudyMoney.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(21, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "责任单位：";
            // 
            // txtStudyTime
            // 
            this.txtStudyTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtStudyTime.Font = new System.Drawing.Font("仿宋", 12F);
            this.txtStudyTime.FormattingEnabled = true;
            this.txtStudyTime.Items.AddRange(new object[] {
            "未知"});
            this.txtStudyTime.Location = new System.Drawing.Point(99, 48);
            this.txtStudyTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtStudyTime.Name = "txtStudyTime";
            this.txtStudyTime.Size = new System.Drawing.Size(157, 24);
            this.txtStudyTime.TabIndex = 14;
            // 
            // txtDutyUnit
            // 
            this.txtDutyUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtDutyUnit.Font = new System.Drawing.Font("仿宋", 12F);
            this.txtDutyUnit.FormattingEnabled = true;
            this.txtDutyUnit.Items.AddRange(new object[] {
            "未知"});
            this.txtDutyUnit.Location = new System.Drawing.Point(99, 112);
            this.txtDutyUnit.Margin = new System.Windows.Forms.Padding(4);
            this.txtDutyUnit.Name = "txtDutyUnit";
            this.txtDutyUnit.Size = new System.Drawing.Size(486, 24);
            this.txtDutyUnit.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 37);
            this.panel1.TabIndex = 16;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10F);
            this.btnCancel.Location = new System.Drawing.Point(552, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 37);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Font = new System.Drawing.Font("宋体", 10F);
            this.btnOK.Location = new System.Drawing.Point(490, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 37);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(262, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "万元";
            // 
            // AddOrUpdateProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 196);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtStudyTime);
            this.Controls.Add(this.txtDutyUnit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStudyMoney);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOrUpdateProjectForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增/编辑专项项目";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStudyMoney;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox txtStudyTime;
        private System.Windows.Forms.ComboBox txtDutyUnit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label5;
    }
}