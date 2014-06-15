'use strict'

iTechArtStudentsManagerApp.controller('IndividualTasksCtrl', ['$scope', 'hubProvider', function ($scope, hubProvider) {

    var updateView = function () {
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    $scope.tasks = [];
   
    $scope.currentTask = null;
    $scope.isTaskDescriptionVisible = false;

    $scope.taskDetails = null;
    
  
    $scope.taskClick = function (groupName) {
        $scope.currentTask = taskName;
        $scope.isTaskDescriptionVisible = true;
        hubProvider.call('serverConnection', 'getTaskDetails', taskName).done(function (data) {
            $scope.taskDetails = data;

            updateView();
        });
    };

    var userName = null;
    userName = window.StudentManager && window.StudentManager.userName;
    hubProvider.call('serverConnection', 'getCurrentTasksNames', userName).done(function (tasksList) {
        $scope.tasks = tasksList;
        updateView();
    });

}]);
