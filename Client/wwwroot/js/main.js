$(document).ready(function SideBar() {
    var body = $('body');
    var html = $('html');
    var container = $("#container");
    var mainHeader = $(".header.navbar");
    var overlay = $(".overlay");
    var sidebarButton = $(".sidebarCollapse");

    sidebarButton.on("click", function () {

        body.toggleClass("sidebar-noneoverflow");
        html.toggleClass("sidebar-noneoverflow");
        container.toggleClass("sidebar-closed sbar-open");
        overlay.toggleClass("show");
        mainHeader.toggleClass("expand-header");

    })
    $('#dismiss, .overlay, cs-overlay').on('click', function () {
        // hide sidebar
        $(container).addClass('sidebar-closed');
        $(container).removeClass('sbar-open');
        // hide overlay
        overlay.removeClass('show');
        $('html,body').removeClass('sidebar-noneoverflow');
    });
})

/*********************************************************
 *  Toast
 * ******************************************************/
function Toast() {
    $(document).ready(function () {
        $('.toast').toast('show');
    });
}

showSuccessToast = function (text) {
    'use strict';
    resetToastPosition();
    $.toast({
        //heading: heading,
        text: "<p class='text-left text-light mt-3' style='font-family:iransans'>" + text + "</p>",
        showHideTransition: 'slide',
        bgColor: 'var(--success)',
        icon: 'success',
        loaderBg: '#eaeaea',
        position: 'top-right'
    })
};

showDangerToast = function (text) {
    'use strict';
    resetToastPosition();
    $.toast({
        //heading: heading,
        text: "<p class='text-left text-light mt-3' style='font-family:iransans'>" + text + "</p>",
        showHideTransition: 'slide',
        bgColor: 'var(--danger)',
        icon: 'error',
        loaderBg: '#eaeaea',
        position: 'top-right'
    })
};

resetToastPosition = function () {
    $('.jq-toast-wrap').removeClass('bottom-left bottom-right top-left top-right mid-center'); // to remove previous position class
    $(".jq-toast-wrap").css({
        "top": "",
        "left": "",
        "bottom": "",
        "right": ""
    }); //to remove previous position style
};

/*********************************************************
 *  Alert
 * ******************************************************/
function Alert() {
    window.setTimeout(function () {
        $(".alert").fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 4000);
}


$(".checkbox-menu").on("change", "input[type='checkbox']", function () {
    $(this).closest("li").toggleClass("active", this.checked);
});



function loadScript(url) {

    let myScript = document.createElement("script");
    myScript.src = url;
    document.body.appendChild(myScript);

}

/********************************************************
 * DatePicker
********************************************************/

function includeDatePicker() {

    loadScript('/datepicker/persian-date.min.js');
    loadScript('/datepicker/persian-datepicker.js');

    $(document).ready(function () {
        setTimeout(() => {
            $(".datepicker").pDatepicker({
                initialValue: false,
                format: 'YYYY/MM/DD',
                onSelect: function (unixDate) {

                    var pdate = new persianDate(unixDate);
                    pdate.formatPersian = this.persianDigit;
                    var date = pdate.format('YYYY/MM/DD');
                    var element = this.model.input.elem;
                    $(element).focus();
                    DotNet.invokeMethodAsync("Client", "SetInputDateValue", date);

                }
            });

        }, 1000);
    });

}


/********************************************************
 * Close Modal
********************************************************/
function closeModal() {
    $('.modal').modal('hide');
}


