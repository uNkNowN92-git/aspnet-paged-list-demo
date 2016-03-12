var JourneyTabs = function () {
    var request,
        previousUrl;

    return {
        OnclickTab: function (tabsId, url, renderPartialView) {
            if (renderPartialView) {
                if (!$("#" + tabsId).attr("partial-render"))
                    JourneyTabs.OnclickRenderPartial(url, tabsId)
            }
            else {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: {
                        tabsId: tabsId,
                        __RequestVerificationToken: $('[name=__RequestVerificationToken]').val()
                    },
                    dataType: "json"
                });
            }
        },
        OnclickRenderPartial: function (url, tabsId) {
            if (request && url === previousUrl) {
                request.abort();
            }
            previousUrl = url;

            request = $.ajax({
                url: url,
                type: "Get",
                data: { tabsId: tabsId },
                dataType: "html",
                cache: false,
                async: true,
                success: function (partialView) {
                    $('#' + tabsId).html(partialView);

                    if ($.validator && $.validator.unobtrusive)
                        $.validator.unobtrusive.parse($('#' + tabsId));

                    $("#" + tabsId).attr("partial-render", "true");
                }
            });
        }
    }
}();



var ErrorTooltip = (function ($) {
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

            var hasErrors = $.trim(fieldValidationList.html()).length > 0;

            var attrs = {
                'data-original-title': fieldValidationList.html(),
                'data-html': 'true'
            };


            $.each(containerIds, function (index, selector) {
                $(selector).removeAttr('title')
                    .attr(attrs)
                    .addClass('error-tooltip');

                if (hasErrors) $(selector).addClass('disabled-look');
                else $(selector).removeClass('disabled-look');
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