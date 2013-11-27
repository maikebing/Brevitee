alert("this file is deprecated and should not be used.  use schema.org.js instead");
$(document).ready(function () {
    function getRadioValue(ele) {
        var val = "";
        $("input[type='radio']", ele).each(function (k, el) {
            if (el.checked) {
                val = $(el).val();
                if (val === null || val === undefined) {
                    val = $(el).attr("value");
                }
            }
        });

        return val;
    }

    function setRadioValue(ele, o) {
        for (prop in o) {
            $("input[type='radio']", ele).each(function (k, v) {
                var name = $(v).attr("name");
                if (name == prop) {
                    var val = $(v).val();
                    if (val === null || val === undefined) {
                        val = $(v).attr("value");
                    }

                    if (val == o[prop]) {
                        $(v).attr("checked", "checked");
                    }
                }
            });
        }
    }

    function getValue(el) {
        // get the type of the parameter
        var dataset = $(el).data("dataset"),
            paramType = dataset.type,
            val = dataset.value;
        // text
        if (paramType == "string" || paramType == "") {
            val = $(el).val();
            if (!val) {
                val = $(el).attr("value");
            }
            if (!val) {
                val = $(el).text();
            }
            if (val === null || val === undefined) {
                val = "";
            }
        } else if (paramType == "boolean") {
            val = el.checked;
        } else if (paramType == "int") {
            val = $(el).val();
            if (val === null || val === undefined) {
                val = $(el).attr("value")
            }
            val = parseInt(val);
            if (isNaN(val)) {
                val = 0;
            }
        } else if (paramType == "enum") {
            val = getRadioValue(el);
            val = parseInt(val);
        } else if (paramType == "datetime") {
            val = new Date($(el).val());
        } else if (paramType == "object") {
            val = deserializeObject(el);
        }

        return val;
    }

    function setValue(el, o) {
        // text
        $("[data-property-name]", $(el)).each(function (i, e) {
            // get the type of the parameter
            var dataset = $(e).data("dataset"),
                paramType = dataset.type,
                propName = dataset.propertyName,
                value = o[propName];

            if (paramType == "string" || paramType == "" || paramType == "int" || paramType == "datetime") {
                $(e).val(value).attr("value", value);
            } else if (paramType == "boolean") {
                if (value) {
                    $(e).attr("checked", "checked");
                } else {
                    $(e).removeAttr("checked");
                }
            } else if (paramType == "enum") {
                var arg = {};
                arg[propName] = value;
                setRadioValue(el, arg);
            } else if (paramType == "object") {
                setValue(el, value);
            }
        });
        return val;
    }

    function deserializeObject(el) {
        var obj = {};
        $("[data-property-name]", el).each(function (k, v) {
            var dataset = $(this).data("dataset");
            obj[dataset.propertyName] = getValue(v);
        });

        return obj;
    }

    function deserializeParameters(el) {
        var params = [];
        $("[data-parameter-name]", el).each(function (k, v) {
            params.push(getValue(v));
        });
        return params;
    }

    $.deserializeObject = deserializeObject; // not chainable
    $.deserializeParameters = deserializeParameters; // not chainable

    function refreshable(el, options) {
        var config = {
            method: null,
            proxy: null,
            className: null,
            params: null,
            refreshOn: null
        },
            dataset = $(el).data("dataset"),
            the = $(el);

        $.extend(config, options);
        $.extend(config, dataset);
        $.extend(config, $.dataSetOptions(el, options));

        if ((config.className === null || config.className === undefined) &&
            (config.proxy !== null && config.proxy !== undefined)) {
            config.className = window[config.proxy].className;
        }

        function fn(conf) {
            if (!conf) {
                conf = the.data("refresh_options");
            }
            var opts = $.extend({ success: function (r) { the.html(r).show(); }, complete: bam.init }, conf),
                args = $.deserializeParameters($("#" + config.params));
            if (opts.args) {
                if ($.isArray(opts.args)) {
                    args = opts.args;
                } else {
                    args = [opts.args];
                }
            }
            $.extend(opts, $.dataSetOptions(el, options));

            bam.invoke(config.className, config.method, args, "html", opts);
        }

        if (config.refreshOn !== null && config.refreshOn !== undefined) {
            the[config.refreshOn](fn);
        }

        the.data("refresh", fn);
        the.data("refresh_options", config);
    }

    $.fn.refreshable = function (options) {
        return $(this).each(function () {
            var the = $(this);
            refreshable(the, options);
        });
    }

    $.fn.refresh = function (options) {
        return $(this).each(function () {
            var the = $(this);
            if ($.isFunction(the.data("refresh"))) {
                if (options === null || options === undefined) {
                    options = {};
                }
                $.extend(options, the.data("refresh_options"));
                the.data("refresh")(options);
            }
        });
    }

    function methodButton(el, options) {
        $(el).one("click", function () {
            var dataset = $(this).data("dataset"),
                className = dataset.className,
                method = dataset.method,
                paramSrc = dataset.parameterSource,
                args = [],
                the = $(this),
                options = $.dataSetOptions(the),
                config = {
                    format: "html",
                    beforeSend: function () { }
                };

            if (paramSrc) {
                args = deserializeParameters($("#" + paramSrc));
            }

            if (options.preProcess && $.isFunction(options.preProcess)) {
                if (!options.preProcess(args)) {
                    return;
                }
            }

            function complete(r) {
                if ($.fn.spin) {
                    the.spin(false);
                }
            }

            if ($.isFunction(options.complete)) {
                var c = options.complete;
                options.complete = function (r) {
                    complete(r);
                    c(r);
                }
            }

            $.extend(config, options);
            if ($.fn.spin) {
                the.spin({
                    length: (the.height() / 2) - 3,
                    radius: 3
                });
            }
            bam.invoke(className, method, args, config.format, config);
        });
    }

    $.fn.methodButton = function (options) {
        return this.each(function () {
            var the = $(this);
            methodButton(the, options);
        });
    }

    bam.initFunctions = [];

    bam.addInit = function (fn) {
        bam.initFunctions.push(fn);
    }

    bam.init = function () {
        $("[role=button]").button();
        $("body").dataSet().dataSetPlugins().dataSetEvents(); // initialize data- attributes to dataset
        $("[data-plugin=datepicker]").datepicker("setDate", new Date());
        $.each(bam.initFunctions, function (i, v) {
            v();
        });
    }

    bam.init();
})