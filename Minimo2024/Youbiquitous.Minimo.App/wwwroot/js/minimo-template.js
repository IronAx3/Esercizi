
////////////////////////////////////////////////////////////////////////////////
//
// Enable show/hide of the sidebar in the main layout
// 

$(document).ready(function() {

    var __sidebarRef = "#sidebar-container";
    var __contentRef = "#page-content-container";
    var __togglerRef = "#sidebarToggle";
    var __storageRef = "sb|hidden";

    // Restore if status is known
    if (localStorage.getItem(__storageRef) === "true") {
        __hideSidebar();
    } else {
        $(__contentRef).css("margin-left", "12rem");
    }

    $(__togglerRef).click(function() {
        var currentlyHidden = $(__sidebarRef).hasClass("d-none");
        if (currentlyHidden) {
            __showSidebar();
        } else {
            __hideSidebar();
        }

        // Save status
        var hidden = $(__sidebarRef).hasClass("d-none");
        localStorage.setItem(__storageRef, hidden);
    });
});

function __hideSidebar() {
    var __sidebarRef = "#sidebar-container";
    var __contentRef = "#page-content-container";

    $(__sidebarRef).addClass("d-none");
    $(__contentRef).css("margin-left", 0);
}

function __showSidebar() {
    var __sidebarRef = "#sidebar-container";
    var __contentRef = "#page-content-container";

    $(__sidebarRef).removeClass("d-none");
    $(__contentRef).css("margin-left", "12rem");
}

$(window).resize(function () {
    var width = $(window).width();
    if (width < 1300) {
        __hideSidebar();
    } else {
        __showSidebar();
    }
});


