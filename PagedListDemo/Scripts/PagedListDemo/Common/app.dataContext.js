window.app = window.app || {};

window.app.dataContext = (function () {
    var dataContext = {
        Add: Add,
        Post: Post,
        GetData: GetData,
        Update: Update,
        Delete: Delete,
        IsModified: IsModified
    };

    var requests = [];
    var request;

    function Add(url, data, options) {
        /// <summary>Add method. Similar to Post but only returns the data.id.</summary>
        /// <param name="url" type="string">URL</param>
        /// <param name="data" type="object">The data to insert</param>
        return Ajax("post", url, data, function (data) {
            return data.id;
        }, options);
    }

    function Post(url, data, options) {
        /// <summary>Post</summary>
        /// <param name="url" type="string">URL</param>
        /// <param name="data" type="object">The data to insert</param>
        return Ajax("post", url, data, function (data) {
            return data;
        }, options);
    }

    function GetData(url, data, options) {
        /// <summary>Get the data</summary>
        /// <param name="url" type="string">URL</param>
        /// <param name="data" type="object">The parameters to pass</param>
        return Ajax("get", url, data, function (data) {
            return data;
        }, options);
    }

    function Update(url, data, options) {
        /// <summary>Update</summary>
        /// <param name="url" type="string">URL with id 'url/id'</param>
        /// <param name="data" type="object">The updated values of the data</param>
        return Ajax("put", url, data, null, options);
    }

    function Delete(url, data, options) {
        /// <summary>Delete</summary>
        /// <param name="url" type="string">URL with id 'url/id'</param>
        /// <param name="data" type="object">The updated values of the data</param>
        return Ajax("delete", url, data, null, options);
    }

    function Ajax(method, url, data, callback, options) {
        abortPreviousRequest(url);

        var opts = {
            url: url,
            method: method,
            data: data,
            cache: false
        };

        $.extend(opts, options);

        request = $.ajax(opts);

        saveRequestInfo(url);

        request
            .fail(Error)
            .always(removeRequestInfo);

        return request.then(callback || function () {
            return;
        })
    }

    function IsModified(newValue, oldValue) {
        /// <summary>Checks if the two values are equal</summary>
        return !_.isEqual(newValue, oldValue);
    }

    function Error(jqXHR, status, error) {
        //toastrNotificationClear();

        // fix for null JsonResult from server
        if (_.contains([200, 0], jqXHR.status)) return;

        //toastr.error(babcock.messages.ERROR);
    }

    // aborts the previous request if the same URL was called
    function abortPreviousRequest(url) {
        var location = getLocation(url);

        var previousRequest = _.findWhere(requests, { 'pathname': location.pathname });
        if (previousRequest) {
            previousRequest.request.abort();
        }
    }

    // saves the request info of the current AJAX request, to be used in cancelling the request
    // if the same URL was called
    function saveRequestInfo(url) {
        var location = getLocation(url);

        if (!_.contains(_.pluck(requests, 'pathname'), location.pathname)) {
            requests.push({
                pathname: location.pathname,
                request: request
            });
        }
    }

    // removes the request in the requests array
    function removeRequestInfo() {
        var location = getLocation(this.url);
        var index = _.findIndex(requests,
            function (item) {
                return item.pathname === location.pathname
            });

        if (index >= 0) {
            requests.splice(0, 1);
        }
    }

    // helper for getting the url details
    function getLocation(href) {
        var l = document.createElement("a");
        l.href = href;
        return l;
    };

    return dataContext;
})();