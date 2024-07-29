//first accordion
$(function(){

    setTimeout(function() {
        $(".loading-modal").addClass("loading-modal--hidden")
    }, 2500);

    $(".nav-bars .dropdown").on("click", function () {
        var self = $(this);
        if ($(window).width() < 768) {
            self.find(".dropdown-ul").slideToggle();
        }
    });

    $(".barsbtn").on('click', function () {
        if ($(window).width() < 768) {
            $(".nav-bars").slideToggle();
        }
    })

    var a = 0;
    $(window).scroll(function() {
        var oTop = $('#acheivements').offset().top - window.innerHeight;
        if (a == 0 && $(window).scrollTop() > oTop) {
            $('.counter-value').each(function() {
            var $this = $(this),
                countTo = $this.attr('data-count');
            $({
                countNum: $this.text()
            }).animate({
                countNum: countTo
                },

                {

                duration: 1500,
                easing: 'swing',
                step: function() {
                    $this.text(Math.floor(this.countNum));
                },
                complete: function() {
                    $this.text(this.countNum);
                   
                }

                });
            });
            a = 1;
        }
    });

    $(window).scroll(function() {    
        var scroll = $(window).scrollTop();
    
        if (scroll >= 200) {
            $("#nav-bar").addClass("nav-bar--fixed");
            $(".edulearn").addClass("edulearn--fix")
        } else {
            $("#nav-bar").removeClass("nav-bar--fixed");
            $(".edulearn").removeClass("edulearn--fix")
        }
    });

    $(".accordion li").on('click', function () {
        const otherElements = $(this).parent().find("li").not(this);
        otherElements.find(".accordion-body").slideUp("fast");
        otherElements.find(".accordion-header").removeClass("active-header");

        $(this).find(".accordion-header").toggleClass("active-header")
        $(this).find(".accordion-body").slideToggle("fast");
    });

    //modal!//
  
    $(".barsbtn").click(function(){
        $(".modal-content").css("right", "360px");

    });
    $("#nav-close").click(function(){
        $(".modal-content").css("right", "0");
    })

    $(".searchbtn").click(function(){
        $(".bg1-modal").css("display", "block");
      

    });
    $(".close5").click(function(){
        $(".bg1-modal").css("display", "none");
    })
    


    var owl = $('.main-slider');
    owl.owlCarousel({
        loop:true,
        margin:0,
        navSpeed:800,
        nav:false,
        items:1,
        touchDrag:false,
        autoplay:true,
        transitionStyle : "fade",
    });


    $('.second-slider').owlCarousel({
        loop:true,
        margin:0,
        navSpeed:800,
        margin:30,
        nav:true,
        responsive: {
            0: {
                items:1
            },
            768: {
                items:2
            },
            992: {
                items:3
            }
        }
    });

    $('.third-slider').owlCarousel({
        autoplay:false,
        loop:true,
        margin:30,
        nav:true,
        responsive:{
            0:{
                items:1
            },
            600:{
                items:2
            },
            800:{
              items:3  
            },
            1000:{
                items:4
            }
        }
    })

    $('.four-slider').owlCarousel({
        autoplay:false,
        loop:true,
        margin:30,
        nav:true,
        responsive:{
            0:{
                items:1
            },
            600:{
                items:2
            },
            1000:{
                items:2
            }
        }
    })

    $('.five-slider').owlCarousel({
        autoplay:true,
        loop:true,
        margin:85,
        nav:true,
        responsive:{
            0:{
                items:2
            },
            600:{
                items:3
            },
            1000:{
                items:5
            }
        }
    })
 })

     
