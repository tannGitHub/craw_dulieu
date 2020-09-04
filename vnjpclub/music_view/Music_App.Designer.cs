namespace vnjpclub.music_view
{
    partial class Music_App
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
            this.btnBrowerFolder = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnBrowerFile = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.brFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.brFile = new System.Windows.Forms.OpenFileDialog();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnBrowerFolder);
            this.panel1.Controls.Add(this.btnRead);
            this.panel1.Controls.Add(this.btnBrowerFile);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 70);
            this.panel1.TabIndex = 4;
            // 
            // btnBrowerFolder
            // 
            this.btnBrowerFolder.Location = new System.Drawing.Point(725, 12);
            this.btnBrowerFolder.Name = "btnBrowerFolder";
            this.btnBrowerFolder.Size = new System.Drawing.Size(46, 23);
            this.btnBrowerFolder.TabIndex = 8;
            this.btnBrowerFolder.Text = "Folder";
            this.btnBrowerFolder.UseVisualStyleBackColor = true;
            this.btnBrowerFolder.Click += new System.EventHandler(this.btnBrowerFolder_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(187, 39);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 7;
            this.btnRead.Text = "Đọc file";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnBrowerFile
            // 
            this.btnBrowerFile.Location = new System.Drawing.Point(673, 12);
            this.btnBrowerFile.Name = "btnBrowerFile";
            this.btnBrowerFile.Size = new System.Drawing.Size(46, 23);
            this.btnBrowerFile.TabIndex = 6;
            this.btnBrowerFile.Text = "File";
            this.btnBrowerFile.UseVisualStyleBackColor = true;
            this.btnBrowerFile.Click += new System.EventHandler(this.btnBrowerFile_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(187, 13);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(426, 20);
            this.txtPath.TabIndex = 5;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(15, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(166, 13);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Nhập đường dẫn file Powerpoint :";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 427);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(800, 23);
            this.progressBar.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 357);
            this.panel2.TabIndex = 6;
            // 
            // gridData
            // 
            this.gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData.Location = new System.Drawing.Point(0, 0);
            this.gridData.Name = "gridData";
            this.gridData.Size = new System.Drawing.Size(800, 357);
            this.gridData.TabIndex = 0;
            // 
            // brFile
            // 
            this.brFile.FileName = "openFileDialog1";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(619, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(48, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Music_App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panel1);
            this.Name = "Music_App";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Music_App";
            this.Load += new System.EventHandler(this.Music_App_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnBrowerFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.FolderBrowserDialog brFolder;
        private System.Windows.Forms.OpenFileDialog brFile;
        private System.Windows.Forms.Button btnBrowerFolder;
        private System.Windows.Forms.Button btnClear;
    }
}