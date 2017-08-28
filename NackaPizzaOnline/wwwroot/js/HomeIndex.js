var modal = document.getElementById('ingredientModal');
var btn = document.getElementById('ingredientsbtn');
var span = document.getElementsByClassName('close')[0];
var ingredientTable = document.getElementById('ingredientsTable');
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