(function ($) {
    if ($.validator && $.validator.unobtrusive) {
        $.validator.unobtrusive.adapters.addBool("mustbetrue", "required");
    }
}(jQuery));

(function ($) {
    if ($.validator) {
        $.validator.addMethod("mustbetrue", function (value, element) {
            return Validation();
        });
    }
}(jQuery));