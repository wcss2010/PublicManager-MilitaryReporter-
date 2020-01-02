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
            this.groupBox1 = new System.Windows.Forms.Panel();
            this.lblUnitA = new System.Windows.Forms.Label();
            this.btnSetUnitA = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.btnToA.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnToA.Location = new System.Drawing.Point(75, 337);
            this.btnToA.Name = "btnToA";
            this.btnToA.Size = new System.Drawing.Size(331, 84);
            this.btnToA.TabIndex = 1;
            this.btnToA.Text = "其它地区入口";
            this.btnToA.UseVisualStyleBackColor = true;
            this.btnToA.Click += new System.EventHandler(this.btnToA_Click);
            // 
            // btnToB
            // 
            this.btnToB.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnToB.Location = new System.Drawing.Point(412, 337);
            this.btnToB.Name = "btnToB";
            this.btnToB.Size = new System.Drawing.Size(331, 84);
            this.btnToB.TabIndex = 1;
            this.btnToB.Text = "军委机构入口";
            this.btnToB.UseVisualStyleBackColor = true;
            this.btnToB.Click += new System.EventHandler(this.btnToB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblUnitA);
            this.groupBox1.Controls.Add(this.btnSetUnitA);
            this.groupBox1.Location = new System.Drawing.Point(75, 429);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 30);
            this.groupBox1.TabIndex = 2;
            // 
            // lblUnitA
            // 
            this.lblUnitA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUnitA.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUnitA.Location = new System.Drawing.Point(0, 0);
            this.lblUnitA.Name = "lblUnitA";
            this.lblUnitA.Size = new System.Drawing.Size(243, 30);
            this.lblUnitA.TabIndex = 0;
            this.lblUnitA.Text = "...";
            this.lblUnitA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSetUnitA
            // 
            this.btnSetUnitA.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSetUnitA.Location = new System.Drawing.Point(243, 0);
            this.btnSetUnitA.Name = "btnSetUnitA";
            this.btnSetUnitA.Size = new System.Drawing.Size(88, 30);
            this.btnSetUnitA.TabIndex = 1;
            this.btnSetUnitA.Text = "设置所属单位";
            this.btnSetUnitA.UseVisualStyleBackColor = true;
            this.btnSetUnitA.Click += new System.EventHandler(this.btnSetUnitA_Click);
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 467);
            this.Controls.Add(this.btnToB);
            this.Controls.Add(this.groupBox1);
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
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnToA;
        private System.Windows.Forms.Button btnToB;
        private System.Windows.Forms.Panel groupBox1;
        private System.Windows.Forms.Label lblUnitA;
        private System.Windows.Forms.Button btnSetUnitA;
    }
}