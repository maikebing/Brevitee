$(document).ready(function () {

    //QUnit.asyncTest("save test", function (assert) {
    //    for (ent in dao.entities) {
    //        var ctor = dao.ctor(ent);
    //        assert.ok(!_.isNull(ctor), ent + " ctor function should not be null");

    //        var inst = new ctor();
    //        assert.ok(!_.isNull(inst));

    //        var pk = inst.pk(),
    //            fks = inst.fks();

    //        for (var p in inst) {
    //            if (p !== pk && !_.isFunction(inst[p]) && !_.isObject(inst[p])) {
    //                inst[p] = _.randomString(5);
    //            }
    //        }
    //        inst.save({
    //            success: function (d) {
    //                assert.ok(d.success, d.message);
    //                for (var p in inst) {
    //                    if (p !== pk && !_.isFunction(inst[p]) && !_.isObject(inst[p])) {
    //                        assert.ok(inst[p] == d[p], p + " should be same after save");
    //                    }
    //                }
    //            },
    //            error: function (d) {
    //                assert.ok(false, "an error occurred saving " + prop);
    //            },
    //            complete: function () {
    //                QUnit.start();
    //            }
    //        });
    //    }
    //});
});
