// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".searchtext").on('change', function postinput() {
        var matchvalue = $(this).val(); // this.value
        $.ajax({
            url: '"@Url.Action("Index","Customers")"',
            data: { search: matchvalue },
            type: 'GET'
        }).done(function (responseData) {
            console.log('Done: ', responseData);
        }).fail(function () {
            console.log('Failed');
        });
    });
});
