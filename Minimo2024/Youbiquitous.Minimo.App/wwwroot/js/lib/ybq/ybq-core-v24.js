///////////////////////////////////////////////////////////////////
//
// Youbiquitous Web Assets
// Copyright (c) Youbiquitous 2024
//
// Author: Youbiquitous Team
// CORE v24  (Feb 28, 2024)
//

var Ybq = Ybq || {};


//////////////////////////////////////////////////////////////////////
//
String.prototype.capitalize = function () {
    return this.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
};

//////////////////////////////////////////////////////////////////////
//
// General-purpose utilities
//
//

//////////////////////////////////////////////////////////////////////
// Ybq.goto()
// Jump to the given (absolute) URL 
//  
Ybq.goto = function(url) {
    window.location = url;
};

//////////////////////////////////////////////////////////////////////
// Ybq.get()
// Helper function to call a remote URL (GET), returns a promise
// 
Ybq.get = function (url, success, error) {
    $.ajax({
        cache: false,
        url: url,
        success: success,
        error: error
    });
    defer.resolve("true");
    return defer.promise();
};

//////////////////////////////////////////////////////////////////////
// Ybq.post()
// Helper function to call a remote URL (POST), returns a promise
// 
Ybq.post = function (url, data, success, error) {
    var defer = $.Deferred();
    $.ajax({
        cache: false,
        url: url,
        type: 'post',
        data: data,
        success: success,
        error: error
    });
    defer.resolve("true");
    return defer.promise();
};


//////////////////////////////////////////////////////////////////////
// Capitalize()
// String transformer
// 
String.prototype.capitalize = function () {
    return this.replace(/(?:^|\s)\S/g, function (a) { return a.toUpperCase(); });
};



//////////////////////////////////////////////////////////////////////
//
// Custom jQuery plugins for UI tasks
//
//

(function($) {
    ///////////////////////////////////////////////////////////////////////////////////
    // spin() : adds a rotating spin to the element
    //
    $.fn.spin = function() {
        var fa = "<i class='ybq-spin ms-1 ml-1 fas fa-spinner fa-pulse'></i>";
        $(this).append(fa);
        return $(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // unspin() : Removes a rotating spin from the element
    //
    $.fn.unspin = function() {
        $(this).find("i.ybq-spin").remove();
        return $(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // hideAfter() : Hides element after given time (secs) 
    //
    $.fn.hideAfter = function(secs) {
        secs = (typeof secs !== 'undefined') ? secs : 3;
        var item = $(this);
        window.setTimeout(function () {
            $(item).addClass("d-none");
        }, secs * 1000);
        return $(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // clearAfter : Clears element content after given time (secs) 
    // 
    $.fn.clearAfter = function(secs) {
        secs = (typeof secs !== 'undefined') ? secs : 3;
        var item = $(this);
        window.setTimeout(function () {
            $(item).html("");
            $(item).val("");
        }, secs * 1000);
        return $(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // reloadAfter() : Reloads current page after given time (secs) 
    //
    $.fn.reloadAfter = function(secs) {
        secs = (typeof secs !== 'undefined') ? secs : 3;
        window.setTimeout(function () {
            window.location.reload();
        }, secs * 1000);
        return $(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // gotoAfter() : Navigate to given URL after given time (secs)
    $.fn.gotoAfter = function(url, secs) {
        secs = (typeof secs !== 'undefined') ? secs : 3;
        window.setTimeout(function () {
            window.location.href = url;
        }, secs * 1000);
        return $(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // statusMessage() : show given text with success/failure style
    // 
    $.fn.statusMessage = function(text, success, css1, css2) {
        var successCss = (typeof css1 !== 'undefined') ? css1 : "text-success";
        var failureCss = (typeof css2 !== 'undefined') ? css2 : "text-danger";
        var css = success ? successCss : failureCss;

        $(this).html(text)
            .removeClass(successCss + " " + failureCss)
            .removeClass("d-none")
            .addClass(css);
        return $(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // addCssFor() : Adds given CSS class(es) for given time (secs) 
    //
    $.fn.addCssFor = function(css, secs) {
        css = (typeof css !== 'undefined') ? css : "";
        secs = (typeof secs !== 'undefined') ? secs : 2;
        var item = $(this);
        $(this).addClass(css);

        window.setTimeout(function () {
            $(item).removeClass(css);
        }, secs * 1000);
        return $(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // grayOutFor() : Adds opacity for given time (secs) 
    //
    $.fn.grayOutFor = function(secs) {
        return addCssFor("opacity-50", secs); 
    }


    ///////////////////////////////////////////////////////////////////////////////////
    // overlay() : Brings on/off the full screen overlay (if defined)
    //
    $.fn.overlay = function(on) {
        return on
            ? $(this).removeClass("d-none")
            : $(this).addClass("d-none");
    }

    ///////////////////////////////////////////////////////////////////////////////////
    // togglePassword() : Toggles password type
    // 
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
    
    ///////////////////////////////////////////////////////////////////////////////////
    // applyFilter() : Filters table rows based on current value of input field 
    // 
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



///////////////////////////////////////////////////////////////////////////////////
// Document ready common handlers
//  
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
