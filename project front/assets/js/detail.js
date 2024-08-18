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
