var queryString = (function ($, _) {
    var utils = {
        trimEnd: function (s, c) {
            if (c) {
                return s.replace(new RegExp(utils.escapeRegExp(c) + "*$"), '');
            } else {
                return s.replace(/\s+$/, '');
            }
        },
        trimStart: function (s, c) {
            if (c) {
                return s.replace(new RegExp("^" + utils.escapeRegExp(c) + "*"), '');
            } else {
                return s.replace(/^\s+/, '');
            }
        },
        escapeRegExp: function (s) {
            return s.replace(/[.*+?^${}()|[\]\/\\]/g, "\\$0");
        }
    }

    function set(k, v, q) {
        q = q || window.location.search;
        q = q + "&";
        var re = new RegExp("[?|&]" + key + "=.*?&");
        if (!re.test(q)) {
            q += key + "=" + encodeURI(v);
        } else {
            q = q.replace(re, "&" + key + "=" + encodeURIComponent(v) + "&");
        }

        q = utils.trimStart(q, "&");
        q = utils.trimEnd(q, "&");
        return q[0] == "?" ? q : q = "?" + q;
    }

    function get(key, qs) {
        if (_.isUndefined(qs)) {
            qs = window.location.search;
        }
        var re = new RegExp("[?|&]" + key + "=(.*?)&"),
            matches = re.exec(qs + "&");

        if (!matches || matches.length < 2) {
            return "";
        }

        return decodeURIComponent(matches[1].replace("+", " "));
    }
    
    _.mixin(utils);

    return {
        get: get,
        set: set,
        fromQueryString: function (o) {
            for (prop in o) {
                o[prop] = get(prop);
            }
        }
    }
})(jQuery, _)