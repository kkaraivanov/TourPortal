(function($) {
    "use strict";

    $(document).ready(function() {
        /*-- sidebar js --*/
        $('#sidebarCollapse').on('click',
            function() {
                $('#sidebar').toggleClass('active');
            });

        $(function() {
            let data = document.querySelector('body footer.footer_section');

            if (data !== null && data !== '') {
                data.parentNode.removeChild(data);
            }
        });
    });

    $('[data-countdown]').each(function () {
        var $this = $(this),
            finalDate = $(this).data('countdown');
        $this.countdown(finalDate, function (event) {
            var $this = $(this).html(event.strftime(''
                + '<div class="time-bar"><span class="time-box">%w</span> <span class="line-b">weeks</span></div> '
                + '<div class="time-bar"><span class="time-box">%d</span> <span class="line-b">days</span></div> '
                + '<div class="time-bar"><span class="time-box">%H</span> <span class="line-b">hr</span></div> '
                + '<div class="time-bar"><span class="time-box">%M</span> <span class="line-b">min</span></div> '
                + '<div class="time-bar"><span class="time-box">%S</span> <span class="line-b">sec</span></div>'));
        });
    });
})(jQuery);

function ShowAlertBox(message) {
    alert(`${message}`);
    window.location.href = '/account/logout';
}

function CloseMessage() {
    setTimeout(function () {
        $('#alert-message').hide();
    }, 8000);
}