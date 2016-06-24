<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myGroup_Students.aspx.cs" Inherits="pages_myGroup_Student" MasterPageFile="~/MasterPage.master" %>

<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">

    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70" />

    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <link href='http://fonts.googleapis.com/css?family=Lato' rel='stylesheet' type='text/css' />
    <script type="text/javascript" src="js/master.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="explorerBar">
        <div class="imagePlacement">
            <img src="images/folderIcon.png" alt="folerIcon" />
        </div>
        <div class="fontPlacement">
            <a href="myGroup_Students.aspx" style="text-decoration: none;">My Groups </a>>
            <asp:Label ID="lblgroupname" runat="server"></asp:Label>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="body2">

        <h2>
            <asp:Label ID="welcomeNote" runat="server" />
        </h2>
        <br />


        <div>
            <asp:Label ID="MyGroupsHeader" runat="server" />

            <asp:Label ID="Dispalylist" runat="server" />

            <local:VideoList ID="videoList" runat="server" DisplayVideoOwner="True" EnablePaging="True"
                EnableSorting="True" ItemsPerPage="10" PageDisplayCount="10" SelectedPage="1" />
        </div>
    </div>
</asp:Content>
