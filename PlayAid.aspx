<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlayAid.aspx.cs" Inherits="PlayAid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <audio id="audioTag" controls="controls"
                preload="auto" runat="server" src="null">
            </audio>
            <br />
            <a id="transcriptLink" runat="server" target="_blank">Transcript</a>
        </div>
    </form>
</body>
</html>
