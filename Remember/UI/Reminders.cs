using Remember.Objects;
using System.Data;
using System.Text.Json;

namespace Remember.UI
{
    public partial class Reminders : Form
    {
        Host frmHost;
        private DataTable tblReminders = new DataTable();
        public List<string> remindItems = new List<string>();

        public Reminders(Host pfrmHost)
        {
            InitializeComponent();
            frmHost = pfrmHost;
            InitializeTable();
        }

        public void InitializeTable()
        {
            tblReminders.Columns.Add("Path", typeof(string));
            dgvReminders.DataSource = tblReminders;
            DataGridViewButtonColumn colButton = new DataGridViewButtonColumn();
            colButton.HeaderText = "Snooze";
            colButton.Text = "Snooze";
            colButton.Name = "Snooze";
            colButton.UseColumnTextForButtonValue = true;
            dgvReminders.Columns.Add(colButton);
            dgvReminders.CellClick += dgvReminders_CellClick;
        }

        public void CheckForReminders()
        {
            if(frmHost.ModalLock == false)
            {
                remindItems.Clear();
                //scan all folders visible in the table for elapsed Reminder dates
                DataView tbvItemsToRemind = new DataView((DataTable)frmHost.dgvFolders.DataSource);
                string strCurrentTime = DateTime.Now.ToString(RefConsts.cstrDateTimeFormat.Substring(1));
                tbvItemsToRemind.RowFilter = $"Reminder < '{strCurrentTime}'";
                if (tbvItemsToRemind.Count > 0)
                {
                    for (int i = 0; i < tbvItemsToRemind.Count; i++)
                    {
                        string strItemPath = (string)tbvItemsToRemind[i]["Path"];
                        //ensure the item is flagged in the reminders modal
                        if (remindItems.Contains(strItemPath) == false) { remindItems.Add(strItemPath); }
                    }
                }

                //DataView object no longer needed
                tbvItemsToRemind.Dispose();

                if (remindItems.Count > 0)
                {
                    RefreshDisplayedReminders();
                    if (!Visible)
                    {
                        StartPosition = FormStartPosition.Manual;
                        Left = frmHost.intLeft + 300;
                        Top = frmHost.intTop + 300;
                        Show();
                    }
                }
                else if (remindItems.Count == 0 && dgvReminders.RowCount > 0)
                {
                    RefreshDisplayedReminders();
                }
                else
                {
                    Visible = false;
                }
            }
        }

        public void RefreshDisplayedReminders()
        {
            //cache current reminders
            List<string> lstExistingReminders = new List<string>();
            foreach (DataRow dr in tblReminders.Rows)
            {
                lstExistingReminders.Add((string)dr["Path"]);
            }

            tblReminders.Rows.Clear();
            foreach (string strItem in remindItems)
            {
                DataRow drRemindItem = tblReminders.NewRow();
                drRemindItem["Path"] = strItem;
                tblReminders.Rows.Add(drRemindItem);
            }
            dgvReminders.DataSource = tblReminders;
            dgvReminders.AutoResizeColumns();

            //determine whether to Activate the reminders form:
            //must be reminding the user of something new
            bool blnNewReminders = false;

            //compare current reminders to previous ones
            foreach (DataRow dr in tblReminders.Rows)
            {
                if (lstExistingReminders.Contains((string)dr["Path"]) == false)
                {
                    blnNewReminders = true;
                }
            }

            if (blnNewReminders)
            {
                WindowState = FormWindowState.Normal;
                TopMost = true;
                Activate();
            }
        }

        private void dgvReminders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //did we click a snooze button?
            if (e.ColumnIndex == 0 && e.RowIndex > (-1))
            {
                //get item path
                string strRelativePathSnoozed = (string)dgvReminders.Rows[e.RowIndex].Cells[1].Value;
                string strPathSnoozed = frmHost.strParentPath + "\\" + strRelativePathSnoozed;

                //get the item at this path (in memory)
                ItemFolder itmFolder = frmHost.dctItemFolders[strPathSnoozed];

                //update the reminder time to 5 minutes from now
                itmFolder.Metadata.Reminder = DateTime.Now.AddMinutes(5);

                //commit change to file
                string jsnUpdatedMdFile = JsonSerializer.Serialize(itmFolder.Metadata);
                File.WriteAllText(itmFolder.Path + "\\" + RefConsts.cstrRmdFile, jsnUpdatedMdFile);

                //refresh the table and the detail pane
                frmHost.RefreshTree();
                frmHost.LoadFolderDetail(strPathSnoozed);

                //refresh reminders
                CheckForReminders();
            }

            //did we click a row header?
            if(e.ColumnIndex < 0 && e.RowIndex > (-1))
            {
                //get item path
                string strRelativePathSelected = (string)dgvReminders.Rows[e.RowIndex].Cells[1].Value;
                string strPathSelected = frmHost.strParentPath + "\\" + strRelativePathSelected;
                frmHost.LoadFolderDetail(strPathSelected);
            }
        }
    }
}
