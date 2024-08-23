namespace Remember
{
    partial class ItemFolderDetail
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemFolderDetail));
            lblFolderType = new Label();
            cmbFolderType = new ComboBox();
            lblCompleted = new Label();
            dtpCompleted = new DateTimePicker();
            dtpCreated = new DateTimePicker();
            lblCreated = new Label();
            dtpDue = new DateTimePicker();
            lblDue = new Label();
            dtpModified = new DateTimePicker();
            lblModified = new Label();
            dtpReminder = new DateTimePicker();
            lblReminder = new Label();
            lblImportance = new Label();
            txtImportance = new TextBox();
            txtUrgency = new TextBox();
            lblUrgency = new Label();
            txtOwner = new TextBox();
            lblOwner = new Label();
            txtDescription = new TextBox();
            lblDescription = new Label();
            lblFiles = new Label();
            dgvFiles = new DataGridView();
            txtName = new TextBox();
            lblName = new Label();
            btnNewSubfolder = new Button();
            btnSaveChanges = new Button();
            btnOpen = new Button();
            btnSetAsRoot = new Button();
            btnDueClear = new Button();
            btnDueNow = new Button();
            btnDueMidnight = new Button();
            btnDueCopy = new Button();
            btnDuePaste = new Button();
            btnReminderClear = new Button();
            btnReminderPaste = new Button();
            btnReminderCopy = new Button();
            btnReminderMidnight = new Button();
            btnReminderNow = new Button();
            btnCompletedPaste = new Button();
            btnCompletedCopy = new Button();
            btnCompletedMidnight = new Button();
            btnCompletedNow = new Button();
            btnCompletedClear = new Button();
            lblStart = new Label();
            dtpStart = new DateTimePicker();
            btnStartPaste = new Button();
            btnStartCopy = new Button();
            btnStartMidnight = new Button();
            btnStartNow = new Button();
            btnStartClear = new Button();
            btnDeleteFolder = new Button();
            lblPath = new Label();
            txtPath = new TextBox();
            btnUp = new Button();
            objToolTips = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dgvFiles).BeginInit();
            SuspendLayout();
            // 
            // lblFolderType
            // 
            lblFolderType.AutoSize = true;
            lblFolderType.Location = new Point(6, 118);
            lblFolderType.Name = "lblFolderType";
            lblFolderType.Size = new Size(31, 15);
            lblFolderType.TabIndex = 0;
            lblFolderType.Text = "Type";
            // 
            // cmbFolderType
            // 
            cmbFolderType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFolderType.FormattingEnabled = true;
            cmbFolderType.Location = new Point(108, 116);
            cmbFolderType.Margin = new Padding(3, 2, 3, 2);
            cmbFolderType.Name = "cmbFolderType";
            cmbFolderType.Size = new Size(133, 23);
            cmbFolderType.TabIndex = 1;
            cmbFolderType.SelectedIndexChanged += cmbFolderType_SelectedIndexChanged;
            // 
            // lblCompleted
            // 
            lblCompleted.AutoSize = true;
            lblCompleted.Location = new Point(6, 270);
            lblCompleted.Name = "lblCompleted";
            lblCompleted.Size = new Size(66, 15);
            lblCompleted.TabIndex = 2;
            lblCompleted.Text = "Completed";
            // 
            // dtpCompleted
            // 
            dtpCompleted.CalendarTitleForeColor = Color.AliceBlue;
            dtpCompleted.CustomFormat = " yyyy-MMM-dd HH:mm:ss";
            dtpCompleted.Format = DateTimePickerFormat.Custom;
            dtpCompleted.Location = new Point(108, 266);
            dtpCompleted.Margin = new Padding(3, 2, 3, 2);
            dtpCompleted.Name = "dtpCompleted";
            dtpCompleted.Size = new Size(219, 23);
            dtpCompleted.TabIndex = 3;
            dtpCompleted.ValueChanged += dtpCompleted_ValueChanged;
            // 
            // dtpCreated
            // 
            dtpCreated.CustomFormat = " yyyy-MMM-dd HH:mm:ss";
            dtpCreated.Enabled = false;
            dtpCreated.Format = DateTimePickerFormat.Custom;
            dtpCreated.Location = new Point(108, 141);
            dtpCreated.Margin = new Padding(3, 2, 3, 2);
            dtpCreated.Name = "dtpCreated";
            dtpCreated.Size = new Size(219, 23);
            dtpCreated.TabIndex = 5;
            // 
            // lblCreated
            // 
            lblCreated.AutoSize = true;
            lblCreated.Location = new Point(6, 143);
            lblCreated.Name = "lblCreated";
            lblCreated.Size = new Size(48, 15);
            lblCreated.TabIndex = 4;
            lblCreated.Text = "Created";
            // 
            // dtpDue
            // 
            dtpDue.CustomFormat = " yyyy-MMM-dd HH:mm:ss";
            dtpDue.Format = DateTimePickerFormat.Custom;
            dtpDue.Location = new Point(108, 216);
            dtpDue.Margin = new Padding(3, 2, 3, 2);
            dtpDue.Name = "dtpDue";
            dtpDue.Size = new Size(219, 23);
            dtpDue.TabIndex = 7;
            dtpDue.ValueChanged += dtpDue_ValueChanged;
            // 
            // lblDue
            // 
            lblDue.AutoSize = true;
            lblDue.Location = new Point(7, 220);
            lblDue.Name = "lblDue";
            lblDue.Size = new Size(28, 15);
            lblDue.TabIndex = 6;
            lblDue.Text = "Due";
            // 
            // dtpModified
            // 
            dtpModified.CustomFormat = " yyyy-MMM-dd HH:mm:ss";
            dtpModified.Enabled = false;
            dtpModified.Format = DateTimePickerFormat.Custom;
            dtpModified.Location = new Point(108, 166);
            dtpModified.Margin = new Padding(3, 2, 3, 2);
            dtpModified.Name = "dtpModified";
            dtpModified.Size = new Size(219, 23);
            dtpModified.TabIndex = 9;
            // 
            // lblModified
            // 
            lblModified.AutoSize = true;
            lblModified.Location = new Point(6, 170);
            lblModified.Name = "lblModified";
            lblModified.Size = new Size(55, 15);
            lblModified.TabIndex = 8;
            lblModified.Text = "Modified";
            // 
            // dtpReminder
            // 
            dtpReminder.CustomFormat = " yyyy-MMM-dd HH:mm:ss";
            dtpReminder.Format = DateTimePickerFormat.Custom;
            dtpReminder.Location = new Point(108, 241);
            dtpReminder.Margin = new Padding(3, 2, 3, 2);
            dtpReminder.Name = "dtpReminder";
            dtpReminder.Size = new Size(219, 23);
            dtpReminder.TabIndex = 11;
            dtpReminder.ValueChanged += dtpReminder_ValueChanged;
            // 
            // lblReminder
            // 
            lblReminder.AutoSize = true;
            lblReminder.Location = new Point(6, 245);
            lblReminder.Name = "lblReminder";
            lblReminder.Size = new Size(58, 15);
            lblReminder.TabIndex = 10;
            lblReminder.Text = "Reminder";
            // 
            // lblImportance
            // 
            lblImportance.AutoSize = true;
            lblImportance.Location = new Point(7, 294);
            lblImportance.Name = "lblImportance";
            lblImportance.Size = new Size(68, 15);
            lblImportance.TabIndex = 12;
            lblImportance.Text = "Importance";
            // 
            // txtImportance
            // 
            txtImportance.Location = new Point(108, 292);
            txtImportance.Margin = new Padding(3, 2, 3, 2);
            txtImportance.MaxLength = 5;
            txtImportance.Name = "txtImportance";
            txtImportance.Size = new Size(80, 23);
            txtImportance.TabIndex = 13;
            txtImportance.TextChanged += txtImportance_TextChanged;
            // 
            // txtUrgency
            // 
            txtUrgency.Location = new Point(108, 316);
            txtUrgency.Margin = new Padding(3, 2, 3, 2);
            txtUrgency.MaxLength = 5;
            txtUrgency.Name = "txtUrgency";
            txtUrgency.Size = new Size(80, 23);
            txtUrgency.TabIndex = 15;
            txtUrgency.TextChanged += txtUrgency_TextChanged;
            // 
            // lblUrgency
            // 
            lblUrgency.AutoSize = true;
            lblUrgency.Location = new Point(7, 319);
            lblUrgency.Name = "lblUrgency";
            lblUrgency.Size = new Size(51, 15);
            lblUrgency.TabIndex = 14;
            lblUrgency.Text = "Urgency";
            // 
            // txtOwner
            // 
            txtOwner.Location = new Point(108, 341);
            txtOwner.Margin = new Padding(3, 2, 3, 2);
            txtOwner.MaxLength = 100;
            txtOwner.Name = "txtOwner";
            txtOwner.Size = new Size(477, 23);
            txtOwner.TabIndex = 17;
            txtOwner.TextChanged += txtOwner_TextChanged;
            // 
            // lblOwner
            // 
            lblOwner.AutoSize = true;
            lblOwner.Location = new Point(7, 344);
            lblOwner.Name = "lblOwner";
            lblOwner.Size = new Size(42, 15);
            lblOwner.TabIndex = 16;
            lblOwner.Text = "Owner";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(11, 391);
            txtDescription.Margin = new Padding(3, 2, 3, 2);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(574, 158);
            txtDescription.TabIndex = 19;
            txtDescription.TextChanged += txtDescription_TextChanged;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(7, 372);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 18;
            lblDescription.Text = "Description";
            // 
            // lblFiles
            // 
            lblFiles.AutoSize = true;
            lblFiles.Location = new Point(8, 554);
            lblFiles.Name = "lblFiles";
            lblFiles.Size = new Size(83, 15);
            lblFiles.TabIndex = 20;
            lblFiles.Text = "Files and Links";
            // 
            // dgvFiles
            // 
            dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFiles.Location = new Point(11, 571);
            dgvFiles.Margin = new Padding(3, 2, 3, 2);
            dgvFiles.Name = "dgvFiles";
            dgvFiles.RowHeadersWidth = 51;
            dgvFiles.Size = new Size(573, 153);
            dgvFiles.TabIndex = 21;
            dgvFiles.CellDoubleClick += dgvFiles_CellDoubleClick;
            // 
            // txtName
            // 
            txtName.Location = new Point(108, 91);
            txtName.Margin = new Padding(3, 2, 3, 2);
            txtName.MaxLength = 100;
            txtName.Name = "txtName";
            txtName.Size = new Size(479, 23);
            txtName.TabIndex = 23;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(7, 94);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 22;
            lblName.Text = "Name";
            // 
            // btnNewSubfolder
            // 
            btnNewSubfolder.Image = (Image)resources.GetObject("btnNewSubfolder.Image");
            btnNewSubfolder.Location = new Point(48, 8);
            btnNewSubfolder.Margin = new Padding(3, 2, 3, 2);
            btnNewSubfolder.Name = "btnNewSubfolder";
            btnNewSubfolder.Size = new Size(33, 25);
            btnNewSubfolder.TabIndex = 24;
            btnNewSubfolder.UseVisualStyleBackColor = true;
            btnNewSubfolder.Click += btnNewSubfolder_Click;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.BackColor = Color.FromArgb(173, 205, 255);
            btnSaveChanges.FlatAppearance.BorderSize = 3;
            btnSaveChanges.Image = (Image)resources.GetObject("btnSaveChanges.Image");
            btnSaveChanges.Location = new Point(551, 8);
            btnSaveChanges.Margin = new Padding(3, 2, 3, 2);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(33, 25);
            btnSaveChanges.TabIndex = 25;
            btnSaveChanges.UseVisualStyleBackColor = false;
            btnSaveChanges.Visible = false;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // btnOpen
            // 
            btnOpen.Image = (Image)resources.GetObject("btnOpen.Image");
            btnOpen.Location = new Point(8, 8);
            btnOpen.Margin = new Padding(3, 2, 3, 2);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(33, 25);
            btnOpen.TabIndex = 26;
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // btnSetAsRoot
            // 
            btnSetAsRoot.Enabled = false;
            btnSetAsRoot.Location = new Point(179, 8);
            btnSetAsRoot.Margin = new Padding(3, 2, 3, 2);
            btnSetAsRoot.Name = "btnSetAsRoot";
            btnSetAsRoot.Size = new Size(86, 25);
            btnSetAsRoot.TabIndex = 27;
            btnSetAsRoot.Text = "Set As Root";
            btnSetAsRoot.UseVisualStyleBackColor = true;
            btnSetAsRoot.Visible = false;
            btnSetAsRoot.Click += btnSetAsRoot_Click;
            // 
            // btnDueClear
            // 
            btnDueClear.Image = (Image)resources.GetObject("btnDueClear.Image");
            btnDueClear.Location = new Point(330, 217);
            btnDueClear.Name = "btnDueClear";
            btnDueClear.Size = new Size(33, 23);
            btnDueClear.TabIndex = 28;
            btnDueClear.UseVisualStyleBackColor = true;
            btnDueClear.Click += btnDueClear_Click;
            // 
            // btnDueNow
            // 
            btnDueNow.Image = (Image)resources.GetObject("btnDueNow.Image");
            btnDueNow.Location = new Point(366, 217);
            btnDueNow.Name = "btnDueNow";
            btnDueNow.Size = new Size(33, 23);
            btnDueNow.TabIndex = 29;
            btnDueNow.UseVisualStyleBackColor = true;
            btnDueNow.Click += btnDueNow_Click;
            // 
            // btnDueMidnight
            // 
            btnDueMidnight.Image = (Image)resources.GetObject("btnDueMidnight.Image");
            btnDueMidnight.Location = new Point(401, 217);
            btnDueMidnight.Name = "btnDueMidnight";
            btnDueMidnight.Size = new Size(33, 23);
            btnDueMidnight.TabIndex = 30;
            btnDueMidnight.UseVisualStyleBackColor = true;
            btnDueMidnight.Click += btnDueMidnight_Click;
            // 
            // btnDueCopy
            // 
            btnDueCopy.Image = (Image)resources.GetObject("btnDueCopy.Image");
            btnDueCopy.Location = new Point(436, 217);
            btnDueCopy.Name = "btnDueCopy";
            btnDueCopy.Size = new Size(33, 23);
            btnDueCopy.TabIndex = 31;
            btnDueCopy.UseVisualStyleBackColor = true;
            btnDueCopy.Click += btnDueCopy_Click;
            // 
            // btnDuePaste
            // 
            btnDuePaste.Enabled = false;
            btnDuePaste.Image = (Image)resources.GetObject("btnDuePaste.Image");
            btnDuePaste.Location = new Point(471, 217);
            btnDuePaste.Name = "btnDuePaste";
            btnDuePaste.Size = new Size(33, 23);
            btnDuePaste.TabIndex = 32;
            btnDuePaste.UseVisualStyleBackColor = true;
            btnDuePaste.Click += btnDuePaste_Click;
            // 
            // btnReminderClear
            // 
            btnReminderClear.Image = (Image)resources.GetObject("btnReminderClear.Image");
            btnReminderClear.Location = new Point(330, 241);
            btnReminderClear.Name = "btnReminderClear";
            btnReminderClear.Size = new Size(33, 23);
            btnReminderClear.TabIndex = 33;
            btnReminderClear.UseVisualStyleBackColor = true;
            btnReminderClear.Click += btnReminderClear_Click;
            // 
            // btnReminderPaste
            // 
            btnReminderPaste.Enabled = false;
            btnReminderPaste.Image = (Image)resources.GetObject("btnReminderPaste.Image");
            btnReminderPaste.Location = new Point(471, 241);
            btnReminderPaste.Name = "btnReminderPaste";
            btnReminderPaste.Size = new Size(33, 23);
            btnReminderPaste.TabIndex = 37;
            btnReminderPaste.UseVisualStyleBackColor = true;
            btnReminderPaste.Click += btnReminderPaste_Click;
            // 
            // btnReminderCopy
            // 
            btnReminderCopy.Image = (Image)resources.GetObject("btnReminderCopy.Image");
            btnReminderCopy.Location = new Point(436, 241);
            btnReminderCopy.Name = "btnReminderCopy";
            btnReminderCopy.Size = new Size(33, 23);
            btnReminderCopy.TabIndex = 36;
            btnReminderCopy.UseVisualStyleBackColor = true;
            btnReminderCopy.Click += btnReminderCopy_Click;
            // 
            // btnReminderMidnight
            // 
            btnReminderMidnight.Image = (Image)resources.GetObject("btnReminderMidnight.Image");
            btnReminderMidnight.Location = new Point(401, 241);
            btnReminderMidnight.Name = "btnReminderMidnight";
            btnReminderMidnight.Size = new Size(33, 23);
            btnReminderMidnight.TabIndex = 35;
            btnReminderMidnight.UseVisualStyleBackColor = true;
            btnReminderMidnight.Click += btnReminderMidnight_Click;
            // 
            // btnReminderNow
            // 
            btnReminderNow.Image = (Image)resources.GetObject("btnReminderNow.Image");
            btnReminderNow.Location = new Point(366, 241);
            btnReminderNow.Name = "btnReminderNow";
            btnReminderNow.Size = new Size(33, 23);
            btnReminderNow.TabIndex = 34;
            btnReminderNow.UseVisualStyleBackColor = true;
            btnReminderNow.Click += btnReminderNow_Click;
            // 
            // btnCompletedPaste
            // 
            btnCompletedPaste.Enabled = false;
            btnCompletedPaste.Image = (Image)resources.GetObject("btnCompletedPaste.Image");
            btnCompletedPaste.Location = new Point(471, 266);
            btnCompletedPaste.Name = "btnCompletedPaste";
            btnCompletedPaste.Size = new Size(33, 23);
            btnCompletedPaste.TabIndex = 42;
            btnCompletedPaste.UseVisualStyleBackColor = false;
            btnCompletedPaste.Click += btnCompletedPaste_Click;
            // 
            // btnCompletedCopy
            // 
            btnCompletedCopy.Image = (Image)resources.GetObject("btnCompletedCopy.Image");
            btnCompletedCopy.Location = new Point(436, 266);
            btnCompletedCopy.Name = "btnCompletedCopy";
            btnCompletedCopy.Size = new Size(33, 23);
            btnCompletedCopy.TabIndex = 41;
            btnCompletedCopy.UseVisualStyleBackColor = true;
            btnCompletedCopy.Click += btnCompletedCopy_Click;
            // 
            // btnCompletedMidnight
            // 
            btnCompletedMidnight.Image = (Image)resources.GetObject("btnCompletedMidnight.Image");
            btnCompletedMidnight.Location = new Point(401, 266);
            btnCompletedMidnight.Name = "btnCompletedMidnight";
            btnCompletedMidnight.Size = new Size(33, 23);
            btnCompletedMidnight.TabIndex = 40;
            btnCompletedMidnight.UseVisualStyleBackColor = true;
            btnCompletedMidnight.Click += btnCompletedMidnight_Click;
            // 
            // btnCompletedNow
            // 
            btnCompletedNow.Image = (Image)resources.GetObject("btnCompletedNow.Image");
            btnCompletedNow.Location = new Point(366, 266);
            btnCompletedNow.Name = "btnCompletedNow";
            btnCompletedNow.Size = new Size(33, 23);
            btnCompletedNow.TabIndex = 39;
            btnCompletedNow.UseVisualStyleBackColor = true;
            btnCompletedNow.Click += btnCompletedNow_Click;
            // 
            // btnCompletedClear
            // 
            btnCompletedClear.Image = (Image)resources.GetObject("btnCompletedClear.Image");
            btnCompletedClear.Location = new Point(330, 266);
            btnCompletedClear.Name = "btnCompletedClear";
            btnCompletedClear.Size = new Size(33, 23);
            btnCompletedClear.TabIndex = 38;
            btnCompletedClear.UseVisualStyleBackColor = true;
            btnCompletedClear.Click += btnCompletedClear_Click;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(7, 194);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(31, 15);
            lblStart.TabIndex = 43;
            lblStart.Text = "Start";
            // 
            // dtpStart
            // 
            dtpStart.CustomFormat = " yyyy-MMM-dd HH:mm:ss";
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(108, 191);
            dtpStart.Margin = new Padding(3, 2, 3, 2);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(219, 23);
            dtpStart.TabIndex = 44;
            dtpStart.ValueChanged += dtpStart_ValueChanged;
            // 
            // btnStartPaste
            // 
            btnStartPaste.Enabled = false;
            btnStartPaste.Image = (Image)resources.GetObject("btnStartPaste.Image");
            btnStartPaste.Location = new Point(471, 191);
            btnStartPaste.Name = "btnStartPaste";
            btnStartPaste.Size = new Size(33, 23);
            btnStartPaste.TabIndex = 49;
            btnStartPaste.UseVisualStyleBackColor = true;
            btnStartPaste.Click += btnStartPaste_Click;
            // 
            // btnStartCopy
            // 
            btnStartCopy.Image = (Image)resources.GetObject("btnStartCopy.Image");
            btnStartCopy.Location = new Point(436, 191);
            btnStartCopy.Name = "btnStartCopy";
            btnStartCopy.Size = new Size(33, 23);
            btnStartCopy.TabIndex = 48;
            btnStartCopy.UseVisualStyleBackColor = true;
            btnStartCopy.Click += btnStartCopy_Click;
            // 
            // btnStartMidnight
            // 
            btnStartMidnight.Image = (Image)resources.GetObject("btnStartMidnight.Image");
            btnStartMidnight.Location = new Point(401, 191);
            btnStartMidnight.Name = "btnStartMidnight";
            btnStartMidnight.Size = new Size(33, 23);
            btnStartMidnight.TabIndex = 47;
            btnStartMidnight.UseVisualStyleBackColor = true;
            btnStartMidnight.Click += btnStartMidnight_Click;
            // 
            // btnStartNow
            // 
            btnStartNow.Image = (Image)resources.GetObject("btnStartNow.Image");
            btnStartNow.Location = new Point(365, 191);
            btnStartNow.Name = "btnStartNow";
            btnStartNow.Size = new Size(33, 23);
            btnStartNow.TabIndex = 46;
            btnStartNow.UseVisualStyleBackColor = true;
            btnStartNow.Click += btnStartNow_Click;
            // 
            // btnStartClear
            // 
            btnStartClear.Image = (Image)resources.GetObject("btnStartClear.Image");
            btnStartClear.Location = new Point(330, 191);
            btnStartClear.Name = "btnStartClear";
            btnStartClear.Size = new Size(33, 23);
            btnStartClear.TabIndex = 45;
            btnStartClear.UseVisualStyleBackColor = true;
            btnStartClear.Click += btnStartClear_Click;
            // 
            // btnDeleteFolder
            // 
            btnDeleteFolder.Image = (Image)resources.GetObject("btnDeleteFolder.Image");
            btnDeleteFolder.Location = new Point(128, 8);
            btnDeleteFolder.Margin = new Padding(3, 2, 3, 2);
            btnDeleteFolder.Name = "btnDeleteFolder";
            btnDeleteFolder.Size = new Size(33, 25);
            btnDeleteFolder.TabIndex = 50;
            btnDeleteFolder.UseVisualStyleBackColor = true;
            btnDeleteFolder.Click += btnDeleteFolder_Click;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(8, 49);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(31, 15);
            lblPath.TabIndex = 51;
            lblPath.Text = "Path";
            // 
            // txtPath
            // 
            txtPath.Enabled = false;
            txtPath.Location = new Point(108, 46);
            txtPath.Margin = new Padding(3, 2, 3, 2);
            txtPath.MaxLength = 100;
            txtPath.Multiline = true;
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(479, 41);
            txtPath.TabIndex = 52;
            // 
            // btnUp
            // 
            btnUp.Image = (Image)resources.GetObject("btnUp.Image");
            btnUp.Location = new Point(88, 8);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(33, 25);
            btnUp.TabIndex = 53;
            btnUp.UseVisualStyleBackColor = true;
            btnUp.Click += btnUp_Click;
            // 
            // ItemFolderDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btnUp);
            Controls.Add(txtPath);
            Controls.Add(lblPath);
            Controls.Add(btnDeleteFolder);
            Controls.Add(btnStartPaste);
            Controls.Add(btnStartCopy);
            Controls.Add(btnStartMidnight);
            Controls.Add(btnStartNow);
            Controls.Add(btnStartClear);
            Controls.Add(dtpStart);
            Controls.Add(lblStart);
            Controls.Add(btnCompletedPaste);
            Controls.Add(btnCompletedCopy);
            Controls.Add(btnCompletedMidnight);
            Controls.Add(btnCompletedNow);
            Controls.Add(btnCompletedClear);
            Controls.Add(btnReminderPaste);
            Controls.Add(btnReminderCopy);
            Controls.Add(btnReminderMidnight);
            Controls.Add(btnReminderNow);
            Controls.Add(btnReminderClear);
            Controls.Add(btnDuePaste);
            Controls.Add(btnDueCopy);
            Controls.Add(btnDueMidnight);
            Controls.Add(btnDueNow);
            Controls.Add(btnDueClear);
            Controls.Add(btnSetAsRoot);
            Controls.Add(btnOpen);
            Controls.Add(btnSaveChanges);
            Controls.Add(btnNewSubfolder);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(dgvFiles);
            Controls.Add(lblFiles);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(txtOwner);
            Controls.Add(lblOwner);
            Controls.Add(txtUrgency);
            Controls.Add(lblUrgency);
            Controls.Add(txtImportance);
            Controls.Add(lblImportance);
            Controls.Add(dtpReminder);
            Controls.Add(lblReminder);
            Controls.Add(dtpModified);
            Controls.Add(lblModified);
            Controls.Add(dtpDue);
            Controls.Add(lblDue);
            Controls.Add(dtpCreated);
            Controls.Add(lblCreated);
            Controls.Add(dtpCompleted);
            Controls.Add(lblCompleted);
            Controls.Add(cmbFolderType);
            Controls.Add(lblFolderType);
            Name = "ItemFolderDetail";
            Size = new Size(598, 748);
            ((System.ComponentModel.ISupportInitialize)dgvFiles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFolderType;
        private ComboBox cmbFolderType;
        private Label lblCompleted;
        private DateTimePicker dtpCompleted;
        private DateTimePicker dtpCreated;
        private Label lblCreated;
        private DateTimePicker dtpDue;
        private Label lblDue;
        private DateTimePicker dtpModified;
        private Label lblModified;
        private DateTimePicker dtpReminder;
        private Label lblReminder;
        private Label lblImportance;
        private TextBox txtImportance;
        private TextBox txtUrgency;
        private Label lblUrgency;
        private TextBox txtOwner;
        private Label lblOwner;
        private TextBox txtDescription;
        private Label lblDescription;
        private Label lblFiles;
        private DataGridView dgvFiles;
        private TextBox txtName;
        private Label lblName;
        private Button btnNewSubfolder;
        private Button btnSaveChanges;
        private Button btnOpen;
        private Button btnSetAsRoot;
        private Button btnDueClear;
        private Button btnDueNow;
        private Button btnDueMidnight;
        private Button btnDueCopy;
        private Button btnDuePaste;
        private Button btnReminderClear;
        private Button btnReminderPaste;
        private Button btnReminderCopy;
        private Button btnReminderMidnight;
        private Button btnReminderNow;
        private Button btnCompletedPaste;
        private Button btnCompletedCopy;
        private Button btnCompletedMidnight;
        private Button btnCompletedNow;
        private Button btnCompletedClear;
        private Label lblStart;
        private DateTimePicker dtpStart;
        private Button btnStartPaste;
        private Button btnStartCopy;
        private Button btnStartMidnight;
        private Button btnStartNow;
        private Button btnStartClear;
        private Button btnDeleteFolder;
        private Label lblPath;
        private TextBox txtPath;
        private Button btnUp;
        private ToolTip objToolTips;
    }
}
