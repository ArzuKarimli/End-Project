$('.slider-for').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false,
    fade: true,
    asNavFor: '.slider-nav'
  });
  $('.slider-nav').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    asNavFor: '.slider-for',
    dots: true,
    centerMode: true,
    focusOnSelect: true
  });

  document.querySelectorAll('.tab-link').forEach(tab => {
    tab.addEventListener('click', () => {
        document.querySelectorAll('.tab-link').forEach(link => {
            link.classList.remove('active');
        });
        tab.classList.add('active');

        document.querySelectorAll('.tab-content').forEach(content => {
            content.classList.remove('active');
        });
        document.getElementById(tab.getAttribute('data-tab')).classList.add('active');
    });
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

    $(document).ready(function () {
        function applyFilters() {
            var selectedCategory = $("#categoryId").val();
            var maxPrice = $("#price").val();
            var sortOption = $("#sort").val();

            $.ajax({
                url: "/product/Filter",
                type: "GET",
                data: {
                    categoryId: selectedCategory,
                    price: maxPrice,
                    sort: sortOption
                },
                success: function (response) {
                    $("#product-list").html(response);
                }
            });
        }   
        $("#categoryId, #price, #sort").on("change", applyFilters);
        $("input[name='brands']").on("change", applyFilters);
        $("#price").on("input", function () {
            $("#max-price").text(`₼${$(this).val()}`);
        });
    });

    $(document).ready(function () {
        $('#search-button').on('click', function () {
            var query = $('#search-input').val();
        
            $.ajax({
                url: '@Url.Action("Search", "Product")',
                type: 'GET',
                data: { request: query },
                success: function (result) {
                    $('#search-results').html(result);
                },
                error: function () {
                    $('#search-results').html('<p>An error occurred</p>');
                }
            });
        });
    });


});




