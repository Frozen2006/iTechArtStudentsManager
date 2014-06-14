'use strict';

iTechArtStudentsManagerApp.controller('MainCtrl', ['$scope', 'hubProvider', function ($scope, hubProvider) {

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
