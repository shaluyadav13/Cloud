// Drew Code - shows logout dropdown menu
function showlayer(layer)
{
    var myLayer = document.getElementById(layer);
    if (myLayer.style.display == "none" || myLayer.style.display == "")
    {
        myLayer.style.display = "block";
    }
    else
    {
        myLayer.style.display = "none";
    }
}

// Toggle Image Size on MyImages page when clicked
function toggleImageSize() 
{
    if (document.getElementById("image").style.width == "15%")
    {    
        document.getElementById("image").style.width = "50%";
    }
    else
    {
        document.getElementById("image").style.width = "15%";
    }
}

// On load
$(function () {
    $("#nextButton").click(uploading);
    $("#submitButton").click(uploadingReplaceMedia);
    $("#transcriptVisibility").css("visibility", "hidden");
    $("#showTranscript").css("visibility", "hidden");
    $("#toggleAddEditGroupMembers").click(toggleAddEditGroupMembers);
    $("#emailCheckBoxContainer").css("visibility", "hidden");
});

// Shows uploading spinner
function uploading() {
    if (uploadFormComplete() == true) {
        $("#uploadingSpinnerContainer").show();
        $("#cancelButton").hide();
        $("#nextButton").hide();
    }
}

// Shows uploading spinner for replacing media items
function uploadingReplaceMedia() {
    if (editMediaFormComplete() == true) {
        $("#uploadingSpinnerContainer").show();
        $("#cancelButton").hide();
        $("#submitButton").hide();
        $("#removeVideoButton").hide();
    }
}

// Checks to see if the upload form has no erros
function uploadFormComplete() {
    //Checks ASP error tags for any content
    if ($("#RequiredFieldValidator").css("visibility") == "visible") {
        return false;
    }
    if ($("#RequiredFieldValidator1").css("visibility") == "visible") {
        return false;
    }
    if ($("#RequiredFieldValidator2").css("visibility") == "visible") {
        return false;
    }
    if ($("#RequiredFieldValidator3").css("visibility") == "visible") {
        return false;
    }
    if ($("#RequiredFieldValidator4").css("visibility") == "visible") {
        return false;
    }
    return true;
}

function editMediaFormComplete() {
    //Checks ASP error tags for any content
    if ($("#RequiredFieldValidator1").css("visibility") == "visible") {
        return false;
    }
    if ($("#RequiredFieldValidator2").css("visibility") == "visible") {
        return false;
    }
    if ($("#RequiredFieldValidator4").css("visibility") == "visible") {
        return false;
    }
    return true;
}

// Changes the upload form based on the media type selected
function mediaTypeChanged() {
    var typeOfMedia = $("#ddlType").val();
    // Shows transcript option if video or audio
    if (typeOfMedia === "Audio" || typeOfMedia === "Video") {
        $("#transcriptVisibility").css("visibility", "visible");
        $("#showTranscript").css("visibility", "hidden");
    }
    else {
        $("#transcriptVisibility").css("visibility", "hidden");
        $("#showTranscript").css("visibility", "hidden");
    }
    // Hide email checkbox
    if (typeOfMedia === "Documents" || typeOfMedia === "Images" || typeOfMedia == "select") {
        $("#emailCheckBoxContainer").css("visibility", "hidden");
    }
    else {
        $("#emailCheckBoxContainer").css("visibility", "visible");
    }

    // Show info about type of media uplodaing
    $("#uploadTypeInfo").css("visibility", "visible");
    // Shows zip info if website
    if (typeOfMedia === "Website") {
        $("#uploadTypeInfo").html("Required to be a \".zip\" file. <br />This zip file must also include an \"index.html\" file.");
        
    }
    else if (typeOfMedia === "Video") {
        $("#uploadTypeInfo").text("Required to be a video format (\".mp4,\" \".wmv,\" \".avi,\" etc.)");
    }
    else if (typeOfMedia === "Audio") {
        $("#uploadTypeInfo").text("Required to be an audio format (\".mp3,\" \".wma,\" \".wav,\" etc.)");
    }
    else if (typeOfMedia === "Documents") {
        $("#uploadTypeInfo").text("Acceptable formats include: \".pdf,\" \".doc,\" \".docx,\" \".txt,\" etc.");
    }
    else if (typeOfMedia === "Images") {
        $("#uploadTypeInfo").text("Required to be an image format (\".jpeg,\" \".png,\" \".gif,\" etc.)");
    }
    else {
        $("#uploadTypeInfo").css("visibility", "hidden");
    }
    
}

function transcriptOptionChanged() {
    if ($("input[id*=transcriptBtn_Yes").is(":checked")) {
        $("#showTranscript").css("visibility", "visible");
        console.log("show");
    }
    else {
        $("#showTranscript").css("visibility", "hidden");
        console.log("hide");
    }

    
}



function toggleAddEditGroupMembers() {
    $("#pnlStudents").slideToggle(200);
    $(".caretClosedIcon").toggleClass("caretOpenIcon");
}