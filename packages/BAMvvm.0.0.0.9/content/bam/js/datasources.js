var datasources = (function($, _, b, d, q, w){
    var sources = {};

    function setDataSource(name, data){
        sources[name] = data;
    }

    function getDataSource(name, data){
        return sources[name];
    }

    return function(opts){
        var config = {

        };

        $.extend(config, opts);

        return {
            getDataSource: getDataSource,
            setDataSource: setDataSource
        }
    }
})(jQuery, _, bam, dao, qi, window || {});