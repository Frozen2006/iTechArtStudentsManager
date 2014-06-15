'use strict';

iTechArtStudentsManagerApp.controller('UsersManagmentController', ['$scope', 'hubProvider', function ($scope, hubProvider) {

    var updateView = function () {
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    /*Markup*/
    $scope.users = [];
    $scope.currentUser = null;
   
    $scope.newUserName = '';
    $scope.newUserPassword = '';

    $scope.createUser = function () {
        if ($.map($scope.users, function (element) { //element existed
            if (element === $scope.newUserName) {
                return element;
            }
        }) > 0) {
            $scope.newUserName = '';
            $scope.newUserPassword = '';
            return;
        }
        $scope.users.push($scope.newUserName);
        hubProvider.call('serverConnection', 'createUser', $scope.newUserName, $scope.newUserPassword);

        $scope.newUserName = '';
        $scope.newUserPassword = '';

        updateView();
    };

    $scope.userClick = function (userName) {
        $scope.currentUser = userName;
       
    };

    hubProvider.call('serverConnection', 'getUsers').done(function (groupsList) {
        $scope.users = usersList;
        updateView();
    });

}]);
