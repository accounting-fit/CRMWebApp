angular.module('CRMApp', []).
    controller('LeadController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {                        
        };       

        $scope.AllClear = function () {
            $scope.model = {
                            
                          };
        }

        $scope.allDataList = []; 

        var lead_sourceList = [
            { id: "Call", text: "Call" }
            , { id: "Customer", text: "Customer" }
            , { id: "Employee", text: "Employee" }
            , { id: "Parner", text: "Parner" }
            , { id: "Confrence", text: "Confrence" }
            , { id: "Customer", text: "Customer" }
            , { id: "Trade", text: "Trade" }
            , { id: "Website", text: "Website" }
            , { id: "Email", text: "Email" }
            , { id: "Other", text: "Other" }
        ]

        $scope.lead_sourceList = lead_sourceList;

        var lead_statusList = [
              { id: "New", text: "New" }
            , { id: "Assigned", text: "Assigned" }
            , { id: "In Process", text: "In Process" }
            ,{ id: "Converted", text: "Converted" }
            , { id: "Recycled", text: "Recycled" }
            , { id: "Other", text: "Other" }
        ]

        $scope.lead_statusList = lead_statusList;
        
        $scope.GetAll = function () {
            var url = '/Api/Lead/GetAll';
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

        $scope.Save = function (isClose) {

            var model = $scope.model;

            var url = '/Api/Lead/Save';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        alert("Save Successfully")
                        if (isClose === 1) {
                            window.location.href = "/Lead/Index";
                        }
                        else {
                            window.location.href = "/Lead/Create";
                        }
                    } else {
                        console.log(response);
                        alert("Save Failed")
                    }
                } else {
                    console.log(response);
                    alert("Something  wrong")
                }

            }, function (response) {
                console.log(response);
            });
        }
    });