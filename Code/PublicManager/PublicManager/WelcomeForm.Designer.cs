namespace PublicManager
{
    partial class WelcomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnToA = new System.Windows.Forms.Button();
            this.btnToB = new System.Windows.Forms.Button();
            this.lblUnitA = new System.Windows.Forms.Label();
            this.btnSetUnitA = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(816, 329);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnToA
            // 
            this.btnToA.BackColor = System.Drawing.Color.SteelBlue;
            this.btnToA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToA.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnToA.ForeColor = System.Drawing.Color.Black;
            this.btnToA.Location = new System.Drawing.Point(399, 363);
            this.btnToA.Name = "btnToA";
            this.btnToA.Size = new System.Drawing.Size(194, 47);
            this.btnToA.TabIndex = 1;
            this.btnToA.Text = "归口管理部门入口";
            this.btnToA.UseVisualStyleBackColor = false;
            this.btnToA.Click += new System.EventHandler(this.btnToA_Click);
            // 
            // btnToB
            // 
            this.btnToB.BackColor = System.Drawing.Color.SteelBlue;
            this.btnToB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToB.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnToB.ForeColor = System.Drawing.Color.Black;
            this.btnToB.Location = new System.Drawing.Point(196, 363);
            this.btnToB.Name = "btnToB";
            this.btnToB.Size = new System.Drawing.Size(197, 47);
            this.btnToB.TabIndex = 1;
            this.btnToB.Text = "军队主管部门入口";
            this.btnToB.UseVisualStyleBackColor = false;
            this.btnToB.Click += new System.EventHandler(this.btnToB_Click);
            // 
            // lblUnitA
            // 
            this.lblUnitA.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUnitA.ForeColor = System.Drawing.Color.Red;
            this.lblUnitA.Location = new System.Drawing.Point(443, 429);
            this.lblUnitA.Name = "lblUnitA";
            this.lblUnitA.Size = new System.Drawing.Size(368, 30);
            this.lblUnitA.TabIndex = 0;
            this.lblUnitA.Text = "...";
            this.lblUnitA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSetUnitA
            // 
            this.btnSetUnitA.Location = new System.Drawing.Point(599, 380);
            this.btnSetUnitA.Name = "btnSetUnitA";
            this.btnSetUnitA.Size = new System.Drawing.Size(88, 30);
            this.btnSetUnitA.TabIndex = 1;
            this.btnSetUnitA.TabStop = true;
            this.btnSetUnitA.Text = "设置所属单位";
            this.btnSetUnitA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSetUnitA.Click += new System.EventHandler(this.btnSetUnitA_Click);
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(816, 467);
            this.Controls.Add(this.lblUnitA);
            this.Controls.Add(this.btnToB);
            this.Controls.Add(this.btnSetUnitA);
            this.Controls.Add(this.btnToA);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WelcomeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "汇总系统";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnToA;
        private System.Windows.Forms.Button btnToB;
        private System.Windows.Forms.Label lblUnitA;
        private System.Windows.Forms.LinkLabel btnSetUnitA;
    }
}