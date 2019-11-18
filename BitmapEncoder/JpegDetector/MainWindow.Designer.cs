namespace JpegDetector
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.pbImage1 = new System.Windows.Forms.PictureBox();
            this.pbImage2 = new System.Windows.Forms.PictureBox();
            this.toolStripMainMenu = new System.Windows.Forms.ToolStrip();
            this.btnOpen1 = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnDetect = new System.Windows.Forms.ToolStripButton();
            this.fdOpen = new System.Windows.Forms.OpenFileDialog();
            this.fdSave = new System.Windows.Forms.SaveFileDialog();
            this.btnToggle = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage2)).BeginInit();
            this.toolStripMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbImage1
            // 
            this.pbImage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage1.Location = new System.Drawing.Point(12, 28);
            this.pbImage1.Name = "pbImage1";
            this.pbImage1.Size = new System.Drawing.Size(300, 300);
            this.pbImage1.TabIndex = 0;
            this.pbImage1.TabStop = false;
            // 
            // pbImage2
            // 
            this.pbImage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage2.Location = new System.Drawing.Point(318, 28);
            this.pbImage2.Name = "pbImage2";
            this.pbImage2.Size = new System.Drawing.Size(300, 300);
            this.pbImage2.TabIndex = 1;
            this.pbImage2.TabStop = false;
            // 
            // toolStripMainMenu
            // 
            this.toolStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen1,
            this.btnSave,
            this.btnDetect,
            this.btnToggle});
            this.toolStripMainMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMainMenu.Name = "toolStripMainMenu";
            this.toolStripMainMenu.Size = new System.Drawing.Size(631, 25);
            this.toolStripMainMenu.TabIndex = 2;
            this.toolStripMainMenu.Text = "toolStrip1";
            // 
            // btnOpen1
            // 
            this.btnOpen1.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen1.Image")));
            this.btnOpen1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen1.Name = "btnOpen1";
            this.btnOpen1.Size = new System.Drawing.Size(84, 22);
            this.btnOpen1.Text = "Open (left)";
            this.btnOpen1.Click += new System.EventHandler(this.btnOpen1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 22);
            this.btnSave.Text = "Save (right)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDetect
            // 
            this.btnDetect.Checked = true;
            this.btnDetect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnDetect.Image = ((System.Drawing.Image)(resources.GetObject("btnDetect.Image")));
            this.btnDetect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(92, 22);
            this.btnDetect.Text = "Detect noise";
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // fdOpen
            // 
            this.fdOpen.Filter = "Supported filetypes (BMP, PNG, JPEG)|*.bmp;*.png;*.jpg;*.jpeg";
            // 
            // fdSave
            // 
            this.fdSave.FileName = "EncryptedImage.png";
            this.fdSave.Filter = "PNG|*.png|BMP|*.bmp";
            // 
            // btnToggle
            // 
            this.btnToggle.Image = ((System.Drawing.Image)(resources.GetObject("btnToggle.Image")));
            this.btnToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(124, 22);
            this.btnToggle.Text = "Toggle view mode";
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 341);
            this.Controls.Add(this.toolStripMainMenu);
            this.Controls.Add(this.pbImage2);
            this.Controls.Add(this.pbImage1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "JPEG noise detector (K. Niedzwiecki)";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage2)).EndInit();
            this.toolStripMainMenu.ResumeLayout(false);
            this.toolStripMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage1;
        private System.Windows.Forms.PictureBox pbImage2;
        private System.Windows.Forms.ToolStrip toolStripMainMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnDetect;
        private System.Windows.Forms.OpenFileDialog fdOpen;
        private System.Windows.Forms.SaveFileDialog fdSave;
        private System.Windows.Forms.ToolStripButton btnOpen1;
        private System.Windows.Forms.ToolStripButton btnToggle;
    }
}

