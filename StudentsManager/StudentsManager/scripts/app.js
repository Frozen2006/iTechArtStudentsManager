'use strict';

var iTechArtStudentsManagerApp = angular.module('iTechArtStudentsManagerApp', []).config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
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
            controller:'MainCtrl'
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
            }
        };
    };
});

iTechArtStudentsManagerApp.provider('AuthProvider', function () {
    var isAuthorized = false;
    var self = this;

    this.$get = function () {
        return {
            isAuthorized: function(){
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