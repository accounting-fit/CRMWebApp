angular.module('LoginApp', []).
    controller('LoginController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {
                              email: ""
                             ,userName: ""
                             ,password:""
        };

        $scope.AllClear = function () {
            $scope.model = {
                             email: ""
                            ,userName: ""
                            ,password: " "
            };
        }        

        $scope.Login = function () {
            var model = $scope.model;
            model.userName = model.email;
            var url = '/api/auth/login';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {                
                    alert(response.data.message);
                    window.location.href = "/Home/Index";
                    $scope.AllClear();
                }
            }, function (response) {
                console.log(response);
                alert(response.data.message)
            });
        }

    
    });




