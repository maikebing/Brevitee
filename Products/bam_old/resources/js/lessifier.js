var lessifier = (function () {
    var indentS = '    ',
        openingBracket = '{',
        closingBracket = '}',
        semiColumn = ':',
        eol = ';';

    function trim(s) {
        return $.trim(s.replace(/\t+/, ' '));
    }

    function parseCSS(s) {
        var least = { children:{} };

        // Remove comments
        s = s.replace(/\/\*[\s\S]*?\*\//gm, '');

        s.replace(/([^{]+)\{([^}]+)\}/g, function (group, selector, declarations) {
            var o = {};
            o.source = group;
            o.selector = trim(selector);

            var path = least;

            if (o.selector.indexOf(',') > -1) {
                // Comma: grouped selector, we skip
                var sel = o.selector;
                if (!path.children[sel]) {
                    path.children[sel] = { children:{}, declarations:[] };
                }
                path = path.children[sel];
            } else {
                // No comma: we process

                // Fix to prevent special chars to break into parts
                o.selector = o.selector.replace(/\s*([>\+~])\s*/g, ' &$1');
                o.selector = o.selector.replace(/(\w)([:\.])/g, '$1 &$2');

                o.selectorParts = o.selector.split(/[\s]+/);
                for (var i = 0; i < o.selectorParts.length; i++) {
                    var sel = o.selectorParts[i];
                    // We glue the special chars fix
                    sel = sel.replace(/&(.)/g, '& $1 ');
                    sel = sel.replace(/& ([:\.]) /g, '&$1');

                    if (!path.children[sel]) {
                        path.children[sel] = { children:{}, declarations:[] };
                    }
                    path = path.children[sel];
                }
            }

            declarations.replace(/([^:;]+):([^;]+)/g, function (decl, prop, val) {
                var declaration = {
                    source:decl,
                    property:trim(prop),
                    value:trim(val)
                };
                path.declarations.push(declaration);
            });
        });
        return exportObject(least);
    }

    var depth = 0;
    var s = '';

    function exportObject(path) {
        var s = '';
        $.each(path.children, function (key, val) {
            s += getIndent() + key + ' ' + openingBracket + '\n';
            depth++;
            for (var i = 0; i < val.declarations.length; i++) {
                var decl = val.declarations[i];
                s += getIndent() + decl.property + semiColumn + ' ' + decl.value + eol + '\n';
            }
            s += exportObject(val);
            depth--;
            s += getIndent() + closingBracket + '\n';
        });

        // Remove blank lines - http://stackoverflow.com/a/4123442
        s = s.replace(/^\s*$[\n\r]{1,}/gm, '');

        return s;
    }

    function getIndent() {
        var s = '';
        for (var i = 0; i < depth; i++) {
            s += indentS;
        }
        return s;
    }

    return{
        lessify: parseCss
    }
})();