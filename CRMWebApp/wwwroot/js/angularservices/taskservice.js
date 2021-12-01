angular.module('CRMApp', []).
    controller('TaskController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {                        
        };


        var statusList = [
                              { id: "Open", text: "Open" }
                            , { id: "Done", text: "Done" }
                            , { id: "Not Done", text: "Not Done" }
                            , { id: "Other", text: "Other" }
                          ]
        $scope.statusList = statusList;

        var refer_typeList = [
              { id: "Account", text: "Account" }
            , { id: "Contact", text: "Contact" }
            , { id: "Lead", text: "Lead" }
                 ]
        $scope.refer_typeList = refer_typeList;

        $scope.AllClear = function () {
            $scope.model = {
                            
                          };
        }

        $scope.allDataList = [];
        
        $scope.GetAll = function () {
            var url = '/api/task/getall';
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