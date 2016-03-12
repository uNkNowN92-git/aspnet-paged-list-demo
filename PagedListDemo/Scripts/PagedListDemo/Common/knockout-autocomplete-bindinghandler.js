(function (ko) {
    ko.bindingHandlers.autocomplete = {
        init: function (element, params) {
            var autocompleteResultContainer = $('<span />')
                .addClass('ui-front autocomplete-result')
                .attr('for', $(element)[0].id);

            $(element).after(autocompleteResultContainer);

            var options = {
                minLength: 1, // babcock.autocomplete.MIN,
                delay: 0, //babcock.autocomplete.DELAY,
                appendTo: $(element).next(),
                response: function (event, ui) {
                    if (!ui.content.length) {
                        var noResult = {
                            value: "",
                            label: "No results found"
                        };
                        ui.content.push(noResult);
                    }
                }
            };

            $.extend(options, params());
            $(element).autocomplete(options);
        },
        update: function (element, params) {
            $(element).autocomplete("option", "source", params().source);
        }
    };
})(ko);