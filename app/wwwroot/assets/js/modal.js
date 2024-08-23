function showCartModal(cartItems) {
    debugger
    var modal = $('#customCartModal');
    var cartItemsList = $('#cart-items');
    cartItemsList.empty();
    cartItems.forEach(function (item) {
        cartItemsList.append('<li>' + item.name + ' - ₼' + item.price + '</li>');
    });

    modal.addClass("d-block"); 
}

$(document).on('click', '.add-to-cart-btn', function () {
    debugger
    var customModal = $("#customCartModal");
    customModal.removeClass("d-none"); 
    var producteId = $(this).data('product-id');
    $.ajax({
        url: '@Url.Action("AddToCart", "Cart")',
        type: 'POST',
        data: { producteId: producteId },
        success: function (response) {
            showCartModal(response.cartItems);
        }
    });
});

$(document).on('click', '.custom-close, #custom-close-btn', function () {
    debugger
    var modal = $("#customCartModal");
    modal.addClass("d-none");
    modal.removeClass("d-block");
});

