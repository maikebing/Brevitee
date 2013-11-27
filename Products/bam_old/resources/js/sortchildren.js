(function ($) {
    var n = 0
    function num() {
        return n++;
    }
    function sortChildren(el, opts) {
        var result = {},
            arr = [],
            opts = $.extend({ sortBy: "text", order: "asc" }, opts);

        $(el).children().each(function (i, v) {
            var txt,
                suffix,
                key;
            if ($.isFunction(opts.sortBy)) {
                txt = opts.sortBy(v);
            } else if (typeof opts.sortBy == "string" && $.isFunction($.fn[opts.sortBy])) {
                txt = $(v)[opts.sortBy]();
            }
            suffix = num();
            key = txt + suffix;
            arr.push(key);
            result[key] = v;
        });

        arr.sort();
        if (opts.order == "desc") {
            arr.reverse();
        }
        $(el).empty();
        for (var i = 0; i < arr.length; i++) {
            var txt = arr[i],
                ch = result[txt];
            $(el).append(ch);
        }
    }

    $.fn.sortChildren = function (opts) {
        return $(this).each(function () {
            sortChildren($(this), opts);
        });
    }
})(jQuery)