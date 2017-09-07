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
    var that = this;
    
    var list = $('input[type="checkbox"]');
    for (var i = 0; i < list.length; i++) {
        if ($(list[i]).is(':checked')) {
            var sibling = list[i].nextSibling;
            var ingredientId = list[i].nextElementSibling;
            listOfIngredients.push(ingredientId.nextElementSibling.value);
        }
    }
    console.log(listOfIngredients);
    $.ajax({
        //datatype: "json",
        type: "GET",
        data: { "id": id, "listOfIngredients": JSON.stringify(listOfIngredients) },
        url: "Cart/AddDishToCart",
        error: function () {
            console.log(listOfIngredients);
        },
        success: function (response) {
            console.log(response);
            $('#cartview').html(response)
        }
    });
}