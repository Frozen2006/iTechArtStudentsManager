'use strict';

iTechArtStudentsManagerApp.controller('ScheduleModalController', ['$scope', 'hubProvider', '$modalInstance', 'groupName', function ($scope, hubProvider, $modalInstance, groupName) {
    $scope.schedule = {};
    $scope.schedule.data = '';

    $scope.group = groupName;


    hubProvider.call('serverConnection', 'getGroupSchedule', groupName).done(function (data) {
        $scope.schedule.data = data;
        $scope.$apply();
    });

    $scope.cancel = function () {
        $modalInstance.dismiss();
    };

    $scope.ok = function () {
        hubProvider.call('serverConnection', 'saveGroupSchedule', groupName, $scope.schedule.data);

        $modalInstance.close();
    };
}]);
