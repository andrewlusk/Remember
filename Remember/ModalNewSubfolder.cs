namespace Remember
{
    /// <summary>
    /// This is the modal screen that opens when creating a new subfolder.
    /// </summary>
    public partial class ModalNewSubfolder : Form
    {
        #region "Properties"
        Host frmHost; // for referencing/accessing main UI
        #endregion

        #region "Constructor"
        public ModalNewSubfolder(Host pfrmHost)
        {
            InitializeComponent();
            frmHost = pfrmHost;
        }
        #endregion

        #region "Event Handlers"
        /// <summary>
        /// Input handler for new subfolder name textbox;
        /// prevent invalid characters from being entered.
        /// </summary>
        private void txtNewSubfolderName_TextChanged(object sender, EventArgs e)
        {
            //store cursor position for stripping invalid characters
            int intCursorPosition = txtNewSubfolderName.SelectionStart + txtNewSubfolderName.SelectionLength;

            bool blnRemovedCharacter = false;

            //invalid characters
            foreach (string strInvalidCharacter in RefConsts.castrFolderInvalidCharacters)
            {
                if (txtNewSubfolderName.Text.IndexOf(strInvalidCharacter) != -1)
                {
                    txtNewSubfolderName.Text = txtNewSubfolderName.Text.Replace(strInvalidCharacter, "");
                    blnRemovedCharacter = true;
                }
            }

            if (blnRemovedCharacter)
            { txtNewSubfolderName.SelectionStart = Math.Max(intCursorPosition - 1, 0); }

            if (txtNewSubfolderName.TextLength > 0) { btnNewSubfolderOK.Enabled = true; }
        }

        /// <summary>
        /// Cancel button click handler;
        /// close the modal.
        /// </summary>
        private void btnNewSubfolderCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// OK button click handler;
        /// attempt to create new subfolder with keyed name.
        /// </summary>
        private void btnNewSubfolderOK_Click(object sender, EventArgs e)
        {
            TryCreateNewSubfolder();
        }

        /// <summary>
        /// New subfolder name keypress handler;
        /// treat Enter the same as clicking 'OK'.
        /// </summary>
        private void txtNewSubfolderName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter key is = clicking OK
            if (e.KeyChar == (char)Keys.Enter)
            {
                TryCreateNewSubfolder();
            }
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Attempt to create a new subfolder under the folder in context
        /// (use keyed/entered new subfolder name),
        /// and load that new folder in the Detail pane.
        /// </summary>
        private void TryCreateNewSubfolder()
        {
            try
            {
                if (Directory.Exists(frmHost.ctlItemFolderDetail.objItemFolder.Path + "\\" + txtNewSubfolderName.Text))
                {
                    MessageBox.Show(text: "Folder with this name already exists", caption: "Folder Create Failed", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    return;
                }
                Directory.CreateDirectory(Path.Combine(frmHost.ctlItemFolderDetail.objItemFolder.Path, txtNewSubfolderName.Text));
                //refresh the form and load it
                string strNewFolder = frmHost.ctlItemFolderDetail.objItemFolder.Path + "\\" + txtNewSubfolderName.Text;
                frmHost.RefreshTree();
                frmHost.LoadFolderDetail(strNewFolder);

                //attempt to find this folder in the tree and highlight if found
                foreach (DataGridViewRow dgvrRow in frmHost.dgvFolders.Rows)
                {
                    if (dgvrRow.Cells[1].Value.ToString() == frmHost.ctlItemFolderDetail.objItemFolder.Path.Substring(frmHost.rootFolder.LastIndexOf('\\') + 1))
                    {
                        frmHost.dgvFolders.ClearSelection();
                        frmHost.dgvFolders.CurrentCell = dgvrRow.Cells[0];
                        dgvrRow.Selected = true;
                    }
                }

                this.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}