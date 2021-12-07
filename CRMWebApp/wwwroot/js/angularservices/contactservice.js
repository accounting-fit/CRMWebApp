angular.module('CRMApp', []).
    controller('ContactController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {                        
        };

        var typeList = [
             { id: false, text: "Customer" }
            , { id: true, text: "Employee" }
        ]
        $scope.typeList = typeList;       

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

        $scope.Save = function (isClose) {

            var model = $scope.model;
            model.type = model.type == 'true'? true:false;

            var url = '/Api/Contact/Save';
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
                            window.location.href = "/Contact/Index";
                        }
                        else {
                            window.location.href = "/Contact/Create";
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
            var url = '/Api/Contact/GetById/' + id;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.model.contact_id = data.singleData.contact_id
                    $scope.model.fname = data.singleData.fname
                    $scope.model.lname = data.singleData.lname
                    $scope.model.mname = data.singleData.mname
                    $scope.model.email = data.singleData.email
                    $scope.model.mobile = data.singleData.mobile
                    $scope.model.phone = data.singleData.phone
                    $scope.model.website = data.singleData.website
                    $scope.model.address1 = data.singleData.address1
                    $scope.model.address2 = data.singleData.address2
                    $scope.model.type = data.singleData.type;
                    $scope.model.des = data.singleData.des
                    $scope.model.other = data.singleData.other                   
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.Update = function () {

            var model = $scope.model;
            model.type = model.type == 'true' ? true : false;

            var url = '/Api/Contact/Update';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        alert("Update Successfully")
                        window.location.href = "/Contact/Index";
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

            var url = '/Api/Contact/DeleteById/' + id;
            $http({
                method: 'POST',
                url: url
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        alert("Delete Successfully")
                        window.location.href = "/Contact/Index";

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
            var url = '/Api/Contact/ExportExcel';
            window.open(url, '_blank');
        }
    });