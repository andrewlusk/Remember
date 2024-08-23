namespace Remember
{
    /// <summary>
    /// Instance object for folder metadata attributes.
    /// Every folder has a _rmd.json file with these attributes;
    /// this object is the in-memory representation.
    /// </summary>
    public class ItemFolderMetadata
    {
        public string Type { get; set; } = "Folder";
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; } = DateTime.Now;
        public DateTime Start { get; set; } = RefConsts.cdtmHighDate;
        public DateTime Completed { get; set; } = RefConsts.cdtmHighDate;
        public DateTime Due { get; set; } = RefConsts.cdtmHighDate;
        public DateTime Reminder { get; set; } = DateTime.MinValue;
        public string Description { get; set; } = "";
        public string Owner { get; set; } = "";
        public int Importance { get; set; } = 0;
        public int Urgency { get; set; } = 0;
    }
}
