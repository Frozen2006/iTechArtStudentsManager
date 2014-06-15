'use strict';

iTechArtStudentsManagerApp.controller('RatingCtrl', ['$scope', 'hubProvider', '$filter', function ($scope, hubProvider, $filter) {
    $scope.students = [];
    $scope.averageMarks = [];
    $scope.data = {
        ratings: []
    };
    $scope.sorter = {
        baseField: '',
        info: [],
        sortable: []
    };
    $scope.query = {};
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;
    $scope.totalItems = 0;
    $scope.maxSize = 10;
    $scope.showItems = [];

    $scope.$watch('sorter.info', function () {
        $scope.totalItems = $scope.sorter.info.length;
        $scope.sorter.sortable = $scope.sorter.info;
    });

    $scope.refillPageNum = function () {
        $scope.sorter.sortable = $filter('filter')($scope.sorter.info, { Mark: $scope.query.Mark });
        $scope.totalItems = $scope.sorter.sortable.length;
    };

    $scope.selectedIndex = 0;

    hubProvider.call('serverConnection', 'getStudents').done(function (data) {
        $scope.students = data;
        //$scope.$apply();
        $scope.showRating();
    });

    $scope.showRating = function () {
        var studentsAmount = $scope.students.length;

        for (var i = 0; i < studentsAmount; i++) {
            getStudentMarks($scope.students[i].UserName, i);
            $scope.$apply();
        };
      //  $scope.ratings = [{ UserName: 'asjfa', Mark: 7.0 }, { UserName: 'ureytl', Mark: 6.5 }];
        $scope.sorter.info = $scope.data.ratings;

        $scope.$apply();
    };

    var getStudentMarks = function (studentName, i) {
        var marks = [];
        hubProvider.call('serverConnection', 'getStudentMarks', studentName).done(function (marks) {
            $scope.averageMarks[i] = countAverageMark(marks);

            $scope.data.ratings[i] = {
                UserName: $scope.students[i].UserName,
                LastAndFirstName: $scope.students[i].LastAndFirstName,
                Mark: $scope.averageMarks[i]
            };
            $scope.$apply();
        });
    };

    var countAverageMark = function (marks) {
        var sum = 0;
        var marksAmount = marks.length;

        if (marksAmount === 0) {
            return 0;
        }

        for (var i = 0; i < marksAmount; i++) {
            sum += marks[i].Mark;
        }

        return sum / marksAmount;
    };

    $scope.headers = [{ name: 'User name', field: 'UserName' }, {name: 'Average mark', field: 'Mark'}];
}]);