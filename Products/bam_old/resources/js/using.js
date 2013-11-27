var using = (function ($,_) {
    var scripts = {};
    function script(sp, cb) {
        if(!scripts[sp]){
            var scr = $("<script>").attr("type", "text/javascript").attr("src", sp).on("load", function(){
                loaded(sp);
                cb(sp);
            });
            $("head").append(scr);
        }else{
            cb(sp);
        }
    }

    function loaded(sp) {
        scripts[sp] = true;
    }

    return {
        script: script
    }
}) (jQuery, _);