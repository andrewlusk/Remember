using Remember.Objects;
using System.Data;

namespace Remember.UI
{
    public partial class Reminders : Form
    {
        #region "Properties"
        Host frmHost;
        private DataTable tblReminders = new DataTable();
        public List<string> remindItems = new List<string>();
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor
        /// </summary>
        public Reminders(Host pfrmHost)
        {
            InitializeComponent();
            frmHost = pfrmHost;
            InitializeTable();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Set up the Reminders datagridview and the table feeding it
        /// </summary>
        public void InitializeTable()
        {
            tblReminders.Columns.Add("Path", typeof(string));
            dgvReminders.DataSource = tblReminders;
            dgvReminders.Columns.Add(new DataGridViewButtonColumn()
            { HeaderText = "Snooze", Text = "Snooze", Name = "Snooze", UseColumnTextForButtonValue = true });
            dgvReminders.CellClick += dgvReminders_CellClick;
            dgvReminders.Columns["Path"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvReminders.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        /// <summary>
        /// Scan all visible items in the host table for elapsed Reminder dates
        /// </summary>
        public void CheckForReminders()
        {
            if (frmHost.ModalLock == false)
            {
                //repopulate internal list of items where reminder date has elapsed
                remindItems.Clear();
                DataView tbvItemsToRemind = new DataView((DataTable)frmHost.dgvFolders.DataSource);
                string strCurrentTime = DateTime.Now.ToString(RefConsts.cstrDateTimeFormat.Substring(1));
                //use DataView's filter functionality to find elapsed reminder dates
                tbvItemsToRemind.RowFilter = $"Reminder < '{strCurrentTime}'";
                if (tbvItemsToRemind.Count > 0)
                {
                    for (int i = 0; i < tbvItemsToRemind.Count; i++)
                    {
                        string strItemPath = (string)tbvItemsToRemind[i]["Path"];
                        //ensure the item is present in the list
                        if (remindItems.Contains(strItemPath) == false) { remindItems.Add(strItemPath); }
                    }
                }

                //finished with DataView object
                tbvItemsToRemind.Dispose();

                if (remindItems.Count > 0)
                {
                    //there are items to display
                    RefreshDisplayedReminders();
                    if (!Visible)
                    {
                        //display reminder modal
                        StartPosition = FormStartPosition.Manual;
                        Left = frmHost.intLeft + 300;
                        Top = frmHost.intTop + 300;
                        Show();
                    }
                }
                else if (remindItems.Count == 0 && dgvReminders.RowCount > 0)
                {
                    //there are stale/cleared reminders
                    RefreshDisplayedReminders();
                }
                else
                {
                    Visible = false;
                }
            }
        }

        /// <summary>
        /// Refresh Reminders table.
        /// Called after RefreshReminders() repopulates 'remindItems' list.
        /// </summary>
        public void RefreshDisplayedReminders()
        {
            //cache currently displayed reminders
            List<string> lstExistingReminders = new List<string>();
            foreach (DataRow dr in tblReminders.Rows)
            {
                lstExistingReminders.Add((string)dr["Path"]);
            }
            
            //repopulate table of reminders from list of items with elapsed reminder dates
            tblReminders.Rows.Clear();
            foreach (string strItem in remindItems)
            {
                DataRow drRemindItem = tblReminders.NewRow();
                drRemindItem["Path"] = strItem;
                tblReminders.Rows.Add(drRemindItem);
            }

            //display table in datagridview
            dgvReminders.DataSource = tblReminders;
            dgvReminders.AutoResizeColumns();

            //determine if there are new reminders being displayed
            bool blnNewReminders = false;

            //compare current reminders to previous ones
            foreach (DataRow dr in tblReminders.Rows)
            {
                if (lstExistingReminders.Contains((string)dr["Path"]) == false) { blnNewReminders = true; }
            }

            if (blnNewReminders)
            {
                //make the Reminders modal pop up if a new reminder is present
                WindowState = FormWindowState.Normal;
                TopMost = true;
                Activate();
            }
        }

        /// <summary>
        /// Set parameter item's Reminder value to 5 minutes from current time
        /// </summary>
        private void Snooze(string pstrPath)
        {
            //get the item at this path
            ItemFolder itmFolder = frmHost.dctItemFolders[pstrPath];

            //update the reminder time to 5 minutes from now
            itmFolder.Metadata.Reminder = DateTime.Now.AddMinutes(5);

            //commit change to file
            itmFolder.SaveMetadataFile();
        }
        #endregion

        #region "Event Handlers"
        /// <summary>
        /// Cell click handler in the reminders datagridview
        /// </summary>
        private void dgvReminders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //ignore if clicking on a column header
            if (e.RowIndex > (-1))
            {
                string strPathSelected = frmHost.strParentPath + "\\" + (string)dgvReminders.Rows[e.RowIndex].Cells[1].Value;

                //Snooze button clicked
                if (e.ColumnIndex == 0)
                {
                    Snooze(strPathSelected);
                    frmHost.RefreshTree();
                    if(frmHost.blnDetailVisible && frmHost.ctlItemFolderDetail.relativePath == (string)dgvReminders.Rows[e.RowIndex].Cells[1].Value)
                    {
                        frmHost.LoadFolderDetail(strPathSelected);
                    }
                    CheckForReminders();
                }

                //Row header clicked
                if (e.ColumnIndex < 0)
                {
                    frmHost.LoadFolderDetail(strPathSelected);
                }
            }
        }

        /// <summary>
        /// Reminders form close header.
        /// Ensure no unsaved changes and then prompt user to confirm snooze all.
        /// </summary>
        private void Reminders_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //do not allow this if there are unsaved changes in the detail pane
                if (frmHost.blnDetailVisible && frmHost.ctlItemFolderDetail.dirty)
                {
                    MessageBox.Show("Please save or discard your changes before snoozing all reminders.");
                    e.Cancel = true;
                    return;
                }

                DialogResult result = MessageBox.Show("Snooze all displayed reminders for 5 minutes?", "Snooze All", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //Snooze all
                    foreach (DataRow drReminder in tblReminders.Rows)
                    {
                        Snooze(frmHost.strParentPath + "\\" + (string)drReminder["Path"]);
                    }
                    frmHost.RefreshTree();

                    //reload detail screen
                    if (frmHost.blnDetailVisible)
                    {
                        frmHost.LoadFolderDetail(frmHost.strParentPath + "\\" + frmHost.ctlItemFolderDetail.relativePath);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion
    }
}
