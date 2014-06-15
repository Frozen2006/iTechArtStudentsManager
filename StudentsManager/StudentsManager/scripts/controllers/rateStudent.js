'use strict';

iTechArtStudentsManagerApp.controller('RateStudentController', ['$scope', 'hubProvider', '$modal', function ($scope, hubProvider, $modal) {
    var updateView = function () {
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };


    $scope.students = [];

    $scope.currentStudent = '';

    $scope.isTaskVisible = false;
    $scope.studentTasks = [];


    hubProvider.call('serverConnection', 'getStudentsWithTasks').done(function (data) {
        $scope.students = data;
        
        updateView();
    });

    $scope.studentClick = function (student) {
        $scope.currentStudent = student;
        $scope.isTaskVisible = true;

        hubProvider.call('serverConnection', 'getUserTasks', student.UserName).done(function (data) {
            $scope.studentTasks = data;

            updateView();
        });
    };

    $scope.rateTask = function (task) {
        var modalInstance = $modal.open({
            templateUrl: 'views/modals/rateStudentTaskModal.html',
            controller: 'RateStudentModalController',
            size: 'lg',
            resolve: {
                taskData: function () {
                    return {
                        task: task,
                        userName: $scope.currentStudent.UserName
                    };
                }
            }
        });
    };

}]);
