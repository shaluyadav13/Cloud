<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Groups.aspx.cs" Inherits="help_Groups" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

    <style>
        .container .content {
            display: none;
            padding: 5px;
        }
    </style>


    <title>Northwest Cloud</title>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="../myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="../myGroups_Student.aspx">
        <div id="groups">
        </div>
    </a>
    <a href="../UploadMedia.aspx">
        <div id="upload"></div>
    </a>

    <a href="help.aspx">

        <div id="help">
            <img src="images/helpIconBlue.png" alt="upload" style="float: left" />
        </div>

    </a>


    <asp:Panel ID="ad" runat="server">
        <a href="../admin.aspx"></a>
    </asp:Panel>
    <a href="../Logout.aspx">
        <div id="logout">
        </div>
    </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <asp:Image ID="Image1" ImageUrl="~/images/folderIcon.png" runat="server" />
        </div>
        <div class="fontPlacement">
            <a href="help.aspx" style="text-decoration: none;">Help</a>
            > Groups
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <p>
        <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
    </p>

    <div id="body1" style="margin-left: 25px; margin-top: 5px; overflow-y: auto; width: 800px;">
        <p><b>What is a Group? </b>A group can be created by a faculty or staff member to allow uploading and sharing of media.  A group always has a group owner.  The owner enrolls the group members and sets the start and end dates for the group.  Groups can be created for departments or among peers to share media.  Groups can also be made for course work.  Students do not have access to Northwest Cloud unless they are a member of a group created by a faculty or staff member.  When they are in a group, they login with their Northwest network username and password.</p>
        <table>
            <tr>
                <td width="50%" align="center">
                    <img src="helpImages/groups.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>A faculty member can create a group for a course to allow students to upload any of the media types.  Use it as a way for student to turn in a multimedia assignments.  Or use it as a way for students to share multimedia assignments amongst each other.
To create a group go to 'Admin' and select 'Create&Edit Groups'.

                </td>
            </tr>
            <tr>
                <td width="50%" align="center">
                    <img src="helpImages/groupManagement2.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>Groups listed here can be edited and new members added.  Or click 'New' to create a new group.
                </td>
            </tr>
            <tr>
                <td width="50%" align="center">
                    <img src="helpImages/groupEdit.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>Fill out the form to create a new group.  Give it the course number for the name if this is for a specific course like the online speech course 29-102-02.  Your username will automatically fill in the Owner ID box.  A description of the group will help identify the group's purpose.  Click the arrow in front of 'Add/Edit Group Members' line, to see the dialog to add members.  Add a start and end date for the group.  Please Note: The group will be automatically deleted at the end date along with its media.  Finally select the 'Share With' status.  Selecting the Group owner only, will allow only the owner – you - to view the media uploaded by the group.  Selecting 'All group members' will allow all the members to view the media uploaded by the other group members.
                </td>
            </tr>

            <tr>
                <td width="50%" align="center">
                    <img src="helpImages/addGroupMembers.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>By clicking the arrow in front of 'Add/Edit Group Members', the dialog to add members shows.  There are two ways to add members.  One – type the username in the first textbox and click submit.  Or two – do a batch upload by submitting a text file of usernames with each username on a separate line.  Members have to be Northwest students or employees to belong to a group. 
                        <br />
                    <br />
                    To create a batch file for a course, copy and paste student usernames into an excel file column and then 'save as' a 'tab delimited' text file.  The file extension will be .txt.   Upload this text file.  The new names will appear below the Current Members label.  Click 'Save' at the bottom of the page to save the group setup.

                </td>
            </tr>
            <tr>
                <td width="50%" align="center">
                    <img src="helpImages/groupsIngroupsOwned.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>Once a group is created and members enrolled, then the owner will see it listed under the Groups section as 'Groups Owned.'  One will also see a listing of what groups they are a member of under the 'Groups In' label.
                </td>
            </tr>






        </table>
        <p>It is important to remember that the group and all of its media will be automatically deleted on the submitted end date for the group.  You can save a media file to your own profile if a student has given you permission to keep and use their media that they created and uploaded.  Keep a copy of this agreements under the 'My Media' Documents area.  This needs to be done before the end date for the group.  </p>
        <table>
           
              <tr>
                <td width="50%" align="center">
                    <img src="helpImages/claim1.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>To claim a media piece as yours, go to the media's edit link.
                </td>
            </tr>
            <tr>
                <td width="50%" align="center">
                    <img src="helpImages/claim2.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>The edit dialog window will open.  This is where you can edit the text content for the media.  The claim video button is at the bottom.
                </td>
            </tr>
            <tr>
                <td width="50%" align="center">
                    <img src="helpImages/claim3.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>Click the 'Claim Video' button to make you the owner of the video.
                </td>
            </tr>
            <tr>
                <td width="50%" align="center">
                    <img src="helpImages/claim5.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>The message 'Claimed Successfully'will appear at the finish.  
                </td>
            </tr>
            <tr>
                <td width="50%" align="center">
                    <img src="helpImages/claim4.png" style="margin-bottom: 10px;" width="90%" />
                </td>
                <td>This video will now appear under your 'My Media'.
                </td>
            </tr>
        </table>
       </div>
</asp:Content>


