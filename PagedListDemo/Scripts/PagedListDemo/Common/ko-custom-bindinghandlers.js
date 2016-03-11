(function () {
    ko.bindingHandlers.toFixedWithTitle = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value, formatted;

            if (ko.isObservable(valueAccessor())) {
                value = ko.utils.unwrapObservable(valueAccessor());
            } else {
                value = allBindingsAccessor().toFixedWithTitle;
            }

            formatted = parseFloat(value).toFixed(2);
            $(element).attr('title', value);
            ko.utils.setTextContent(element, formatted);
        }
    };
})();