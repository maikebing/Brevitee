/**
 * Convenience 'reminder' to use the mediator pattern.
 * Basic wrapper around jQuery's 'on' and 'trigger'
 * @type {*}
 */
var mediator = (function(b, $, _, win){
    var events = {};
    /**
     * Register an event with the specified name
     * @param name
     */
    function on(name, fn){
        $(events).on(name, fn);
    }

    function trigger(name){
        $(events).trigger(name)
    }

    var result = {
        on: on
    }
    win.mediator = result;

    return result;
})(bam || {}, jQuery, _, window || {})
