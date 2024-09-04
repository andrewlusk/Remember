namespace Remember.Objects
{
    /// <summary>
    /// In-memory representation of user settings file
    /// </summary>
    public class UserSettings
    {
        public string RootFolder { get; set; } //the currently set Root folder
        public UserQuery[] userQueries { get; set; } //all saved queries
        public string currentQuery { get; set; } //the query currently loaded

        public void InitializePresetQueries()
        {
            Dictionary<string, string> dctPresetQueries = new Dictionary<string, string>();
            dctPresetQueries.Add("All Tasks", "Type = 'Task'");
            dctPresetQueries.Add("Incomplete Tasks by Due", "Type = 'Task' AND Completed is null ORDER BY Due desc");
            dctPresetQueries.Add("Completed Items Log", "Completed is not null ORDER BY Completed desc");
            dctPresetQueries.Add("Path Contains Text", "Path like '%your text here%'");
            dctPresetQueries.Add("Description Contains Text", "Description like '%your text here%'");

            userQueries = new UserQuery[dctPresetQueries.Count];
            UserQuery uqWorking;
            int counter = 0;
            foreach (KeyValuePair<string, string> kvpPresetQuery in dctPresetQueries)
            {
                uqWorking = new UserQuery();
                uqWorking.queryName = kvpPresetQuery.Key;
                uqWorking.queryString = kvpPresetQuery.Value;
                userQueries[counter] = uqWorking;
                counter++;
            }
        }
    }

    /// <summary>
    /// Instance object of a single query string (name and query)
    /// </summary>
    public class UserQuery
    {
        public string queryName { get; set; }
        public string queryString { get; set; }
    }
}
