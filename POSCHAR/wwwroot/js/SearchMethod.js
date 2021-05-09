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
