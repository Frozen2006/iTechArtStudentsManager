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
            init: function(callback) {
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
                var deffered = new $.Deffered();

                if (typeof connection !== 'undefined') {
                    return connection[hubName].client[methodName](params);
                };

                self.init(function () {
                    connection[hubName].client[methodName](params).done(function (data) {
                        deffered.resolve(data);
                    });
                });

                return deffered.promise;
            }
        };
    };
});