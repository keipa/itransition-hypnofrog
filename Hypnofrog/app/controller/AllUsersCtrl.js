﻿angular.module("Hypnofrog.AllUsersController", [])
    .controller("AllUsersCtrl",
    [
        "$scope", "$http", function($scope, $http) {
            $http.get("AllUsersVM/")
                .success(function (data) {
                    $scope.visibility = true;
                    $scope.model = data;
                });
            $http.get("WhoamiVM/")
                .success(function(data) {
                    $scope.currentuser = data;
                });
        }
    ]);