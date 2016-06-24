<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentGroups.aspx.cs" Inherits="StudentGroups" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>

    <title>Northwest Cloud</title>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups" >
        </div>
    </a>
    <a href="UploadMedia.aspx">
        <div id="upload"></div>
    </a>
    <a href="Help/help.aspx">
        <div id="help"></div>
    </a>
    <asp:Panel ID="ad" runat="server">
        <a href="admin.aspx">
            <div id="admin">
                <img src="images/adminIconBlue.png" alt="myVideosIcon" style="float: left" />
            </div>
        </a>
    </asp:Panel>
        <a href="Logout.aspx">
        <div id="logout">
        </div>
    </a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="navText">
        Group Management
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <p>
                Group members may be given temporary Northwest Media privileges by adding them into an authorized group.
        Each group has a start date and end date during which its members may upload media.
            </p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
            <ul class="formList">
                <li>
                    <h3>Groups</h3>
                </li>
                <li>
                    <asp:CheckBox ID="showAllCheckBox" runat="server" Text="Show All"
                        AutoPostBack="True" OnCheckedChanged="showAllCheckBox_CheckedChanged" />
                </li>
                <li>
                    <asp:ListBox ID="studentGroupsListBox" runat="server" Height="215px" />
                    <asp:Label ID="noGroupsLabel" runat="server" Text="No Groups" />
                </li>
                <li>
                    <asp:Button ID="newGroupButton" runat="server" Text="New" CssClass="button"
                        OnClick="newGroupButton_Click" />
                    <asp:Button ID="editGroupButton" runat="server" Text="Edit" CssClass="button"
                        OnClick="editGroupButton_Click" />
                    <asp:Button ID="deleteGroupButton" runat="server" Text="Delete"
                        CssClass="button" OnClick="deleteGroupButton_Click"
                        OnClientClick="return confirm('You are about to delete all the videos in the group. Are you sure you want to permanently delete this student group?');" />
                </li>
            </ul>
        </div>

    </div>
</asp:Content>
