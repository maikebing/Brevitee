$(function () {
    var iq = qi("Analytics"),
        highId = 1;

    function getNextSet(){

        _.act("Images", "GetSome", {count: 2, fromId: 1}, function(d){
            if(d.Success){
                highId = d.Data[d.Data.length - 1].Id;
                //alert(highId);
                _.each(d.Data, function(img, i){
                    $("#image" + i + "Ph").hide();
                    $("#image" + i + "").attr("src", img.Url).show();
                })
            }else{
                $("#errors").text(d.Message).show(bam.helloEffect());
            }
        });

    }

    /**
     * Used by sdo.getItem to extract the Url from the img tags
     * @param el
     * @returns {*|jQuery}
     */
    function getUrl(el){
        return $(el).attr("src");
    }

    function setUrl(el, v){
        $(el).attr("src", v);
    }

    getNextSet();
});