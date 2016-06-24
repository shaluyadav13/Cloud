
$(function () {
    $("#smallNavigation").hide();
    $("#carrot").on("click", function () {
        $("#navigation, #smallNavigation").toggle();
        $("#topNavigationContainer").css("left", "67px");
        $("#topNavigationContainer").css("width", "calc(100% - 67px)");
        $("#body1").css("margin-left", "67px");
    });
});

$(function () {
    
    $("#carrot2").on("click", function () {
        $("#smallNavigation, #navigation").toggle();
        $("#topNavigationContainer").css("left", "202px");
        $("#body1").css("margin-left", "202px");
    });
});
