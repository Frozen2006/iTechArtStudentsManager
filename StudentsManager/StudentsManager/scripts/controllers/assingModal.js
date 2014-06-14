'use strict';

iTechArtStudentsManagerApp.controller('AssignModalController', ['$scope', 'hubProvider', '$modalInstance', 'taskInfo', function ($scope, hubProvider, $modalInstance, taskInfo) {
    $scope.isErrorVisible;

    $scope.users = [];
    $scope.selectedUser;

    $scope.taskInfo = taskInfo;

    hubProvider.call('serverConnection', 'getStudentsAvalableForTask', taskInfo.Title, taskInfo.Tag).done(function (data) {
        $scope.users = data;
        $scope.$apply();
    });

    $scope.cancel = function () {
        $modalInstance.dismiss();
    };

    $scope.ok = function () {
        if (!$scope.selectedUser) {
            $scope.isErrorVisible = true;
            return;
        };
        $modalInstance.close($scope.selectedUser);
    };

}]);
