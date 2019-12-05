namespace OnionClient
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExportRelays = new System.Windows.Forms.Button();
            this.btnImportRelays = new System.Windows.Forms.Button();
            this.btnAddRelay = new System.Windows.Forms.Button();
            this.btnRemoveRelay = new System.Windows.Forms.Button();
            this.btnDequeueRelay = new System.Windows.Forms.Button();
            this.btnEnqueueRelay = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listUsedRelays = new System.Windows.Forms.ListBox();
            this.listKnownRelays = new System.Windows.Forms.ListBox();
            this.wbHtmlPreview = new System.Windows.Forms.WebBrowser();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbSaveToDisk = new System.Windows.Forms.CheckBox();
            this.cbDisplayHtml = new System.Windows.Forms.CheckBox();
            this.btnChoosePath = new System.Windows.Forms.Button();
            this.tbDiskPath = new System.Windows.Forms.TextBox();
            this.btnRunRequest = new System.Windows.Forms.Button();
            this.tbFileLocation = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExportRelays);
            this.groupBox1.Controls.Add(this.btnImportRelays);
            this.groupBox1.Controls.Add(this.btnAddRelay);
            this.groupBox1.Controls.Add(this.btnRemoveRelay);
            this.groupBox1.Controls.Add(this.btnDequeueRelay);
            this.groupBox1.Controls.Add(this.btnEnqueueRelay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listUsedRelays);
            this.groupBox1.Controls.Add(this.listKnownRelays);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 220);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Relay configuration";
            // 
            // btnExportRelays
            // 
            this.btnExportRelays.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExportRelays.Location = new System.Drawing.Point(93, 191);
            this.btnExportRelays.Name = "btnExportRelays";
            this.btnExportRelays.Size = new System.Drawing.Size(46, 23);
            this.btnExportRelays.TabIndex = 10;
            this.btnExportRelays.Text = "Export";
            this.btnExportRelays.UseVisualStyleBackColor = true;
            // 
            // btnImportRelays
            // 
            this.btnImportRelays.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnImportRelays.Location = new System.Drawing.Point(47, 191);
            this.btnImportRelays.Name = "btnImportRelays";
            this.btnImportRelays.Size = new System.Drawing.Size(46, 23);
            this.btnImportRelays.TabIndex = 9;
            this.btnImportRelays.Text = "Import";
            this.btnImportRelays.UseVisualStyleBackColor = true;
            // 
            // btnAddRelay
            // 
            this.btnAddRelay.Location = new System.Drawing.Point(5, 191);
            this.btnAddRelay.Name = "btnAddRelay";
            this.btnAddRelay.Size = new System.Drawing.Size(18, 23);
            this.btnAddRelay.TabIndex = 8;
            this.btnAddRelay.Text = "+";
            this.btnAddRelay.UseVisualStyleBackColor = true;
            this.btnAddRelay.Click += new System.EventHandler(this.btnAddRelay_Click);
            // 
            // btnRemoveRelay
            // 
            this.btnRemoveRelay.Location = new System.Drawing.Point(22, 191);
            this.btnRemoveRelay.Name = "btnRemoveRelay";
            this.btnRemoveRelay.Size = new System.Drawing.Size(18, 23);
            this.btnRemoveRelay.TabIndex = 7;
            this.btnRemoveRelay.Text = "-";
            this.btnRemoveRelay.UseVisualStyleBackColor = true;
            this.btnRemoveRelay.Click += new System.EventHandler(this.btnRemoveRelay_Click);
            // 
            // btnDequeueRelay
            // 
            this.btnDequeueRelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDequeueRelay.Location = new System.Drawing.Point(213, 191);
            this.btnDequeueRelay.Name = "btnDequeueRelay";
            this.btnDequeueRelay.Size = new System.Drawing.Size(70, 23);
            this.btnDequeueRelay.TabIndex = 5;
            this.btnDequeueRelay.Text = "Dequeue";
            this.btnDequeueRelay.UseVisualStyleBackColor = true;
            this.btnDequeueRelay.Click += new System.EventHandler(this.btnDequeueRelay_Click);
            // 
            // btnEnqueueRelay
            // 
            this.btnEnqueueRelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnEnqueueRelay.Location = new System.Drawing.Point(143, 191);
            this.btnEnqueueRelay.Name = "btnEnqueueRelay";
            this.btnEnqueueRelay.Size = new System.Drawing.Size(70, 23);
            this.btnEnqueueRelay.TabIndex = 4;
            this.btnEnqueueRelay.Text = "Enqueue";
            this.btnEnqueueRelay.UseVisualStyleBackColor = true;
            this.btnEnqueueRelay.Click += new System.EventHandler(this.btnEnqueueRelay_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Used relays:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Known relays:";
            // 
            // listUsedRelays
            // 
            this.listUsedRelays.FormattingEnabled = true;
            this.listUsedRelays.Location = new System.Drawing.Point(144, 42);
            this.listUsedRelays.Name = "listUsedRelays";
            this.listUsedRelays.Size = new System.Drawing.Size(138, 147);
            this.listUsedRelays.TabIndex = 1;
            // 
            // listKnownRelays
            // 
            this.listKnownRelays.FormattingEnabled = true;
            this.listKnownRelays.Location = new System.Drawing.Point(6, 42);
            this.listKnownRelays.Name = "listKnownRelays";
            this.listKnownRelays.Size = new System.Drawing.Size(132, 147);
            this.listKnownRelays.TabIndex = 0;
            this.listKnownRelays.DoubleClick += new System.EventHandler(this.listKnownRelays_DoubleClick);
            // 
            // wbHtmlPreview
            // 
            this.wbHtmlPreview.Location = new System.Drawing.Point(6, 19);
            this.wbHtmlPreview.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbHtmlPreview.Name = "wbHtmlPreview";
            this.wbHtmlPreview.Size = new System.Drawing.Size(469, 348);
            this.wbHtmlPreview.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbSaveToDisk);
            this.groupBox2.Controls.Add(this.cbDisplayHtml);
            this.groupBox2.Controls.Add(this.btnChoosePath);
            this.groupBox2.Controls.Add(this.tbDiskPath);
            this.groupBox2.Controls.Add(this.btnRunRequest);
            this.groupBox2.Controls.Add(this.tbFileLocation);
            this.groupBox2.Location = new System.Drawing.Point(12, 238);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 147);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File download";
            // 
            // cbSaveToDisk
            // 
            this.cbSaveToDisk.AutoSize = true;
            this.cbSaveToDisk.Location = new System.Drawing.Point(6, 68);
            this.cbSaveToDisk.Name = "cbSaveToDisk";
            this.cbSaveToDisk.Size = new System.Drawing.Size(85, 17);
            this.cbSaveToDisk.TabIndex = 13;
            this.cbSaveToDisk.Text = "Save to disk";
            this.cbSaveToDisk.UseVisualStyleBackColor = true;
            // 
            // cbDisplayHtml
            // 
            this.cbDisplayHtml.AutoSize = true;
            this.cbDisplayHtml.Checked = true;
            this.cbDisplayHtml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDisplayHtml.Location = new System.Drawing.Point(6, 45);
            this.cbDisplayHtml.Name = "cbDisplayHtml";
            this.cbDisplayHtml.Size = new System.Drawing.Size(144, 17);
            this.cbDisplayHtml.TabIndex = 12;
            this.cbDisplayHtml.Text = "Display in HTML preview";
            this.cbDisplayHtml.UseVisualStyleBackColor = true;
            // 
            // btnChoosePath
            // 
            this.btnChoosePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnChoosePath.Location = new System.Drawing.Point(249, 66);
            this.btnChoosePath.Name = "btnChoosePath";
            this.btnChoosePath.Size = new System.Drawing.Size(33, 23);
            this.btnChoosePath.TabIndex = 11;
            this.btnChoosePath.Text = "...";
            this.btnChoosePath.UseVisualStyleBackColor = true;
            // 
            // tbDiskPath
            // 
            this.tbDiskPath.Location = new System.Drawing.Point(93, 67);
            this.tbDiskPath.Name = "tbDiskPath";
            this.tbDiskPath.Size = new System.Drawing.Size(150, 20);
            this.tbDiskPath.TabIndex = 4;
            // 
            // btnRunRequest
            // 
            this.btnRunRequest.Location = new System.Drawing.Point(6, 95);
            this.btnRunRequest.Name = "btnRunRequest";
            this.btnRunRequest.Size = new System.Drawing.Size(276, 46);
            this.btnRunRequest.TabIndex = 1;
            this.btnRunRequest.Text = "Run";
            this.btnRunRequest.UseVisualStyleBackColor = true;
            this.btnRunRequest.Click += new System.EventHandler(this.btnRunRequest_Click);
            // 
            // tbFileLocation
            // 
            this.tbFileLocation.Location = new System.Drawing.Point(9, 19);
            this.tbFileLocation.Name = "tbFileLocation";
            this.tbFileLocation.Size = new System.Drawing.Size(273, 20);
            this.tbFileLocation.TabIndex = 0;
            this.tbFileLocation.Text = "https://drupal.kniedzwiecki.eu/";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.wbHtmlPreview);
            this.groupBox3.Location = new System.Drawing.Point(307, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 373);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File preview";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 394);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.Text = "K. Niedźwiecki :: Onion client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExportRelays;
        private System.Windows.Forms.Button btnImportRelays;
        private System.Windows.Forms.Button btnAddRelay;
        private System.Windows.Forms.Button btnRemoveRelay;
        private System.Windows.Forms.Button btnDequeueRelay;
        private System.Windows.Forms.Button btnEnqueueRelay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listUsedRelays;
        private System.Windows.Forms.ListBox listKnownRelays;
        private System.Windows.Forms.WebBrowser wbHtmlPreview;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbSaveToDisk;
        private System.Windows.Forms.CheckBox cbDisplayHtml;
        private System.Windows.Forms.Button btnChoosePath;
        private System.Windows.Forms.TextBox tbDiskPath;
        private System.Windows.Forms.Button btnRunRequest;
        private System.Windows.Forms.TextBox tbFileLocation;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

