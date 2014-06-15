'use strict';

iTechArtStudentsManagerApp.controller('ReportsController', ['$scope', 'hubProvider', function ($scope, hubProvider) {

    $scope.students = [];

    $scope.MyChart = {
        width: $("#chartContainer").width() - 50,
        height: $("#chartContainer").height() < (screen.height - 400) ? screen.height - 400 : $("#chartContainer").height(),
        options: {},
        data: {
            labels: [],
            datasets: [            ]
        }
    };

    $scope.isStudentSelected = false;
    $scope.currentStudent = null;
    $scope.selectStudent = function (student) {
        $scope.isStudentSelected = false;

        /*if (!$scope.$$phase) {
            $scope.$apply();
        }*/

        $scope.currentStudent = student;
        hubProvider.call('serverConnection', 'getStudentMarks', student.UserName).done(function (data) {

            $scope.isStudentSelected = true;
            //$scope.MyChart.data.datasets[0].data[0] = Math.floor(Math.random() * 10);
            $scope.MyChart.data =  {
                    labels: $.map(data, function (element) {
                        return element.Tag
                    }),
                    datasets: [{
                        fillColor: "rgba(205,0,0,0.5)",
                        strokeColor: "rgba(220,220,220,1)",
                        pointColor: "rgba(220,220,220,1)",
                        pointStrokeColor: "#fff",
                        data: $.map(data, function (element) {
                            return element.Mark
                        })
                    }]
                };

            if (!$scope.$$phase) {
                $scope.$apply();
            }
        });

    };

    hubProvider.call('serverConnection', 'getStudents').done(function (data) {
        $scope.students = data;
        $scope.$apply();
    });
    

}]);
