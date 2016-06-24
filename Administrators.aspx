<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Administrators.aspx.cs" Inherits="Administrators" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    c

    <title>Northwest Cloud</title>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="leftNav" runat="Server">
    <a href="myMedia.aspx">
        <div id="myVideos">
        </div>
    </a>
    <a href="myGroups_Student.aspx">
        <div id="groups">
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
        Administrators
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <div style="margin-left: 20px">
            <p>
                These accounts have administrative access to the application.
            </p>
            <p>
                <asp:ListBox ID="adminListBox" runat="server" Height="275px"></asp:ListBox>
                <br />
                <asp:Button ID="removeAdminButton" runat="server" Text="Remove Selected"
                    OnClick="removeAdminButton_Click" />
            </p>
            <h2>Add Administrator</h2>
            <p>
                To add a new administrator, enter his or her Northwest login ID below and click 
        submit.
        <br />
                <asp:TextBox ID="usernameTextBox" runat="server" Width="200px"></asp:TextBox>
                <br />
                <asp:Button ID="addAdminButton" runat="server" OnClick="addAdminButton_Click"
                    Text="Submit" />
            </p>
            <p>
                <asp:Label ID="errorLabel" runat="server" ForeColor="Red"></asp:Label>
            </p>
        </div>
    </div>

</asp:Content>
