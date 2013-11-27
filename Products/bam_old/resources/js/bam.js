/*

* Copyright 2011, Brevitee
* Available via the MIT or new BSD license.

*/

var bam = {};

(function (b, $, dst, _) {
    var appName = "";
    /**
     * Sets the name of the current application.
     * Should be used specifically to help determine
     * whether the app is in the root or deployed as a
     * subdirectory
     * @param {String} n The name to set the application name to
     */
    b.setAppName = function (n) {
        var m = n.match("/$");
        if ((m === null || m === undefined) && !(n === null || n === undefined)) {
            n = n + "/";
        }
        appName = n;
    };

    /**
     * Gets a random letter from a to z
     *
     * @return {String}
     */
    b.randomLetter = function(){
        var chars = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];
        return chars[Math.floor(Math.random() * 26)];
    }

    /**
     * Gets a random number from 0 to 9
     *
     * @return {number}
     */
    b.randomNumber = function(){
        var nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 0];
        return nums[Math.floor(Math.random() * 10)];
    }

    /**
     *  Gets a random boolean
     *
     * @return {Boolean}
     */
    b.randomBool = function(){
        return Math.random() > .5;
    }

    /**
     * Gets a random string of the specified length
     *
     * @param {number} l the length of the string to return
     * @return {String}
     */
    b.randomString = function(l){
        var val = "";
        for(var i = 0; i < l;i++){
            if(b.randomBool()){
                val += b.randomLetter();
            }else{
                val += b.randomLetter().toUpperCase();
            }
        }
        return val;
    }

    /**
     * Gets a date from the specified string
     *
     * @param s a json date string
     * @return {Date}
     */
    b.toDate = function(s) {
        return new Date(parseInt(s.substr(6)));
    }

    /**
     * Gets a local date/time from the specified string
     *
     * @param s
     * @return {Date}
     */
    b.toLocal = function(s) {
        return new Date(b.toDate(s).toLocaleString() + " UTC");
    }

    /**
     * Gets the root of the current app by appending the appName to the http
     * protocol and host.
     *
     * @return {String}
     */
    b.getAppRoot = function () {
        return window.location.protocol + "//" + window.location.host + "/" + appName;
    }

    /**
     * Invokes the specified method on the specified class using the specified
     * arguments returning the specified format.  The specified class must
     * be registered to handle invoke requests.
     * @param {String} className The name of the class that the method to invoke is defined on
     * @param {String} method The name of the method to be invoked
     * @param args the argument or array of arguments to pass
     * @param format the format to return the data in
     * @param options overrides to the default options argument passed to jQuery's ajax call
     * @return {*}
     */
    b.invoke = function (className, method, args, format, options) {
        if (!$.isArray(args)) {
            var a = [];
            a.push(args);
            args = a;
        }
        var strings = [],
            orig = function () { }; // the original complete function if provided

        for (var i = 0; i < args.length; i++) {
            strings.push(JSON.stringify(args[i]));
        }
        var config = {
                url: b.getAppRoot() + className + "/" + method + "." + format + "?wrapped=yes",
                dataType: format,
                data: JSON.stringify(strings),
                global: false,
                success: function () { },
                error: function () { },
                type: "POST",
                contentType: "application/json; charset=utf-8"
            };

        $.extend(config, options);
        if (typeof config.view === "string") {
            var m = config.url.match("&$"),
                a = (m !== null && m.toString() !== config.url) ? "&" : "";
            config.url += a + "view=" + config.view;
        }

        function complete() {
            if ($.isFunction(orig)) {
                orig();
            }
        }

        if ($.isFunction(config.complete)) {
            orig = config.complete;
        }
        
        config.complete = complete;
        
        return $.ajax(config);
    }

    /**
     * Renders the specified data using the specified dust template.
     * The dust template should be prefixed by 'partial_'
     * @param viewName
     * @param data
     * @param opts
     */
    b.partial = function (viewName, data, opts) {
        var conf = {
            error: function (e) {
                throw e;
            },
            complete: function (r) { }
        };
        if (_.isObject(opts)) {
            _.extend(conf, opts);
        }
        dst.render("partial_" + viewName, data, function(e, r){
            if(e){
                conf.error(e);
            } else {
                if (_.isString(opts)) { // assume its jquery selector
                    $(opts).html(r);
                } else if (_.isFunction(opts)) { // assume its the complete handler
                    opts(r);
                } else {
                    conf.complete(r);
                }
            }
        })
    }

    /**
     * Alias for partial
     * @type {Function}
     */
    b.render = b.partial;

    $(function(){
        if(_ !== undefined && _.mixin !== undefined){
            _.mixin(b);
        }
    })
})(bam, jQuery, dust, _)