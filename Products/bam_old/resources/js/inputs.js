(function(b, $, _){
    $.fn.check = function(){
        return $(this).attr("checked", "checked");
    }

    $.fn.uncheck = function(){
        return $(this).removeAttribute("checked");
    }
})(bam || {}, jQuery, _);
