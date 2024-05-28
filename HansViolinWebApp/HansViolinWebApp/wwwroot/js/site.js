// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');
    //$('button[data-toggle="ajax-modal"]').click(function (e) {
    $('#admin-panel').on('click', 'button[data-toggle="ajax-modal"]', function (e) {
        e.preventDefault();
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal({ backdrop: 'static', keyboard: false, show: true });
        })
    })
    
    PlaceHolderElement.on('click', '[data-save="modal"]', function (e) {
        //e.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done(function (e) {
            PlaceHolderElement.find('.modal').modal('hide');
            $("#instrument-list").load(location.href + " #instrument-list>*", "");
            $("#business-profile").load(location.href + " #business-profile>*", "");
            $("#news-manager-publish").load(location.href + " #news-manager-publish>*", "");
            $("#news-manager-draft").load(location.href + " #news-manager-draft>*", "");
        }).fail(function (e) {
            e.preventDefault();
            PlaceHolderElement.find('.modal').modal({ backdrop: 'static', keyboard: false, show: true });
        })
    })
})