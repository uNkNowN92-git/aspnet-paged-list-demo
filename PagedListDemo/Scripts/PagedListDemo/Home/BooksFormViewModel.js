//"use strict";

function GetDefaultDateFormat(e, toISOString) {
    if (e === null) return;
    
    var result;
    var format = $.fn.datetimepicker.defaults.format;

    switch (typeof (e)) {
        case 'string':
            result = moment(e, format);

            if (toISOString) result = result.toISOString();
            break;
        case 'object':
            result = moment(e.date._d).format(format);
            break;
        default:
    }
    return result;
}

function GetUrlWithId(baseUrl, id) {
    return baseUrl + (baseUrl.match(/\/$/) === null ? "/" : "") + id;
}

window.app = window.app || {};

window.app.dataContext = (function () {
    var dataContext = {
        Add: Add,
        GetValues: GetValues,
        Update: Update,
        Delete: Delete,
        IsModified: IsModified
    };

    function Add(url, data) {
        return Ajax("post", url, data, function (data) {
            return data.id;
        });
    }

    function GetValues(url, data) {
        return Ajax("get", url, data, function (data) {
            return data;
        });
    }

    function Update(url, data) {
        return Ajax("put", url, data);
    }

    function Delete(url) {
        return Ajax("delete", url);
    }

    function Ajax(method, url, data, callback) {
        return $.ajax({
            url: url,
            method: method,
            data: data
        }).then(callback || function () {
            return;
        }).fail(Error);
    }

    function IsModified(newValue, oldValue) {
        return !_.isEqual(newValue, oldValue);
    }

    function Error() {
        toastr.error("An error has occured!");
    }

    return dataContext;
})();

window.app.VM = (function (dataContext) {
    var self = this;
    var url;
    var $form = $('#books-form');
    var $bookId = $('#BookId');
    var pagedBooksUrl;

    self.init = function (params) {
        url = params.url;
        pagedBooksUrl = params.pagedBooksUrl;

        var book = params.book;
        self.book(book);
        self.form(book);

        $publishDateDP = $('#PublishDate').parent();

        $publishDateDP.datetimepicker({
            defaultDate: GetDefaultDateFormat(book.publishDate)
        });

        $publishDateDP.on("dp.change", function (e) {
            if (moment(e.date).isValid())
                self.form().publishDate = GetDefaultDateFormat(e);
        });
    };

    self.book = ko.observable();
    self.form = ko.observable();
    self.isEditing = ko.observable(false);

    self.beginEdit = function () {
        self.isEditing(true);
        var bookValues = ko.toJS(self.book());
        self.form(bookValues);
    };

    self.isReady = ko.observable(true);
    
    self.getValues = function () {
        var data = {
            sortBy: 'BookId'
        };
        dataContext.GetValues(pagedBooksUrl, data).done(function (data) {
            console.log(data);
        });
    };

    self.getValue = function () {
        dataContext.GetValues(GetUrlWithId(url, $bookId.val())).done(function (data) {
            self.form(data);
            ApplyChanges();
        });
    };

    self.add = function () {
        if ($form.valid()) {
            var formValues = ko.toJS(self.form());

            formValues.publishDate = GetDefaultDateFormat(formValues.publishDate, true);
            formValues.AcceptAndAgree = true;
            formValues.Conferencing = true;

            dataContext.Add(url, formValues).done(function (id) {
                if (id) {
                    self.form().bookId = id;
                    ApplyChanges();
                }
            });
        }
    };

    self.archive = function () {
        dataContext.Delete(GetUrlWithId(url, $bookId.val())).done(function () {
            toastr.success('deleted');
            self.book({});
        });
    };

    self.update = function () {
        if ($form.valid()) {

            var bookValues = ko.toJS(self.book());
            var formValues = ko.toJS(self.form());

            if (dataContext.IsModified(bookValues, formValues)) {
                formValues.publishDate = GetDefaultDateFormat(formValues.publishDate, true);
                formValues.AcceptAndAgree = true;
                formValues.Conferencing = true;

                dataContext.Update(GetUrlWithId(url, $bookId.val()), formValues).done(function () {
                    ApplyChanges();
                });
            } else {
                ApplyChanges();
            }
        }
    };

    function ApplyChanges() {
        self.book(self.form());
        self.isEditing(false);
    }

    return self;
})(window.app.dataContext);