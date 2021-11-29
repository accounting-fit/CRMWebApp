angular.module('AuthApp', []).
    controller('RegisterController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {
                             firstName: ""
                            ,lastName: ""
                            ,email: ""
                            ,userName:""
                            ,password: ""
                            ,passwordConfrim:""
                      
        };

        $scope.AllClear = function () {
            $scope.model = {
                                 firstName: ""
                                ,lastName: ""
                                ,email: ""
                                ,userName: ""
                                ,password: ""                               
                                ,passwordConfrim: ""
                          };
        }        

        $scope.Register = function () {

            var model = $scope.model;
            model.userName = model.email;

            var url = '/api/auth/register';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {                
                    alert(response.data.message);
                    window.location.href = "/Authentication/Login";
                    $scope.AllClear();
                }
            }, function (response) {
                console.log(response);
                alert(response.data.message)
            });
        }

    
    });




