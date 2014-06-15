'use strict';

var iTechArtStudentsManagerApp = angular.module('iTechArtStudentsManagerApp', ['chartjs-directive', 'ui.bootstrap', 'ngRoute', 'ui.tinymce', 'ngSanitize', "firebase"])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
       .when('/', {
           templateUrl: 'views/main.html',
           controller: 'MainCtrl'
       })
      .when('/Login', {
          templateUrl: 'views/partials/loginPartial.html',
          controller: 'LoginCtrl'
      })
        .when('/Home', {
            templateUrl: 'views/partials/homePartial.html',
            controller: 'MainCtrl'
        })
      .when('/Reports', {
          templateUrl: 'views/partials/reportsPartial.html',
          controller: 'ReportsController'
      })
      .when('/GroupsManagment', {
          templateUrl: 'views/partials/groupManagmentPartial.html',
          controller: 'GroupsManagmentController'
      })
     .when('/Rating', {
         templateUrl: 'views/partials/ratingPartial.html',
         controller: 'RatingCtrl'
     })
    .when('/TaskManagment', {
        templateUrl: 'views/partials/taskBankPartial.html',
        controller: 'TaskBankController'
    })
     .when('/RateStudent', {
         templateUrl: 'views/partials/rateStudentPartial.html',
         controller: 'RateStudentController'
     })
    .otherwise({
        redirectTo: '/'
    });
        $locationProvider.html5Mode(true);
    }]);



iTechArtStudentsManagerApp.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});


iTechArtStudentsManagerApp.provider('hubProvider', function () {
    var connection;
    var self = this;


    this.init = function (callback) {
        if (!connection) {
            connection = $.connection;
            connection.hub.start().done(callback);
        } else {
            callback();
        }
    };

    this.$get = function () {
        return {
            init: function (callback) {
                self.init(callback);
            },
            callbacks: {
            },
            getHub: function (hubname) {
                if (typeof connection !== 'undefined') {
                    return connection[hubname].server;
                }
                return null;
            },
            getListener: function (hubname) {
                if (typeof connection !== 'undefined') {
                    return connection[hubname].client;
                }
                return null;
            },
            reInit: function () {
                self.init();
            },
            call: function (hubName, methodName, params) {
                var deffered = new $.Deferred;

                var argsForExternal = [];
                for (var i = 2; i < arguments.length; i++) {
                    argsForExternal.push(arguments[i]);
                };

                if (typeof connection !== 'undefined') {
                    if (params) {
                        return $.connection[hubName].server[methodName].apply(this, argsForExternal);
                    } else {
                        return $.connection[hubName].server[methodName]();
                    }

                };

                self.init(function () {
                    if (params) {
                        $.connection[hubName].server[methodName].apply(this, argsForExternal).done(function (data) {
                            deffered.resolve(data);
                        });
                    } else {
                        $.connection[hubName].server[methodName]().done(function (data) {
                            deffered.resolve(data);
                        });
                    }
                });

                return deffered.promise();
            }
        };
    };
});

iTechArtStudentsManagerApp.provider('AuthProvider', function () {
    var isAuthorized = false;
    var self = this;

    this.$get = function () {
        return {
            isAuthorized: function () {
                return isAuthorized;
            },
            authorize: function () {
                isAuthorized = true;
            },
            logOut: function () {
                isAuthorized = false;
            },
            subscribe: function (callback) {

            }
        };
    };
});