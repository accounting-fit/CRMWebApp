angular.module('CRMApp', []).
    controller('EventController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {                        
        };       

        $scope.AllClear = function () {
            $scope.model = {
                            
                          };
        }

        var typeList = [
            { id: "Call", text: "Call" }
            , { id: "Conference", text: "Conference" }
            , { id: "Meeting", text: "Meeting" }
            , { id: "Social", text: "Social" }
            , { id: "Fundraising", text: "Fundraising" }
            , { id: "Other", text: "Other" }
        ]
        $scope.typeList = typeList;
        var statusList = [
              { id: "Planned", text: "Planned" }
            , { id: "Held", text: "Held" }
            , { id: "Not Held", text: "Not Held" }
            , { id: "Other", text: "Other" }
        ]
        $scope.statusList = statusList;
        $scope.allDataList = [];
        
        $scope.GetAll = function () {
            var url = '/api/event/getall';
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