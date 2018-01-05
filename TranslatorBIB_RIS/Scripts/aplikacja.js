
    var aplikacja = angular.module('aplikacja', []);

        aplikacja.filter('dateToISO', function () {
            return function (input) {
                var dateTime = input.split("T");
                var date = dateTime[0];
                var datePartials = date.split("-");
                var time = dateTime[1];
                var timePartials = time.split(":");
                var formattedDate = new Date();
                formattedDate.setFullYear(datePartials[0]);
                formattedDate.setMonth(datePartials[1] - 1);
                formattedDate.setDate(datePartials[2]);
                formattedDate.setHours(timePartials[0]);
                formattedDate.setMinutes(timePartials[1]);
                return formattedDate;
            };
        });

        aplikacja.controller('tableController', ['$scope', '$http', '$filter', function ($scope, $http, $filter) {



        $http.get("/api/records/")
            .then(function (response) {
                $scope.records = response.data;
            })

            $http.get("/api/records/headlines/")
                .then(function (response) {
        $scope.headlines = response.data;
    })

            $scope.deleteOneBook = function (id) {
                var url = "/api/records/" + id;
                $http.delete(url)
                    .then(function (response) {
        $scope.del = response.data;
    })

                window.location = location.href;
            }

            

            $scope.getDate = function (date) {
                return $filter('dateToISO')(date);
            };


        }]);






