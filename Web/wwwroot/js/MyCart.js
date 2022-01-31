function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

const CookieVal = getCookie("cartItem")

let ProductIds = CookieVal !== "" ? CookieVal.split("-") : [];

$(".btn-add-cart").click(function () {
    Swal.fire(
        'Good job!',
        'Product Added',
        'success'
    )
    const ProductId = $(this).attr("pro-id")
    ProductIds.push(ProductId);
    setCookie("cartItem", ProductIds.join("-") , 1)
    

})

$(".plus-btn").on("click", function () {
    debugger;
    const inputVal = Number($(this).parent().find("input").val()) + 1;
    if (inputVal !== 1) {
        $(".minus-btn").css({ "pointer-events": "auto" })
    }
    const productId = $(this).parent().attr("pro-id")
    ProductIds = ProductIds.filter(x => x !== productId)
    for (let i = 0; i < inputVal; i++) {
        ProductIds.push(productId)
    }
    setCookie("cartItem", ProductIds.join("-"), 1)

    const Price = parseFloat($(this).parent().attr("pro-price"))
    $(this).parents("tr").find(".subtotal-amount").text("$" + Price * inputVal);
   
   
})


$(".minus-btn").on("click", function () {
    const inputVal = Number($(this).parent().find("input").val()) - 1
    if (inputVal === 1) {
        $(this).css({ "pointer-events": "none" })
    }
    const productId = $(this).parent().attr("pro-id")
    ProductIds = ProductIds.filter(x => x !== productId)
    for (let i = 0; i < inputVal; i++) {
        ProductIds.push(productId)
    }
    setCookie("cartItem", ProductIds.join("-"), 1)

    const Price = parseFloat($(this).parent().attr("pro-price"))
    $(this).parents("tr").find(".subtotal-amount").text("$" + (Price * inputVal));
   
})
