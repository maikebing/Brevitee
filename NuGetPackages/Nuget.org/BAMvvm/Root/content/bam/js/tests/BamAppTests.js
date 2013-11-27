$(document).ready(function(){
    "use strict";



    QUnit.test("function should be an event", function(assert){
        var result = false,
            eventSource = { go: function(){
                $(this).trigger("go");
            }};

        $(eventSource).on("go", function(){
            result = true;
        })

        assert.equal(result, false);
        eventSource.go();
        assert.equal(result, true);
    })

    QUnit.test("Should create and play transition", 8, function(assert){
        var ran = false,
            startCalled = false,
            endCalled = false,
            th = application.createPageTransition("testTransition", "first", "second", function(t){
                assert.equal(t.name, "testTransition");
                assert.equal(t.from, "first");
                assert.equal(t.to, "second");
                ran = true;
            });

        assert.equal("testTransition", th.name);
        $(th)
            .on("start", function(){
                startCalled = true;
            })
            .on("end", function(){
                endCalled = true;
            });

        assert.equal(startCalled, false);
        assert.equal(endCalled, false);
        th.play();
        assert.equal(startCalled, true);
        assert.equal(endCalled, true);
    })
});