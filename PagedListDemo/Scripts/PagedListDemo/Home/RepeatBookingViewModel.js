var enums = {
    dateTime: {
        FORMAT: 'DD/MM/YYYY'
    }
};

var RepeatBookingViewModel = function (params) {
    "use strict";
    var self = this;

    self.form = ko.observable({
        days: 1
    });
    self.daysNext = ko.observable();
    self.weeksNext = ko.observable();
    self.monthsNext = ko.observable();
    self.dailyDayOfWeek = ko.observable();

    self.startDate = ko.observable(moment('7/3/2016', enums.dateTime.FORMAT));
    self.daysOfWeek = ko.observableArray(params.daysOfWeek);
};

(function () {
    ko.bindingHandlers.dateAdd = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            updateOutput();

            $(element)
                .spinner({})
                .on('keyup', function () {
                    updateOutput();
                })
                .on("spinstop", function () {
                    updateOutput($(element).spinner("value"));
                });

            $(element).spinner({});

            function updateOutput(val) {
                var period = valueAccessor().period,
                format = valueAccessor().format || enums.dateTime.FORMAT,
                startDate = valueAccessor().startDate
                dayOfWeek = valueAccessor().dayOfWeek;

                var value = val || getValue(allBindingsAccessor().value) || 1;
                var intValue = parseInt(value);
                var output = valueAccessor().output;

                if (!isNaN(intValue) || $.trim(value).length !== 0) {
                    intValue = intValue < 1 ? 1 : intValue; // value must not be less than 1

                    var start = getValue(startDate) || moment();
                    var result = start.clone();
                    result.add(value, period);

                    if (dayOfWeek !== undefined) {
                        var dayOfWeekValue = getValue(valueAccessor().dayOfWeek);
                        var resultDayOfWeek = result.day(); // day of week of result (0 = sunday)

                        if (typeof (dayOfWeekValue) === 'string') {
                            switch (dayOfWeekValue.toLowerCase()) {
                                case 'weekday':
                                case 'weekdays':
                                    // resultDayOfWeek is on weekdays
                                    if (resultDayOfWeek >= 1 && resultDayOfWeek <= 5) {
                                        dayOfWeekValue = resultDayOfWeek;
                                    } else {
                                        // default to monday
                                        dayOfWeekValue = 1;
                                    }
                                    break;
                            }
                        }

                        var daysToNext = dayOfWeekValue - resultDayOfWeek;

                        if (daysToNext < 0) {
                            daysToNext += 7;
                        }

                        result.add(daysToNext, 'd');
                    }

                    var formatted = result.format(format);

                    output(formatted);
                } else {
                    output('Invalid input');
                }
            }

            function getValue(field) {
                return ko.isObservable(field) ? ko.unwrap(field) : field;
            }
        }
    };
})();