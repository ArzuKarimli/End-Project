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
$(document).on("click", ".add-to-cart-btn", function (e) {

    e.preventDefault();
    let id = parseInt($(this).attr("data-id"));

    $.ajax({
        url: `/Product/AddProductToModal`,
        type: "POST",
        data: { id: id },
        success: function (response) {
            $(".total-count").text(response.count);
            $(".total-price").text(response.total);
        }
    });
});
$(document).ready(function () {
    function updateCart(response, row) {
        var productCountElement = row.find(".product-count");
        var productInputElement = row.find("input[type='number']");
        var basketTotalPriceElement = $('.total-price');
        var productPriceElement = row.find(".product-price");

        productInputElement.val(response.count);
        basketTotalPriceElement.text(`${response.totalPrice}`);
        productPriceElement.text(`${response.price}`);

        if (response.count === 0) {
            row.remove();
        }
    }

    $(document).on("click", ".btn-minus", function () {
        let id = parseInt($(this).attr("data-id"));
        var row = $(this).closest(".cart-item");

        $.ajax({
            url: `/cart/ReductionCounterProduct`,
            type: "POST",
            data: { id: id },
            success: function (response) {
                updateCart(response, row);
            }
        });
    });

    $(document).on("click", ".btn-plus", function () {
        let id = parseInt($(this).attr("data-id"));
        var row = $(this).closest(".cart-item");

        $.ajax({
            url: `/cart/IncrementCounterProduct`,
            type: "POST",
            data: { id: id },
            success: function (response) {
                updateCart(response, row);
            }
        });
    });

    $(document).ready(function () {
        $(document).on("click", ".cart-item-remove button", function () {
            let id = parseInt($(this).closest(".cart-item").find(".btn-minus").attr("data-id"));
            var row = $(this).closest(".cart-item");
            var formData = new FormData();
            formData.append("id", id);

            $.ajax({
                url: `/cart/Delete`,
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function () {
                    row.remove();
                    updateCartSummary();
                },
            });
        });

        function updateCartSummary() {
            var totalItems = 0;
            var totalPrice = 0;

            $(".cart-item").each(function () {
                var itemQuantity = parseInt($(this).find("input[type='number']").val());
                var itemPrice = parseFloat($(this).find(".product-price").text().replace("₼", ""));

                totalItems += itemQuantity;
                totalPrice += itemQuantity * itemPrice;
            });

            $(".cart-summary-item span").eq(1).text(totalItems);
            $(".cart-summary-item span.total-price").text(`${totalPrice.toFixed(2)}`);
        }
    });


    $('#myModal').modal(options)
})
