iTechArtStudentsManagerApp.controller('LoginCtrl', ['$scope', function ($scope) {
    $scope.login = '';
    $scope.pass = '';

    $scope.authenticate = function () {
        console.log('Auth with' + $scope.login + ' ' + $scope.pass);
        var url = "AjaxWebService.asmx";
        $.ajax({
            type: "POST",
            url: url + "/Login",
            data: "{'username':'" + $scope.login + "', 'passwordHash':'" + $scope.pass + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessCall,
            error: OnErrorCall
        });

        function OnSuccessCall(responseJson) {
            var response = JSON.parse(responseJson.d);
            alert(response.authenticationResult);
        }


        function OnErrorCall(response) {
            alert(response.status + " " + response.statusText);
        }
    }


}]);