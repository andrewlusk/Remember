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

        /// <summary>
        /// Provide some sample queries out of the box (called when initially creating settings file)
        /// </summary>
        public void InitializePresetQueries()
        {
            Dictionary<string, string> dctPresetQueries = new Dictionary<string, string>
            {
                { "Path alphabetical", "Order by Path"},
                { "Todo Tasks", "Type = 'Task' and Completed is null order by Due asc" },
                { "Todo Tasks w/ Due Date", "Type = 'Task' and Completed is null and Due is not null order by Due asc" },
                { "Current", "Completed is null order by Path asc" },
                { "Completed Items Log", "Completed is not null order by Completed desc" },
                { "Info Only", "Type = 'Info' order by Path asc" },
                { "Path search", "Path like '%your text here%'" }
            };

            userQueries = new UserQuery[dctPresetQueries.Count];
            UserQuery uqWorking;
            int intCountQueries = 0;

            foreach (KeyValuePair<string, string> kvpPresetQuery in dctPresetQueries)
            {
                uqWorking = new UserQuery();
                uqWorking.queryName = kvpPresetQuery.Key;
                uqWorking.queryString = kvpPresetQuery.Value;
                userQueries[intCountQueries] = uqWorking;
                intCountQueries++;
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
