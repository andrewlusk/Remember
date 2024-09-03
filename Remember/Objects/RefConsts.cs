namespace Remember.Objects
{
    /// <summary>
    /// Static key values object
    /// </summary>
    internal static class RefConsts
    {
        public static readonly string cstrAppDataFolderName = "Remember";
        public static readonly string cstrRSettingsFile = "rSettings.json";
        public static readonly DateTime cdtmHighDate = new DateTime(9998, 12, 31);
        public static readonly string[] castrItemFolderTypes = ["Folder", "Task", "Event", "Info"];
        public static readonly string[] castrFolderInvalidCharacters = ["\\", "/", ":", "*", "?", "\"", "<", ">", "|"];
        public static readonly string cstrDateTimeFormat = " yyyy-MMM-dd HH:mm:ss";
        public static readonly string cstrRmdFile = "_rmd.json";
        public static readonly int cintMaxSetupFolders = 1000;
        public static readonly string cstrUnhandledErrorText = "What have you done?!";
    }
}
