var modal = document.getElementById('ingredientModal');
var btn = document.getElementById('ingredientsbtn');
var span = document.getElementsByClassName('close')[0];
var modalDishName = document.getElementById('modalDishName');

btn.onclick = function () {
    modal.style.display = 'block';   
};
span.onclick = function () {
    modal.style.display = 'none';
}
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = 'none';
    }
}

function GetDishInfo(id) {
    $.ajax({
        type: "GET",
        data: { "id": id },
        url: "Home/GetDishInfoForModal",
        success: function (response) {
        }
    });
};