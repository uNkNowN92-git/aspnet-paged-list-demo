if ($.validator !== undefined) {
    $.validator.unobtrusive.options = {
        errorPlacement: function ($error, $element) {
            $element.triggerHandler("updateErrorTooltip");
        }
    };
}

var _errorTooltipTemplate;

var ErrorTooltip = function () {
    return {
        Init: function (containerIds, _url) {
            var url = _url !== undefined ? _url : "/Components/ErrorTooltip",
                $form = $("form");

            $("input").on("updateErrorTooltip", function () {

                // get errors that were created using jQuery.validate.unobtrusive
                var $errors = $form.find(".field-validation-error span");

                var validationSummaryList = $('<ul>');

                $errors.each(function () {
                    var errorMessage = $(this).html();

                    if (errorMessage != "") {
                        validationSummaryList.append('<li>' + errorMessage);
                    }
                });

                var attrs = {
                    'data-original-title': validationSummaryList.html(),
                    'data-html': 'true',
                };

                $.each(containerIds, function (index, value) {
                    $(value).removeAttr('title');
                    $(value).attr(attrs);
                    $(value).addClass('error-tooltip');
                });

                ErrorTooltip.ActivateErrorTooltip();
            });

            ErrorTooltip.GetErrorTooltipTemplate(url);
        },
        ActivateErrorTooltip: function () {
            $('.error-tooltip').tooltip({
                template: _errorTooltipTemplate
            });
        },
        GetErrorTooltipTemplate: function (url) {
            $.ajax({
                url: url,
                type: "GET",
                dataType: "html",
                success: function (html) {
                    _errorTooltipTemplate = html;
                }
            });
        },
    };
}();