
//////////////////////////////////////////////////////////////////////////////////////
// Click handler for the RESET-PASSWORD button  
//
$("#trigger-reset-pswd").click(function () {
    var button = $(this);
    var formSelector = "#" + $(this).data("formid");
    var input1 = $("#password");
    var input2 = $("#password2");
    var minLength = parseInt(input1.data("minlength"));

    var pswd = $.trim(input1.val());
    if (pswd.length === 0 || pswd.length < minLength) {
        Ybq.alert(App_MainDisplayNameEx + Err_InvalidOrMissingData, false);
        return;
    }
    var pswd2 = $.trim(input2.val());
    if (pswd !== pswd2) {
        Ybq.alert(App_MainDisplayNameEx + Err_PasswordsDontMatch, false);
        return;
    }

    button.spin();
    Ybq.postForm(formSelector,
        function(data) {
            button.unspin();
            var response = JSON.parse(data);
            if (response.success) {
                Ybq.alert(App_MainDisplayNameEx + response.message, response.success)
                    .then(function() {
                        if (response.success)
                            Ybq.gotoRelative("/");
                    });
            }
        });
});




// §§§§§§§§§§§§§§§§§§§§§§§§§§


//////////////////////////////////////////////////////////////////////////////////////
// Click handler for the RESEND-EMAIL button when emailed link expired
//
$("#trigger-resend-email").click(function() {
    var button = $(this);
    var email = $.trim($("#email").val());
    var ekey = $.trim($("#ekey").val());
    var firstAccess = $("#firstaccess").val() === "True";
    var isGuest = $("#isguest").val() === "True";
    if (firstAccess) {
        Ybq.goto("/first/access");
        return;
    }

    button.spin();
    var url = isGuest ? "/guest/recover" : "/account/recover";
    Ybq.post(url,
        {
            email: email,
            ekey: ekey
        },
        function(response) {
            button.unspin();
            Ybq.alert(App_MainDisplayNameEx + response.message, response.success)
                .then(function () {
                    Ybq.goto("/");
                });
        },
        function() {
            button.unspin();
            Ybq.alert(App_MainDisplayNameEx + System_SomethingWentWrong);
        });
});

//////////////////////////////////////////////////////////////////////////////////////
// Click handler for the FIRST-ACCESS button to set the initial password after email
//
$("#trigger-init-pswd").click(function () {
    var button = $(this);
    var formSelector = "#" + $(this).data("formid");
    var input1 = $("#password");
    var input2 = $("#password2");
    var minLength = parseInt(input1.data("minlength"));
    var isGuest = $("#isguest").val() === "True";
    var ekey = $.trim($("#ekey").val());


    var pswd = $.trim(input1.val());
    if (pswd.length === 0 || pswd.length < minLength) {
        Ybq.alert(App_MainDisplayNameEx + Err_InvalidOrMissingData, false);
        return;
    }
    var pswd2 = $.trim(input2.val());
    if (pswd !== pswd2) {
        Ybq.alert(App_MainDisplayNameEx + Err_PasswordsDontMatch, false);
        return;
    }

    button.spin();
    Ybq.postForm(formSelector,
        function(data) {
            button.unspin();
            var response = JSON.parse(data);
            Ybq.alert(response.message, response.success)
                .then(() => {
                    if (response.success) {
                        var url = isGuest ? "/guest/login/" + ekey : "/";
                        Ybq.goto(url);
                    }
                });

        }, function() {
            button.unspin();
            Ybq.alert(App_MainDisplayNameEx + System_SomethingWentWrong);
        });
});

