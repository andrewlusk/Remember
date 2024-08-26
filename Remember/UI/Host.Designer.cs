namespace Remember
{
    partial class Host
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Host));
            dgvFolders = new DataGridView();
            btnRefresh = new Button();
            btnSelectFolder = new Button();
            txtRootFolder = new TextBox();
            lblRootFolder = new Label();
            btnOpenRootFolder = new Button();
            btnToggleDetail = new Button();
            txtQueryString = new TextBox();
            lblQueryString = new Label();
            lblQueryErrorText = new Label();
            btnQueryClear = new Button();
            btnSaveQuery = new Button();
            btnLoadQuery = new Button();
            objToolTips = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dgvFolders).BeginInit();
            SuspendLayout();
            // 
            // dgvFolders
            // 
            dgvFolders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFolders.Location = new Point(10, 99);
            dgvFolders.Name = "dgvFolders";
            dgvFolders.RowHeadersWidth = 51;
            dgvFolders.Size = new Size(971, 512);
            dgvFolders.TabIndex = 0;
            dgvFolders.RowHeaderMouseClick += dgvFolders_RowHeaderMouseClick;
            // 
            // btnRefresh
            // 
            btnRefresh.Image = (Image)resources.GetObject("btnRefresh.Image");
            btnRefresh.Location = new Point(88, 67);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(33, 25);
            btnRefresh.TabIndex = 4;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(692, 7);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(69, 25);
            btnSelectFolder.TabIndex = 2;
            btnSelectFolder.Text = "Change...";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectRootFolder_Click;
            // 
            // txtRootFolder
            // 
            txtRootFolder.Enabled = false;
            txtRootFolder.Location = new Point(88, 9);
            txtRootFolder.Name = "txtRootFolder";
            txtRootFolder.ReadOnly = true;
            txtRootFolder.Size = new Size(560, 23);
            txtRootFolder.TabIndex = 3;
            // 
            // lblRootFolder
            // 
            lblRootFolder.AutoSize = true;
            lblRootFolder.Location = new Point(7, 13);
            lblRootFolder.Name = "lblRootFolder";
            lblRootFolder.Size = new Size(71, 15);
            lblRootFolder.TabIndex = 4;
            lblRootFolder.Text = "Root Folder:";
            // 
            // btnOpenRootFolder
            // 
            btnOpenRootFolder.Image = (Image)resources.GetObject("btnOpenRootFolder.Image");
            btnOpenRootFolder.Location = new Point(654, 7);
            btnOpenRootFolder.Margin = new Padding(3, 2, 3, 2);
            btnOpenRootFolder.Name = "btnOpenRootFolder";
            btnOpenRootFolder.Size = new Size(33, 25);
            btnOpenRootFolder.TabIndex = 1;
            btnOpenRootFolder.UseVisualStyleBackColor = true;
            btnOpenRootFolder.Click += btnOpenRootFolder_Click;
            // 
            // btnToggleDetail
            // 
            btnToggleDetail.Location = new Point(939, 7);
            btnToggleDetail.Name = "btnToggleDetail";
            btnToggleDetail.Size = new Size(42, 25);
            btnToggleDetail.TabIndex = 99;
            btnToggleDetail.Text = "Dtl";
            btnToggleDetail.UseVisualStyleBackColor = true;
            btnToggleDetail.Click += btnToggleDetail_Click;
            // 
            // txtQueryString
            // 
            txtQueryString.Location = new Point(88, 38);
            txtQueryString.Name = "txtQueryString";
            txtQueryString.Size = new Size(560, 23);
            txtQueryString.TabIndex = 3;
            txtQueryString.KeyPress += txtQueryString_KeyPress;
            // 
            // lblQueryString
            // 
            lblQueryString.AutoSize = true;
            lblQueryString.Location = new Point(11, 41);
            lblQueryString.Name = "lblQueryString";
            lblQueryString.Size = new Size(42, 15);
            lblQueryString.TabIndex = 9;
            lblQueryString.Text = "Query:";
            // 
            // lblQueryErrorText
            // 
            lblQueryErrorText.AutoSize = true;
            lblQueryErrorText.ForeColor = Color.Maroon;
            lblQueryErrorText.Location = new Point(205, 72);
            lblQueryErrorText.Name = "lblQueryErrorText";
            lblQueryErrorText.Size = new Size(85, 15);
            lblQueryErrorText.TabIndex = 100;
            lblQueryErrorText.Text = "QueryErrorText";
            lblQueryErrorText.Visible = false;
            // 
            // btnQueryClear
            // 
            btnQueryClear.Image = (Image)resources.GetObject("btnQueryClear.Image");
            btnQueryClear.Location = new Point(654, 37);
            btnQueryClear.Name = "btnQueryClear";
            btnQueryClear.Size = new Size(33, 25);
            btnQueryClear.TabIndex = 101;
            btnQueryClear.UseVisualStyleBackColor = true;
            btnQueryClear.Click += btnQueryClear_Click;
            // 
            // btnSaveQuery
            // 
            btnSaveQuery.Image = (Image)resources.GetObject("btnSaveQuery.Image");
            btnSaveQuery.Location = new Point(166, 67);
            btnSaveQuery.Name = "btnSaveQuery";
            btnSaveQuery.Size = new Size(33, 25);
            btnSaveQuery.TabIndex = 102;
            btnSaveQuery.UseVisualStyleBackColor = true;
            btnSaveQuery.Click += btnSaveQuery_Click;
            // 
            // btnLoadQuery
            // 
            btnLoadQuery.Image = (Image)resources.GetObject("btnLoadQuery.Image");
            btnLoadQuery.Location = new Point(127, 67);
            btnLoadQuery.Name = "btnLoadQuery";
            btnLoadQuery.Size = new Size(33, 25);
            btnLoadQuery.TabIndex = 103;
            btnLoadQuery.UseVisualStyleBackColor = true;
            btnLoadQuery.Click += btnLoadQuery_Click;
            // 
            // Host
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1604, 622);
            Controls.Add(btnLoadQuery);
            Controls.Add(btnSaveQuery);
            Controls.Add(btnQueryClear);
            Controls.Add(lblQueryErrorText);
            Controls.Add(lblQueryString);
            Controls.Add(txtQueryString);
            Controls.Add(btnToggleDetail);
            Controls.Add(btnOpenRootFolder);
            Controls.Add(lblRootFolder);
            Controls.Add(txtRootFolder);
            Controls.Add(btnSelectFolder);
            Controls.Add(btnRefresh);
            Controls.Add(dgvFolders);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Host";
            Text = " Remember";
            Load += AppLoad;
            Resize += Host_Resized;
            ((System.ComponentModel.ISupportInitialize)dgvFolders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public DataGridView dgvFolders;
        private Button btnRefresh;
        private Button btnSelectFolder;
        private TextBox txtRootFolder;
        private Label lblRootFolder;
        private Button btnOpenRootFolder;
        private Button btnToggleDetail;
        private TextBox txtQueryString;
        private Label lblQueryString;
        private Label lblQueryErrorText;
        private Button btnQueryClear;
        private Button btnSaveQuery;
        private Button btnLoadQuery;
        private ToolTip objToolTips;
    }
}
