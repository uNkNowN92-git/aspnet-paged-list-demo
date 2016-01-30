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

window.app = window.app || {};

window.app.dataContext = (function () {
    return {
        Add: Add,
        Edit: Edit,
        //Delete: Delete
        IsModified: IsModified
    };

    function Edit(id, data) {
        return $.ajax({
            url: "api/books/" + id,
            method: "put",
            data: data
        });
    }

    function Add(data) {
        /// <summary>
        /// Adds a new an entry
        /// </summary>
        /// <param name="data">The entry to add.</param>
        /// <returns></returns>
        return $.post("api/books", data);
    }

    function IsModified(newValue, oldValue) {
        return !_.isEqual(newValue, oldValue);
    }
})();

window.app.VM = (function (dataContext) {
    var self = this;

    self.init = function (params) {
        var book = params.book;
        self.book(book);
        self.form(book);

        $('#PublishDate').parent().datetimepicker({
            defaultDate: GetDefaultDateFormat(book.publishDate)
        });

        $('#PublishDate').parent().on("dp.change", function (e) {
            if (moment(e.date).isValid()) {
                self.form().publishDate = GetDefaultDateFormat(e);
            }
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

    self.save = function () {
        var $form = $('#books-form');
        if ($form.valid()) {

            var bookValues = ko.toJS(self.book());
            var formValues = ko.toJS(self.form());

            if (dataContext.IsModified(bookValues, formValues)) {
                var bookId = $('#BookId').val();

                formValues.publishDate = GetDefaultDateFormat(formValues.publishDate, true);
                formValues.AcceptAndAgree = true;
                formValues.Conferencing = true;

                dataContext.Edit(bookId, formValues).then(function (data, textStatus, jqXHR) {
                    console.log(data);
                    console.log(textStatus);
                    console.log(jqXHR);

                    if (jqXHR.status != 202) {
                        alert('error!');
                        return;
                    }
                    self.book(self.form());
                    self.isEditing(false);
                });
            } else {
                self.book(self.form());
                self.isEditing(false);
            }
        }
    };

    return self;
})(window.app.dataContext);