var modal = document.getElementById('ingredientModal');
var btn = document.getElementById('ingredientsbtn');
var span = document.getElementsByClassName('close')[0];

btn.onclick = function () {
    modal.style.display = 'block';
}
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
        type: 'POST',
        data: id,
        datatype:'json',
        url: "/Home/GetDishInfoForModal",
        success: function (response) {
            if (!response == null) {
                //Lägg upp all data från objektet i modalen.
            }
            else {
                //Visa felmeddelande att maträtten inte finns.
            }
        }
    });
};