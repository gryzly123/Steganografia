namespace BitmapEncoder
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
            this.btnOpen2 = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnEncode = new System.Windows.Forms.ToolStripButton();
            this.btnDecode = new System.Windows.Forms.ToolStripButton();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.fdOpen = new System.Windows.Forms.OpenFileDialog();
            this.fdSave = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbEncodeKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSteganographicKey = new System.Windows.Forms.TextBox();
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
            this.btnOpen2,
            this.btnSave,
            this.btnEncode,
            this.btnDecode});
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
            // btnOpen2
            // 
            this.btnOpen2.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen2.Image")));
            this.btnOpen2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen2.Name = "btnOpen2";
            this.btnOpen2.Size = new System.Drawing.Size(92, 22);
            this.btnOpen2.Text = "Open (right)";
            this.btnOpen2.Click += new System.EventHandler(this.btnOpen2_Click);
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
            // btnEncode
            // 
            this.btnEncode.Image = ((System.Drawing.Image)(resources.GetObject("btnEncode.Image")));
            this.btnEncode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(152, 22);
            this.btnEncode.Text = "Encode message (L->R)";
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Image = ((System.Drawing.Image)(resources.GetObject("btnDecode.Image")));
            this.btnDecode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(134, 22);
            this.btnDecode.Text = "Decode message (R)";
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(119, 334);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(499, 20);
            this.tbMessage.TabIndex = 4;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 337);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Message to encode:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Encode key:";
            // 
            // tbEncodeKey
            // 
            this.tbEncodeKey.Location = new System.Drawing.Point(119, 360);
            this.tbEncodeKey.Name = "tbEncodeKey";
            this.tbEncodeKey.Size = new System.Drawing.Size(499, 20);
            this.tbEncodeKey.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 389);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Steganographic key:";
            // 
            // tbSteganographicKey
            // 
            this.tbSteganographicKey.Enabled = false;
            this.tbSteganographicKey.Location = new System.Drawing.Point(119, 386);
            this.tbSteganographicKey.Name = "tbSteganographicKey";
            this.tbSteganographicKey.Size = new System.Drawing.Size(499, 20);
            this.tbSteganographicKey.TabIndex = 8;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 420);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSteganographicKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbEncodeKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.toolStripMainMenu);
            this.Controls.Add(this.pbImage2);
            this.Controls.Add(this.pbImage1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "SHA512+AES+XOR image encryptor (K. Niedzwiecki)";
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
        private System.Windows.Forms.ToolStripButton btnOpen2;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnEncode;
        private System.Windows.Forms.ToolStripButton btnDecode;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.OpenFileDialog fdOpen;
        private System.Windows.Forms.SaveFileDialog fdSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbEncodeKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSteganographicKey;
        private System.Windows.Forms.ToolStripButton btnOpen1;
    }
}

