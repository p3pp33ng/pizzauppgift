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

function GetChecked(bool) {
    if (!bool){
        return "checked";
    }
    else {
        return "";
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
                console.log(response);
                modal.style.display = 'block';
                modalDishName.innerHTML += response.name;
                for (var i = 1; i <= response.number; ++i) {
                    $('#ingredientsTable').append('<tr><td><input type="checkbox" ' + GetChecked(response.allIngredients[i].isChecked) + ' />' + response.allIngredients[i] + '</td></tr>');
                    //if (response.dishAlreadyHaveIngredients && response.dishAlreadyHaveIngredients.length == 0 || response.dishAlreadyHaveIngredients[i] == undefined) {
                    //    $('#ingredientsTable')
                    //        .append('<tr><td><input type="checkbox" name="' + response.remainingIngredients[i].key + '"/>' + response.remainingIngredients[i].value + '</td></tr>');
                    //}
                    //else {
                    //    $('#ingredientsTable')
                    //        .append('<tr><td><input type="checkbox" name="' + i + '"/>' + response.dishAlreadyHaveIngredients[i] + '</td></tr>');
                    //}                                  
                }
                
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