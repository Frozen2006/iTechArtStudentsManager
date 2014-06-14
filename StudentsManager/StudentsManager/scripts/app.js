'use strict';

var iTechArtStudentsManagerApp = angular.module('iTechArtStudentsManagerApp', ['chartjs-directive']).config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
      .when('/', {
          templateUrl: 'views/main.html',
          controller: 'MainCtrl'
      })
      .when('/Login', {
          templateUrl: 'views/partials/loginPartial.html',
          controller: 'LoginCtrl'
      })
      .when('/Reports', {
          templateUrl: 'views/partials/reportsPartial.html',
          controller: 'ReportsController'
      })
      .when('/GroupsManagment', {
          templateUrl: 'views/partials/groupManagmentPartial.html',
          controller: 'GroupsManagmentController'
      })
      .otherwise({
          redirectTo: '/'
      });
    $locationProvider.html5Mode(true);
}]);


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