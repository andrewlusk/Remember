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
