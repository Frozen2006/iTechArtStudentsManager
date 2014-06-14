'use strict';

var iTechArtStudentsManagerApp = angular.module('iTechArtStudentsManagerApp', []).config(['$routeProvider', function ($routeProvider) {
    $routeProvider
      .when('/', {
          templateUrl: 'views/main.html',
          controller: 'MainCtrl'
      })
      .otherwise({
          redirectTo: '/'
      });
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