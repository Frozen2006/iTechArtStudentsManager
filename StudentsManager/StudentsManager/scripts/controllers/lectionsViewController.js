'use strict'

iTechArtStudentsManagerApp.controller('LectionsViewController', ['$scope', 'hubProvider', function ($scope, hubProvider) {

    var updateView = function () {
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    $scope.lections = [];

    $scope.currentLection = {
        name: '',
        src: '',
        page: 1,
        pdfObj: {}
    };

    hubProvider.call('serverConnection', 'getLections').done(function (data) {
        $scope.lections = data;

        updateView();
    });

    $scope.isLectionVisible = false;

    $scope.onLectionSelect = function (lection) {
        $scope.isLectionVisible = true;

        $scope.currentLection.name = lection.Name;
        $scope.currentLection.src = lection.Src;
        $scope.currentLection.page = 1;
        updateView();

        PDFJS.getDocument($scope.currentLection.src).then(function (pdf) {
            $scope.currentLection.pdfObj = pdf;
            renderPage(pdf, 1);
        });

    };


    $scope.toLeft = function () {
        if ($scope.currentLection.page === 1) {
            return;
        }
        $scope.currentLection.page -= 1;
        renderPage($scope.currentLection.pdfObj, $scope.currentLection.page);
    };

    $scope.toRight = function () {
        if ($scope.currentLection.page > $scope.currentLection.pdfObj.numPages) {
            return;
        }
        $scope.currentLection.page += 1;
        renderPage($scope.currentLection.pdfObj, $scope.currentLection.page);
    };

    var renderPage = function (pdf, page) {
        pdf.getPage(page).then(function (page) {
            var scale = 1.5;
            var viewport = page.getViewport(scale);

            var canvas = document.getElementById('pdfPage');
            var context = canvas.getContext('2d');
            canvas.height = viewport.height;
            canvas.width = viewport.width;

            var renderContext = {
                canvasContext: context,
                viewport: viewport
            };
            page.render(renderContext);
        });
    }
}]);
