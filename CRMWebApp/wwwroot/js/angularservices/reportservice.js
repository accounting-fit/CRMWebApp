/**//*angular.module('CRMApp', []).*//**/

var app = angular.module('CRMApp', []);
    app.controller('ReportController', function ($scope, $timeout, $http,Excel) {

        $scope.model = {
        };

        var reportNameList = [
            { id: "contact", text: "contact" }
            , { id: "task", text: "task" }
            , { id: "event", text: "event" }
            , { id: "lead", text: "lead" }
        ]

        $scope.reportNameList = reportNameList;
      
        ReportHtmlHide();

        $scope.GetReport = function () {
            var reportName = '';
            reportName = $scope.model.reportName;
            var url = '';

            ReportHtmlHide();

            if (reportName == 'contact') {
                url = '/Api/Contact/GetAll';
                $scope.IsVisibleContact = true;
            }
            else if (reportName == 'task') {
                url = '/Api/Task/GetAll';
                $scope.IsVisibleTask = true;
            }
            else if (reportName == 'event') {
                url = '/Api/Event/GetAll';
                $scope.IsVisibleEvent = true;
            }
            else if (reportName == 'lead') {
                url = '/Api/Lead/GetAll';
                $scope.IsVisibleLead = true;
            }
            else {
                url = '';
                ReportHtmlHide();
            }

            if (url != '') {
                GetAll(url)
            }          
        }


        function ReportHtmlHide() {
            $scope.IsVisibleContact = false;
            $scope.IsVisibleTask = false;
            $scope.IsVisibleEvent = false;
            $scope.IsVisibleLead = false;
        }

        function GetAll (url) {          
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



        $scope.ExportToPdf = function (report) {
            html2canvas(document.getElementById(report), {
                onrendered: function (canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 500,
                        }]
                    };
                    pdfMake.createPdf(docDefinition).download(report+".pdf");
                    
                }
            });
        }

        $scope.ExportToExcel = function (report) { // ex: '#my-table'
            debugger;
            var exportHref = Excel.tableToExcel(report, 'Sheet1');
            $timeout(function () { location.href = exportHref; }, 100); // trigger download
        }

    });

app.factory('Excel', function ($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {
        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };

})