var modal = document.getElementById('ingredientmodal');
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
        datatype: "json",
        type: "GET",
        data: { "id": id, "stringOfIngredients": JSON.stringify(listOfIngredients) },
        url: "Cart/AddDishToCartAfterCustomizing",
        error: function () {
            alert('Något gick fel.');
        },
        success: function (response) {
            
            $('#cartview').html(response)
        }
    });
}
function BuyDishNoCustomizing(id) {
    $.ajax({
        type: "GET",
        data: { "id": id },
        url: "Cart/AddDishWithoutCustomizing",
        error: function () {
            alert('Något gck fel.');
        },
        success: function (response) {
            $('#cartview').html(response)
        }
    });
}
