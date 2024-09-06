<h1>Intro</h1>
put a screenshot here when I've got a good image of test data<br/>
<br/>
This is the todo app that I've needed for my entire career as a consultant.  It's not revolutionary, it just meets my particular requirements, which are:<br/><br/>
<ol>
<li>I want to organize units of work and information hierarchically (like in a folder/subfolder tree), so they can be grouped together and live within specific contexts.  I also want to be able to copy or relocate these branches of units to other places within the tree, like moving folders around.</li>
<li>I want the option to give each unit a Due date, a priority score, a Completion date, long text Description for my own notes and status updates, and to attach files and links to each one (eg emails, documentation, links to Jira tickets).</li>
<li>I want to be able to query (filter and sort) the entire hierarchy by any of the abovementioned attributes using SQL-like language, and easily save/recall those queries.</li>
<li>I want to be able to assign a reminder date+time to any unit, and have the app notify me when the time arrives.</li>
<li>I need the solution to be 100% local to my machine.  I work on government systems, which means that SaaS tools are off the table because all the communications, files, data, and deliverables pertaining to my work potentially contain protected information.  I can't upload any of that to random cloud servers because it's government property.</li>
<li>The solution can't require purchasing an external license/key (it needs to be free).</li>
</ol>

<br/>
<h1>What it does</h1><br/>

<ol>
    <li>Imbues a folder in your Windows filesystem, and all its subfolders, with the following attributes:
        <ul style="margin:25px;">
            <li>Type (Folder, Task, Event, Information);</li>
            <li>Start, Due, Reminder, and Completed dates;</li>
            <li>Description;</li>
            <li>Owner;</li>
            <li>Importance and Urgency levels (ie Eisenhower matrix scores); and</li>
            <li>Reminder notification date+time.</li>
        </ul>
    </li>
    <li>Displays your enhanced folder tree as a table, allowing you to query (filter+sort) it by path and/or any combination of the
        above attributes.
    </li>
    <li>Saves and recalls your queries so you can easily retrieve, for example:
        <ul style="margin:25px;">
            <li>all incomplete Tasks assigned to a particular person, ordered by Due date;</li>
            <li>all complete Tasks, ordered by Completed date, to see a chronological log of what has been done;</li>
            <li>a specific piece of Information with a particular word/phrase in its description; or</li>
            <li>any folders within a particular subfolder (or along a particular subfolder path).</li>
        </ul>
    </li>
    <li>Reminds you via notification modal of any Reminder dates that have elapsed.
    </li>
    <li>Responds to changes in your folders made outside the app (ie in Windows Explorer), for example:
        <ul style="margin:25px;">
            <li>placement of files and documents in your folders; and</li>
            <li>relocating/deleting/copying subfolders or files.</li>
        </ul>
    </li>
    <li>Does all of the above 100% locally and 100% for free - no internet connection, account creation, or subscription
        required. If you would like to run Remember on a different machine and access the same data, you can connect
        your root folder to the cloud storage provider of your choice using their native integrations - for example, both Google
        Drive and OneDrive have desktop apps with real-time synchronization to cloud folders.
    </li><br />
</ol>


**Attribution of Icons Used**:
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
- Notes icon by Smashicons: https://www.flaticon.com/free-icon/notes_10741290