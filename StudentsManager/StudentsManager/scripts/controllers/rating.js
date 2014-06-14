'use strict';

iTechArtStudentsManagerApp.controller('RatingCtrl', ['$scope', 'hubProvider', '$filter', function ($scope, hubProvider, $filter) {
    $scope.students = [];
    $scope.averageMarks = [];
    $scope.ratings = [];

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
        $scope.$apply();
    });

    $scope.showRating = function () {
        var studentsAmount = $scope.students.length;

        for (var i = 0; i < studentsAmount; i++) {
            var marks = getStudentMarks($scope.students[i].UserName);
            $scope.averageMarks[i] = countAverageMark(marks);

            $scope.ratings[i] = {
                UserName: $scope.students[i].UserName,
                Mark: $scope.averageMarks[i]
            };
            $scope.$apply();
        };
      //  $scope.ratings = [{ UserName: 'asjfa', Mark: 7.0 }, { UserName: 'ureytl', Mark: 6.5 }];
        $scope.sorter.info = $scope.ratings;
    };

    var getStudentMarks = function (studentName) {
        var marks = [];
        hubProvider.call('serverConnection', 'getStudentMarks', student.UserName).done(function (data) {
            marks = data;
        });
        return marks;
    };

    var countAverageMark = function (marks) {
        var sum;
        var marksAmount = marks.length;

        for (var i = 0; i < marksAmount; i++) {
            sum += marks[i].Mark;
        }

        return sum / marksAmount;
    };

    $scope.headers = [{ name: 'User name', field: 'UserName' }, {name: 'Average mark', field: 'Mark'}];
}]);