angular.module('ForgotPasswordApp', []).
    controller('ForgotPasswordController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {
                              email: ""                            
                       };

        $scope.AllClear = function () {
            $scope.model = {
                            email: ""
            };
        }        

        $scope.Login = function () {
            var model = $scope.model;           
            var url = '/api/auth/ForgotPassword';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {                
                    alert(response.data.message);                   
                    $scope.AllClear();
                }
            }, function (response) {
                console.log(response);
                alert(response.data.message)
            });
        }

    
    });




