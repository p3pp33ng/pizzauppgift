var modal = document.getElementById('ingredientModal');
var btn = document.getElementById('ingredientsbtn');
var span = document.getElementsByClassName('close')[0];
var modalDishName = document.getElementById('modalDishName');

function GetDishInfo(id) {
    $.ajax({
        type: "GET",
        data: { "id": id },
        url: "Home/GetDishInfoForModal",
        success: function (response) {
            $('#ingredientmodal').html(response);

        }
    });
}