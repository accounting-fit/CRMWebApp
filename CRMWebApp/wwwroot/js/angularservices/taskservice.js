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
     
        GetAllContacts('1');

        function GetAllContacts(type) {
            var url = '/Api/Contact/GetAllContacts/' + type;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    debugger;
                    $scope.assigned_to_list = data.getAllContacts;
                }

            }, function (response) {
                console.log(response);
            });
        }

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


        $scope.Save = function (isClose) {

            var model = $scope.model;

            var url = '/api/task/save';
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
                            window.location.href = "/Task/Index";
                        }
                        else {
                            window.location.href = "/Task/Create";
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
            var url = '/Api/Task/GetById/' + id;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;   
                    $scope.model.task_id = data.singleData.task_id;
                    $scope.model.task_name = data.singleData.task_name;
                    $scope.model.status = data.singleData.status;
                    $scope.model.refer_type = data.singleData.refer_type;
                    $scope.model.assigned_to = (data.singleData.assigned_to).toString();
                    $scope.model.priority = data.singleData.priority;
                    $scope.model.des = data.singleData.des;
                }          
                
            }, function (response) {
                console.log(response);
            });
        }

        $scope.Update = function () {

            var model = $scope.model;

            var url = '/Api/Task/Update';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        alert("Update Successfully")                       
                            window.location.href = "/Task/Index";                    
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

            var url = '/Api/Task/DeleteById/' + id;
            $http({
                method: 'POST',
                url: url
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        alert("Delete Successfully")                       
                            window.location.href = "/Task/Index";                  
                       
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
            var url = '/Api/Task/ExportExcel';
            window.open(url, '_blank');
        }
    });