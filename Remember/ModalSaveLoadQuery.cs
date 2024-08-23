using System.Text.Json;

namespace Remember
{
    /// <summary>
    /// This is the modal screen that opens when saving or loading query strings.
    /// </summary>
    public partial class ModalSaveLoadQuery : Form
    {
        #region "Properties"
        Host frmHost; // for referencing/accessing main UI
        bool blnSave = false;
        bool blnLoad = false;
        #endregion

        #region "Constructor"
        public ModalSaveLoadQuery(Host pfrmHost, bool pblnSave, bool pblnLoad)
        {
            InitializeComponent();
            frmHost = pfrmHost;

            //validate bool parms
            if (pblnSave && pblnLoad)
            { throw new Exception("Both pblnSave and pblnLoad passed in as true"); }
            else if (!pblnSave && !pblnLoad) { throw new Exception("Both pblnSave and pblnLoad passed in as false"); }

            blnSave = pblnSave;
            blnLoad = pblnLoad;

            if (frmHost.userSettings.userQueries.Length > 0)
            {
                //prepop combo box with existing queries from settings
                foreach (UserQuery qry in frmHost.userSettings.userQueries)
                { cmbQueryNames.Items.Add(qry.queryName); }
            }

            if (pblnSave)
            {
                btnSaveLoad.Text = "Save";
                cmbQueryNames.DropDownStyle = ComboBoxStyle.DropDown;
            }
            if (pblnLoad)
            {
                btnSaveLoad.Text = "Load";
                cmbQueryNames.DropDownStyle = ComboBoxStyle.DropDownList;
                btnSaveLoad.Visible = false; //until they select one from the dropdown
            }
        }
        #endregion

        #region "Event Handlers"
        /// <summary>
        /// Click handler for Save/Load button;
        /// (When saving) Update settings + settings file, or
        /// (When loading) Load the selected query string into the main UI and refresh the tree.
        /// </summary>
        private void btnSaveLoad_Click(object sender, EventArgs e)
        {
            if (blnSave)
            {
                //validate text is present
                if(cmbQueryNames.Text == "")
                {
                    MessageBox.Show(
                    text: "Cannot save query.  You must provide a (non-blank) query name.",
                    caption: "Failed to Save Query",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                    return;
                }
                
                //check if the query name already exists in the settings
                bool blnNewQuery = true;

                foreach (UserQuery qry in frmHost.userSettings.userQueries)
                {
                    if (blnNewQuery == true && qry.queryName == cmbQueryNames.Text)
                    {
                        blnNewQuery = false;
                        //confirm and update the existing setting

                        DialogResult result = MessageBox.Show(
                            text: $"Are you sure you want to overwrite query '{cmbQueryNames.Text}'?",
                            caption: "Overwrite Query",
                            buttons: MessageBoxButtons.YesNoCancel,
                            icon: MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            qry.queryString = frmHost.strQueryString;
                        }
                        else { return; }

                    }
                }

                if (blnNewQuery)
                {
                    ////Determine number of queries in current userSettings
                    int intQueryCount = frmHost.userSettings.userQueries.Length;

                    UserQuery[] newQueries = new UserQuery[intQueryCount + 1];

                    //copy old queries to new array
                    for (int i = 0; i < intQueryCount; i++)
                    {
                        newQueries[i] = frmHost.userSettings.userQueries[i];
                    }

                    //add this query to the array of queries in the settings
                    UserQuery qryAdd = new UserQuery();
                    qryAdd.queryName = cmbQueryNames.Text;
                    qryAdd.queryString = frmHost.strQueryString;
                    newQueries[intQueryCount] = qryAdd;
                    frmHost.userSettings.userQueries = newQueries;
                }

                //save settings file with new query
                string jsnSettings = JsonSerializer.Serialize(frmHost.userSettings);
                File.WriteAllText(frmHost.userSettingsFolder + "\\" + RefConsts.cstrRSettingsFile, jsnSettings);
            }
            if (blnLoad)
            {
                string strQueryStringToLoad = "";
                foreach (UserQuery qry in frmHost.userSettings.userQueries)
                {
                    if (strQueryStringToLoad == "" && qry.queryName == cmbQueryNames.Text)
                    {
                        strQueryStringToLoad = qry.queryString;
                    }
                }
                frmHost.LoadQueryStringFromSettings(strQueryStringToLoad);
            }
            this.Close();
        }

        /// <summary>
        /// Selection handler for dropdown of saved, named queries;
        /// determine visibility of load, delete buttons
        /// </summary>
        private void cmbQueryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbQueryNames.Items.Count > 0)
            {
                //control whether load button is visible
                if (blnLoad && cmbQueryNames.SelectedIndex != -1 && cmbQueryNames.Text != "")
                {
                    btnSaveLoad.Visible = true;
                }

                //control whether delete button is visible
                if (cmbQueryNames.SelectedIndex != -1 && cmbQueryNames.Text != "")
                {
                    btnDeleteQuery.Visible = true;
                }
            }
        }

        /// <summary>
        /// Delete button click handler;
        /// prompt user with confirm modal, then update user settings and settings file
        /// </summary>
        private void btnDeleteQuery_Click(object sender, EventArgs e)
        {
            //modal confirmation
            DialogResult result = MessageBox.Show(
                            text: $"Are you sure you want to delete query '{cmbQueryNames.Text}'?",
                            caption: "Delete Query",
                            buttons: MessageBoxButtons.YesNoCancel,
                            icon: MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int intQueryCount = frmHost.userSettings.userQueries.Length;

                UserQuery[] newQueries = new UserQuery[intQueryCount - 1];
                int intNewQueryPosition = 0;

                //rebuild the queries array without the one being deleted here
                for (int i = 0; i < intQueryCount; i++)
                {
                    if (frmHost.userSettings.userQueries[i].queryName != cmbQueryNames.Text)
                    {
                        newQueries[intNewQueryPosition] = frmHost.userSettings.userQueries[i];
                        intNewQueryPosition++;
                    }
                }

                //update the in-memory queries with the new array
                frmHost.userSettings.userQueries = newQueries;

                //update the jsn file
                string jsnSettings = JsonSerializer.Serialize(frmHost.userSettings);
                File.WriteAllText(frmHost.userSettingsFolder + "\\" + RefConsts.cstrRSettingsFile, jsnSettings);

                //repopulate the combo box with the new options
                cmbQueryNames.Items.Clear();
                foreach (UserQuery qry in frmHost.userSettings.userQueries)
                { cmbQueryNames.Items.Add(qry.queryName); }

                //hide actions if nothing is selected
                btnSaveLoad.Visible = (cmbQueryNames.SelectedIndex != -1);
                btnDeleteQuery.Visible = (cmbQueryNames.SelectedIndex != -1);
            }
        }
        #endregion
    }
}
