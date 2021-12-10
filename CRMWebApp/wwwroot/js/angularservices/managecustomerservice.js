angular.module('CRMApp', []).
    controller('ManageCustomerController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {                        
        };

           

        $scope.AllClear = function () {
            $scope.model = {
                            
                          };
        }

        $scope.allDataList = [];
        
        $scope.GetAll = function () {
            var url = '/api/ManageCustomer/getall';
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
            var url = '/Api/ManageCustomer/GetById/' + id;
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
                    $scope.model.type = (data.singleData.type);
                    $scope.model.des = data.singleData.des
                    $scope.model.other = data.singleData.other
                    $scope.subDataList = data.subDataList;
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



        /////////


        

        function GetAllContacts(type, idValue) {
            debugger;
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
                    $scope.model.contact_id = idValue.toString();
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

        $scope.OnInitEvent = function (id) {
            GetAllContacts('0',id);
           
        }

        $scope.SaveEvent = function () {
            var model = $scope.model;
            model.contact_list = [];
            model.contact_list.push(model.contact_id);
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
                            window.location.href = "/ManageCustomer/Edit/"+model.contact_id;                       
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