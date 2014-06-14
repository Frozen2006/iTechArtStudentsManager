'use strict';

iTechArtStudentsManagerApp.controller('MainCtrl', ['$rootScope', 'hubProvider', '$location', 'AuthProvider', function ($scope, hubProvider, $location, authProvider) {
    $scope.isAuthentificated = {
        value: ''
    };

    $scope.selected = {
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

  $scope.awesomeThings = [
    'HTML5 Boilerplate',
    'AngularJS',
    'Testacular'
  ];
      if (!hubProvider.getHub('serverConnection')) {
          hubProvider.init(function () {
              hubProvider.getHub('serverConnection').helloWorld().done(function (data) {
                  $scope.awesomeThings = data;
                  $scope.$apply();
              });
          });
      } else {
          hubProvider.getHub('serverConnection').helloWorld().done(function (data) {
              $scope.awesomeThings = data;
              $scope.$apply();
          });
      }


}]);
