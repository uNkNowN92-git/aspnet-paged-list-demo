(function () { 'use strict';

    function Init() {
        var $parent = $('.radio-button-toggle');

        if ($parent.length) {
            var $input = $parent.find('input[checked]');
            if ($input.prop('type') === 'radio') $input.parent().addClass('active');
        }
    }

    $('.radio-button-toggle label').click(function (event) {
        var htmlFor = event.target.htmlFor;
        if ($.trim(htmlFor).length) $('#' + htmlFor).trigger('click');
    });

    $('.radio-button-toggle input[type="radio"]').change(function (event) {
        var $parent = $(this).closest('.radio-button-toggle');

        if ($parent.length) {
            var $input = $(this).parent().find('input')
              .prop('checked', !$(this).parent().hasClass('active'));

            if ($input.prop('type') === 'radio') $parent.find('.active').removeClass('active')
        }

        $(this).parent().toggleClass('active');
    });

    Init();
})(jQuery);