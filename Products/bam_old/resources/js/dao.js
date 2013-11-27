/*
Copyright 2012, Brevitee

Data Access Object
*/
var dao = (function ($, _) {
    function value(n, v, own) {
        var listeners = [];

        this.name = n;
        this.value = v;
        this.oldvalue = v;
        this.owner = own;

        this.get = function () {
            return this.value;
        }

        this.set = function (v) {
            this.oldvalue = this.value;
            this.value = v;
            if (listeners.length > 0 && this.oldvalue != this.value) {
                for (var i = 0; i < listeners.length; i++) {
                    listeners[i](this);
                }
            }
        }

        this.change = function (name, fn) {
            if (_.isFunction(name) && _.isUndefined(fn)) {
                fn = name;
                name = this.name;
            }
            if (name == this.name) {
                if (_.isFunction(fn)) {
                    listeners.push(fn);
                }
            }
        }
    }

    function observeArray(a) {
        var result = [];
        result.addListeners = [];

        result.toData = function () {
            var dataArr = [];
            _.each(this, function (d) {
                dataArr.push(d.toData());
            });
            return dataArr;
        }
        result.toJson = function () {
            return JSON.stringify(this.toData());
        }

        result.changeAny = function (name, fn) {
            _.each(this, function (d) {
                d.change(name, fn);
            });
        }

        result.change = result.changeAny;

        result.changeAt = function (i, p, fn) {
            if (_.isUndefined(fn) && _.isFunction(p)) {
                this[i].change(p);
            } else {
                this[i].change(p, fn);
            }
        }

        var opush = result.push;
        result.push = function () {
            var args = arguments;
            opush.apply(result, args);
            _.each(this.addListeners, function (f) {
                f(args);
            });
        }

        result.add = function (fn) {
            if (_.isFunction(fn)) {
                this.addListeners.push(fn);
            }
        }

        if (!_.isArray(a)) {
            result.push(observe(a));
        } else {
            _.each(a, function (o) {
                result.push(observe(o));
            });
        }
        return result;
    }

    function observe(o) {
        if (_.isArray(o)) {
            return observeArray(o);
        }
        var result = { actions: {}, listeners: [], properties: [] };
        result.toData = function () {
            var l = this.properties.length,
                the = this,
                result = {};

            _.each(this.properties, function (prop) {
                if (_.isObject(the[prop]) && _.isFunction(the[prop].toData)) {
                    result[prop] = the[prop].toData();
                } else {
                    result[prop] = the[prop]();
                }
            });
            return result;
        }

        result.toJson = function () {
            return JSON.stringify(this.toData());
        }

        for (var prop in o) {
            if (_.isObject(o[prop]) && !_.isFunction(o[prop])) {
                var setVal = observe(o[prop]);
                result[prop] = setVal;
                result[prop].value = new value(prop, setVal, result);
                result[prop].toString = function () {
                    return arguments.callee.value.value;
                }
                result.properties.push(prop);
            } else if (!_.isFunction(o[prop])) {
                var setVal = o[prop];
                function accessor(pName, val) {
                    if (!_.isNull(val) && !_.isUndefined(val)) {
                        this[pName].value.set(val);
                    }
                    return this[pName].value;
                }

                result[prop] = function (val) {
                    var p = arguments.callee.value.name;
                    return accessor.apply(result, [p, val]).value;
                }

                result[prop].value = new value(prop, setVal, result);

                result[prop].toString = function () {
                    return arguments.callee.value.value;
                }

                result.properties.push(prop);
            } else if (_.isFunction(o[prop])) {
                result.actions[prop] = o[prop];
            }
        }
        result.change = function (pName, fn) {
            if (_.isUndefined(fn) && _.isFunction(pName)) {
                for (var i = 0; i < result.properties.length; i++) {
                    var p = result.properties[i];
                    result[p].value.change(p, pName); // pName is a function here
                }
            } else {
                result[pName].value.change(pName, fn);
            }
        }
        return result;
    }

    function entity(n, i) {
        var loaded = false,
            name = n,
            id = i;

        this.value = {};

        var the = this;
        this.load = function (cb) {
            if(loaded){
                cb(the.value);
            }else{
                dao.entities[name].retrieve(id, function (d) {
                    the.value = d;
                    loaded = true;
                    cb(d);
                });
            }
        }
    }

    function getFks(t) {
        var vals = [];
        _.each(dao.fks, function (v) {
            if (v.ft == t) {
                vals.push(v);
            }
        });

        return vals;
    }

    function ctor(name) {
        return bam.getFunction(dao.entities[name].className);
    }

    function collection(o, pt, pk, ft, fk) {
        var loaded = false;
        this.owner = o;
        this.pt = pt;
        this.pk = pk;
        this.ft = ft;
        this.fk = fk;

        this.values = [];

        var the = this;
        this.getValues = function (cb) {
            if (!loaded) {
                var ctx = dao.entities[the.ft].ctx;
                qi(ctx).from(ft).where(fk + " = " + o[pk]).getItems(function (d) {
                    the.values = d;
                    loaded = true;
                    cb(the.values);
                });
            } else {
                cb(the.values);
            }
        }

        this.reload = function (cb) {
            loaded = false;
            the.getValues(cb);
        }
    }

    function missing() {
        alert("schema.org.js is not included");
    }

    var r = {
        ctor: ctor,
        value: value,
        collection: collection,
        getFks: getFks,
        entity: entity,
        observe: observe,
        observeArray: observeArray,
        bindData: function (selector, data) {
            alert("databinder not set");
        },
        fks: [],
        sdo: { // schema dot org
            item: missing,
            items: missing,
            getItem: missing,
            getItems: missing
        },
        entities: {} // placeholder for dao proxies
    }

    try {
        if (schema !== undefined) {
            r.sdo = schema;
            r.dataBinder = schema;
            r.dataBind = function (selector, data) {
                r.dataBinder.dataBind(selector, data);
            }
        }
    } catch (e) { }

    r.getItem = r.sdo.getItem;
    r.getItems = r.sdo.getItems;

    try {
        if (qi !== undefined) {
            r.qi = qi;
        } else {
            function missing() {
                alert("qi.js is not loaded");
            }
            r.qi = {
                from: missing,
                where: missing
            }
        }
    } catch (e) { }

    _.mixin(r);

    return r;
})(jQuery, _)