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

        $scope.GetById = function (id) {
            var url = '/Api/Lead/GetById/' + id;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.model.lead_id = data.singleData.lead_id;
                    $scope.model.fname = data.singleData.fname;
                    $scope.model.lname = data.singleData.lname;
                    $scope.model.mname = data.singleData.mname;
                    $scope.model.sales_person = data.singleData.sales_person;
                    $scope.model.dep = data.singleData.dep;
                    $scope.model.comp = data.singleData.comp;
                    $scope.model.industry = data.singleData.industry;
                    $scope.model.lead_source = data.singleData.lead_source;
                    $scope.model.lead_status = data.singleData.lead_status;
                    $scope.model.no_empl = data.singleData.no_empl;
                    $scope.model.revenue = data.singleData.revenue;
                    $scope.model.des = data.singleData.des;
                    $scope.model.referred = data.singleData.referred;
                    $scope.model.address1 = data.singleData.address1;
                    $scope.model.address2 = data.singleData.address2;
                    $scope.model.email = data.singleData.email;
                    $scope.model.mobile = data.singleData.mobile;
                    $scope.model.phone = data.singleData.phone;
                    $scope.model.website = data.singleData.website;
                    $scope.model.other = data.singleData.other;
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.Update = function () {

            var model = $scope.model;

            var url = '/Api/Lead/Update';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        alert("Update Successfully")
                        window.location.href = "/Lead/Index";
                    }
                    else {
                        console.log(response);
                        alert("Update Failed")
                    }
                } else {
                    console.log(response);
                    alert("Something  wrong")
                }

            }, function (response) {
                console.log(response);
            });
        }




        $scope.DeleteById = function (id) {
            debugger;

            var url = '/Api/Lead/DeleteById/' + id;
            $http({
                method: 'POST',
                url: url
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        alert("Delete Successfully")
                        window.location.href = "/Lead/Index";

                    } else {
                        console.log(response);
                        alert("Delete Failed")
                    }
                } else {
                    console.log(response);
                    alert("Something  wrong")
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.ExportToExcel = function () {
            var url = '/Api/Lead/ExportExcel';
            window.open(url, '_blank');
        }
    });