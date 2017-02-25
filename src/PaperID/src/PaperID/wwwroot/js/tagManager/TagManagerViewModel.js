
function Tag(data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);
    self.isInEditMode = ko.observable(false);
    self.elementId = function (inputId) {
        return "form-element-" + self.id() + "-" + inputId;
    };
    
    self.uploadState = ko.observable(0);
    self.save = function () {
        // TODO: Upload
        $.ajax("/api/tags/"+self.id(), {
            data: ko.mapping.toJSON(self),
            type: "put", contentType: "application/json",
            success: function (result) {
                self.isInEditMode(false);
            },
            error: function () { alert("failed"); }
        });
    }

    self.uploadImage = function (image) {
        self.uploadState(1);
        var formData = new FormData();
        formData.append('file', image);
        $.ajax("/api/ImageUpload", {
            data: formData,
            type: "post",
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
                self.imageUrl(result.imageUrl);
                self.uploadState(200);
            },
            error: function () {
                self.uploadState(400);
            },
            xhr: function () {
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) {
                    // For handling the progress of the upload
                    myXhr.upload.addEventListener('progress', function (e) {
                        if (e.lengthComputable) {
                            $('progress').attr({
                                value: e.loaded,
                                max: e.total,
                            });
                        }
                    }, false);
                }
                return myXhr;
            }
        });
    }
}

function TagManager(data) {
    var self = this;
    self.isLoaded = ko.observable(false);
    self.tags = ko.observableArray([]);
    self.addTag = function () {
        $.ajax("/api/tags", {
            data: ko.mapping.toJSON(self.newTag()),
            type: "post", contentType: "application/json",
            success: function (result) {
                var tag = new Tag(result);
                tag.isInEditMode(true);
                self.tags.push(tag);
                self.newTag(emptyTag());
            },
            error: function () { alert("failed"); }
        });
    }

    self.emptyTag = function () {
        return new Tag({
            id: null,
            title: null,
            manufacturer: null,
            price: null,
            salePrice: null,
            imageUrl: null
        });
    }

    self.deleteItem = function (item) {
        $.ajax("/api/tags/" + item.id(), {
            type: "delete",
            success: function (result) {
                self.tags.remove(item);
            },
            error: function () { alert("failed"); }
        });
    }
    
    self.newTag = ko.observable(self.emptyTag());
    
    
    // Load initial state from server, convert it to Task instances, then populate self.tasks
    $.getJSON("/api/tags", function (allData) {
        var mappedTags = $.map(allData, function (item) { return new Tag(item) });
        self.tags(mappedTags);
        self.isLoaded(true);
    });  
}

ko.applyBindings(new TagManager(viewModel));