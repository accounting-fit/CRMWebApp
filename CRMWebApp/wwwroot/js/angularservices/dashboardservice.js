angular.module('CRMApp', []).
    controller('DashboardController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {
        };


        $scope.AllClear = function () {
            $scope.model = {

            };
        }

        $scope.allDataList = [];

        $scope.GetAll = function () {
            var url = '/api/Dashboard/GetAll';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.AllDataList = data.allDataList;
                    var xValues = [data.allDataList[0].name, data.allDataList[1].name, data.allDataList[2].name, data.allDataList[3].name];
                    var yValues = [data.allDataList[0].count, data.allDataList[1].count, data.allDataList[2].count, data.allDataList[3].count];
                    var barColors = [
                        "#007bff",
                        "#ffc107",
                        "#28a745",
                        "#dc3545"
                    ];

                    new Chart("myChart", {
                        type: "pie",
                        data: {
                            labels: xValues,
                            datasets: [{
                                backgroundColor: barColors,
                                data: yValues
                            }]
                        },
                        options: {
                            title: {
                                display: true,
                                text: "Status"
                            }
                        }
                    });

                    new Chart("myBarChart", {
                        type: "bar",
                        data: {
                            labels: xValues,
                            datasets: [{
                                backgroundColor: barColors,
                                data: yValues
                            }]
                        },
                        options: {
                            legend: { display: false },
                            title: {
                                display: true,
                                text: "Status"
                            }
                        }
                    });
                   
                }

            }, function (response) {
                console.log(response);
            });
        }

        


    });