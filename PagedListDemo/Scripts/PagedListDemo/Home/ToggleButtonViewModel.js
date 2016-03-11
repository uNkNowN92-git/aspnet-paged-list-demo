var ToggleButtonViewModel = function () {
    "use strict";

    var self = this;

    self.buttons = ko.observableArray();
    self.buttonAttr = function (value) {
        var attr = {
            'class': 'btn '
        };

        if (value) {
            attr['class'] += 'btn-primary';
        } else {
            attr['class'] += 'btn-default';
        }
        return attr;
    };

    self.getJSON = function () {
        var data = $('#toggle-button-form').serializeArray();

        var request = $.ajax({
            url: 'home/get',
            data: data
        });
        request.done(function (data, a) {
            console.log(data, a);
        });

        request.then(function (data) {

        }).fail(function (jqXHR, status, error) {
            if (jqXHR.status === 200) return;

            console.log(jqXHR);
            console.log(status);
            console.log(error);
        })
    };

    // dummy
    function valueTextModel(text, value) {
        var self = this;

        self.text = text;
        self.value = value;
    }
    // dummy
    self.submit = function () {
        var data = $('#toggle-button-form').serialize();
        $.post("", data);
    };

    // dummy
    self.init = function () {
        self.buttons.push(new valueTextModel("feature 1", 1));
        self.buttons.push(new valueTextModel("feature 2", 2));
        self.buttons.push(new valueTextModel("feature 3", 3));
        self.buttons.push(new valueTextModel("feature 4", 4));
    };
    self.init();
};


// create file: knockout-toggle-attr-bindinghandler.js
(function () {
    ko.bindingHandlers.toggleAttr = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var $element = $(element),
                $child = $element.find('input'),
                type = $child[0].type,
                toggleAttr = allBindingsAccessor().toggleAttr,
                attrFn = toggleAttr.attrFn;

            switch (type) {
                case "checkbox":
                    updateCheckboxProperty(element);
                    $(element).click(function () {
                        updateCheckboxProperty(this, true);
                    });
                    break;
                default:
            }

            function updateCheckboxProperty(element, toggle) {
                var $this = $(element);
                var $child = $this.find('input');
                var isChecked = $child.is(':checked');

                if (toggle) {
                    isChecked = !isChecked;
                    $child.prop('checked', isChecked);
                }

                var value = attrFn(isChecked);
                $this.attr(value);
            }
        }
    };
})();