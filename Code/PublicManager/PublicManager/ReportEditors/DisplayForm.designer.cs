namespace PublicReporter
{
    partial class DisplayForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayForm));
            this.ssHintBar = new System.Windows.Forms.StatusStrip();
            this.tsslHint = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsButtonBar = new System.Windows.Forms.ToolStrip();
            this.tssrLine = new System.Windows.Forms.ToolStripSeparator();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.scContent = new System.Windows.Forms.SplitContainer();
            this.tvSubjects = new System.Windows.Forms.TreeView();
            this.ilNodeImage = new System.Windows.Forms.ImageList(this.components);
            this.plTreeButtons = new System.Windows.Forms.Panel();
            this.ssHintBar.SuspendLayout();
            this.tsButtonBar.SuspendLayout();
            this.scContent.Panel1.SuspendLayout();
            this.scContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssHintBar
            // 
            this.ssHintBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslHint});
            this.ssHintBar.Location = new System.Drawing.Point(0, 555);
            this.ssHintBar.Name = "ssHintBar";
            this.ssHintBar.Size = new System.Drawing.Size(982, 22);
            this.ssHintBar.TabIndex = 0;
            this.ssHintBar.Text = "statusStrip1";
            // 
            // tsslHint
            // 
            this.tsslHint.Font = new System.Drawing.Font("仿宋", 15F);
            this.tsslHint.Name = "tsslHint";
            this.tsslHint.Size = new System.Drawing.Size(0, 17);
            this.tsslHint.Tag = "DefaultDisplayControl";
            // 
            // tsButtonBar
            // 
            this.tsButtonBar.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsButtonBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssrLine,
            this.btnExit});
            this.tsButtonBar.Location = new System.Drawing.Point(0, 0);
            this.tsButtonBar.Name = "tsButtonBar";
            this.tsButtonBar.Size = new System.Drawing.Size(982, 59);
            this.tsButtonBar.TabIndex = 1;
            // 
            // tssrLine
            // 
            this.tssrLine.Name = "tssrLine";
            this.tssrLine.Size = new System.Drawing.Size(6, 59);
            this.tssrLine.Tag = "DefaultDisplayControl";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("仿宋", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(53, 56);
            this.btnExit.Tag = "DefaultDisplayControl";
            this.btnExit.Text = "退出";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // scContent
            // 
            this.scContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContent.Location = new System.Drawing.Point(0, 59);
            this.scContent.Name = "scContent";
            // 
            // scContent.Panel1
            // 
            this.scContent.Panel1.Controls.Add(this.tvSubjects);
            this.scContent.Panel1.Controls.Add(this.plTreeButtons);
            // 
            // scContent.Panel2
            // 
            this.scContent.Panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.scContent.Size = new System.Drawing.Size(982, 496);
            this.scContent.SplitterDistance = 250;
            this.scContent.TabIndex = 2;
            // 
            // tvSubjects
            // 
            this.tvSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSubjects.Font = new System.Drawing.Font("仿宋", 15F);
            this.tvSubjects.FullRowSelect = true;
            this.tvSubjects.HideSelection = false;
            this.tvSubjects.ImageIndex = 0;
            this.tvSubjects.ImageList = this.ilNodeImage;
            this.tvSubjects.Location = new System.Drawing.Point(0, 0);
            this.tvSubjects.Name = "tvSubjects";
            this.tvSubjects.SelectedImageIndex = 0;
            this.tvSubjects.Size = new System.Drawing.Size(250, 486);
            this.tvSubjects.TabIndex = 0;
            // 
            // ilNodeImage
            // 
            this.ilNodeImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ilNodeImage.ImageSize = new System.Drawing.Size(16, 16);
            this.ilNodeImage.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // plTreeButtons
            // 
            this.plTreeButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plTreeButtons.Location = new System.Drawing.Point(0, 486);
            this.plTreeButtons.Margin = new System.Windows.Forms.Padding(0);
            this.plTreeButtons.Name = "plTreeButtons";
            this.plTreeButtons.Size = new System.Drawing.Size(250, 10);
            this.plTreeButtons.TabIndex = 1;
            // 
            // DisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 577);
            this.Controls.Add(this.scContent);
            this.Controls.Add(this.tsButtonBar);
            this.Controls.Add(this.ssHintBar);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "申报系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ssHintBar.ResumeLayout(false);
            this.ssHintBar.PerformLayout();
            this.tsButtonBar.ResumeLayout(false);
            this.tsButtonBar.PerformLayout();
            this.scContent.Panel1.ResumeLayout(false);
            this.scContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssHintBar;
        private System.Windows.Forms.ToolStripStatusLabel tsslHint;
        private System.Windows.Forms.ToolStrip tsButtonBar;
        private System.Windows.Forms.SplitContainer scContent;
        private System.Windows.Forms.TreeView tvSubjects;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripSeparator tssrLine;
        private System.Windows.Forms.ImageList ilNodeImage;
        private System.Windows.Forms.Panel plTreeButtons;
    }
}

