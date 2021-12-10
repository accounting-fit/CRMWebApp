var array = [];
var allCustomer = [];
angular.module('CRMApp', []).
    controller('EventController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {                        
        };       

        $scope.AllClear = function () {
            $scope.model = {
                            
                          };
        }


        GetAllContacts('0');

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
                    $scope.contact_id_list = data.getAllContacts;
                    allCustomer = data.getAllContacts;
                }

            }, function (response) {
                console.log(response);
            });
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


        $scope.Save = function (isClose) {

            var model = $scope.model;
            model.contact_list = array;
            var url = '/Api/Event/Save';
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
                            window.location.href = "/Event/Index";
                        }
                        else {
                            window.location.href = "/Event/Create";
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
            var url = '/Api/Event/GetById/' + id;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.model.event_id = data.singleData.event_id;
                    $scope.model.topic = data.singleData.topic;                 
                    $scope.model.type = data.singleData.type;
                    $scope.model.status = data.singleData.status;
                    $scope.model.des = data.singleData.des;
                    $scope.model.start_date = data.singleData.start_date == null ? "": new Date(data.singleData.start_date);
                    $scope.model.start_time = data.singleData.start_time == null ? "" :  new Date(data.singleData.start_time);
                    $scope.model.end_date = data.singleData.end_date == null ? "" :  new Date(data.singleData.end_date);
                    $scope.model.end_time = data.singleData.end_time == null ? "" : new Date(data.singleData.end_time);
                    
                    var savedData = data.subData;
                    var allSelectedData = [];
                    for (var i = 0; i < allCustomer.length; i++) {
                        selectData = {};
                        selectData.id = allCustomer[i].id;
                        selectData.text = allCustomer[i].text;
                        if (userExists(savedData, allCustomer[i].id)) {
                            selectData.status = true;
                        }
                        else {
                            selectData.status = false;
                        }
                        allSelectedData.push(selectData);
                    }

                    $scope.contact_id_list = allSelectedData;
                }

            }, function (response) {
                console.log(response);
            });
        }

        function userExists(arr,name) {
            return arr.some(function (el) {
                return el.contact_id === name;
            });
        }

        $scope.Update = function () {

            var model = $scope.model;
            model.contact_list = array;
            var url = '/Api/Event/Update';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        alert("Update Successfully")
                        window.location.href = "/Event/Index";
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

            var url = '/Api/Event/DeleteById/' + id;
            $http({
                method: 'POST',
                url: url
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();
                        alert("Delete Successfully")
                        window.location.href = "/Event/Index";

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
            var url = '/Api/Event/ExportExcel';
            window.open(url, '_blank');
        }

        $scope.selectedList=[];
                
        $scope.btnSaveCustomer = function () {
            array = [];
            angular.forEach($scope.selectedList, function (selected, item) {
                if (selected) {                  
                    array.push(parseInt(item));                  
                }
            });           

        }
    });