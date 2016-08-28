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

window.app.VM = (function (_dataContext) {
    "use strict";

    var dataContext = new window.app.dataContext();
    var url;
    var $form;
    var $publishDateDP;
    var pagedBooksUrl;
    var autocompleteUrl;

    function init(params) {
        url = params.url;
        autocompleteUrl = params.autocompleteUrl;
        pagedBooksUrl = params.pagedBooksUrl;

        var book = params.book || {};
        self.book(book);
        //self.selectedBook(book);
        self.form(book);
        self.searchForOptions(params.searchForOptions);

        $form = $('#books-form');
        $publishDateDP = $('#PublishDate').parent();

        $publishDateDP.datetimepicker({
            defaultDate: GetDefaultDateFormat(book.publishDate)
        });

        $publishDateDP.on("dp.change", function (e) {
            if (moment(e.date).isValid())
                self.form().publishDate = GetDefaultDateFormat(e);
        });
    };
    var self = init.prototype;

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
        dataContext.GetData(pagedBooksUrl, data).done(function (data) {
            console.log(data);
            var books = data.Data;
            var myBooks = _.pluck(_.where(books, { 'title': 'New Title' }), 'bookId');
            self.cards(books);
            self.selectedCards(myBooks);
        });
    };

    self.getValue = function () {
        dataContext.GetData(GetUrlWithId(url, self.form().bookId)).done(function (data) {
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
        dataContext.Delete(GetUrlWithId(url, self.form().bookId)).done(function () {
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

                dataContext.Update(GetUrlWithId(url, self.form().bookId), formValues).done(function () {
                    ApplyChanges();
                });
            } else {
                ApplyChanges();
            }
        }
    };

    var searchForOptions = [];
    var autocompleteOptions = {
        searchFor: searchForOptions[0],
        maxResults: 100
    };

    self.searchForOptions = ko.observableArray();
    self.autocompleteOptions = ko.observable(autocompleteOptions);

    //self.selectedBook = ko.observable();

    self.selectBook = function (event, ui) {
        self.book(ui.item.book);
    };

    self.cards = ko.observableArray([]);
    self.selectedCards = ko.observableArray([]);
    
    self.getCards = function () {
        //var cards = [{
        //    id: "2",
        //    description: "Card 1"
        //},
        //{
        //    id: 4,
        //    description: "Card Abc"
        //}];
        //cards = ko.toJS(cards);
        
        self.selectedCards([7,8]);
        //self.cards(cards);
    };

    self.getBooks = function (request, response) {
        var data = {
            searchTerm: request.term
        };
        $.extend(data, self.autocompleteOptions());

        dataContext
            .GetData(autocompleteUrl, data)
            .done(function (data) {
                response($.map(data, function (book) {
                    var result = {
                        book: book,
                        value: "",
                    };
                    var searchForIndex = self.autocompleteOptions().searchFor;
                    switch (self.searchForOptions()[searchForIndex].Text) {
                        case 'Title':
                            result.value = book.title;
                            break;
                        case 'Description':
                            result.value = book.description;
                            break;
                        case 'Author Name':
                            result.value = book.authorName;
                            break;
                    }

                    return result;
                }));
            });
    };

    function ApplyChanges() {
        var formValues = ko.toJS(self.form());
        setAuthorName(formValues);
        self.book(formValues);
        self.isEditing(false);
    }

    function setAuthorName(form) {
        form.authorName = form.authorFirstName + " " + form.authorLastName;
    };

    return init;
}(window.app.dataContext));