(function (dsgn, b, $, _) {
    var selectors ={
        colorScheme: "#colorScheme"
    }

    function setColorScheme(colors) {
        $(selectors.colorScheme).empty();
        $.each(colors, function (i, v) {
            b.partial("hexcolor", v, function (r) {
                $(selectors.colorScheme).append(r);
            });
        });

        $(selectors.colorScheme).dataSetPlugins();
    }

    $(function () {
        onDocumentReady();
        var originalColors = {};
        /*
        initialize the color scheme
         */
        dsgn.getColorScheme(function (d) {
            if (!d.success) {
                throw d.message;
            } else {
                originalColors = d.data.Colors;
                setColorScheme(originalColors);
            }
        });

        /**
         * Used to configure the edit
         * @param o
         */
        dsgn.hexConfig = function (o) {
            $(o.input)
                .css("display", "inline")
                .addClass("editInput");
        }

        dsgn.changeColor = function (o) {
            if (o.original != o.current) {
                if(!_.isHexColor(o.current)){

                }else{
                    $("#" + o.domId).css("background-color", o.current);
                }
            }
        }

        dsgn._saveColorScheme = function(){
            var colors = _.getItems("#colorScheme");
            dsgn.saveColorScheme({Colors: colors}, function(d){
                if (!d.success) {
                    throw d.message;
                } else {
                    setColorScheme(d.data.Colors);
                }
            });
        }

        dsgn._resetColorScheme = function(){
            setColorScheme(originalColors)
        }
    });

    function onDocumentReady() {
        
        $("#addColorHex").change(function () {
            $("#addColorContainer").css("background-color", $(this).val());
        });
        $("[data-plugin]").dataSetPlugins();
    }
})(design || {}, bam, jQuery, _)