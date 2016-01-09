var BooksViewModel = function (params) {
    var self = this;
    PagedList.call(self, params);

    self.ready = ko.observable(true);

    self.selectedItem = ko.observable(null);

    self.getResult = function (data, event) {
        self.selectedItem(null);
        self.getList(data, event);
    }

    self.isSelected = function (id) {
        return self.selectedItem() === id;
    }

    self.isVisible = function (data) {
        return self.isSelected(data.id);
    }

    self.rowItemEnabled = function (data) {
        console.log(data);
        return self.isSelected(data.id);
    };

    self.validInput = ko.computed(function () {
        return self.selectedItem() !== null;
    });

    self.selectItem = function (data) {
        var id = data.id;
        
        if (self.isSelected(id) === false) {
            self.selectedItem(id);
        } else {
            self.selectedItem(null);
        }
    };

    var headers = {
        'Authorization': 'Basic faskd52352rwfsdfs',
        'X-PartnerKey': '3252352-sdgds-sdgd-dsgs-sgs332fs3f'
    };

    self.headers = ko.observable(headers);

    console.log(self.headers()['Authorization']);

    headers['Authorization'] = 'new value';
    self.headers = ko.observable(headers);

    console.log(self.headers()['Authorization']);
    console.log(self.headers()['Authorization']);
};

var viewModel = new BooksViewModel({
    url: "/api/values",
    entriesPerPage: 5,
    queryOnLoad: false,
    queryOnFilterChangeOnly: false
});

ko.applyBindings(viewModel, $("#paged-list-demo")[0]);