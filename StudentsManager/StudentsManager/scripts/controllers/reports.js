'use strict';

iTechArtStudentsManagerApp.controller('ReportsController', ['$scope', 'hubProvider', function ($scope, hubProvider) {

    $scope.students = [];


    $scope.MyChart = {
        width: 500,
        height: 500,
        options: {},
        data: {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
                {
                    fillColor: "rgba(220,220,220,0.5)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    data: [65, 59, 90, 81, 56, 55, 40]
                },
                {
                    fillColor: "rgba(151,187,205,0.5)",
                    strokeColor: "rgba(151,187,205,1)",
                    pointColor: "rgba(151,187,205,1)",
                    pointStrokeColor: "#fff",
                    data: [28, 48, 40, 19, 96, 27, 100]
                }
            ]
        }
    };

    $scope.isStudentSelected = false;
    $scope.currentStudent = null;
    $scope.selectStudent = function (student) {
        $scope.isStudentSelected = false;
        $scope.$apply();

        $scope.currentStudent = student;
        hubProvider.call('serverConnection', 'getStudentMarks', student).done(function (data) {

            $scope.isStudentSelected = true;

            $scope.MyChart = {
                data: {
                    labels: $.map(data, function (element) {
                        return elememt.tag
                    }),
                    datasets: [{
                        fillColor: "rgba(220,220,220,0.5)",
                        strokeColor: "rgba(220,220,220,1)",
                        pointColor: "rgba(220,220,220,1)",
                        pointStrokeColor: "#fff",
                        data: $.map(data, function (element) {
                            return element.mark
                        })
                    }]
                }
            };
        });

    };

    hubProvider.call('serverConnection', 'getStudents', null).done(function (data) {
        $scope.students = data;
        $scope.$apply();
    });
    

}]);
