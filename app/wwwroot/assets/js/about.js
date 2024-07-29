document.addEventListener('DOMContentLoaded', function() {
  
    var menu = document.querySelector('.menu');

  
    menu.classList.add('hidden');

   
    window.onscroll = function() {
        if (window.scrollY > 100) { 
            menu.classList.add('sticky');
            menu.classList.remove('hidden');
        } else {
            menu.classList.remove('sticky');
            menu.classList.add('hidden');
        }
    };
});

