<%@ Page Language="C#" AutoEventWireup="true" CodeFile="status.aspx.cs" Inherits="status" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>
     <link href="css/status.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#keywords').tablesorter();
        });
    </script>
   
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
        Application Status
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">
        <br />
        <p>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
        </p>


        <div id="wrapper">
        <h1>Application Status</h1>

        <table id="keywords" cellspacing="0" cellpadding="0">
            <thead>
                <tr>
                    <th><span style="color:white">Media</span></th>
                    <th><span style="color:white">Users</span></th>
                    <th><span style="color:white">Items</span></th>
                    <th><span style="color:white">Total Size</span></th>
                    <th><span style="color:white">Average Items per User</span></th>
                    <th><span style="color:white">Average Item Size</span></th>
                    <th><span style="color:white">Total Views</span></th>
                </tr>
            </thead>
            <tbody>
               
                <tr>
                    <td class="lalign">Audio</td>

                    <td><asp:Label ID="audioActiveUsers" runat="server"></asp:Label></td>
                    <td><asp:Label ID="audioItems" runat="server"></asp:Label></td>
                    <td><asp:Label ID="audioSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="audioAverageItems" runat="server"></asp:Label></td>
                     <td><asp:Label ID="audioAverageSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="audioViews" runat="server"></asp:Label></td>
                </tr>
               
                <tr>
                    <td class="lalign">Documents</td>
                    <td><asp:Label ID="documentsActiveUsers" runat="server"></asp:Label></td>
                    <td><asp:Label ID="documentsItems" runat="server"></asp:Label></td>
                    <td><asp:Label ID="documentsSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="documentsAverageItems" runat="server"></asp:Label></td>
                     <td><asp:Label ID="documentsAverageSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="documentsViews" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lalign">Images</td>
                   <td><asp:Label ID="imagesActiveUsers" runat="server"></asp:Label></td>
                    <td><asp:Label ID="imagesItems" runat="server"></asp:Label></td>
                    <td><asp:Label ID="imagesSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="imagesAverageItems" runat="server"></asp:Label></td>
                     <td><asp:Label ID="imagesAverageSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="imagesViews" runat="server"></asp:Label></td>
                </tr>
                 <tr>
                    <td class="lalign">Video</td>
                  <td><asp:Label ID="videoActiveUsers" runat="server"></asp:Label></td>
                    <td><asp:Label ID="videoItems" runat="server"></asp:Label></td>
                    <td><asp:Label ID="videoSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="videoAverageItems" runat="server"></asp:Label></td>
                     <td><asp:Label ID="videoAverageSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="videoViews" runat="server"></asp:Label></td>
                </tr>
                 <tr>
                    <td class="lalign">Website</td>
                    <td><asp:Label ID="websiteActiveUsers" runat="server"></asp:Label></td>
                    <td><asp:Label ID="websiteItems" runat="server"></asp:Label></td>
                    <td><asp:Label ID="websiteSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="websiteAverageItems" runat="server"></asp:Label></td>
                     <td><asp:Label ID="websiteAverageSize" runat="server"></asp:Label></td>
                    <td><asp:Label ID="websiteViews" runat="server"></asp:Label></td>
                </tr>

                <tr>
                    <td class="lalign">Total</td>
                    <td><asp:Label ID="totalActiveUsers" runat="server" Font-Bold="true"></asp:Label></td>
                    <td><asp:Label ID="totalItems" runat="server" Font-Bold="true"></asp:Label></td>
                    <td><asp:Label ID="totalSizelbl" runat="server" Font-Bold="true"></asp:Label></td>
                    <td><asp:Label ID="totalAverageItems" runat="server" Text =" -- " Font-Bold="true"></asp:Label></td>
                     <td><asp:Label ID="totalAverageSize" runat="server" Text =" -- " Font-Bold="true"></asp:Label></td>
                    <td><asp:Label ID="totalViews" runat="server" Font-Bold="true"></asp:Label></td>
                </tr>
            </tbody>
        </table>
        <br />

        <h1>Server Status</h1>
        <asp:Label ID="freeDiskSpace" style="font-size: 1.2em; font-weight: bold" runat="server"></asp:Label>

           
        
            
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
