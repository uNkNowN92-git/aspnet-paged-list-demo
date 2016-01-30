(function ($) {
    var dateFormat = "DD/MM/YYYY";
    $.fn.datetimepicker.defaults.format = dateFormat;
    $.fn.datetimepicker.defaults.debug = true;

    if ($.validator && $.validator.unobtrusive) {
        $.validator.unobtrusive.adapters.addBool("mustbetrue");

        $.validator.addMethod("mustbetrue", function (value, element) {
            return value === 'true' || value === 'True' || value === true || value === 1;
        });

        $.validator.methods.date = function (value, element) {
            return this.optional(element) || moment(value, dateFormat).isValid();
        }
    }
}(jQuery));