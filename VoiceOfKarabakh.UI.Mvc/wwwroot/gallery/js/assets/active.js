(function($){
	'use strict';
	var alime_window=$(window);

	if($.fn.imagesLoaded){
		$('.alime-portfolio').imagesLoaded(function(){$('.portfolio-menu').on('click','button',function(){
			var filterValue=$(this).attr('data-filter');$grid.isotope({filter:filterValue});});
		    var $grid=$('.alime-portfolio').isotope({itemSelector:'.single_gallery_item',percentPosition:true,masonry:{columnWidth:'.single_gallery_item'}});});}
		$('.portfolio-menu button.btn').on('click',function(){$('.portfolio-menu button.btn').removeClass('active');
		$(this).addClass('active');})
$('.search-btn').on('click',function(){$('.search-form').toggleClass('search-form-active');
})

if($.fn.magnificPopup){$('.video-play-btn').magnificPopup({type:'iframe'});
$('.portfolio-img').magnificPopup({type:'image',gallery:{enabled:true,preload:[0,2],navigateByImgClick:true,tPrev:'Previous',tNext:'Next'}});
}
if($.fn.tooltip){$('[data-toggle="tooltip"]').tooltip();
}
if(alime_window.width()>767){new WOW().init();
}
if($.fn.jarallax){$('.jarallax').jarallax({speed:0.5});
}

$('a[href="#"]').on('click',function($){$.preventDefault();});})(jQuery);