var application = (function($, _, d, b, w){
    "use strict";

    var pages ={}, // {<pageName1>: <pageObject>, <pageName2>: <pageObject>...}
        currentPage = "start",
        previousPage = currentPage,
        pageTransitions = {},// {from[f].to[t]}
        pageTransitionFilters = {},
        pageActivationHandlers = {},
        appData = {},
        contentSelector = "#pageContent",
        goodByeEffect = "fade",
        helloEffect = "fade",
        defaultPageTransitionHandler = function(tx, data){
            pages[tx.from].goodBye(function(){
                pages[tx.to].hello(data);
            });
        },
        defaultPageStateTransitionHandler = function(tx, data){
            var p = pages[currentPage];
            $("[data-state]", $(contentSelector)).hide(p.getStateGoodByeEffect(tx.from));
            $("[data-state=" + tx.to + "]", $(contentSelector)).show(p.getStateHelloEffect(tx.to));
        };


    // -- ctors
    /**
     * @constructor
     * @param from
     * @param to
     * @param fn
     */
    function transitionHandler(name, from, to, fn){
        var the = this,
            transition = fn;

        this.name = name;
        this.from = from;
        this.to = to;

        if(_.isString(name) && _.isString(from) && _.isFunction(to)){
            this.from = name;
            this.to = from;
            this.name = this.from + "to" + this.to;
            transition = to;
        }

        var _start = function(){
            $(the).trigger("start");
        };

        this.play = function(data){
            _start();
            transition(the, data);
            _end();
        };

        var _end = function(){
            $(the).trigger("end", the);
        }
    }

    /**
     * @constructor
     * @param n
     */
    function page(n, appName){
        var currentState = "initial",
            previousState = currentState,
            states = [],
            stateTransitions = {},
            stateGoodByeEffects = {},
            stateHelloEffects = {},
            stateTransitionFilters = {},
            uiState = null,
            the = this;

        this.name = n;
        this.stateGoodByeEffect = goodByeEffect;
        this.stateHelloEffect = helloEffect;
        this.contentSelector = contentSelector;
        this.content = null; // gets set in load line 100
        this.loaded = false;
        this.activated = false; // set by b.activate, determines if plugins have been activated and activation handlers called
        this.appName = appName;

        /**
         * data set by transitionTo (which is called by b.setState) to
         * allow data passing from state to state
         * @type {{}}
         */
        this.stateData = {};

        this.load = function(){
            return $.ajax({
                url: b.getAppRoot() + "bam/" + the.appName + "/pages/" + the.name + ".html?nocache=" + b.randomString(4),
                dataType: "html",
                success: function(html){
                    var p = document.createElement("iframe");
                    $(p).append(html);
                    the.title = $("title", p).text().trim();
                    the.content = $(the.contentSelector, p);
                    the.loaded = true;
                }
            }).promise();
        };

        this.hello = function(d){
            var _hello = function(){
                document.title = the.title;

                if(uiState != null && the.activated){
                    $(contentSelector).replaceWith(uiState).show(helloEffect);
                    the.activate();
                }else{
                    $(contentSelector).empty();
                    $(contentSelector).hide().append(the.content.html()).show({
                        effect: helloEffect,
                        complete: the.loadStates
                    });
                }
            };

            if(the.loaded){
                _hello();
            }else{
                the.load()
                    .done(function(){
                        _hello();
                    });
            }
        };

        /**
         * Play the hide effect and call the specified complete handler.
         * This method is called by the default transitionHandler and
         * should be called by custom implementations if a hide effect
         * is not provided.
         * @param complete function
         */
        this.goodBye = function(complete){
            if(_.isUndefined(complete)){
                complete = function(){};
            }
            if(the.activated){
                var uiClone = $(contentSelector).clone(),
                    $parent = $(contentSelector).parent();

                uiState = $(contentSelector).detach();
                $parent.append(uiClone);
            }

            $(contentSelector).show().hide({
                effect: goodByeEffect,
                complete: complete
            });
        };

        /**
         *
         */
        this.loadStates = function(){
            states = ["initial"];
            $("[data-state]", $(contentSelector)).each(function(i,v){
                var state = $(v).hide().attr("data-state");
                if(!_.contains(states, state)){
                    states.push(state);
                }
            });

            // create a state transition between all states including state to self
            _.each(states, function(s, i){ // state, index
                the.setStateTransition(s,s, defaultPageStateTransitionHandler);
                _.each(_.rest(states, i+1), function(ns){ // next state
                    the.setStateTransition(s, ns, defaultPageStateTransitionHandler);
                    the.setStateTransition(ns, s, defaultPageStateTransitionHandler);
                })
            });

            $("[data-state=initial]", $(contentSelector)).show(the.getStateHelloEffect("initial"));
            b.activatePlugins();
            b.activate(the);
        };

        this.activate = function(){
            b.activate(the);
        };

        /**
         * Set the name of the effect to use when the specified state
         * goes goodBye (is hidden/transitions away).
         * @param state
         * @param e
         */
        this.setStateGoodByeEffect = function(state, e){
            stateGoodByeEffects[state] = e;
        };

        this.getStateGoodByeEffect = function(state){
            return stateGoodByeEffects[state] || the.stateGoodByeEffect;
        };

        /**
         * Set the name of the effect to use when the specified state
         * says hello (is shown/transitions to).
         * @param state
         * @param e
         */
        this.setStateHelloEffect = function(state, e){
            stateHelloEffects[state] = e;
        };

        this.getStateHelloEffect = function(state){
            return stateHelloEffects[state] || the.stateHelloEffect;
        };

        this.setStateTransition = function(f, t, impl){
            stateTransitions.from = stateTransitions.from || {};
            stateTransitions.from[f] = stateTransitions.from[f] || {};
            stateTransitions.from[f].to = stateTransitions.from[f].to || {};
            stateTransitions.from[f].to[t] = stateTransitions.from[f].to[t] || {};
            stateTransitions.from[f].to[t] = new transitionHandler(f+"To"+t, f,t, impl);
        };

        /**
         * Set a filter to be run when transitioning from the specified
         * state f to the specified state t.  Can be used to stop the transition
         * or direct to a different state by analyzing the current page state.
         * @param f
         * @param t
         * @param filter
         */
        this.setStateTransitionFilter = function(f,t,filter){
            stateTransitionFilters.from = stateTransitionFilters.from || {};
            stateTransitionFilters.from[f] = stateTransitionFilters.from[f] || {};
            stateTransitionFilters.from[f].to = stateTransitionFilters.from[f].to || {};
            stateTransitionFilters.from[f].to[t] = stateTransitionFilters.from[f].to[t] || {};
            stateTransitionFilters.from[f].to[t] = filter;
        };

        this.transitionTo = function(ts, data){
            if(_.isUndefined(data)){
                data = the.stateData;
            }
            if(stateTransitions.from &&
                stateTransitions.from[currentState] &&
                stateTransitions.from[currentState].to &&
                stateTransitions.from[currentState].to[ts])
            {
                try{
                    if(_.isFunction(stateTransitionFilters.from[currentState].to[ts])){
                        var result = stateTransitionFilters.from[currentState].to[ts](stateTransitions.from[currentState].to[ts], data);
                        if(!_.isUndefined(result)){
                            if(result == false){
                                return;
                            } else if(_.isString(result) && !_.isUndefined(stateTransitions.from[currentState].to[result])){
                                ts = result;
                            }
                        }
                    }
                }catch(e){
                    // play the transition
                }
                stateTransitions.from[currentState].to[ts].play(data);
                the.stateData = data;
                the.setState(ts);
            }
        };

        this.setState = function(s){
            previousState = currentState;
            currentState = s;
        }
    }

    /**
     * @constructor
     */
    function history(){
        var previous = -1,
            current = -1,
            next = -1,
            pageStack = [],
            the = this;

        this.init = function(){
            $(w).on("popstate", function(e){
                var p = pages[e.originalEvent.state];

                p.navigatingHistory = true; // prevents the addition of a new entry into the history stack
                transitionToPage(e.originalEvent.state, p.stateData);
            });
            return this;
        };

        this.add = function(page){
            if(page.navigatingHistory){
                delete page.navigatingHistory;
            }else{
                previous = current;
                ++current;
                next = current + 1;
                pageStack.push(page);

                // enables the browser back button to navigate back and forth in the app
                // works with popstate listener in the init method above
                w.history.pushState(page.name, page.name, "#" + page.name);
            }
        };

        this.back = function(){
            if(the.canBack()){
                current = previous;
                previous = previous - 1;
                next = current + 1;
                var page = pageStack[current];
                page.navigatingHistory = true;
                transitionToPage(page.name, page.stateData);
                setNavButtonState();
            }
        };

        this.forward = function(){
            if(the.canForward()){
                previous = current;
                current = next;
                next = current + 1;
                var page = pageStack[current];
                page.navigatingHistory = true;
                transitionToPage(page.name, page.stateData);
                setNavButtonState();
            }
        };

        this.canBack = function(){
            return previous >= 0;
        };

        this.canForward = function(){
            return next > 0 && next < pageStack.length;
        };
    }
    // -- end ctors

    function setNavButtonState(){
        var $back = $("[data-navigate=back][data-app=" + b.app.name + "]"),
            $forward = $("[data-navigate=forward][data-app=" + b.app.name + "]");
        if(!b.app.history.canForward()){
            $forward.addClass("disabled");
        }else{
            $forward.removeClass("disabled");
        }

        if(!b.app.history.canBack()){
            $back.addClass("disabled");
        }else{
            $back.removeClass("disabled");
        }
    }

    function _setPageTransition(f, t, th) {
        pageTransitions.from = pageTransitions.from || {};
        pageTransitions.from[f] = pageTransitions.from[f] || {};
        pageTransitions.from[f].to = pageTransitions.from[f].to || {};
        pageTransitions.from[f].to[t] = th;
    }

    function createPageTransition(n, f, t, impl){
        var th = new transitionHandler(n, f, t, impl);
        _setPageTransition(f, t, th);
        return th;
    }

    /**
     * Set the transition function to be executed when transitioning from
     * f to t.  The specified handler impl function should have the
     * signature of (transitionHandler[, data]) where 'data' may or may not
     * be provided.  To preserve default animations the specified handler impl
     * function should call pages[f].goodBye() and pages[t].hello(data)
     * @param f name of the page transitioning from
     * @param t name of the page transitioning to
     * @param impl the function to execute
     * @returns {transitionHandler}
     */
    function setPageTransition(f, t, impl){
        var th = new transitionHandler(f+"To"+t, f, t, impl);
        _setPageTransition(f,t,th);
        return th;
    }

    function run(appName, startPageName){
        if(_.isUndefined(appName) && _.isUndefined(startPageName)){
            appName = "main";
            startPageName = "start";
        }else if(_.isUndefined(startPageName)){
            startPageName = appName;
            appName = "main";
        }

        _loadPages(appName, startPageName);
        b.app.name = appName;
    }

    function view(viewName, data, opts){
        var def = $.Deferred(function(){
            var prom = this;
            using.dustTemplates(b.app.name /* set by run()*/, function(an){ // appName
                b.app.templatesLoaded = an;
                b.view(_.format("{0}.{1}", an, viewName), data, opts)
                    .done(function(r){
                        prom.resolve(r);
                    })
                    .fail(function(r){
                        prom.reject(r);
                    })
            });
        });

        return def.promise();
    }

    function transitionToPage(to, data){
        pageTransition(currentPage, to, data);
    }

    function pageTransition(from, to, data){
        if(_.isUndefined(to)){
            to = from;
        }

        try{
            if(_.isFunction(pageTransitionFilters.from[from].to[to]))
            {
                var result = pageTransitionFilters.from[from].to[to](pageTransitions.from[from].to[to], data);
                if(!_.isUndefined(result)){
                    if(result == false){
                        return;
                    }
                }
            }
        }catch(e){
            // play the transition;
        }

        pageTransitions.from[from].to[to].play(data);
        previousPage = from;
        currentPage = to;

        var p = pages[to];
        b.app.history.add(p);
        b.page = p;
        setNavButtonState();
    }

    /**
     * Set the data filter on the specified transition
     * @param from
     * @param to
     * @param filter
     */
    function setPageTransitionFilter(from, to, filter){
        pageTransitionFilters.from = pageTransitionFilters.from || {};
        pageTransitionFilters.from[from] = pageTransitionFilters.from[from] || {};
        pageTransitionFilters.from[from].to = pageTransitionFilters.from[from].to || {};
        pageTransitionFilters.from[from].to[to] = filter;
    }

    function _loadPages(subAppName, startPageName){
        // load html from ~/pages/ using Pages
        _.act("pages", "getpages", { bamAppName: subAppName }).done(function(result){
            if(result.Success){
                var vals = result.Data;
                _.each(vals, function(cp, i){
                    // create the page
                    pages[cp] = new page(cp, subAppName);
                    createPageTransition(cp + "To" + cp, cp, cp, defaultPageTransitionHandler);
                    _.each(_.rest(vals, i+1), function(np){
                        var currentToNextName = cp + "To" + np,
                            nextToCurrentName = np + "To" + cp;
                        // create a transition to all the other pages
                        createPageTransition(currentToNextName, cp, np, defaultPageTransitionHandler);
                        // and back again
                        createPageTransition(nextToCurrentName, np, cp, defaultPageTransitionHandler);
                    });
                });

                using.script(_.format("bam/{0}/init.js", subAppName), function(){
                    pageTransition(startPageName, startPageName);
                });
            }else{
                $(contentSelector).text(result.Message);
            }
        })
    }

    b.navigateTo = function(pageName, data){
        transitionToPage(pageName, data);
    };

    b.goToState = function(ts, data){
        if(ts == "" || _.isUndefined(ts)){
            ts = "initial";
        }
        pages[currentPage].transitionTo(ts, data);
    };

    b.pageActivated = function(pageName, handler){
        pageActivationHandlers[pageName] = handler;
    };

    b.activate = function(page){
        var $back = $("[data-navigate=back][data-app=" + b.app.name + "]"),
            $forward = $("[data-navigate=forward][data-app=" + b.app.name + "]");

        $("[data-navigate-to]", $(contentSelector)).each(function(i,v){
            var tp = $(v).attr("data-navigate-to"),
                data = $.dataSetOptions(v);
            $(v).off("click").on("click", function(){
                b.navigateTo(tp, data);
            })
        });

        $back.each(function(i,v){
            $(v).off("click").on("click", function(){
                b.app.history.back();
                setNavButtonState();
            })
        });

        $forward.each(function(i,v){
            $(v).off("click").on("click", function(){
                b.app.history.forward();
                setNavButtonState();
            })
        });

        $("[data-set-state]", $(contentSelector)).each(function(i,v){
            var s = $(v).attr("data-set-state"),
                data = $.dataSetOptions(v);
            $(v).off("click").on("click", function(){
                b.goToState(s, data);
            })
        });

        if(_.isFunction(pageActivationHandlers[page.name])){
            pageActivationHandlers[page.name](page);
        }

        page.activated = true;
    };

    var app = {
        run: run,
        currentPage: currentPage,
        contentSelector: contentSelector,
        setPageTransition: setPageTransition,
        transitionToPage: transitionToPage,
        setPageTransitionFilter: setPageTransitionFilter,
        appData: appData,
        pages: pages,
        view: view,
        writeTemplate: function(obj){
            return _.act("dust", "writeTemplate", {appName: this.name, json: JSON.stringify(obj)});
        },
        history: new history().init(),
        goodByeEffect: function(ef, sp){ // effect, (bool)setPages
            if(!_.isUndefined(ef)){
                goodByeEffect = ef;
                if(sp){
                    _.each(pages, function(v){
                        v.stateGoodByeEffect = ef;
                    })
                }
            }
            return goodByeEffect;
        },
        helloEffect: function(ef, sp){
            if(!_.isUndefined(ef)){
                helloEffect = ef;
                if(sp){
                    _.each(pages, function(v){
                        v.stateHelloEffect = ef;
                    })
                }
            }
            return helloEffect;
        },
        showEffect: function(ef, sp){
            this.helloEffect(ef, sp);
        },
        hideEffect: function(ef, sp){
            this.goodByeEffect(ef, sp);
        }
    };

    b.app = app;
    b.pages = app.pages;

    $(function(){
        if(_ !== undefined && _.mixin !== undefined){
            _.mixin(b);
        }
    });
    return app;
})(jQuery, _, dao, bam, window || {});