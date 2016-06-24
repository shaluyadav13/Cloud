<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SwitchLogin.aspx.cs" Inherits="SwitchLogin" MasterPageFile="~/MasterPage.master" %>


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
        Switch Login
    </div>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="body2">
        <br />
        <p>
            This is an administrative feature that allows admins to log in as any other user,
            as long as that user meets the requirements to login to Northwest Video. The user's
            password is not required.
        </p>
        <p>&nbsp;</p>
        <asp:Panel ID="p" runat="server" DefaultButton="loginButton">
        <ul class="formList">
            <li>
                <span class="formLabel">Network ID</span>
                <asp:TextBox ID="usernameTextBox" autofocus runat="server" />
            </li>
            <li>
                <span class="formLabel">&nbsp;</span>
                <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click" />
            </li>
            <li>
                <span class="formLabel">&nbsp;</span>
                <asp:Label ID="errorLabel" runat="server" ForeColor="#ff0000" Text="" />
            </li>
        </ul>
        </asp:Panel>
    </div>
</asp:Content>
