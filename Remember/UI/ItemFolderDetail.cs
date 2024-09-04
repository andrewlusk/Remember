using System.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
using Remember.Objects;

namespace Remember
{
    /// <summary>
    /// Detail pane for currently selected item folder
    /// </summary>
    public partial class ItemFolderDetail : UserControl
    {
        #region "Properties"
        Host frmHost;
        public ItemFolder objItemFolder;
        ModalNewSubfolder frmNewSubfolder;

        string folderName;
        public string relativePath;
        bool loading = false;
        bool pasteDate = false;
        public bool dirty = false;

        private DateTime dtmClipboard;
        public DateTime dateTimeClipboard
        {
            get { return dtmClipboard; }
            set
            {
                dtmClipboard = value;
                btnStartPaste.Enabled = true;
                btnDuePaste.Enabled = true;
                btnReminderPaste.Enabled = true;
                btnCompletedPaste.Enabled = true;
            }
        }
        #endregion

        #region "Constructor"
        public ItemFolderDetail(Host pfrmHost, ItemFolder pobjItemFolder)
        {
            InitializeComponent();
            frmHost = pfrmHost;
            loading = true;
            objItemFolder = pobjItemFolder;

            //initialize clipboard date = highdate
            //todo: store this at the parent form level
            dtmClipboard = RefConsts.cdtmHighDate;

            //folder name
            folderName = pobjItemFolder.Path.Substring(pobjItemFolder.Path.LastIndexOf('\\') + 1);
            txtName.Text = folderName;

            //relative path
            relativePath = pobjItemFolder.Path.Substring(frmHost.rootFolder.LastIndexOf('\\') + 1);
            txtPath.Text = relativePath;

            //type
            int intFolderTypeAddCounter = -1;
            foreach (string strFoldertype in RefConsts.castrItemFolderTypes)
            {
                cmbFolderType.Items.Add(strFoldertype);
                intFolderTypeAddCounter++;
                if (strFoldertype == pobjItemFolder.Metadata.Type)
                {
                    cmbFolderType.SelectedIndex = intFolderTypeAddCounter;
                }
            }

            //created
            dtpCreated.Value = pobjItemFolder.Metadata.Created;

            //modified
            dtpModified.Value = pobjItemFolder.Metadata.Modified;

            //start
            if (pobjItemFolder.Metadata.Start == RefConsts.cdtmHighDate) { dtpStart.CustomFormat = " "; }
            dtpStart.Value = pobjItemFolder.Metadata.Start;

            //completed
            if (pobjItemFolder.Metadata.Completed == RefConsts.cdtmHighDate) { dtpCompleted.CustomFormat = " "; }
            dtpCompleted.Value = pobjItemFolder.Metadata.Completed;

            //due
            if (pobjItemFolder.Metadata.Due == RefConsts.cdtmHighDate) { dtpDue.CustomFormat = " "; }
            dtpDue.Value = pobjItemFolder.Metadata.Due;

            //reminder
            if (pobjItemFolder.Metadata.Reminder == RefConsts.cdtmHighDate) { dtpReminder.CustomFormat = " "; }
            dtpReminder.Value = pobjItemFolder.Metadata.Reminder;

            //owner
            txtOwner.Text = pobjItemFolder.Metadata.Owner;

            //importance
            if (pobjItemFolder.Metadata.Importance != 0) { txtImportance.Text = pobjItemFolder.Metadata.Importance.ToString(); }

            //urgency
            if (pobjItemFolder.Metadata.Urgency != 0) { txtUrgency.Text = pobjItemFolder.Metadata.Urgency.ToString(); }

            //description
            txtDescription.Text = pobjItemFolder.Metadata.Description;

            //Set as Root button visibility
            btnSetAsRoot.Visible = (objItemFolder.Path != frmHost.rootFolder);
            btnSetAsRoot.Enabled = (objItemFolder.Path != frmHost.rootFolder);

            //Go Up button visibility
            btnUp.Visible = (objItemFolder.Path != frmHost.rootFolder);

            //delete button visibility
            btnDeleteFolder.Visible = (objItemFolder.Path != frmHost.rootFolder);
            btnDeleteFolder.Enabled = (objItemFolder.Path != frmHost.rootFolder);

            //files table
            DataTable tblFiles = new DataTable();
            tblFiles.Columns.Add("Path", typeof(string));
            tblFiles.Columns.Add("Name", typeof(string));

            foreach (string strFile in objItemFolder.ChildFiles)
            {
                DataRow drNewFile = tblFiles.NewRow();
                drNewFile["Path"] = strFile;
                drNewFile["Name"] = strFile.Substring(objItemFolder.Path.Length + 1);
                tblFiles.Rows.Add(drNewFile);
            }

            dgvFiles.DataSource = tblFiles;
            dgvFiles.ReadOnly = true;
            dgvFiles.AllowUserToDeleteRows = false;
            dgvFiles.AllowUserToAddRows = false;
            dgvFiles.AllowUserToResizeColumns = true;

            dgvFiles.Columns["Path"].Visible = false;
            dgvFiles.Columns["Name"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvFiles.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvFiles.Columns["Name"].Width = dgvFiles.Width - 100;

            //add event to double click on 

            InitializeTipText();

            //turn off loading flag
            loading = false;
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Set up all mouseover tip/alt text for image buttons on the control
        /// </summary>
        public void InitializeTipText()
        {
            objToolTips.SetToolTip(btnOpen, "Open Folder");
            objToolTips.SetToolTip(btnUp, "Load Parent");
            objToolTips.SetToolTip(btnDeleteFolder, "Delete Folder");
            objToolTips.SetToolTip(btnNewSubfolder, "New Subfolder...");
            objToolTips.SetToolTip(btnSaveChanges, "Save Changes");

            Button[] arrDateClearButtons = [btnStartClear, btnDueClear, btnReminderClear, btnCompletedClear];
            foreach (Button btnButton in arrDateClearButtons) { objToolTips.SetToolTip(btnButton, "Clear"); }

            Button[] arrDateNowButtons = [btnStartNow, btnDueNow, btnReminderNow, btnCompletedNow];
            foreach (Button btnButton in arrDateNowButtons) { objToolTips.SetToolTip(btnButton, "Set to Today (1 click: Midnight, 2 clicks: Now)"); }

            Button[] arrDateMidnightButtons = [btnStartMidnight, btnDueMidnight, btnReminderMidnight, btnCompletedMidnight];
            foreach (Button btnButton in arrDateMidnightButtons) { objToolTips.SetToolTip(btnButton, "Set to Midnight (for Today's Date)"); }
            
            Button[] arrDateCopyButtons = [btnStartCopy, btnDueCopy, btnReminderCopy, btnCompletedCopy];
            foreach (Button btnButton in arrDateCopyButtons) { objToolTips.SetToolTip(btnButton, "Copy Date"); }

            Button[] arrDatePasteButtons = [btnStartPaste, btnDuePaste, btnReminderPaste, btnCompletedPaste];
            foreach (Button btnButton in arrDatePasteButtons) { objToolTips.SetToolTip(btnButton, "Paste Date"); }
        }
        /// <summary>
        /// Compare field values against last saved attributes to determine if there are changes;
        /// Change the color of the detail pane and show the Save button if true.
        /// </summary>
        private void dirtyRecalc()
        {
            if (!loading)
            {
                dirty = (
                txtName.Text != folderName
                || cmbFolderType.Text != objItemFolder.Metadata.Type
                || dtpStart.Value != objItemFolder.Metadata.Start
                || dtpDue.Value != objItemFolder.Metadata.Due
                || dtpReminder.Value != objItemFolder.Metadata.Reminder
                || dtpCompleted.Value != objItemFolder.Metadata.Completed
                || (txtImportance.Text == "" ? "0" : txtImportance.Text) != objItemFolder.Metadata.Importance.ToString()
                || (txtUrgency.Text == "" ? "0" : txtUrgency.Text) != objItemFolder.Metadata.Urgency.ToString())
                || txtOwner.Text != objItemFolder.Metadata.Owner
                || txtDescription.Text != objItemFolder.Metadata.Description;
                btnSaveChanges.Visible = dirty;
                this.BackColor = (dirty ? Color.FromArgb(204, 222, 253) : Color.FromArgb(220, 220, 220));
            }
        }

        /// <summary>
        /// Save updated field values to metadata file
        /// </summary>
        public void saveChanges()
        {
            //update the in-memory FolderMetadata object with all properties
            objItemFolder.Metadata.Type = cmbFolderType.Text;
            objItemFolder.Metadata.Modified = DateTime.Now;
            objItemFolder.Metadata.Start = dtpStart.Value;
            objItemFolder.Metadata.Due = dtpDue.Value;
            objItemFolder.Metadata.Reminder = dtpReminder.Value;
            objItemFolder.Metadata.Completed = dtpCompleted.Value;

            if (txtImportance.Text != "")
            { objItemFolder.Metadata.Importance = int.Parse(txtImportance.Text); }
            else { objItemFolder.Metadata.Importance = 0; }

            if (txtUrgency.Text != "") { objItemFolder.Metadata.Urgency = int.Parse(txtUrgency.Text); }
            else { objItemFolder.Metadata.Urgency = 0; }

            objItemFolder.Metadata.Owner = txtOwner.Text;
            objItemFolder.Metadata.Description = txtDescription.Text;

            //get the host app to save the FolderMetadata to json
            string jsnUpdatedMdFile = JsonSerializer.Serialize(objItemFolder.Metadata);
            File.WriteAllText(objItemFolder.Path + "\\" + RefConsts.cstrRmdFile, jsnUpdatedMdFile);

            //rename the folder if the name has changed

            if (folderName != txtName.Text)
            {
                string strOldPath = objItemFolder.Path;
                string strNewPath = strOldPath.Substring(0, (strOldPath.Length - folderName.Length)) + txtName.Text;
                Directory.Move(objItemFolder.Path, strNewPath); //rename folder
                objItemFolder.Path = strNewPath;
                folderName = txtName.Text;
                if (strOldPath == frmHost.rootFolder) { frmHost.SetRootFolder(pstrNewRootFolder: strNewPath, pblnNewRootHistory: true); }
            }

            //clear Dirty
            dirtyRecalc();

            //get the host app to refresh its datagridview
            frmHost.RefreshTree();
        }
        #endregion

        #region "Event Handlers"
        private void dtpDue_ValueChanged(object sender, EventArgs e)
        {
            //if setting to highdate, hide value
            if (dtpDue.Value == RefConsts.cdtmHighDate) { dtpDue.CustomFormat = " "; }
            else
            {
                if (dtpDue.CustomFormat == " ")
                {
                    //value is being set FROM highdate
                    if (!pasteDate)
                    { dtpDue.Value = dtpDue.Value.Date; } //set to midnight
                    dtpDue.CustomFormat = RefConsts.cstrDateTimeFormat; //display date
                }
            }

            btnDueClear.Enabled = (dtpDue.Value != RefConsts.cdtmHighDate);
            btnDueMidnight.Enabled = (dtpDue.Value != RefConsts.cdtmHighDate);
            btnDueCopy.Enabled = (dtpDue.Value != RefConsts.cdtmHighDate);

            if (!loading) { dirtyRecalc(); }
        }

        private void dtpReminder_ValueChanged(object sender, EventArgs e)
        {
            //if setting to highdate, hide value
            if (dtpReminder.Value == RefConsts.cdtmHighDate) { dtpReminder.CustomFormat = " "; }
            else
            {
                if (dtpReminder.CustomFormat == " ")
                {
                    //value is being set FROM highdate
                    if (!pasteDate) { dtpReminder.Value = dtpReminder.Value.Date; }  //set to midnight
                    dtpReminder.CustomFormat = RefConsts.cstrDateTimeFormat; //display date
                }
            }

            btnReminderClear.Enabled = (dtpReminder.Value != RefConsts.cdtmHighDate);
            btnReminderMidnight.Enabled = (dtpReminder.Value != RefConsts.cdtmHighDate);
            btnReminderCopy.Enabled = (dtpReminder.Value != RefConsts.cdtmHighDate);

            if (!loading) { dirtyRecalc(); }
        }

        private void dtpCompleted_ValueChanged(object sender, EventArgs e)
        {
            //if setting to highdate, hide value
            if (dtpCompleted.Value == RefConsts.cdtmHighDate) { dtpCompleted.CustomFormat = " "; }
            else
            {
                if (dtpCompleted.CustomFormat == " ")
                {
                    //value is being set FROM highdate
                    if (!pasteDate) { dtpCompleted.Value = dtpCompleted.Value.Date; } //set to midnight
                    dtpCompleted.CustomFormat = RefConsts.cstrDateTimeFormat; //display date
                }
            }

            btnCompletedClear.Enabled = (dtpCompleted.Value != RefConsts.cdtmHighDate);
            btnCompletedMidnight.Enabled = (dtpCompleted.Value != RefConsts.cdtmHighDate);
            btnCompletedCopy.Enabled = (dtpCompleted.Value != RefConsts.cdtmHighDate);

            if (!loading) { dirtyRecalc(); }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            //if setting to highdate, hide value
            if (dtpStart.Value == RefConsts.cdtmHighDate) { dtpStart.CustomFormat = " "; }
            else
            {
                if (dtpStart.CustomFormat == " ")
                {
                    //value is being set FROM highdate
                    if (!pasteDate) { dtpStart.Value = dtpStart.Value.Date; } //set to midnight
                    dtpStart.CustomFormat = RefConsts.cstrDateTimeFormat; //display date
                }
            }

            btnStartClear.Enabled = (dtpStart.Value != RefConsts.cdtmHighDate);
            btnStartMidnight.Enabled = (dtpStart.Value != RefConsts.cdtmHighDate);
            btnStartCopy.Enabled = (dtpStart.Value != RefConsts.cdtmHighDate);

            if (!loading) { dirtyRecalc(); }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            //store cursor position for stripping invalid characters
            int intCursorPosition = txtName.SelectionStart + txtName.SelectionLength;

            bool blnRemovedCharacter = false;

            //invalid characters
            foreach (string strInvalidCharacter in RefConsts.castrFolderInvalidCharacters)
            {
                if (txtName.Text.IndexOf(strInvalidCharacter) != -1)
                {
                    txtName.Text = txtName.Text.Replace(strInvalidCharacter, "");
                    blnRemovedCharacter = true;
                }
            }

            if (blnRemovedCharacter)
            { txtName.SelectionStart = Math.Max(intCursorPosition - 1, 0); }

            if (!loading) { dirtyRecalc(); }
        }

        private void cmbFolderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading) { dirtyRecalc(); }
        }

        private void btnStartClear_Click(object sender, EventArgs e)
        {
            dtpStart.Value = RefConsts.cdtmHighDate;
        }

        private void btnStartNow_Click(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Now;
        }

        private void btnStartMidnight_Click(object sender, EventArgs e)
        {
            dtpStart.Value = dtpStart.Value.Date;
        }

        private void btnStartCopy_Click(object sender, EventArgs e)
        {
            dateTimeClipboard = dtpStart.Value;
        }

        private void btnStartPaste_Click(object sender, EventArgs e)
        {
            pasteDate = true;
            dtpStart.Value = dateTimeClipboard;
            pasteDate = false;
        }

        private void btnDueClear_Click(object sender, EventArgs e)
        {
            dtpDue.Value = RefConsts.cdtmHighDate;
        }

        private void btnDueNow_Click(object sender, EventArgs e)
        {
            dtpDue.Value = DateTime.Now;
        }

        private void btnDueMidnight_Click(object sender, EventArgs e)
        {
            dtpDue.Value = dtpDue.Value.Date;
        }

        private void btnDueCopy_Click(object sender, EventArgs e)
        {
            dateTimeClipboard = dtpDue.Value;
        }

        private void btnDuePaste_Click(object sender, EventArgs e)
        {
            pasteDate = true;
            dtpDue.Value = dateTimeClipboard;
            pasteDate = false;
        }

        private void btnReminderClear_Click(object sender, EventArgs e)
        {
            dtpReminder.Value = RefConsts.cdtmHighDate;
        }

        private void btnReminderNow_Click(object sender, EventArgs e)
        {
            dtpReminder.Value = DateTime.Now;
        }

        private void btnReminderMidnight_Click(object sender, EventArgs e)
        {
            dtpReminder.Value = dtpReminder.Value.Date;
        }

        private void btnReminderCopy_Click(object sender, EventArgs e)
        {
            dateTimeClipboard = dtpReminder.Value;
        }

        private void btnReminderPaste_Click(object sender, EventArgs e)
        {
            pasteDate = true;
            dtpReminder.Value = dateTimeClipboard;
            pasteDate = false;
        }

        private void btnCompletedClear_Click(object sender, EventArgs e)
        {
            dtpCompleted.Value = RefConsts.cdtmHighDate;
        }

        private void btnCompletedNow_Click(object sender, EventArgs e)
        {
            dtpCompleted.Value = DateTime.Now;
        }

        private void btnCompletedMidnight_Click(object sender, EventArgs e)
        {
            dtpCompleted.Value = dtpCompleted.Value.Date;
        }

        private void btnCompletedCopy_Click(object sender, EventArgs e)
        {
            dateTimeClipboard = dtpCompleted.Value;
        }

        private void btnCompletedPaste_Click(object sender, EventArgs e)
        {
            pasteDate = true;
            dtpCompleted.Value = dateTimeClipboard;
            pasteDate = false;
        }

        private void txtImportance_TextChanged(object sender, EventArgs e)
        {
            //clean characters
            int intCursorPosition = txtImportance.SelectionStart + txtImportance.SelectionLength;
            string preClean = txtImportance.Text;
            string cleaned = Regex.Replace(txtImportance.Text, "[^0-9]", "");
            if (cleaned != preClean)
            {
                txtImportance.Text = cleaned;
                txtImportance.SelectionStart = intCursorPosition;
            }
            if (!loading) { dirtyRecalc(); }
        }

        private void txtUrgency_TextChanged(object sender, EventArgs e)
        {
            //clean characters
            int intCursorPosition = txtUrgency.SelectionStart + txtUrgency.SelectionLength;
            string preClean = txtUrgency.Text;
            string cleaned = Regex.Replace(txtUrgency.Text, "[^0-9]", "");
            if (cleaned != preClean)
            {
                txtUrgency.Text = cleaned;
                txtUrgency.SelectionStart = intCursorPosition;
            }
            if (!loading) { dirtyRecalc(); }
        }

        private void txtOwner_TextChanged(object sender, EventArgs e)
        {
            if (!loading) { dirtyRecalc(); }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            if (!loading) { dirtyRecalc(); }
        }

        /// <summary>
        /// Open Folder click handler
        /// Open the folder in context in windows explorer
        /// </summary>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            { Process.Start("explorer.exe", objItemFolder.Path); }
        }

        /// <summary>
        /// Save Changes click handler
        /// Commit changes in UI to the filesystem; refresh entire tree and reload detail
        /// </summary>
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            saveChanges();

            //update relative path
            relativePath = objItemFolder.Path.Substring(frmHost.rootFolder.LastIndexOf('\\') + 1);
            txtPath.Text = relativePath;

            //attempt to find this folder in the table and highlight if found
            frmHost.SelectFolderInTable(relativePath);
        }

        /// <summary>
        /// Set as root click handler
        /// Pop modal confirmation dialog and reset root folder
        /// </summary>
        private void btnSetAsRoot_Click(object sender, EventArgs e)
        {
            frmHost.ModalLock = true;

            //confirm root change
            DialogResult result = MessageBox.Show(
                text: $"You are about to switch your root folder to: \n\n {relativePath}" +
                "\n\nPlease confirm you want to set this as your root folder.",
                caption: "Change Root Folder",
                buttons: MessageBoxButtons.OKCancel,
                icon: MessageBoxIcon.Warning);

            //delete
            if (result == DialogResult.OK)
            {
                frmHost.SetRootFolder(pstrNewRootFolder: objItemFolder.Path, pblnNewRootHistory: true);
            }
            frmHost.ModalLock = false;
        }

        /// <summary>
        /// New subfolder click handler
        /// Pop modal confirmation dialog
        /// </summary>
        private void btnNewSubfolder_Click(object sender, EventArgs e)
        {
            frmHost.ModalLock = true;
            frmNewSubfolder = new ModalNewSubfolder(frmHost);
            frmNewSubfolder.FormBorderStyle = FormBorderStyle.FixedDialog; // Optional: Set a fixed dialog border style
            frmNewSubfolder.Text = "Create New Subfolder";
            frmNewSubfolder.StartPosition = FormStartPosition.CenterScreen;
            frmNewSubfolder.ShowDialog();
            frmNewSubfolder.Dispose();
            frmHost.ModalLock = false;
        }

        /// <summary>
        /// Delete folder click handler
        /// Pop confirmation dialog and delete the folder - along with all files and descendents
        /// </summary>
        private void btnDeleteFolder_Click(object sender, EventArgs e)
        {
            frmHost.ModalLock = true;

            //determine how many folders will be deleted, including this one
            int intFoldersToDelete = Directory.GetDirectories(objItemFolder.Path, "", SearchOption.AllDirectories).Length + 1;

            //determine how many files will be deleted, exlcuding the rmd.json files
            int intFilesToDelete = Directory.GetFiles(
                path: objItemFolder.Path,
                searchPattern: "*",
                searchOption: SearchOption.AllDirectories)
              .Where(f => Path.GetFileName(f) != RefConsts.cstrRmdFile).ToArray().Length;

            //confirm deletion
            DialogResult result = MessageBox.Show(
                text: $"You are about to delete {intFoldersToDelete} {(intFoldersToDelete > 1 ? "folders" : "folder")}" +
                $" and {intFilesToDelete} {(intFilesToDelete > 1 || intFilesToDelete == 0 ? "files" : "file")}." +
                "\n\nAre you sure you want to delete this folder and all its descendants?",
                caption: "Delete Folder",
                buttons: MessageBoxButtons.YesNoCancel,
                icon: MessageBoxIcon.Warning);

            //delete
            if (result == DialogResult.Yes)
            {
                string strFolderToDelete = objItemFolder.Path;
                string strParentOfDeletedFolder = Directory.GetParent(strFolderToDelete)!.FullName;
                Directory.Delete(path: strFolderToDelete, recursive: true);
                frmHost.LoadFolderDetail(strParentOfDeletedFolder);
                frmHost.RefreshTree();
            }

            frmHost.ModalLock = false;
        }

        /// <summary>
        /// File table click handler
        /// Open the actual file using windows explorer (default app for the file extension)
        /// </summary>
        private void dgvFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //has to be a data cell
            if (e.RowIndex >= 0)
            {
                string strFilePath = (string)dgvFiles.Rows[e.RowIndex].Cells["Path"].Value;
                Process.Start("explorer.exe", strFilePath);
            }
        }

        /// <summary>
        /// Up arrow click handler
        /// Navigates away from the folder in context, loads the parent into the detail pane
        /// </summary>
        private void btnUp_Click(object sender, EventArgs e)
        {
            frmHost.LoadFolderDetail(Directory.GetParent(objItemFolder.Path)!.FullName);
        }
        #endregion


    }
}
