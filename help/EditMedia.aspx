<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditMedia.aspx.cs" Inherits="help_EditMedia" %>

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
            > Editing Media 
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </p>
        <div id="body1" style="margin-left: 25px; margin-top: 5px; width:900px;">


            <h3>Editing the Media File</h3>
            <table>
                <tr>
                    <td>
                        <img src="helpImages/editmedia.png" />
                    </td>
                    <td>
                        <p style="margin-left: 55px; margin-bottom: 300px;">Click the ‘Edit’ link located at the bottom to edit the media information.  </p>
                    </td>
                </tr>
                <tr>
                    <td >
                        <img src="helpImages/editmedia1.png" />
                    </td>
                    <td >
                        <p style="margin-left: 55px; margin-top: -386px;">
                            <ol>
                                <li>The title and description can be tweaked.  The group can be changed. 
                                </li>
                                <li>The original media file can be replaced, as well as either add or replace a transcript file. 
                               
                                </li>
                                <li>When replacing the media, the original URL given to the media will stay the same for the new replacement file, so it will not need to be re-linked or embedded.
                                </li>
                                <li>
                                     You can also tweak the Author and Auto-removal date.
                                </li>
                                <li>Click Submit to save the changes.  New replacement files will have to upload and be converted before they can be accessed.
                                </li>
                            </ol>



                        </p>
                       
                    </td>
                </tr>
            </table>
            <%--    <p >
                    You may edit the any of the information for any media file you want at any time, you may also delete the video, but exchanging the file for another is not possible. For that, you must upload it separately.
                </p>
                <ol>
                    <li>The edit a media file, navigate your way from the “My Media” Tab to the where the file is located.
                    </li>
                    <li>Click on the “Edit” button under the Title and description of the file.
                    </li>
                    <li>Use the form to change the information to what you like.
                    </li>
                    <li>You are also able to add or remove an auto-removal date if you wish. Click the textbox and a calendar popup will help you select a date. If you select a date, this media file will automatically be deleted from the Northwest Cloud on that date.
                    </li>
                    <li>Click “Submit” to save your changes or “Cancel” if you changed your mind. In either case, you will be sent back to the “My Media” page.
                    </li>
                </ol>--%>

            <br />

            <%--      <h3>Deleting the Media File</h3>
            <ol>
                <li>The delete the media file, navigate your way from the “My Media” tab to where the file is located.              </li>
                <li>Then Click the “Delete” button under the title and description of the file, right next to the “Edit” Button.
                </li>
                <li>You will be prompted if you are certain that you would like to delete the file.
                </li>
                <li>Click “Yes” if you wish to delete the file and “No” if you changed your mind.
                </li>
            </ol>--%>
        </div>

    </div>

</asp:Content>

