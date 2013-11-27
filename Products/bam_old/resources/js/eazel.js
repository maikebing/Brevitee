var eazel = (function (b, $, _) {
    function edit(el, opts) {
        var o = { // the edit object/configuration
                config: function () { },
                change: function (n, o) { } // new and old values
            },
            otxt = $(el).text(),
            the = $(el);

        o.el = the;
        o.input = $("<input>")
            .attr("type", "text")
            .val(otxt)
            .on("blur", function () {
                var ntxt = $(this).val();
                the.text(ntxt).show();
                o.config(o);
                $(this).hide();
                if (ntxt !== otxt) {
                    o.current = ntxt;
                    if(_.isString(o.change)){
                        o.change = _.getFunction(o.change);
                        if(!_.isFunction(o.change)){
                            o.change = function(){};
                        }
                    }
                    o.change(o);
                }
            });

        o.config(o);
        $(o.input).hide();
        $.extend(o, opts);
        o.original = otxt;
        o.current = "";

        $(el)
            .data("eazel.edit", o)
            .after(o.input)
            .on("click", function () {
                $(this).hide();
                o.config(o);
                $(o.input).show().select();
            });
    }

    $.fn.edit = function (options) {
        return $(this).each(function () {
            var the = $(this);
            edit(the, options);
        });
    }

    function isHexColor(hex) {
        return /^#[0-9a-f]{3}([0-9a-f]{3})?$/i.test(hex);
    }

    var value = {
        edit: edit,
        isHexColor: isHexColor
    }

    _.mixin(value);

    return value;
})(bam, jQuery, _);