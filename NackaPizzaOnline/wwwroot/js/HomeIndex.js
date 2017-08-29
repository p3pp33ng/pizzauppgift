var modal = document.getElementById('ingredientModal');
var btn = document.getElementById('ingredientsbtn');
var span = document.getElementsByClassName('close')[0];
var modalDishName = document.getElementById('modalDishName');

//btn.onclick = function () {
//    modal.style.display = 'block';
//}
span.onclick = function () {
    modal.style.display = 'none';
}
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = 'none';
    }
}

function GetDishInfo(id) {
    console.log(id);
    $.ajax({
        type: 'GET',
        data: { 'id': id },  
        url: "/Home/GetDishInfoForModal",
        success: function (response) {
            if (response != null) {
                //Lägg upp all data från objektet i modalen.   
                modal.style.display = 'block';
                modalDishName.innerHTML += response.dishName;
                for (var i = 0; i < response.number; i++) {
                    $('#ingredientTable').append($('tr'));
                }
                console.log(response);
            }
            else {
                //Visa felmeddelande att maträtten inte finns.
                console.log(response);
            }
        },
        fail: function () {
            console.log("Fel");
        }
    });
};