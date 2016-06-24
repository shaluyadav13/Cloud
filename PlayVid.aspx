<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlayVid.aspx.cs" Inherits="PlayVid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://vjs.zencdn.net/4.5/video-js.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <video id="videoTag" class="video-js vjs-default-skin" controls="controls"
             preload="auto" width="640" height="264" poster="null"
             data-setup="{}" runat="server">
             This video has not been converted yet.
             <source type="video/mp4" id="source1" runat="server" />
         </video>
         <br />
         <a id="transcriptLink" runat="server" target="_blank">Transcript</a>
    </div>
    </form>
</body>
</html>
