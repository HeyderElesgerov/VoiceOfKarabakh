var module_28345597914 = function() {
    var s = {};
    i18n_getmessage = function() { return hs_i18n_getMessage(s, hsVars.language, arguments) }, i18n_getlanguage = function() { return hsVars.language }, $((function() { $(".js-toast").appendTo("body"), $(".js-toast-cta").on("click", (function() { $(".js-toast-popup").toggleClass("active") })) })), $(document).on("scroll", (function() { $(this).scrollTop() > 400 ? $(".js-toast").addClass("toast-bar--is-open") : ($(".js-toast").removeClass("toast-bar--is-open"), $(".js-toast-popup").removeClass("active")) }))
}();