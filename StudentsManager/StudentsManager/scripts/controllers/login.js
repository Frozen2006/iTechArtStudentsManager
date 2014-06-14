iTechArtStudentsManagerApp.controller('LoginCtrl', ['$scope', 'AuthProvider', '$location', function ($scope, authProvider, $location) {
    $scope.login = '';
    $scope.pass = '';
    $scope.message = ''

    $scope.authenticate = function () {
        console.log('Auth with' + $scope.login + ' ' + $scope.pass);
        var url = "AjaxWebService.asmx";
        $.ajax({
            type: "POST",
            url: url + "/Login",
            data: "{'username':'" + $scope.login + "', 'passwordHash':'" + $scope.pass + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function OnSuccessCall(responseJson) {
                var response = responseJson.d;
                if (response.authenticationResult) {

                    $scope.message = 'Authenticated...';
                    $scope.$apply();

                    authProvider.authorize();
                    $location.path("/");

                    console.log('Authenticated...');
                } else {
                    $scope.message = 'Authentication failed.';
                    $scope.$apply();
                    console.log('Authentication failed.');
                }
               
                console.log('Success');
            },
            error: function OnErrorCall(response) {
                $scope.message = response.responseText;
            }
           
        });

    }
}]);