iTechArtStudentsManagerApp.controller('LoginCtrl', ['$scope', 'AuthProvider', '$location', function ($scope, authProvider, $location) {
    $scope.login = '';
    $scope.pass = '';
    $scope.message = ''

    var serviceUrl = "AjaxWebService.asmx";

    $scope.authenticate = function () {
        console.log('Auth with' + $scope.login + ' ' + $scope.pass);
        
        $.ajax({
            type: "POST",
            url: serviceUrl + "/Login",
            data: "{'username':'" + $scope.login + "', 'passwordHash':'" + $scope.pass + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function OnSuccessCall(responseJson) {
                var response = responseJson.d;
                if (response.authenticationResult) {

                    if (!window.StudentsManager) {
                        window.StudentsManager = {};
                    }
                    window.StudentsManager.userName = resoinse.userName;

                    $scope.message = 'Authenticated...';
                    $scope.$apply();

                    authProvider.authorize();
                    $location.path("/");

                    //getRoleName();
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

    var getRoleName = function () {
        $.ajax({
            type: "POST",
            url: serviceUrl + "/GetRoleName",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function OnSuccessCall(responseJson) {
                $scope.roleName = '';
            },
            error: function OnErrorCall(response) {
                $scope.message = $scope.message + 'Error getting role';
            }

        });

    };
}]);