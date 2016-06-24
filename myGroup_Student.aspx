<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="myGroup_Student.aspx.cs" Inherits="pages_myGroup_Student" %>
<%@ Register TagPrefix="local" Namespace="NorthwestVideo.Controls" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=.70"/>
    <link href="css/external.css" rel="stylesheet" type="text/css" media="all" />
    <script type="text/javascript" src="js/master.js"></script>
    
    <title>Northwest Cloud</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id = "container">


	<div id = "leftNavigationContainer">
    
		<div id = "navigation">
    	
        	<div id="middlebar" onclick="javascript:showlayer('sm_1')" >
            <ul class="menu">
            <li ><a runat="server" id="sNumber" href="#" >Welcome User </a></li>
            <ul class="submenu" id="sm_1">
            <li><a href="p1.html">Profile</a></li>
            <li><a href="p2.hmtl">Inbox </a></li>
            <li><a href="Login.aspx">Log-out</a></li>
            </ul>
            </ul>
              <div id ="middlebar2"></div>
         </div>
           
             <a href="myVideos.aspx"><div id = "myVideos"></div></a>
             <a href="myGroup_Student.aspx"><div id = "groups"><img src="images/groupsIconBlue.png" alt="groupsIcon" style="float: left" /></div></a>
             
            <a href="UploadMedia.aspx"><div id = "upload"></div></a>
            <a href="Help/index.aspx" target="_new"><div id = "help"></div></a>
            <a href="admin.aspx"><div id = "admin"></div></a>
    
    	</div>
    
    </div>

	<div id = "topNavigationContainer">
	
    
		<div id = "topNavigation">
        
			<div id ="northwestLogo">
            
            </div>
            
            <div id ="searchBar">
            
    
       
        <asp:TextBox ID="searchBox" runat="server" placeholder="Search" OnClick="searchButton_Click" />
        <asp:Button ID="searchButton" runat="server" Text="Search" OnClick="searchButton_Click" />
   
    
    <asp:Label ID="errorLabel" runat="server" ForeColor="Red" />
    <div>
        <local:VideoList ID="videoList1" DisplayVideoOwner="True" EnablePaging="True"
            EnableSorting="False" runat="server" SelectedPage="1" ItemsPerPage="10" PageDisplayCount="10" />
    </div>
            
    
            </div>

		</div>
        
        
        <div id = "fileNavigation">
        
        	<div id = "explorerBar">
            
            </div>
            
        </div>
        

        </div>
        
        <div id = "body1">
        

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
        
        
	</div>
    </form>
</body>
</html>