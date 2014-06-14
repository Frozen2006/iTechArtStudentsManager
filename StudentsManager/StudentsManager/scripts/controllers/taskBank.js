'use strict';

iTechArtStudentsManagerApp.controller('TaskBankController', ['$scope', 'hubProvider', '$modal', '$sce', function ($scope, hubProvider, $modal, $sce) {

    var updateView = function () {
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };


    $scope.taskTags = [];


    $scope.createTask = function () {
        var modal = $modal.open({
            templateUrl: 'views/modals/createTask.html',
            controller: 'CreateTaskModalController',
            size: 'lg'
        });

        modal.result.then(function (taskData) {
            createTask(taskData);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };


    var createTask = function (taskData) {
        var existedTag = $.map($scope.taskTags, function (element) {
            if (element.Tag.toLowerCase() === taskData.Tag.toLowerCase()) {
                return element;
            }
        });

        if (existedTag.length > 0) {
            existedTag[0].Tasks.push(taskData);
        } else {
            var tagData = {
                Tag: taskData.Tag,
                Tasks: [taskData]
            };

            $scope.taskTags.push(tagData);
        }


        hubProvider.call('serverConnection', 'createTask', taskData);
    };

    $scope.currentTaskInfo = {};
    $scope.currentTaskInfo.isShow = false;
    $scope.currentTaskInfo.title = '';
    $scope.currentTaskInfo.tag = '';
    $scope.currentTaskInfo.text = '';
    $scope.currentTaskInfo.level = '';

    $scope.onTaskSelected = function (task) {
        hubProvider.call('serverConnection', 'getTaskText', task.Title, task.Tag).done(function (taskData) {
            $scope.currentTaskInfo.isShow = true;
            $scope.currentTaskInfo.text = $sce.trustAsHtml(taskData);
            $scope.currentTaskInfo.title = task.Title;
            $scope.currentTaskInfo.tag = task.Tag;
            $scope.currentTaskInfo.level = task.complexLevel;
            updateView();
        });
    };


    $scope.onAssign = function () {
        var modalInstance = $modal.open({
            templateUrl: 'views/modals/assignTask.html',
            controller: 'AssignModalController',
            size: 'lg',
            resolve: {
                taskInfo: function () {
                    return {
                        Title: $scope.currentTaskInfo.title,
                        Tag: $scope.currentTaskInfo.tag
                    };
                }
            }
        });

        modalInstance.result.then(function (selectedUser) {
            hubProvider.call('serverConnection', 'assignTaskToUser', $scope.currentTaskInfo.title, $scope.currentTaskInfo.tag, selectedUser.UserName).done(function () {
                alert('Assigned!');
            });
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    
    };


    hubProvider.call('serverConnection', 'getTaskList').done(function (taskList) {
        $scope.taskTags = taskList;
        updateView();
    });

}]);
