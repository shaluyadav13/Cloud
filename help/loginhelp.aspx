<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="loginhelp.aspx.cs" Inherits="help_loginhelp" %>

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
      
            <div id="help"><img src="images/helpIconBlue.png" alt="upload" style="float: left" /></div>
         
    </a>

    <asp:Panel ID="ad" runat="server">
        <a href="../admin.aspx">
          
        </a>
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
            > Logging In 
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </p>


        <div id="body1" style="margin-left:25px;margin-top:10px; width:700px;">

                <h3>How to log into Northwest Cloud</h3>

                <p>To log into Northwest Cloud is very simple, just follow the steps below.</p>

                
                <table>
                    <tr>
                        <td width="50%" align="center">
                            <img src="../help/helpImages/loggingInPicOne.png" style="margin-bottom: 10px;" width="90%"/>
                        </td>
                        <td>
                            Open up any web browser or create another tab on the current one, and navigate to <a href="http://cloud.nwmissouri.edu/">http://cloud.nwmissouri.edu/</a>.
                            <br />
                            <br />
                            Enter your Northwest network username and password in the login form just like you would for your campus or email login. Then just click “Sign in”.
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                            <img src="../help/helpImages/loggingInPicTwo.png" style="margin-bottom: 10px;" width="90%"/>
                        </td>
                        <td>
                            After you login, the “My Media” page will load up and you are all set to go!
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                            <img src="../help/helpImages/loggingInPicThree.png" style="margin-bottom:10px;" width="90%"/>
                        </td>
                        <td>
                            To logout, click the Log Out button on the bottom of the menu.
                        </td>
                    </tr>
                </table>

                <p>
                </p>
            </div>







      
    </div>

</asp:Content>


