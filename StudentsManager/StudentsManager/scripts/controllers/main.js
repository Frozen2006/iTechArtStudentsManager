'use strict';

iTechArtStudentsManagerApp.controller('MainCtrl', ['$rootScope', 'hubProvider', '$location', 'AuthProvider','$sce' function ($scope, hubProvider, $location, authProvider, $sce) {
    $scope.isAuthentificated = {
        value: ''
    };

    $scope.$watch('isAuthentificated.value', function (newValue, oldValue) {
        if (newValue !== oldValue) {
            $scope.isAuthentificated.value = newValue;
            if ((typeof $scope.isAuthentificated !== 'undefined') && (!$scope.isAuthentificated.value)) {
                $location.path("/Login");
            }
            else {
                $location.path("/");
            }
        }
    }, true);


    $scope.isAuthentificated.value = authProvider.isAuthorized();

    if ((typeof $scope.isAuthentificated !== 'undefined') && (!$scope.isAuthentificated.value)) {
        $location.path("/Login");
    }
    else {
        $location.path("/");
    }

 
    $socpe.myData = {
        CompleatedTasksCount: 0,
        NewTaskCount: 0,
        Groups: [],
        Schedule: '' // $sce.trustAsHtml(taskData);
    };

    hubProvider.call('serverConnection', 'getHomePageData', window.StudentManager.userName).done(function (data) {
        $socpe.myData.CompleatedTasksCount = data.CompleatedTasksCount;
        $socpe.myData.NewTaskCount = data.NewTaskCount;
        $socpe.myData.Groups = data.Groups;
        $socpe.myData.Schedule = data.Schedule;

        $scope.$apply();
        
    });

}]);
