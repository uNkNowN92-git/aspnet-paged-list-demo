var BookModel = function (data) {
    ko.mapping.fromJS(data, {}, this);
    // new 
    // Sample additional attribute or property not coming from the server
    this.titleLength = ko.computed(function () {
        // check variable if it is an observable before accessing
        if (typeof (this.title) === "function" && this.title() !== null) {
            return this.title().length;
        }
        return 0;
    }, this);
};

var BooksViewModel = function (params) {
    var self = this;
    PagedList.call(self, params);

    self.sortIcon = function (value) {
        var result;
        if (self.activeSort().column === value) {
            result = 'glyphicon ' + (self.activeSort().asc ? 'glyphicon-sort-by-attributes' : 'glyphicon-sort-by-attributes-alt');
        }
        return { 'class': result };
    };

    self.title = ko.pureComputed({
        write: function (value) {
            self.filter().title = value;
            self.getResult();
        },
        read: function () {
            return;
        }
    });

    self.description = ko.pureComputed({
        write: function (value) {
            self.filter().description = value;
            self.getResult();
        },
        read: function () {
            return;
        }
    });

    self.author = ko.pureComputed({
        write: function (value) {
            self.filter().author = value;
            self.getResult();
        },
        read: function () {
            return;
        }
    });

    self.ready = ko.observable(false);

    self.selectedItem = ko.observable(null);

    self.validInput = ko.computed(function () {
        return self.selectedItem() !== null;
    });

    self.getResult = function (data, event) {
        self.selectedItem(null);
        self.getList(data, event);
    };

    self.isChecked = function (data) {
        if (data !== undefined && typeof (data.bookId) === "function") {
            return self.isSelected(data.bookId());
        }
        else {
            return self.isSelected(data.bookId);
        }
    };

    self.isSelected = function (id) {
        return self.selectedItem() === id;
    }

    self.selectItem = function (data) {
        var id = typeof (data.bookId) === "function" ? data.bookId() : data.bookId;

        if (self.isSelected(id) === false) {
            self.selectedItem(id);
        } else {
            self.selectedItem(null);
        }
    };

    function GetValue(data) {
        return typeof (data) === "object" ? data.bookId : data.bookId();
    }

    var headers = {
        'Authorization': 'Basic faskd52352rwfsdfs',
        'X-PartnerKey': '3252352-sdgds-sdgd-dsgs-sgs332fs3f'
    };

    self.headers(headers);

    self.ready(true);

    //console.log(self.headers()['Authorization']);

    //headers['Authorization'] = 'new value';
    //self.headers = ko.observable(headers);

    //console.log(self.headers()['Authorization']);
};

var viewModel1 = new BooksViewModel({
    url: "/api/values",
    entriesPerPage: 5,
    queryOnFilterChangeOnly: false,
    //dataAsObservable: false,
});

var viewModel2 = new BooksViewModel({
    url: "/api/values",
    entriesPerPage: 5,
    queryOnLoad: false,
    //queryOnFilterChangeOnly: false
    dataAsObservable: true,
    // setting dataAsObservable to false will make the mapping option to be useless
    mapping: {
        create: function (options) {
            return new BookModel(options.data);
        }
    }
});

ko.applyBindings(viewModel1, $("#paged-list-demo")[0]);
//ko.applyBindings(viewModel2, $("#paged-list-demo-2")[0]);