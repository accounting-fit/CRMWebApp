angular.module('CRMWebApp', []).
    controller('AuthenticationController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {
                         firstName:""
        };      

        $scope.AllClear = function () {
            $scope.model = {
                             firstName:""
                          };
        }       

        $scope.Save = function (isClose) {
            debugger;
            var model = $scope.model;       

            var url = '/api/Auth/Register';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        

                    } else {
                        console.log(response);
                       
                    }
                } else {
                    console.log(response);
                    
                }

            }, function (response) {
                console.log(response);
            });
        }

      
    });