using System.Text.Json;

namespace Remember
{
    /// <summary>
    /// Instance representation of an item folder: path, metadata, child folders and child files
    /// </summary>
    public class ItemFolder
    {
        #region "Properties
        public string Path { get; set; }
        public ItemFolderMetadata Metadata { get; set; }
        public List<string> ChildFolders = new List<string>();
        public List<string> ChildFiles = new List<string>();
        #endregion

        #region "Constructor"
        public ItemFolder(string pstrPath)
        {
            Path = pstrPath;

            //check if there is a metadata file
            if (File.Exists(pstrPath + "\\" + RefConsts.cstrRmdFile))
            {
                //open file into a FolderMetadata object
                string jsonString = File.ReadAllText(Path + "\\" + RefConsts.cstrRmdFile);
                Metadata = JsonSerializer.Deserialize<ItemFolderMetadata>(jsonString);
                
            }
            else
            {
                //no rmd file; create it
                Metadata = new ItemFolderMetadata();

                string strParentPath = Directory.GetParent(Path)!.FullName;
                if (File.Exists(strParentPath + "/" + RefConsts.cstrRmdFile))
                {
                    // parent folder has rmd; prepopulate key attributes from parent
                    string jsonString =  File.ReadAllText(strParentPath + "/" + RefConsts.cstrRmdFile);
                    ItemFolderMetadata parentMD = JsonSerializer.Deserialize<ItemFolderMetadata>(jsonString);
                    if (parentMD != null)
                    {
                        Metadata.Start = parentMD.Start;
                        Metadata.Due = parentMD.Due;
                        Metadata.Importance = parentMD.Importance;
                        Metadata.Urgency = parentMD.Urgency;
                        Metadata.Owner = parentMD.Owner;
                    }
                }
                else
                //parent folder does not have rmd; use default values
                {
                    Metadata.Start = RefConsts.cdtmHighDate;
                    Metadata.Due = RefConsts.cdtmHighDate;
                    Metadata.Importance = 0;
                    Metadata.Urgency = 0;
                }
                Metadata.Type = "Folder";
                Metadata.Created = DateTime.Now;
                Metadata.Modified = DateTime.Now;
                Metadata.Description = "";
                Metadata.Reminder = RefConsts.cdtmHighDate;

                //save rmd file in folder
                string jsnNewrmd = JsonSerializer.Serialize(Metadata);
                File.WriteAllText(Path + "\\" + RefConsts.cstrRmdFile, jsnNewrmd);
            
            }

            //get child folders
            string[] arrChildFolders = Directory.GetDirectories(Path);
            foreach (string strChildFolder in arrChildFolders) { ChildFolders.Add(strChildFolder); }

            //get child files (ignore metadata file)
            string[] arrChildFiles = Directory.GetFiles(Path);
            foreach (string strChildFile in arrChildFiles) { if (strChildFile != (Path + "\\" + RefConsts.cstrRmdFile)) { ChildFiles.Add(strChildFile); } }
        }
        #endregion
    }
}
