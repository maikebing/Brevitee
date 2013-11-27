(function ($) {
    $.fn.enterkey = function (fn) {
        return $(this).keyup(function (ev) {
            if (ev.keyCode == 13 && $.isFunction(fn)) {
                fn($(this));
            };
        });
    }
})(jQuery)