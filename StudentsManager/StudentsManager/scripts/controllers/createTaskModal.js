'use strict';

iTechArtStudentsManagerApp.controller('CreateTaskModalController', ['$scope', 'hubProvider', '$modalInstance', function ($scope, hubProvider, $modalInstance) {

    $scope.currentTask = {
        title: '',
        tag: '',
        content: '',
        complexLevel: ''
    };

    $scope.isErrorVisible = false;

    $scope.cancel = function () {
        $modalInstance.dismiss();
    };

    $scope.ok = function () {
        if (!$scope.currentTask.title || !$scope.currentTask.tag || !$scope.currentTask.content 
            || !$scope.currentTask.complexLevel.match(/^[0-9]+$/) ) {
            $scope.isErrorVisible = true;
            return;
        };
        $modalInstance.close({
            Title: $scope.currentTask.title,
            Tag: $scope.currentTask.tag,
            Content: $scope.currentTask.content,
            ComplexLevel: $scope.currentTask.complexLevel
        });
    };

}]);
