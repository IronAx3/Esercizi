///////////////////////////////////////////////////////////////////
//
// Youbiquitous Web Assets
// Copyright (c) Youbiquitous 2022
//
// Author: Youbiquitous Team
// v2.0.0  (May 5, 2022)
//


String.prototype.capitalize = function () {
    return this.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
};

// **************************************************************************************************//

// <summary>
// Root object for any script function used throughout the application
// </summary>
var Ybq = Ybq || {};
Ybq.Internal = {};
Ybq.RootServer = "";        // Should be set to /vdir when deployed


// <summary>
// Return a root-based path
// </summary>
Ybq.fromServer = function (relativeUrl) {
    return Ybq.RootServer + relativeUrl;
};

// <summary>
// Helper function to call a remote URL (GET)
// </summary>
Ybq.invoke = function (url, success, error) {
    $.ajax({
        cache: false,
        url: Ybq.fromServer(url),
        success: success,
        error: error
    });
};

// <summary>
// Jump to the given ABSOLUTE URL (no transformation made on the URL)
// </summary>
Ybq.goto = function(url) {
    window.location = url;
};

// <summary>
// Jump to the given RELATIVE URL (modified with ROOTSERVER)
// </summary>
Ybq.gotoRelative = function(url) {
    window.location = Ybq.fromServer(url);
};

// <summary>
// Helper function to call a remote URL (POST)
// </summary>
Ybq.post = function (url, data, success, error) {
    var defer = $.Deferred();
    $.ajax({
        cache: false,
        url: Ybq.fromServer(url),
        type: 'post',
        data: data,
        success: success,
        error: error
    });
    defer.resolve("true");
    return defer.promise();
};



// <summary>
// Custom plugins for (animated) messages in UI
// </summary>
(function($) {
    // Add a rotating spin to the element
    $.fn.spin = function() {
        var fa = "<i class='ybq-spin ms-1 ml-1 fas fa-spinner fa-pulse'></i>";
        $(this).append(fa);
        return $(this);
    }

    // Remove a rotating spin from the element
    $.fn.unspin = function() {
        $(this).find("i.ybq-spin").remove();
        return $(this);
    }

    // Add CSS decoration temporarily
    $.fn.setCssFor = function(css, secs) {
        css = (typeof css !== 'undefined') ? css : "opacity5";
        secs = (typeof secs !== 'undefined') ? secs : 2;
        var item = $(this);
        $(this).addClass(css);

        window.setTimeout(function () {
            $(item).removeClass(css);
        }, secs * 1000);
        return $(this);
    }

    // Add a hiding timer for hiding the element
    $.fn.hideAfter = function(secs) {
        secs = (typeof secs !== 'undefined') ? secs : 3;
        var item = $(this);
        window.setTimeout(function () {
            $(item).addClass("d-none");
        }, secs * 1000);
        return $(this);
    }

    // Add a cleaning timer for the HTML content of the element
    $.fn.clearAfter = function(secs) {
        secs = (typeof secs !== 'undefined') ? secs : 3;
        var item = $(this);
        window.setTimeout(function () {
            $(item).html("");
        }, secs * 1000);
        return $(this);
    }

    // Add a reload timer for the current page
    $.fn.reloadAfter = function(secs) {
        secs = (typeof secs !== 'undefined') ? secs : 3;
        window.setTimeout(function () {
            window.location.reload();
        }, secs * 1000);
        return $(this);
    }

    // Add a goto timer to navigate away
    $.fn.gotoAfter = function(url, secs) {
        secs = (typeof secs !== 'undefined') ? secs : 3;
        window.setTimeout(function () {
            window.location.href = url;
        }, secs * 1000);
        return $(this);
    }

    // HTML writer context-sensitive
    $.fn.setMsg = function(text, success, css1, css2) {
        var successCss = (typeof css1 !== 'undefined') ? css1 : "text-success";
        var failureCss = (typeof css2 !== 'undefined') ? css2 : "text-danger";

        var css = success ? successCss : failureCss;
        $(this).html(text)
            .removeClass(successCss + " " + failureCss)
            .removeClass("d-none")
            .addClass(css);
        return $(this);
    }

    // HTML writer (error message) 
    $.fn.fail = function(text) {
        return $(this).setMsg(text, false);
    }

    // Show/Hide via d-none (mostly for form overlays)
    $.fn.overlayOn = function() {
        return $(this).removeClass("d-none");
    }
    $.fn.overlayOff = function() {
        return $(this).addClass("d-none");
    }

    // Toggle password type
    $.fn.togglePassword = function(eyeIcon, passwordHidden, passwordClear) {
        var pswd = $(this);
        var icon = $("#" + eyeIcon);
        var hidden = pswd.attr("type") === "password";
        if (hidden) {
            pswd.attr("type", "text");
            icon.removeClass(passwordHidden).addClass(passwordClear);
        } else {
            pswd.attr("type", "password");
            icon.removeClass(passwordClear).addClass(passwordHidden);
        }
        return $(this);
    }
    
    // Apply row filter to given HTML table
    $.fn.applyFilter1 = function(tableSelector, colIndex) {
        var id = $(this).attr("id");
        $(this).on("input",
            function() {
                var remover = $("#" + id + "-remover");
                var filter = $(this).val().toLowerCase();

                if (filter.length > 0) {
                    $($(tableSelector + " tbody tr")).filter(function() {
                        $(this).toggle($(this)
                            .find("td:eq(" + colIndex + ")")
                            .text()
                            .toLowerCase()
                            .indexOf(filter) > -1);
                    });
                    $(remover).removeClass("d-none");
                } else {
                    $(tableSelector + " tbody tr").toggle(true);
                    $(remover).addClass("d-none");
                }
            });
        $("#" + id + "-remover").click(function() {
            $("#" + id).val("").trigger("input");
        });
    }

    // Apply row filter to given HTML table
    $.fn.applyFilter = function(tableSelector, cols, css = 'mark') {
        var id = $(this).attr("id");
        if (Number(cols) === cols) {
            cols = [cols];
        }
        if (String(cols) === cols) {
            cols = cols.split(',');
        }

        $(this).on("input",
            function() {
                var remover = $("#" + id + "-remover");
                var filter = $(this).val().toLowerCase();

                if (filter.length > 0) {
                    // decorate headers
                    $(tableSelector + " thead tr").filter(function() {
                        for (var i = 0; i < cols.length; i++) {
                            $(this).find("th:eq(" + cols[i] + ")").addClass(css);
                        }
                    });

                    // show/hide rows
                    $(tableSelector + " tbody tr").filter(function() {
                        var content = "";
                        for (var i = 0; i < cols.length; i++) {
                            content += $(this).find("td:eq(" + cols[i] + ")").text() + "|";
                        }
                        $(this).toggle(content.toLowerCase().indexOf(filter) > -1);

                    });
                    $(remover).removeClass("d-none");
                } else {
                    $(tableSelector + " tbody tr").toggle(true);
                    $(tableSelector + " thead th").removeClass(css);
                    $(remover).addClass("d-none");
                }
            });
        $("#" + id + "-remover").click(function() {
            $("#" + id).val("").trigger("input");
        });
    }

}(jQuery));



// <summary>
// Need to run after page loading
// </summary>
$(document).ready(function () {

    //////////////////////////////////////////
    // Add a tooltip if the text is clipped
    // 
    $('tbody tr td, thead tr th, .ybq-overflow')
        .on('mouseover', function () {
            var container = this;
            var overflowed = container.scrollWidth > container.clientWidth;
            this.title = overflowed
                ? $.trim(this.textContent.replaceAll("  ", '').replaceAll("\n", " "))
                : '';
        })
        .on('mouseout', function() {
            this.title = '';
        });
});
