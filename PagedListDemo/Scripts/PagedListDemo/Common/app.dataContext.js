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

    function Add(url, data) {
        /// <summary>Add</summary>
        /// <param name="url" type="string">URL</param>
        /// <param name="data" type="object">The data to insert</param>
        return Ajax("post", url, data, function (data) {
            return data.id;
        });
    }

    function Post(url, data) {
        /// <summary>Post</summary>
        /// <param name="url" type="string">URL</param>
        /// <param name="data" type="object">The data to insert</param>
        return Ajax("post", url, data, function (data) {
            return data;
        });
    }

    function GetData(url, data) {
        /// <summary>Get the data</summary>
        /// <param name="url" type="string">URL</param>
        /// <param name="data" type="object">The parameters to pass</param>
        return Ajax("get", url, data, function (data) {
            return data;
        });
    }

    function Update(url, data) {
        /// <summary>Update</summary>
        /// <param name="url" type="string">URL with id 'url/id'</param>
        /// <param name="data" type="object">The updated values of the data</param>
        return Ajax("put", url, data);
    }

    function Delete(url, data) {
        /// <summary>Delete</summary>
        /// <param name="url" type="string">URL with id 'url/id'</param>
        /// <param name="data" type="object">The updated values of the data</param>
        return Ajax("delete", url, data);
    }

    function Ajax(method, url, data, callback) {
        return $.ajax({
            url: url,
            method: method,
            data: data,
            cache: false
        }).then(callback || function () {
            return;
        }).fail(Error);
    }

    function IsModified(newValue, oldValue) {
        /// <summary>Checks if the two values are equal</summary>
        return !_.isEqual(newValue, oldValue);
    }

    function Error(jqXHR, status, error) {
        toastrNotificationClear();

        if (jqXHR.status === 200) return;

        toastr.error(babcock.messages.ERROR);
    }

    return dataContext;
})();