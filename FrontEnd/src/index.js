$(document).ready(function(){
    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('#back-to-top').addClass("show");
        } else {
            $('#back-to-top').removeClass("show");
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').on('click', function (e) {
        $('body,html').animate({
            scrollTop: 0
        }, 400);
        return false;
    });

    $('#themeLabel').on('click', function (e) {
        var value = $('#themeSwitch').is(':checked');

        if (value) {
            $('#themeLabel').text("🌞 Light Mode");
        } else {
            $('#themeLabel').text("🌙 Dark Mode");
        }
    });
});