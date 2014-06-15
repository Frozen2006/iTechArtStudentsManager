'use strict';

iTechArtStudentsManagerApp.controller('RateStudentModalController', ['$scope', 'hubProvider', '$modalInstance', 'taskData', function ($scope, hubProvider, $modalInstance, taskData) {
    $scope.task = taskData.task;

    $scope.mark = {
        val: 0,
        comment: ''
    };

    $scope.cancel = function () {
        $modalInstance.dismiss();
    };

    $scope.ok = function () {
        hubProvider.call('serverConnection', 'saveTaskResults', taskData.userName, $scope.task.Title, $scope.task.Tag, $scope.mark.val, $scope.mark.comment);
        $modalInstance.close();
    };
}]);
