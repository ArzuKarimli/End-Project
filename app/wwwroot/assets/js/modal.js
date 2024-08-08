function showCartModal(cartItems) {
    var modal = $('#customCartModal');
    var cartItemsList = $('#cart-items');
    cartItemsList.empty();

    cartItems.forEach(function (item) {
        cartItemsList.append('<li>' + item.name + ' - ₼' + item.price + '</li>');
    });

    modal.addClass("d-block"); 
}

$(document).on('click', '.add-to-cart-btn', function () {
    var customModal = $("#customCartModal");
    customModal.removeClass("d-none"); 
    var productId = $(this).data('product-id');
    $.ajax({
        url: '@Url.Action("AddToCart", "Cart")',
        type: 'POST',
        data: { productId: productId },
        success: function (response) {
            showCartModal(response.cartItems);
        }
    });
});

$(document).on('click', '.custom-close, #custom-close-btn', function () {
    var modal = $("#customCartModal");
    modal.addClass("d-none");
    modal.removeClass("d-block");
});

