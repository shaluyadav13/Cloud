//-----------------Commented out by Lawrence Foley on 03/13/2015, the flash media server is no longer used, the files are store on the server directly------------//

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

///// <summary>
///// Generates the mixture of JavaScript and HTML needed to load and render the NWVideo.swf flash
///// player component.
///// </summary>
//public static class VideoScript
//{
//    public static String GetVideoScript(String videoID)
//    {
//        String flashVars = String.Format("\"videoID={0}&webServer={1}&flashServer={2}\"",
//                                         videoID,
//                                         AppSettings.AppWebPath,
//                                         AppSettings.FlashMediaServerPath);

//        String videoScript = "<script language=\"JavaScript\" type=\"text/javascript\">" +
//            "\n<!--" +
//            "\n// Version check for the Flash Player that has the ability to start Player Product Install (6.0r65)" +
//            "\nvar hasProductInstall = DetectFlashVer(6, 0, 65);" +

//            "\n// Version check based upon the values defined in globals" +
//            "\nvar hasRequestedVersion = DetectFlashVer(requiredMajorVersion, requiredMinorVersion, requiredRevision);" +

//            "\nif ( hasProductInstall && !hasRequestedVersion ) {" +
//            "\n	// DO NOT MODIFY THE FOLLOWING FOUR LINES" +
//            "\n	// Location visited after installation is complete if installation is required" +
//            "\n	var MMPlayerType = (isIE == true) ? \"ActiveX\" : \"PlugIn\";" +
//            "\n	var MMredirectURL = window.location;" +
//            "\n    document.title = document.title.slice(0, 47) + \" - Flash Player Installation\";" +
//            "\n    var MMdoctitle = document.title;" +

//            "\n	AC_FL_RunContent(" +
//            "\n		\"src\", \"playerProductInstall\"," +
//            "\n		\"FlashVars\", \"MMredirectURL=\"+MMredirectURL+'&MMplayerType='+MMPlayerType+'&MMdoctitle='+MMdoctitle+\"\"," +
//            "\n		\"width\", \"512\"," +
//            "\n		\"height\", \"384\"," +
//            "\n		\"align\", \"middle\"," +
//            "\n		\"id\", \"NWVideo\"," +
//            "\n		\"quality\", \"high\"," +
//            "\n		\"bgcolor\", \"#869ca7\"," +
//            "\n		\"name\", \"NWVideo\"," +
//            "\n		\"allowScriptAccess\",\"sameDomain\"," +
//            "\n		\"type\", \"application/x-shockwave-flash\"," +
//            "\n		\"pluginspage\", \"http://www.adobe.com/go/getflashplayer\"" +
//            "\n	);" +
//            "\n} else if (hasRequestedVersion) {" +
//            "\n	// if we've detected an acceptable version" +
//            "\n	// embed the Flash Content SWF when all tests are passed" +
//            "\n	AC_FL_RunContent(" +
//            "\n			\"src\", \"NWVideo\"," +
//            "\n			\"width\", \"512\"," +
//            "\n			\"height\", \"384\"," +
//            "\n			\"align\", \"middle\"," +
//            "\n			\"id\", \"NWVideo\"," +
//            "\n			\"quality\", \"high\"," +
//            "\n			\"bgcolor\", \"#000000\"," +
//            "\n			\"name\", \"NWVideo\"," +
//            "\n			\"allowScriptAccess\",\"sameDomain\"," +
//            "\n     \"allowFullScreen\", \"true\"," +
//            "\n			\"type\", \"application/x-shockwave-flash\"," +
//            "\n			\"pluginspage\", \"http://www.adobe.com/go/getflashplayer\"," +
//            "\n         \"FlashVars\", " + flashVars +
//            "\n	);" +
//            "\n  } else {  // flash is too old or we can't detect the plugin" +
//            "\n    var alternateContent = 'Alternate HTML content should be placed here. '" +
//            "\n  	+ 'This content requires the Adobe Flash Player. '" +
//            "\n   	+ '<a href=http://www.adobe.com/go/getflash/>Get Flash</a>';" +
//            "\n    document.write(alternateContent);  // insert non-flash content" +
//            "\n  }" +
//            "\n// -->" +
//            "\n</script>" +
//            "\n<noscript>" +
//            GetObjectTag(videoID) +
//            "\n</noscript>";

//        return videoScript;
//    }

//    public static String GetObjectTag(String videoID)
//    {
//        String flashVars = String.Format("\"videoID={0}&webServer={1}&flashServer={2}\"",
//                                 videoID,
//                                 AppSettings.AppWebPath,
//                                 AppSettings.FlashMediaServerPath);

//        String objectTag = 
//            "\n  	<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"" +
//            "\n			id=\"NWVideo\" width=\"512\" height=\"384\"" +
//            "\n			codebase=\"http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab\">" +
//            "\n			<param name=\"movie\" value=\"http://" + AppSettings.AppWebPath + "NWVideo.swf\" />" +
//            "\n			<param name=\"quality\" value=\"high\" />" +
//            "\n			<param name=\"bgcolor\" value=\"#000000\" />" +
//            "\n			<param name=\"allowScriptAccess\" value=\"sameDomain\" />" +
//            "\n         <param name=\"allowFullScreen\" value=\"true\" />" +
//            "\n         <param name=\"FlashVars\" value=" + flashVars + " />" +
//            "\n			<embed src=\"http://" + AppSettings.AppWebPath + "NWVideo.swf\" quality=\"high\" bgcolor=\"#000000\"" +
//            "\n				width=\"512\" height=\"384\" name=\"NWVideo\" align=\"middle\"" +
//            "\n				play=\"true\"" +
//            "\n				loop=\"false\"" +
//            "\n				quality=\"high\"" +
//            "\n				allowScriptAccess=\"sameDomain\"" +
//            "\n             allowFullScreen=\"true\"" +
//            "\n				type=\"application/x-shockwave-flash\"" +
//            "\n				pluginspage=\"http://www.adobe.com/go/getflashplayer\"" +
//            "\n             flashVars=" + flashVars + ">" +
//            "\n			</embed>" +
//            "\n	</object>";

//        return objectTag;
//    }
//}
