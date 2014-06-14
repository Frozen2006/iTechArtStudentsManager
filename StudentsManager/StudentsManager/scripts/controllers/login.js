iTechArtStudentsManagerApp.controller('LoginCtrl', ['$scope', 'AuthProvider', '$location', function ($scope, authProvider, $location) {
    $scope.login = '';
    $scope.pass = '';
    //auth flag is in MainCtrl

    $scope.auth = function () {
        if (!authProvider.isAuthorized()) {
            authProvider.authorize();
            $location.path("/");
        }
    };
}]);