"use strict";

$(document).ready(function () {
    /*-- sidebar js --*/
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

    $(function () {
        let data = document.querySelector('body footer.footer_section');

        if (data !== null && data !== '') {
            data.parentNode.removeChild(data);
        }
    });
});