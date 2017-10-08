var modal = document.getElementById('ingredientmodal');

function GetDishInfo(id) {
    $.ajax({
        type: "GET",
        data: { "id": id },
        url: "Home/GetDishInfoForModal",
        success: function (response) {
            $('#ingredientmodal').html(response);
        },
        error: function () {
            alert("Något gick fel i GetDishInfo");
        }
    });
}

function BuyDishAfterCustomize(id) {
    console.log(id);
    var listOfIngredients = [];

    //Refactor this code
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
            alert('Något gick fel i BuyDishAfterCustomize');
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
            alert('Något gick fel i BuyDishNoCustomizing');
        },
        success: function (response) {
            $('#cartview').html(response)
        }
    });
}

function DeleteFromCart(cartId, cartIemId) {
    $.ajax({
        type: "GET",
        data: { "cartId": cartId, "cartItemId": cartIemId },
        url: "Cart/RemoveFromCart",
        error: function () {
            alert("Något gick fel i DeleteFromCart");
        },
        success: function (response) {
            $('#cartview').html(response)
        }
    });
}

function getCartIfExists() {
    var id = document.getElementById('session').value;
    return id;
}

if ($(document).ready()) {
    var id = getCartIfExists();
    if (id != null) {
        $.ajax({
            type: "GET",
            data: { "cartId": id },
            url: "Cart/GetCartIfExsists",
            error: function () {
                alert("Något gick fel i getCartIfExists");
            },
            success: function (response) {
                $('#cartview').html(response);
            }
        });
    }
}
