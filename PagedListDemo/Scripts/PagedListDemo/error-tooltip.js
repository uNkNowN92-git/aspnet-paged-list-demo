var ErrorTooltip = (function () {
    'use strict';

    var ErrorTooltip = {};
    var _errorTooltipTemplate;

    if ($.validator && $.validator.unobtrusive) {
        $.validator.unobtrusive.options = {
            errorPlacement: function ($error, $element) {
                $element.triggerHandler("updateErrorTooltip");
            }
        };
    }

    ErrorTooltip.Init = function (containerIds, _url) {
        var url = _url !== undefined ? _url : "/Components/ErrorTooltip",
            $form = $("form");

        $("input").on("updateErrorTooltip", function () {
            // get errors that were created using jQuery.validate.unobtrusive
            var $errors = $form.find(".field-validation-error span");

            var fieldValidationList = $('<ul>');

            $errors.each(function () {
                var errorMessage = $(this).html();

                if (errorMessage)
                    fieldValidationList.append('<li>' + errorMessage);
            });

            var attrs = {
                'data-original-title': fieldValidationList.html(),
                'data-html': 'true',
            };

            $.each(containerIds, function (index, value) {
                $(value).removeAttr('title')
                    .attr(attrs)
                    .addClass('error-tooltip');
            });

            ErrorTooltip.ActivateErrorTooltip();
        });

        ErrorTooltip.GetErrorTooltipTemplate(url);
    };

    ErrorTooltip.ActivateErrorTooltip = function () {
        $('.error-tooltip').tooltip({
            template: _errorTooltipTemplate
        });
    };

    ErrorTooltip.GetErrorTooltipTemplate = function (url) {
        $.ajax({
            url: url,
            type: "GET",
            dataType: "html",
            success: function (html) {
                _errorTooltipTemplate = html;
            }
        });
    };

    return ErrorTooltip;
})(jQuery);