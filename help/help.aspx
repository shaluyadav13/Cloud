<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="help_help" %>

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
            <div id="admin">
              
            </div>
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
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </p>


     <div id="Div2">
        <asp:Panel ID="pnladmin" runat="server">
            <a href="howtouse.aspx">
                <p>Introduction</p>
            </a>
            <a href="questions.aspx">
                <p>Frequently Asked Questions (FAQ)</p>
            </a>
            <a href="loginhelp.aspx">
                <p>Logging In </p>
            </a>
            <a href="uploadvideo.aspx">
                <p>Uploading  Media</p>
            </a>
            <a href="publishmedia.aspx">
                <p>Publishing Media </p>
            </a>
           <a href="editmedia.aspx">
                <p>Editing Media </p>
            </a>
            <a href="groups.aspx">
                <p>Groups
                </p>
            </a>
          
        </asp:Panel>
       
    </div>








        <%--  <h3>Web Counter</h3>
        <p>
            Sitewide hit statistics are tracked through the CITE web counter. This single counter is for the entire site
        (except the help pages). To view detailed statistics, click on the counter image below.
        </p>
        <a target="_new" href="http://cite.nwmissouri.edu/WebCounter/PageDetails.aspx?id=72">
            <img src="http://cite.nwmissouri.edu/webcounter/72n/Black/White/Rounded.png"
                alt="webCounter" border="none" />
        </a>
        <p>&nbsp;</p>--%>
    </div>

</asp:Content>
