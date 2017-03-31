const TAG_LOCATION_SHELF = "OnShelf";
const TAG_LOCATION_FITTING_ROOM = "InFittingRoom";
const UPDATE_DELAY = 500;

function Tag(data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.interest = ko.observable("Unknown");
    self.location = ko.observable("Unknown");
    self.proximity = ko.observable("None");
}

function Tab(name, template, displayName) {
    this.name = name;
    this.template = template;
    this.displayName = displayName;
}

function DashboardViewModel(data) {
    var self = this;
    self.isLoaded = ko.observable(false);
    self.tags = ko.observableArray([]);

    self.tabs = [
        //new Tab('title', 'template-page-title', 'Title'),
        new Tab('summary', 'template-page-summary', 'Summary'),
        new Tab('location', 'template-page-location', 'Location'),
        new Tab('interest', 'template-page-interest', 'Interest'),
        new Tab('proximity-3', 'template-page-proximity-3', "Proximity Heat Map"),
        //new Tab('proximity', 'template-page-proximity', "Proximity"),
        //new Tab('proximity-2', 'template-page-proximity-2', "Proximity Heat Map"),
        //new Tab('sampledisplay', 'template-page-sample-display', 'Sample Display'),
        new Tab('retailbehavior', 'template-page-retail-behavior', "Retail Behavior")
    ];

    self.activeTab = ko.observable(self.tabs[0]);

    self.filteredTags = function (filter) {
        return ko.computed(function () {
            return ko.utils.arrayFilter(this.tags(), function (tag) {
                var result = filter(tag);
                return result;
            });
        }, this).extend({ deferred: true });
    }

    // Location arays
    self.tagsOnRack = self.filteredTags(function (tag) { return tag.location() == TAG_LOCATION_SHELF });
    self.tagsInFitting = self.filteredTags(function (tag) { return tag.location() == TAG_LOCATION_FITTING_ROOM });

    // Interest
    self.tagsByInterestNone = self.filteredTags(function (tag) { return tag.interest() == "None" });
    self.tagsByInterestBrowse = self.filteredTags(function (tag) { return tag.interest() == "Browse" });
    self.tagsByInterestInterested = self.filteredTags(function (tag) { return tag.interest() == "Interested" });

    self.tagsByProximityNearShopper = self.filteredTags(function (tag) { return tag.proximity() == "NearShopper" });
    self.tagsByProximityNotNearShopper = self.filteredTags(function (tag) { return tag.proximity() != "NearShopper" });

    self.proximityRackClass = ko.computed(function() {
        return 'hot-' + Math.min(self.tagsByProximityNearShopper().length, 4);
    });
    self.activeTag = ko.computed(function () {
        for (var i = 0; i < self.tags().length; i++) {
            var tag = self.tags()[i];
            if (tag.interest() == "Interested") {
                return tag;
            }
        }
        
        return null;
    });
    
    self.loadStatus = function () {
        $.ajax("/api/tagsstatus", {
            type: "get",
            cache: false,
            success: function (result) {
                for (var i = 0; i < self.tags().length; i++) {
                    var tag = self.tags()[i];
                    var tagData = result[tag.id()] || { location: "Unknown", interest: "None", proximity: "Unknown" };

                    tag.location(tagData.location);
                    tag.interest(tagData.interest);
                    tag.proximity(tagData.proximity);
                }

                setTimeout(self.loadStatus, UPDATE_DELAY);
            },
            error: function () {
                setTimeout(self.loadStatus, UPDATE_DELAY);
            }
        });
    }

    // Load initial state from server, convert it to Task instances, then populate self.tasks
    $.getJSON("/api/tags", function (allData) {
        var mappedTags = $.map(allData, function (item) { return new Tag(item) });
        self.tags(mappedTags);
        self.isLoaded(true);
        self.loadStatus();
    });
}

ko.applyBindings(new DashboardViewModel());