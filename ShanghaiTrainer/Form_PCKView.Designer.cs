namespace ShanghaiTrainer
{
    partial class Form_PCKView
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
            this.listView_PCKList = new System.Windows.Forms.ListView();
            this.num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileActualSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileCompressionSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_UnPackSingle = new System.Windows.Forms.Button();
            this.label_PCKInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView_PCKList
            // 
            this.listView_PCKList.AllowDrop = true;
            this.listView_PCKList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.num,
            this.file,
            this.fileOffset,
            this.fileActualSize,
            this.fileCompressionSize});
            this.listView_PCKList.FullRowSelect = true;
            this.listView_PCKList.GridLines = true;
            this.listView_PCKList.HideSelection = false;
            this.listView_PCKList.Location = new System.Drawing.Point(12, 47);
            this.listView_PCKList.Name = "listView_PCKList";
            this.listView_PCKList.Size = new System.Drawing.Size(878, 341);
            this.listView_PCKList.TabIndex = 27;
            this.listView_PCKList.UseCompatibleStateImageBehavior = false;
            this.listView_PCKList.View = System.Windows.Forms.View.Details;
            this.listView_PCKList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_PCKList_MouseDoubleClick);
            // 
            // num
            // 
            this.num.Text = "序号";
            this.num.Width = 80;
            // 
            // file
            // 
            this.file.Text = "文件名称";
            this.file.Width = 400;
            // 
            // fileOffset
            // 
            this.fileOffset.Text = "文件偏移";
            this.fileOffset.Width = 100;
            // 
            // fileActualSize
            // 
            this.fileActualSize.Text = "实际大小";
            this.fileActualSize.Width = 100;
            // 
            // fileCompressionSize
            // 
            this.fileCompressionSize.Text = "压缩后大小";
            this.fileCompressionSize.Width = 109;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(790, 410);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(100, 30);
            this.btn_OK.TabIndex = 28;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_UnPackSingle
            // 
            this.btn_UnPackSingle.Location = new System.Drawing.Point(675, 410);
            this.btn_UnPackSingle.Name = "btn_UnPackSingle";
            this.btn_UnPackSingle.Size = new System.Drawing.Size(100, 30);
            this.btn_UnPackSingle.TabIndex = 29;
            this.btn_UnPackSingle.Text = "解包选中";
            this.btn_UnPackSingle.UseVisualStyleBackColor = true;
            this.btn_UnPackSingle.Click += new System.EventHandler(this.Btn_UnPackSingle_Click);
            // 
            // label_PCKInfo
            // 
            this.label_PCKInfo.AutoSize = true;
            this.label_PCKInfo.Location = new System.Drawing.Point(12, 15);
            this.label_PCKInfo.Name = "label_PCKInfo";
            this.label_PCKInfo.Size = new System.Drawing.Size(55, 15);
            this.label_PCKInfo.TabIndex = 30;
            this.label_PCKInfo.Text = "label1";
            // 
            // Form_PCKView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 453);
            this.Controls.Add(this.label_PCKInfo);
            this.Controls.Add(this.btn_UnPackSingle);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.listView_PCKList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_PCKView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "浏览PCK文件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_PCKList;
        private System.Windows.Forms.ColumnHeader num;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.ColumnHeader fileOffset;
        private System.Windows.Forms.ColumnHeader fileActualSize;
        private System.Windows.Forms.ColumnHeader fileCompressionSize;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_UnPackSingle;
        private System.Windows.Forms.Label label_PCKInfo;
    }
}