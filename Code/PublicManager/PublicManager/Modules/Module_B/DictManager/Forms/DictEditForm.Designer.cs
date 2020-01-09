namespace PublicManager.Modules.Module_B.DictManager.Forms
{
    partial class DictEditForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtProfessionName = new System.Windows.Forms.TextBox();
            this.lblTextBoxTitle = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbIsAcceptModify = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 37);
            this.panel1.TabIndex = 18;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Font = new System.Drawing.Font("宋体", 10F);
            this.btnOK.Location = new System.Drawing.Point(217, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 37);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10F);
            this.btnCancel.Location = new System.Drawing.Point(279, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 37);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(341, 68);
            this.panel2.TabIndex = 19;
            // 
            // txtProfessionName
            // 
            this.txtProfessionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProfessionName.Font = new System.Drawing.Font("宋体", 10F);
            this.txtProfessionName.Location = new System.Drawing.Point(106, 0);
            this.txtProfessionName.Multiline = true;
            this.txtProfessionName.Name = "txtProfessionName";
            this.txtProfessionName.Size = new System.Drawing.Size(215, 27);
            this.txtProfessionName.TabIndex = 1;
            // 
            // lblTextBoxTitle
            // 
            this.lblTextBoxTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTextBoxTitle.Font = new System.Drawing.Font("宋体", 10F);
            this.lblTextBoxTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTextBoxTitle.Name = "lblTextBoxTitle";
            this.lblTextBoxTitle.Size = new System.Drawing.Size(106, 27);
            this.lblTextBoxTitle.TabIndex = 0;
            this.lblTextBoxTitle.Text = "专业类别名称：";
            this.lblTextBoxTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtProfessionName);
            this.panel3.Controls.Add(this.lblTextBoxTitle);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(321, 27);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbIsAcceptModify);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 37);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(321, 27);
            this.panel4.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "是否允许修改：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbIsAcceptModify
            // 
            this.cbIsAcceptModify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbIsAcceptModify.Location = new System.Drawing.Point(106, 0);
            this.cbIsAcceptModify.Name = "cbIsAcceptModify";
            this.cbIsAcceptModify.Size = new System.Drawing.Size(215, 27);
            this.cbIsAcceptModify.TabIndex = 1;
            this.cbIsAcceptModify.UseVisualStyleBackColor = true;
            // 
            // DictEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 105);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DictEditForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加/编辑专业类别";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtProfessionName;
        private System.Windows.Forms.Label lblTextBoxTitle;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbIsAcceptModify;
    }
}