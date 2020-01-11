(function () {
    'use strict';

    var ctx = {};

    function init() {
        ctx.Templates = {};
        ctx.Templates.OnPreRender = function (ctx) {
            var statusField = ctx.ListSchema.Field.filter(function (f) {
                return f.Name === 'SomeTextColumnWithJsLinkAttched';
            });
            if (statusField && typeof statusField[0].AllowGridEditing !== 'undefined') {
                statusField[0].AllowGridEditing = false;
            }
        }
        ctx.Templates.Fields = {
            'SomeTextColumnWithJsLinkAttched': {
                'EditForm': ReadOnly,
                //'NewForm': ReadOnly // for now lets just make the field only readonly when it is in edit mode
            }
        };

        function ReadOnly(ctx) {
            try {
                var val = ctx.CurrentFieldValue;
                if (val === "")
                    val = "field not set"
            }
            catch (ex) {
                console.error("JSlink error, check -> " + ex.message);
            }
            return "<span class='ms-textLarge'>" + val + "</span>";
        }

        SPClientTemplates.TemplateManager.RegisterTemplateOverrides(ctx);
    }

    init();

})();
