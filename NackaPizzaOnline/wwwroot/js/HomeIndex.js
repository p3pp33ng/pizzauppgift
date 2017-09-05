//var modal = document.getElementById('ingredientModal');
//var btn = document.getElementById('ingredientsbtn');
//var span = document.getElementsByClassName('close')[0];
//var modalDishName = document.getElementById('modalDishName');


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

function BuyDishAfterCustomize(id) {
    console.log(id);
    var listOfIngredients = [];
    
    var list = $('input[type="checkbox"]');
    for (var i = 0; i < list.length; i++) {
        if ($(list[i]).is(':checked')) {
            var sibling = list[i].nextSibling;
            var ingredientId = list[i].nextElementSibling;
            listOfIngredients.push(ingredientId.nextElementSibling.value);
        }
    }
    
    $.ajax({
        type: "GET",
        data: { "id": id, "listOfIngredients": listOfIngredients },
        url: "Cart/AddDishToCart",
        success: function (response) {
            console.log(response);
        }
    });
}