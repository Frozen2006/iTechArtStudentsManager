'use strict';

iTechArtStudentsManagerApp.controller('MainCtrl', ['$rootScope', 'hubProvider', '$location', 'AuthProvider','$sce', function ($scope, hubProvider, $location, authProvider, $sce) {
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

 
    $scope.myData = {
        CompleatedTasksCount: 0,
        NewTaskCount: 0,
        Groups: [],
        Schedule: '' // $sce.trustAsHtml(taskData);
    };

    hubProvider.call('serverConnection', 'getHomePageData', window.StudentManager.userName).done(function (data) {
        $scope.myData.CompleatedTasksCount = data.CompleatedTasksCount;
        $scope.myData.NewTaskCount = data.NewTaskCount;
        $scope.myData.Groups = data.Groups;
        $scope.myData.Schedule = data.Schedule;

        $scope.$apply();
        
    });


    var messagesRef = new Firebase("https://shining-fire-8690.firebaseio.com/people");
    // Automatically syncs everywhere in realtime
    $scope.messages = $firebase(messagesRef);
    $scope.lastMessages = [];


    $scope.$watch('messages', function(){
        var count = 0;
        $scope.lastMessages = [];

        for(var i=$scope.messages.length; i>0; i--) {
            if (count > 4) {
                return;
            }
            $scope.lastMessages.push($scope.messages[i]);
        } 
    });

    $scope.currentMessage = '';

    $scope.addMessage = function() {
        // AngularFire $add method
        $scope.messages.$add($scope.currentMessage);
 
        $scope.currentMessage = "";
    };

}]);
