function modalPopup(editUrl, itemId) {
    var options = { "backdrop": "static", keyboard: true };
    $.ajax({
        type: "GET",
        url: editUrl,
        contentType: "application/json; charset=utf-8",
        data: { "Id": itemId },
        datatype: "json",
        success: function (data) {
            $('#myModalContent').html(data);
            $('#myModal').modal(options);
            $('#myModal').modal('show');
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
}

function softDelete(targetUrl, targetId, elem) {
    var row = elem.closest('tr');
    var obj = $(elem).parent().parent();

    $.ajax({
        type: "POST",
        url: targetUrl,
        data: { id: targetId },
        success: function (result) {

            $('#successful-toast').toast('show');

            obj.fadeTo(10, 1, () => {
                obj.children('td', 'th')
                    .animate({ 'padding-top': '0', 'padding-bottom': '0' })
                    .wrapInner('<div />')
                    .children()
                    .slideUp(100, () => { obj.remove(); });
            });
        },
        error: function (data) {
            $('#failed-toast').toast('show');
        }
    });
}

function ajaxPost(targetUrl, targetId, elem) {
    $.ajax({
        type: "POST",
        url: targetUrl,
        data: { id: targetId },
        success: function (result) {

            $('#successful-toast').toast('show');

            //obj.fadeTo(10, 1, () => {
            //    obj.children('td', 'th')
            //        .animate({ 'padding-top': '0', 'padding-bottom': '0' })
            //        .wrapInner('<div />')
            //        .children()
            //        .slideUp(100, () => { obj.remove(); });
            //});
        },
        error: function (data) {
            $('#failed-toast').toast('show');
        }
    });
}

function sendApplication(userId) {
    ajaxPost('/Solicitations/Apply', userId);

    $('#myModal').modal('hide');
}

$('.dropdown-menu a.dropdown-toggle').on('click', function (e) {
    if (!$(this).next().hasClass('show')) {
        $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
    }
    var $subMenu = $(this).next(".dropdown-menu");
    $subMenu.toggleClass('show');


    $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
        $('.dropdown-submenu .show').removeClass("show");
    });

    return false;
});
