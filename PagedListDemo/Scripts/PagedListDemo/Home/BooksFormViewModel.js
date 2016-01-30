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
        return $.post("api/books", data);
    }

    function IsModified(newValue, oldValue) {
        return !_.isEqual(newValue, oldValue);
    }
})();

window.app.VM = (function (dataContext) {
    var self = this;

    self.init = function (params) {
        self.book(params.book);
    };

    self.book = ko.observable();
    self.form = ko.observable([]);
    self.isEditing = ko.observable(false);

    self.beginEdit = function () {
        self.isEditing(true);
        var bookValues = ko.mapping.toJS(self.book());
        self.form(bookValues);
    };

    self.isReady = ko.observable(true);

    self.save = function () {
        var $form = $('#books-form');
        if ($form.valid()) {
            if (dataContext.IsModified(self.book(), self.form())) {
                var bookId = $('#BookId').val();
                var data = $form.serialize();

                dataContext.Edit(bookId, data).then(function (data, textStatus, jqXHR) {
                    console.log(data);
                    console.log(textStatus);
                    console.log(jqXHR);

                    if (jqXHR.status != 202) {
                        alert('error!');
                        return;
                    }
                });
            }
            self.book(self.form());
            self.isEditing(false);
        }
    };

    return self;
})(window.app.dataContext);