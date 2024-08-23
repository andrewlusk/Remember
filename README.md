**What it does**:

<ol><li>Imbue a folder in your Windows filesystem, and all its subfolders, with:
<ul style="margin:25px;"><li>Type (Folder, Task, Event, Information)</li><li>Start, Due, Completed dates</li><li>Description</li><li>Owner</li><li>Importance and Urgency levels (ie Eisenhower matrix scores)</li><li>Reminder notificaiton date+time</li>
</ul>
</li>
</ol>

2. Displays your enhanced folder tree as a table, allowing you to query (filter+sort) it by any combination of the above attributes.

3. Saves and recalls your queries so you can easily retrieve, for example:
- all incomplete Tasks assigned to a particular person, ordered by Due date;
- all complete tasks, ordered by completed date, to see a chronological log of what has been done;
- a specific piece of Information with a particular word/phrase in its description; or
- any folders within a particular subfolder (or along a particular subfolder path).

4. Responds to changes in your folders made outside the app (ie in Windows Explorer), for example: 
- placement of files and documents in your folders; and
- relocating/deleting/copying subfolders or files.

5. Does all of the above 100% locally and 100% for free - no internet connection, account creation, or subscription required.  If you would like to run Remember on a different Windows machine and access the same data, you can connect your root folder to the cloud storage provider of your choice using their native integrations (for example, both Google Drive and OneDrive have desktop apps that allow live connections to cloud folders).


**How it works**:

- When you select a root folder, an \_rmd.json file is created in that folder and all its descendant folders; this is where the additional attributes get stored.  
- Clicking on the row header for any folder in the table will open the Detail pane for that folder, where you can view and edit the folder's attributes.  
- With every folder creation/update, root folder change, or manual refresh, Remember crawls the folder tree and updates the table with all the up-to-date attributes.
- Your last selected root folder and all your saved queries live in C:\\Users\\{UserName}\\AppData\\Local\\Remember\\rSettings.json. 


**Icons attribution**:
- Pyramid icon by Freepik: https://www.flaticon.com/free-icon/pyramid_1903915
- Refresh icon by Maxim Baskinski: https://www.flaticon.com/free-icon/arrow_9497023
- (red X) Delete icon by Pixelmeetup: https://www.flaticon.com/free-icon/clear_1632708
- (black X) Delete icon by Radhe Icon: https://www.flaticon.com/free-icon/clear_9428870
- Open folder icon by KMG Design: https://www.flaticon.com/free-icon/open-folder_3748664
- Diskette (save) icon by Freepik: https://www.flaticon.com/free-icon/disk_2493373
- Up arrow icon by Pixel perfect:https://www.flaticon.com/free-icon/up-arrow_626075
- Now (clock with checkmark) by Freepik: https://www.flaticon.com/free-icon/on-time_4474370
- Midnight clock icon by Freepik: https://www.flaticon.com/free-icon/night_2972569
- Copy icon by Saepul Nahwan: https://www.flaticon.com/free-icon/copy_10628587
- Paste icon by Smashicons: https://www.flaticon.com/free-icon/paste_1151209
