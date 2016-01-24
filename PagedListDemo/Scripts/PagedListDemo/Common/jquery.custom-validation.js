(function ($) {
    if ($.validator && $.validator.unobtrusive) {
        $.validator.unobtrusive.adapters.addBool("mustbetrue");

        $.validator.addMethod("mustbetrue", function (value, element) {
            return value === 'true' || value === 'True' || value === true || value === 1;
        });
    }
}(jQuery));