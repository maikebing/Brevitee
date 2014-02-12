
function effectViewModel(el) {
    $(el).off("change").on("change", function () {
        var ef = $(this).val();
        bam.app("localhost").setHelloEffect(ef, true);
        bam.app("localhost").setGoodByeEffect(ef, true);
    });
}