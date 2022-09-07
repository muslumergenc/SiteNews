(function ($) {
    "use strict";
    var inews = {
        initialize: function () {
            this.pageLoader();
            this.toTop();
            this.carousel();
            this.stickySidebar();
            this.youtubeVideo();
            this.tab();
            this.tabPanel();
            this.bgImage();
            this.skyicon();
            this.progresber();
        },
// -------------------------------------------------------------------------- //
// Page loader
// -------------------------------------------------------------------------- //  
        pageLoader: function () {
            $(".se-pre-con").fadeOut("slow");
        },
// -------------------------------------------------------------------------- //
// Back to top
// -------------------------------------------------------------------------- //  
        toTop: function () {
            $('body').append('<div id="toTop" class="btn back-top"><span class="ti-arrow-up"></span></div>');
            $(window).on("scroll", function () {
                if ($(this).scrollTop() !== 0) {
                    $('#toTop').fadeIn();
                } else {
                    $('#toTop').fadeOut();
                }
            });
            $('#toTop').on("click", function () {
                $("html, body").animate({
                    scrollTop: 0
                }
                , 600);
                return false;
            });
        },
// -------------------------------------------------------------------------- //
//owlCarousel
// -------------------------------------------------------------------------- //  
        carousel: function () {
            //NewsTicker
            $('.news-ticker').owlCarousel({
                loop: true,
                items: 1,
                dots: false,
                animateOut: 'slideOutDown',
                animateIn: 'flipInX',
                autoplay: true,
                autoplayTimeout: 5000, //Set AutoPlay to 4 seconds
                autoplayHoverPause: true,
                nav: true,
                navText: [
                    "<i class='ti-angle-left'></i>",
                    "<i class='ti-angle-right'></i>"
                ]
            });
            //NewsTicker rtl
            $('.news-ticker-rtl').owlCarousel({
                rtl: true,
                loop: true,
                items: 1,
                dots: false,
                animateOut: 'slideOutDown',
                animateIn: 'flipInX',
                autoplay: true,
                autoplayTimeout: 5000, //Set AutoPlay to 4 seconds
                autoplayHoverPause: true,
                nav: true,
                navText: [
                    "<i class='ti-angle-left'></i>",
                    "<i class='ti-angle-right'></i>"
                ]
            });
            //slider
            $('#owl-slider').owlCarousel({
                loop: true,
                items: 1,
                dots: true,
                animateOut: 'fadeOut',
                animateIn: 'fadeIn',
                autoplay: true,
                autoplayTimeout: 4000, //Set AutoPlay to 4 seconds
                autoplayHoverPause: true,
                nav: true,
                navText: [
                    "<i class='ti-angle-left'></i>",
                    "<i class='ti-angle-right'></i>"
                ]
            });
            //slider rtl
            $('#owl-slider-rtl').owlCarousel({
                rtl: true,
                loop: true,
                items: 1,
                dots: true,
                animateOut: 'fadeOut',
                animateIn: 'fadeIn',
                autoplay: true,
                autoplayTimeout: 4000, //Set AutoPlay to 4 seconds
                autoplayHoverPause: true,
                nav: true,
                navText: [
                    "<i class='ti-angle-left'></i>",
                    "<i class='ti-angle-right'></i>"
                ]
            });
            //Featured carousel
            $('#featured-owl').owlCarousel({
                loop: true,
                nav: false,
                dots: false,
                lazyLoad: true,
                autoplay: true,
                autoplayTimeout: 4000, //Set AutoPlay to 4 seconds
                autoplayHoverPause: true,
                responsive: {
                    0: {
                        items: 1
                    }
                    , 479: {
                        items: 2
                    }
                    , 768: {
                        items: 2
                    }
                    , 980: {
                        items: 3
                    }
                    , 1199: {
                        items: 4
                    }
                }
            });
            //Featured carousel rtl
            $('#featured-owl-rtl').owlCarousel({
                rtl: true,
                loop: true,
                nav: false,
                dots: false,
                lazyLoad: true,
                autoplay: true,
                autoplayTimeout: 4000, //Set AutoPlay to 4 seconds
                autoplayHoverPause: true,
                responsive: {
                    0: {
                        items: 1
                    }
                    , 479: {
                        items: 2
                    }
                    , 768: {
                        items: 2
                    }
                    , 980: {
                        items: 3
                    }
                    , 1199: {
                        items: 4
                    }
                }
            });
            //Post carousel
            $('.post-slider').owlCarousel({
                items: 1,
                loop: true,
                dots: false,
                animateOut: 'fadeOut',
                animateIn: 'fadeIn',
                nav: true,
                navText: [
                    "<i class='ti-angle-left'></i>",
                    "<i class='ti-angle-right'></i>"
                ]
            });
            //Post carousel rtl
            $('.post-slider-rtl').owlCarousel({
                rtl: true,
                loop: true,
                dots: false,
                animateOut: 'fadeOut',
                animateIn: 'fadeIn',
                items: 1,
                nav: true,
                navText: [
                    "<i class='ti-angle-left'></i>",
                    "<i class='ti-angle-right'></i>"
                ]
            });
        },
// -------------------------------------------------------------------------- //
// Sticky Sidebar
// -------------------------------------------------------------------------- //
        stickySidebar: function () {
            $('.main-content, .rightSidebar, .leftSidebar').theiaStickySidebar({
                additionalMarginTop: 30
            });
        },
// -------------------------------------------------------------------------- //
// Youtube video
// -------------------------------------------------------------------------- //    
        youtubeVideo: function () {
            // This key only works for this demo on newspaper
            // You must create your own at:
            // https://developers.google.com/youtube/v3/getting-started
            window.api_key='AIzaSyDbVOKeaP-xOLHBEXIcKTyb5ehdjOoptlE';
            // Start two players by ID, with default settings
            $('#rypp-demo-1').rypp(api_key, {
                update_title_desc: true, // Default false
                autoplay: false, autonext: false, loop: false, mute: false, debug: false
            });
        },
// -------------------------------------------------------------------------- //
// Tab 
// -------------------------------------------------------------------------- //    
        tab: function () {
            $(".weather-week>div.list-group>a").click(function (e) {
                e.preventDefault();
                $(this).siblings('a.active').removeClass("active");
                $(this).addClass("active");
                var index = $(this).index();
                $("div.bhoechie-tab>div.weather-temp-wrap").removeClass("active");
                $("div.bhoechie-tab>div.weather-temp-wrap").eq(index).addClass("active");
            });
        },
// -------------------------------------------------------------------------- //
// Tab panel 
// -------------------------------------------------------------------------- //    
        tabPanel: function () {
            $('.collapse.in').prev('.panel-heading').addClass('active');
            $('#accordion').on('show.bs.collapse', function (a) {
                $(a.target).prev('.panel-heading').addClass('active');
            }
            ).on('hide.bs.collapse', function (a) {
                $(a.target).prev('.panel-heading').removeClass('active');
            }
            );
        },
// -------------------------------------------------------------------------- //
// Tab panel 
// -------------------------------------------------------------------------- //    
        bgImage: function () {
            //Background image
            $(".bg-img").css('backgroundImage', function () {
                var bg = ('url(' + $(this).data("image-src") + ')');
                return bg;
            });
        },
// -------------------------------------------------------------------------- //
// Tab panel 
// -------------------------------------------------------------------------- //    
        skyicon: function () {
            //Skyicon
            var icons = new Skycons({"color": "#fff"}),
                    list = [
                        "clear-day", "clear-night", "partly-cloudy-day",
                        "partly-cloudy-night", "cloudy", "rain", "sleet", "snow", "wind",
                        "fog"
                    ],
                    i;

            for (i = list.length; i--; )
                icons.set(list[i], list[i]);

            icons.play();
        },
// -------------------------------------------------------------------------- //
// Progresber
// -------------------------------------------------------------------------- //    
        progresber: function () {
            var el = document.getElementsByClassName('progressber'), l = el.length;
            for (var i = 0;
                    i < l;
                    i++) {
                var options = {
                    percent: el[i].getAttribute('data-percent'), size: el[i].getAttribute('data-size') || 60, lineWidth: el[i].getAttribute('data-line') || 4
                }
                ;
                var canvas = document.createElement('canvas');
                var span = document.createElement('span');
                span.textContent = options.percent + '%';
                if (typeof (G_vmlCanvasManager) !== 'undefined') {
                    G_vmlCanvasManager.initElement(canvas);
                }
                var ctx = canvas.getContext('2d');
                canvas.width = canvas.height = options.size;
                el[i].appendChild(span);
                el[i].appendChild(canvas);
                ctx.translate(options.size / 2, options.size / 2); // change center
                var radius = (options.size - options.lineWidth) / 2;
                var drawCircle = function (color, lineWidth, percent) {
                    percent = Math.min(Math.max(0, percent || 1), 1);
                    ctx.beginPath();
                    ctx.arc(0, 0, radius, 0, Math.PI * 2 * percent, false);
                    ctx.strokeStyle = color;
                    ctx.lineCap = 'round';
                    ctx.lineWidth = lineWidth;
                    ctx.stroke();
                }
                ;
                drawCircle('transparent', options.lineWidth, 100 / 100);
                drawCircle('#eb0254', options.lineWidth, options.percent / 100);
            }
        }
    };
    // Initialize
    $(document).ready(function () {
        inews.initialize();
        $("#datepicker").datepicker();
    });
    // Reset on resize
    $(window).on("load", function () {
        inews.pageLoader();
    });
}(jQuery));