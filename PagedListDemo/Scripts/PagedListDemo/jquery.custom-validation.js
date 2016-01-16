(function ($) {
    if ($.validator && $.validator.unobtrusive) {
        $.validator.unobtrusive.adapters.addBool("mustbetrue", "required");
    }
}(jQuery));