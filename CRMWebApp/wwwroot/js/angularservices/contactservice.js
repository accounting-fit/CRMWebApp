angular.module('CRMApp', []).
    controller('ContactController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {                        
        };       

        $scope.AllClear = function () {
            $scope.model = {
                            
                          };
        }

        $scope.allDataList = [];
        
        $scope.GetAll = function () {
            var url = '/api/contact/getall';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.AllDataList = data.allDataList;
                }

            }, function (response) {
                console.log(response);
            });
        }        
    });