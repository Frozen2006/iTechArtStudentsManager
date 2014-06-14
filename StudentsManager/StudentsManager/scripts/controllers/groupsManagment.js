'use strict';

iTechArtStudentsManagerApp.controller('GroupsManagmentController', ['$scope', 'hubProvider', function ($scope, hubProvider) {

    var updateView = function () {
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    /*Markup*/
    $scope.groups = [];
    $scope.currentAssignedUsers = [];
    $scope.currentUnassignedUsers = [];
    $scope.currentGroup = null;
    $scope.isUserListsIsVisible = false;


    $scope.unassingn = function (user) {
        hubProvider.call('serverConnection', 'unassignUser', $scope.currentGroup, user.UserName);

        $scope.currentAssignedUsers = $.map($scope.currentAssignedUsers, function (el) {
            if (el !== user) {
                return el;
            }
        });

        $scope.currentUnassignedUsers.push(user);

        updateView();
    };

    $scope.assingn = function (user) {
        hubProvider.call('serverConnection', 'assignUser', $scope.currentGroup, user.UserName);

        $scope.currentUnassignedUsers = $.map($scope.currentUnassignedUsers, function (el) {
            if (el !== user) {
                return el;
            }
        });

        $scope.currentAssignedUsers.push(user);

        updateView();
    };

    $scope.newGroupeName = '';

    $scope.createGroup = function () {
        if ($.map($scope.groups, function(element) { //element existed
            if (element === $scope.newGroupeName) {
                return element; 
                }
        }) > 0) {
            $scope.newGroupeName = '';
            return;
        }
        $scope.groups.push($scope.newGroupeName);
        hubProvider.call('serverConnection', 'createGroup', $scope.newGroupeName);
        $scope.newGroupeName = '';

        updateView();
    };

    $scope.groupClick = function (groupName) {
        $scope.currentGroup = groupName;
        $scope.isUserListsIsVisible = true;
        hubProvider.call('serverConnection', 'getAssignStatusForGroup', groupName).done(function (data) {
            $scope.currentAssignedUsers = data.Assigned;
            $scope.currentUnassignedUsers = data.Unassigned;

            updateView();
        });
    };

    hubProvider.call('serverConnection', 'getGroups').done(function (groupsList) {
        $scope.groups = groupsList;
        updateView();
    });

}]);
