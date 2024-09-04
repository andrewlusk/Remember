using System.Data;
using System.Diagnostics;
using System.Text.Json;
using Remember.Objects;
using Remember.UI;

namespace Remember
{
    /// <summary>
    ///  Main UI container
    /// </summary>
    public partial class Host : Form
    {
        #region "Properties"
        public string rootFolder; // Root folder of the item tree
        public UserSettings userSettings; // Loaded from rSettings.json
        public string userSettingsFolder; // Location of settings file
        public string strParentPath; // The root's parent path (used for substringing descendents)
        public Dictionary<string, ItemFolder> dctItemFolders; // In-memory collection of all descendent item folders with metadata and files
        public DataTable tblItemFolders; // In-memory table of all item folders; source for the dgvFolders DataGridView.
        public ItemFolderDetail ctlItemFolderDetail; // Container object for right-side "Detail" panel for a particular item folder
        public bool blnDetailVisible = false; // Whether the item folder detail screen is open or collapsed
        public string strQueryString; // Filter/sort string to apply to on-screen view of tblItemFolders
        public ModalSaveLoadQuery frmSaveLoadQuery; // Container for the "Save/Load Query" modal where sorts/filters can be saved/recalled
        private bool blnModalLock = false; // Whether a modal is currently displayed and the main UI is locked
        public Panel pnlModaLock; // 'Locked' screen container object (darkened image of UI at the moment a modal opens)
        public Reminders frmReminders; // Container object for the "Reminders" modal
        public int intLeft; // Host form position tracker, for recalling while form is minimized and .Left is -32000
        public int intTop; // Host form position tracker, for recalling while form is minimized and .Top is -32000

        /// <summary>
        /// Modal lock flag getter/setter
        /// </summary>
        public bool ModalLock
        {
            get { return blnModalLock; }
            set
            {
                blnModalLock = value;
                ToggleUILock();
            }
        }
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor
        /// </summary>
        public Host()
        {
            InitializeComponent();
        }
        #endregion

        #region "Functions"

        /// <summary>
        /// Form.Load handler
        /// Load user settings and set root folder
        /// </summary>
        private void AppLoad(object sender, EventArgs e)
        {
            userSettingsFolder = $"C:\\Users\\{Environment.UserName}\\AppData\\Local\\{RefConsts.cstrAppDataFolderName}";
            GetUserSettings();
            if (userSettings.RootFolder == "")
            {
                //prompt user to indicate root item folder
                SelectRootFolder();
            }
            else
            {
                SetRootFolder(userSettings.RootFolder);
            }
            InitializeTipText();
            tmrReminders.Start();
            frmReminders = new Reminders(this);
        }

        /// <summary>
        /// Lock controls and darken the screen while the user works with a modal
        /// </summary>
        public void ToggleUILock()
        {
            if (ModalLock)
            {
                //hide the Reminders modal, if it is visible
                if (frmReminders != null && frmReminders.Visible) { frmReminders.Visible = false; }

                // take a screenshot of the UI and darken it:
                Bitmap bmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
                using (Graphics G = Graphics.FromImage(bmp))
                {
                    G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    G.CopyFromScreen(this.PointToScreen(new Point(0, 0)), new Point(0, 0), this.ClientRectangle.Size);
                    double percent = 0.60;
                    Color darken = Color.FromArgb((int)(255 * percent), Color.Black);
                    using (Brush brsh = new SolidBrush(darken))
                    {
                        G.FillRectangle(brsh, this.ClientRectangle);
                        brsh.Dispose();
                    }
                    G.Dispose();
                }

                // put the darkened screenshot into a Panel and bring it to the front:
                pnlModaLock = new Panel();
                pnlModaLock.Location = new Point(0, 0);
                pnlModaLock.Size = this.ClientRectangle.Size;
                pnlModaLock.BackgroundImage = bmp;
                this.Controls.Add(pnlModaLock);
                pnlModaLock.BringToFront();
            }
            else
            {
                pnlModaLock.BackgroundImage.Dispose();
                pnlModaLock.Dispose();
                frmReminders.CheckForReminders();
            }
        }

        /// <summary>
        /// Load user settings from file into memory
        /// </summary>
        private void GetUserSettings()
        {
            string jsnSettings = "";

            if (!File.Exists(userSettingsFolder + "\\" + RefConsts.cstrRSettingsFile))
            {
                //no settings file yet; create it
                userSettings = new UserSettings();
                userSettings.userQueries = [];
                //provide some preset sample queries out of the box
                userSettings.InitializePresetQueries();
                userSettings.RootFolder = "";
                jsnSettings = JsonSerializer.Serialize(userSettings);
                if (!Directory.Exists(userSettingsFolder)) { Directory.CreateDirectory(userSettingsFolder); }
                File.WriteAllText(userSettingsFolder + "\\" + RefConsts.cstrRSettingsFile, jsnSettings);
            }
            else
            {
                //settings file exists; open it
                jsnSettings = File.ReadAllText(userSettingsFolder + "\\" + RefConsts.cstrRSettingsFile);
                userSettings = JsonSerializer.Deserialize<UserSettings>(jsnSettings);
                //put current query in the QueryString UI field
                txtQueryString.Text = userSettings.currentQuery;
            }
        }

        /// <summary>
        /// Prompt user to select root item folder
        /// </summary>
        private void SelectRootFolder()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK) { SetRootFolder(fbd.SelectedPath); }
        }

        /// <summary>
        /// Set the root/home item folder to work out of.
        /// validate parameter folder, update settings,
        /// trigger full datagridview refresh, then load the root folder in the Detail pane.
        /// </summary>
        public void SetRootFolder(string pstrNewRootFolder)
        {

            //validate root folder path
            if (pstrNewRootFolder.Length == 0) { throw new Exception("No root folder specified"); }

            //replace / with \
            pstrNewRootFolder.Replace("/", "\\");

            //ensure path does NOT have slash at the end
            if (pstrNewRootFolder[pstrNewRootFolder.Length - 1] == '\\' || pstrNewRootFolder[pstrNewRootFolder.Length - 1] == '/')
            { pstrNewRootFolder = pstrNewRootFolder.Substring(0, pstrNewRootFolder.Length - 1); }

            //confirm this folder exists
            if (!Directory.Exists(pstrNewRootFolder))
            {
                MessageBox.Show(
                    text: "Cannot set root folder to : " + pstrNewRootFolder + ":\n\nFolder does not exist",
                    caption: "Failed to Set Root Folder",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                return;
            }

            //confirm this folder isn't something super low level
            if (pstrNewRootFolder == @"C:" ||
                pstrNewRootFolder == @"C:\Users" ||
                pstrNewRootFolder == @"C:\Windows" ||
                pstrNewRootFolder == @"C:\Program Files" ||
                pstrNewRootFolder == @"C:\Program Files (x86)")
            {
                MessageBox.Show(
                    text: "Cannot set root folder to : " + pstrNewRootFolder + ":\n\nFolder is too low-level",
                    caption: "Failed to Set Root Folder",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                return;
            }

            //if there's no rmd file (we're about to create rmd files in every folder underneath this new root),
            //make sure we're not going to create files in a ridiculous number of folders - because it's probably a mistake
            if (!File.Exists(pstrNewRootFolder + "\\" + RefConsts.cstrRmdFile))
            {
                if (Directory.GetDirectories(pstrNewRootFolder, "", SearchOption.AllDirectories).Length > RefConsts.cintMaxSetupFolders)
                {
                    MessageBox.Show(
                        text: $"Cannot set root folder to {pstrNewRootFolder}:\n\nThis would create metadata files in over {RefConsts.cintMaxSetupFolders} (sub)folders" +
                        "\n\nChoose another location",
                        caption: "Failed to Set Root Folder",
                        buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Error);
                    return;
                }
            }

            rootFolder = pstrNewRootFolder;
            txtRootFolder.Text = rootFolder;
            userSettings.RootFolder = rootFolder;

            //update settings json
            string jsnSettings = JsonSerializer.Serialize(userSettings);
            File.WriteAllText(userSettingsFolder + "\\" + RefConsts.cstrRSettingsFile, jsnSettings);

            //store the parent of the root folder (for substringing)
            strParentPath = "";
            strParentPath = Directory.GetParent(rootFolder)!.FullName;

            RefreshTree();
            LoadFolderDetail(rootFolder);
        }

        /// <summary>
        /// Traverse the tree of subfolders from the root,
        /// load the item folder metadata for each child folder,
        /// populate a table and datagridview for all loaded folders,
        /// and if there is a folder loaded in the Detail screen, highlight that folder in the table.
        /// </summary>
        public void RefreshTree()
        {

            //create a list to queue folders for loading and add the root folder to the queue
            List<string> lstFoldersToLoad = new List<string>();
            lstFoldersToLoad.Add(rootFolder);

            //create a dictionary for loaded folders with their path as their key
            dctItemFolders = new Dictionary<string, ItemFolder>();

            do
            {
                ItemFolder fldLoadedFolder = new ItemFolder(pstrPath: lstFoldersToLoad[lstFoldersToLoad.Count - 1]);
                lstFoldersToLoad.RemoveAt(lstFoldersToLoad.Count - 1);
                dctItemFolders.Add(fldLoadedFolder.Path, fldLoadedFolder);
                if (fldLoadedFolder.ChildFolders.Count > 0)
                {
                    foreach (string strChildFolder in fldLoadedFolder.ChildFolders) { lstFoldersToLoad.Add(strChildFolder); }
                }
            } while (lstFoldersToLoad.Count > 0);

            // create table structure for item folders
            tblItemFolders = CreateItemFolderTableStructure();

            //add a row to the table for each loaded item folder
            foreach (KeyValuePair<string, ItemFolder> kvpFld in dctItemFolders)
            { AddItemFolderDataRow(pkvpItemFolder: kvpFld); }

            //bind the table to the displayed datagridview (UI table control)
            dgvFolders.DataSource = tblItemFolders;
            dgvFolders.AutoResizeColumns();
            dgvFolders.ReadOnly = true;
            dgvFolders.AllowUserToDeleteRows = false;
            dgvFolders.AllowUserToAddRows = false;
            dgvFolders.AllowUserToResizeColumns = true;

            //format date columns to use military time to save space
            string[] astrDateColumns = ["Created", "Modified", "Start", "Due", "Reminder", "Completed"];
            foreach (string strColumn in astrDateColumns)
            { dgvFolders.Columns[strColumn].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"; }

            //allow Path column to wrap on resize
            dgvFolders.Columns["Path"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvFolders.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            //keep the width under 400 upon initial render
            if (dgvFolders.Columns["Path"].Width > 350)
            {
                dgvFolders.Columns["Path"].Width = 350;
            }
            //concatenate Description column so it doesn't spill off the edge
            dgvFolders.Columns["Description"].Width = 325;

            //hide the Load button if there are no userqueries
            btnLoadQuery.Enabled = (userSettings.userQueries.Length > 0);

            //apply filter and sort
            ApplyQuery();

            //if a folder is currently loaded in detail, find and highlight it in the table;
            //attempt to find this folder in the tree and highlight if found
            if (ctlItemFolderDetail != null) { SelectFolderInTable(ctlItemFolderDetail.relativePath); }
        }

        /// <summary>
        /// Initialize table of item folders (columns and types, no data)
        /// </summary>
        private static DataTable CreateItemFolderTableStructure()
        {
            DataTable tblItemFolderStructure = new DataTable();
            tblItemFolderStructure.Columns.Add("Type", typeof(string));
            tblItemFolderStructure.Columns.Add("Path", typeof(string));
            tblItemFolderStructure.Columns.Add("Created", typeof(DateTime));
            tblItemFolderStructure.Columns.Add("Modified", typeof(DateTime));
            tblItemFolderStructure.Columns.Add("Start", typeof(DateTime));
            tblItemFolderStructure.Columns.Add("Due", typeof(DateTime));
            tblItemFolderStructure.Columns.Add("Completed", typeof(DateTime));
            tblItemFolderStructure.Columns.Add("Reminder", typeof(DateTime));
            tblItemFolderStructure.Columns.Add("Description", typeof(string));
            tblItemFolderStructure.Columns.Add("Owner", typeof(string));
            tblItemFolderStructure.Columns.Add("Importance", typeof(int));
            tblItemFolderStructure.Columns.Add("Urgency", typeof(int));

            return tblItemFolderStructure;
        }

        /// <summary>
        /// For a parameter ItemFolder object, load that item folder into the initialized table of item folders.
        /// </summary>
        private void AddItemFolderDataRow(KeyValuePair<string, ItemFolder> pkvpItemFolder)
        {
            DataRow drNewPath = tblItemFolders.NewRow();
            drNewPath["Type"] = pkvpItemFolder.Value.Metadata.Type;
            drNewPath["Path"] = pkvpItemFolder.Value.Path.Substring((strParentPath.Length + 1), (pkvpItemFolder.Value.Path.Length - strParentPath.Length - 1));
            drNewPath["Created"] = pkvpItemFolder.Value.Metadata.Created;
            drNewPath["Modified"] = pkvpItemFolder.Value.Metadata.Modified;
            if (pkvpItemFolder.Value.Metadata.Start != RefConsts.cdtmHighDate) { drNewPath["Start"] = pkvpItemFolder.Value.Metadata.Start; }
            if (pkvpItemFolder.Value.Metadata.Completed != RefConsts.cdtmHighDate) { drNewPath["Completed"] = pkvpItemFolder.Value.Metadata.Completed; }
            if (pkvpItemFolder.Value.Metadata.Due != RefConsts.cdtmHighDate) { drNewPath["Due"] = pkvpItemFolder.Value.Metadata.Due; }
            if (pkvpItemFolder.Value.Metadata.Reminder != RefConsts.cdtmHighDate) { drNewPath["Reminder"] = pkvpItemFolder.Value.Metadata.Reminder; }
            drNewPath["Description"] = pkvpItemFolder.Value.Metadata.Description;
            drNewPath["Owner"] = pkvpItemFolder.Value.Metadata.Owner;
            if (pkvpItemFolder.Value.Metadata.Importance != 0) { drNewPath["Importance"] = pkvpItemFolder.Value.Metadata.Importance; }
            if (pkvpItemFolder.Value.Metadata.Urgency != 0) { drNewPath["Urgency"] = pkvpItemFolder.Value.Metadata.Urgency; }
            tblItemFolders.Rows.Add(drNewPath);
        }

        /// <summary>
        /// Apply keyed filter/sort string to the table+datagridview of item folders
        /// </summary>
        private void ApplyQuery()
        {
            //clear any existing, visible query error
            txtQueryString.BackColor = Color.White;
            lblQueryErrorText.Visible = false;

            //parse query into filter and sort strings
            string[] astrQuery = ParseQuery(txtQueryString.Text);
            string strFilter = astrQuery[0];
            string strSort = astrQuery[1];

            //create a view for the filtered/sorted data
            DataView tbvFilteredSorted = new DataView((DataTable)dgvFolders.DataSource);

            //try to apply the sort
            try
            {
                tbvFilteredSorted.RowFilter = strFilter;
                tbvFilteredSorted.Sort = strSort;
                //save current query to settings if this worked
                userSettings.currentQuery = txtQueryString.Text;
                string jsnSettings = JsonSerializer.Serialize(userSettings);
                File.WriteAllText(userSettingsFolder + "\\" + RefConsts.cstrRSettingsFile, jsnSettings);
            }
            catch (Exception ex)
            {
                txtQueryString.BackColor = Color.FromArgb(255, 196, 196);
                lblQueryErrorText.Text = "Not a valid query: " + ex.Message;
                lblQueryErrorText.Visible = true;
            }

            //update list based on query
            dgvFolders.DataSource = tbvFilteredSorted.ToTable();
            tbvFilteredSorted.Dispose();
        }

        /// <summary>
        /// Parse out the keyed query string into dinstinct sort and filter pieces.
        /// </summary>
        private string[] ParseQuery(string pstrQuery)
        {

            string strFilter = "";
            string strSort = "";

            int intOrderByPosition = pstrQuery.ToLower().IndexOf("order by ");
            if (intOrderByPosition > 0)
            {
                strFilter = pstrQuery.Substring(startIndex: 0, length: intOrderByPosition);
                strSort = pstrQuery.Substring(intOrderByPosition + 9);
            }
            else if (intOrderByPosition == 0)
            {
                //only sort, no filter
                strSort = pstrQuery.Substring(intOrderByPosition + 9);
            }
            else
            {
                //(index is -1): only filter, no sort
                strFilter = pstrQuery;
            }

            return [strFilter, strSort];
        }

        /// <summary>
        /// Search the datagridview table of item folders for a parameter folder path
        /// and highlight that table row if it is found
        /// </summary>
        public void SelectFolderInTable(string pstrRelativePath)
        {
            foreach (DataGridViewRow dgvrRow in dgvFolders.Rows)
            {
                if (dgvrRow.Cells[1].Value.ToString() == pstrRelativePath)
                {
                    dgvFolders.ClearSelection();
                    dgvFolders.CurrentCell = dgvrRow.Cells[0];
                    dgvrRow.Selected = true;
                }
            }
        }

        /// <summary>
        /// Open the detail panel and load the item folder for a parameter folder path
        /// </summary>
        public void LoadFolderDetail(string pstrItemFolder)
        {
            if (ctlItemFolderDetail != null)
            {
                //detail pane is already loaded
                if (ctlItemFolderDetail.dirty)
                {
                    //there are unsaved changes in the currently loaded detail
                    ModalLock = true;
                    string strRelativePath = pstrItemFolder.Substring(rootFolder.LastIndexOf('\\') + 1);
                    //prompt the user to cancel or save before navigating away
                    DialogResult result = MessageBox.Show(
                        text: $"Changes to {ctlItemFolderDetail.relativePath} have not been saved." +
                        "\n\nDo you want to save your changes before navigating away?",
                        caption: "Save Changes?",
                        buttons: MessageBoxButtons.YesNoCancel,
                        icon: MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        ctlItemFolderDetail.saveChanges();
                        AddDetailPane(pstrItemFolder);
                    }
                    else if (result == DialogResult.No)
                    {
                        //just navigate away
                        AddDetailPane(pstrItemFolder);
                    }
                    else
                    {
                        //cancel: highlight the previously highlighted row
                        SelectFolderInTable(ctlItemFolderDetail.relativePath);
                    }
                    ModalLock = false;
                }
                else
                {
                    AddDetailPane(pstrItemFolder);
                }
            }
            else
            {
                AddDetailPane(pstrItemFolder);
            }
        }

        public void AddDetailPane(string pstrItemFolder)
        {
            //get reference to currently loaded detail pane, if it exists
            ItemFolderDetail ctlCurrentlyLoadedDetail = (ctlItemFolderDetail != null ? ctlItemFolderDetail : null);

            //generate the control and add to the form
            ctlItemFolderDetail = new ItemFolderDetail(pfrmHost: this,
                pobjItemFolder: dctItemFolders[pstrItemFolder]);
            ctlItemFolderDetail.Top = 8;
            ctlItemFolderDetail.Left = dgvFolders.Right + 10;
            this.Controls.Add(ctlItemFolderDetail);

            //find this row in the table and highlight it
            SelectFolderInTable(ctlItemFolderDetail.relativePath);

            //ensure detail pane is visible
            if (!blnDetailVisible) { ToggleDetailVisible(); }

            //dispose of previously loaded detail pane
            if (ctlCurrentlyLoadedDetail != null) { ctlCurrentlyLoadedDetail.Dispose(); }
        }

        /// <summary>
        /// Open or close the Detail pane
        /// </summary>
        private void ToggleDetailVisible()
        {
            if (blnDetailVisible)
            {
                ctlItemFolderDetail.Size = new Size(0, 0);
                ctlItemFolderDetail.Visible = false;
                this.btnToggleDetail.Text = "<";
            }
            else
            {

                if (dgvFolders.Columns["Path"].Width > 350)
                {
                    dgvFolders.Columns["Path"].Width = 350;
                }
                ctlItemFolderDetail.Size = new Size(512, 748);
                ctlItemFolderDetail.Visible = true;
                this.btnToggleDetail.Text = ">";

            }
            blnDetailVisible = !blnDetailVisible;
            ScaleToSize();
        }

        /// <summary>
        /// Resize the width of the item folders table, based on whether the Detail pane is visible
        /// </summary>
        private void ScaleToSize()
        {
            if (blnDetailVisible)
            {
                dgvFolders.Size = new Size(this.Size.Width - 560, this.Size.Height - 150);
                ctlItemFolderDetail.Left = dgvFolders.Right + 10;
                this.btnToggleDetail.Left = dgvFolders.Right - btnToggleDetail.Width;
            }
            else
            {
                dgvFolders.Size = new Size(this.Size.Width - 40, this.Size.Height - 150);
                this.btnToggleDetail.Left = dgvFolders.Right - btnToggleDetail.Width;
            }
            if (WindowState != FormWindowState.Minimized)
            {
                intLeft = Left;
                intTop = Top;
            }
            if (blnDetailVisible && Size.Width < 1355) { Width = 1355; }
            if (!blnDetailVisible && Size.Width < 835) { Width = 835; }
        }

        /// <summary>
        /// Load a parameter query string into the textbox and apply it to the table by calling RefreshTree.
        /// </summary>
        public void LoadQueryStringFromSettings(string pstrQueryString)
        {
            txtQueryString.Text = pstrQueryString;
            RefreshTree();
        }

        /// <summary>
        /// Set up all mouseover tip/alt text for image buttons on the control
        /// </summary>
        public void InitializeTipText()
        {
            objToolTips.SetToolTip(btnOpenRootFolder, "Open Root Folder");
            objToolTips.SetToolTip(btnRefresh, "Refresh Data");
            objToolTips.SetToolTip(btnQueryClear, "Clear Query");
            objToolTips.SetToolTip(btnLoadQuery, "Load Query");
            objToolTips.SetToolTip(btnSaveQuery, "Save Query");
        }
        #endregion

        #region "Event Handlers"
        /// <summary>
        /// Click handler for the query Refresh button;
        /// call RefreshTree
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTree();
        }

        /// <summary>
        /// Click handler for the Select Root Folder button;
        /// call SelectRootFolder
        /// </summary>
        private void btnSelectRootFolder_Click(object sender, EventArgs e)
        {
            SelectRootFolder();
        }

        /// <summary>
        /// Click handler for the Open Root Folder button;
        /// Opens the root folder in windows explorer.
        /// </summary>
        private void btnOpenRootFolder_Click(object sender, EventArgs e)
        { Process.Start("explorer.exe", rootFolder); }

        /// <summary>
        /// Click handler for datagridview row selection;
        /// Loads the selected item folder in the detail pane.
        /// </summary>
        private void dgvFolders_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //one row selected
            if (dgv.SelectedRows.Count == 1)
            {
                string strClickedFolderRelativePath = (string)dgvFolders.SelectedRows[0].Cells[1].Value;
                LoadFolderDetail(strParentPath + "\\" + strClickedFolderRelativePath);
            }

            //more than 1 row selected
            else if (dgv.SelectedRows.Count > 1) // >1
            {
                //todo multiple select detail screen?
            }
            else
            {
                //invalid path
                throw new Exception(RefConsts.cstrUnhandledErrorText + "\n\n You somehow selected ZERO rows in the folders table?!");
            }
        }

        /// <summary>
        /// Click handler for the Detail expand/collapse button;
        /// Expands or collapses the Detail pane.
        /// </summary>
        private void btnToggleDetail_Click(object sender, EventArgs e)
        {
            //toggle detail visibility
            if (ctlItemFolderDetail != null)
            { ToggleDetailVisible(); }
        }

        /// <summary>
        /// Key press handler for the query string textbox.
        /// Calls RefreshTree upon pressing Enter.
        /// </summary>
        private void txtQueryString_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                RefreshTree();
            }
        }

        /// <summary>
        /// Click handler for the Clear button on the query string textbox.
        /// Clears out the query string and calls RefreshTree.
        /// </summary>
        private void btnQueryClear_Click(object sender, EventArgs e)
        {
            txtQueryString.Text = "";
            RefreshTree();
        }

        /// <summary>
        /// Click handler for the Save button on the query string textbox.
        /// Locks the main UI and opens the Save/Load Query modal.  Unlocks the UI upon modal close.
        /// </summary>
        private void btnSaveQuery_Click(object sender, EventArgs e)
        {
            strQueryString = txtQueryString.Text;
            ModalLock = true;
            frmSaveLoadQuery = new ModalSaveLoadQuery(pfrmHost: this, pblnSave: true, pblnLoad: false);
            frmSaveLoadQuery.FormBorderStyle = FormBorderStyle.FixedDialog; // Optional: Set a fixed dialog border style
            frmSaveLoadQuery.Text = "Save Query";
            frmSaveLoadQuery.StartPosition = FormStartPosition.CenterScreen;
            frmSaveLoadQuery.ShowDialog();
            frmSaveLoadQuery.Dispose();
            ModalLock = false;
        }

        /// <summary>
        /// Click handler for the Load button on the query string textbox;
        /// Locks the main UI for the modal Load confirmation dialog box.
        /// </summary>
        private void btnLoadQuery_Click(object sender, EventArgs e)
        {
            ModalLock = true;
            frmSaveLoadQuery = new ModalSaveLoadQuery(pfrmHost: this, pblnSave: false, pblnLoad: true);
            frmSaveLoadQuery.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmSaveLoadQuery.Text = "Load Query";
            frmSaveLoadQuery.StartPosition = FormStartPosition.CenterScreen;
            frmSaveLoadQuery.ShowDialog();
            frmSaveLoadQuery.Dispose();
            ModalLock = false;
        }

        /// <summary>
        /// Resize handler for the main UI form.
        /// Triggers the auto-scaling for the item folders datagridview.
        /// </summary>
        private void Host_Resized(object sender, EventArgs e)
        {
            ScaleToSize();
        }
        #endregion

        /// <summary>
        /// Tick handler for the reminder check timer
        /// Check for new/changed reminders every 3s
        /// </summary>
        private void tmrReminders_Tick(object sender, EventArgs e)
        {
            if (frmReminders == null || frmReminders.IsDisposed) { frmReminders = new Reminders(this); }
            frmReminders.CheckForReminders();
        }

        /// <summary>
        /// Form close handler
        /// Prevent closing app if there are unsaved changes in the detail pane
        /// </summary>
        private void Host_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //do not allow this if there are unsaved changes in the detail pane
                if (ctlItemFolderDetail != null && ctlItemFolderDetail.dirty)
                {
                    DialogResult result = MessageBox.Show("Discard unsaved changes?", "Exit", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No) { e.Cancel = true; }
                }
            }
        }
    }
}
