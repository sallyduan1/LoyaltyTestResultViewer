/* global angular */
angular.module("loyalty-report-app").controller("lrChartController", function ($scope, $http, $localStorage) {
    var vm = this;
    vm.showTable = false;
    vm.showChart = true;

    vm.chartObject = {};
    vm.lastNDays = 7;
    vm.$storage = $localStorage.$default({ lastNDays: 7 });

    vm.tableTitle = '';
    vm.chartObject.data = {
        cols: [
            { id: "Date", label: "Date", type: "string" },
            { id: "Passed", label: "Passed", type: "number" },
            { id: "Failed", label: "Failed", type: "number" },
            { id: "Inconclusive", label: "Inconclusive", type: "number" },
            { id: "NotExecuted", label: "NotExecuted", type: "number" },
            { id: "Pending", label: "Pending", type: "number" },
            { id: "Aborted", label: "Aborted", type: "number" },

        ],
        rows: [
            {
                c: [
                    { v: "week1" },
                    { v: 3 },
                    { v: 4 },
                    { v: 5 },
                    { v: 6 },
                ]
            },
            {
                c: [
                    { v: "week2" },
                    { v: 31 },
                    { v: 41 },
                    { v: 15 },
                    { v: 16 },
                ]
            },
            {
                c: [
                    { v: "week3" },
                    { v: 1 },
                    { v: 4 },
                    { v: 5 },
                    { v: 6 },
                ]
            },
            {
                c: [
                    { v: "week4" },
                    {
                        v: 20,
                        detail: [
                            {
                                c: [
                                    { v: "test1" },
                                    { v: "Jan 11, 2016" },
                                    { v: "test1 detail is here" }
                                ]
                            },
                            {
                                c: [
                                    { v: "test2" },
                                    { v: "Feb 12, 2015" },
                                    { v: "test2 detail is here" }
                                ]
                            }
                        ]
                    },
                    {
                        v: 22,
                        detail: [
                            {
                                c: [
                                    { v: "test3" },
                                    { v: "Jan 3, 2016" },
                                    { v: "test3 detail is here" }
                                ]
                            },
                            {
                                c: [
                                    { v: "test4" },
                                    { v: "Feb 4, 2015" },
                                    { v: "test4 detail is here" }
                                ]
                            }
                        ]

                    },
                    {
                        v: 25,
                        detail: [
                            {
                                c: [
                                    { v: "test5" },
                                    { v: "Jan 5, 2016" },
                                    { v: "test5 detail is here" }
                                ]
                            },
                            {
                                c: [
                                    { v: "test6" },
                                    { v: "Feb 6, 2015" },
                                    { v: "test6 detail is here" }
                                ]
                            }
                        ]
                    },
                    {
                        v: 26,
                        detail: [
                            {
                                c: [
                                    { v: "test7" },
                                    { v: "Jan 7, 2016" },
                                    { v: "test7 detail is here" }
                                ]
                            },
                            {
                                c: [
                                    { v: "test8" },
                                    { v: "Feb 8, 2015" },
                                    { v: "test8 detail is here" }
                                ]
                            }
                        ]

                    },
                ]
            }
        ]
    };
    vm.chartObject.view = {
        columns: [0, 1, 2, 3, 4, 5, 6]
    };
    vm.chartObject.type = 'ColumnChart';
    vm.chartObject.options = {
        'title': 'ECommerce Loyalty Test Report',
        backgroundColor: { stroke: "#000", strokeWidth: 4, fill: "#ddd" },
        titleTextStyle: { fontSize: 19 },
        tooltip: { showColorCode: true },
        series: {
            0: { color: 'green' },
            1: { color: 'red' },
            2: { color: 'brown' },
            3: { color: 'blue' },
            4: { color: 'magenta' },
            5: { color: 'purple' }
        }
    };
    vm.selectHandler = function (selectedItem) {
        if (!selectedItem) return;
        else if (selectedItem.row == null) {
            var col = selectedItem.column;
            if (vm.chartObject.view.columns[col] === col) {
                vm.chartObject.view.columns[col] = {
                    label: vm.chartObject.data.cols[col].label,
                    type: vm.chartObject.data.cols[col].type,
                    calc: function() {
                        return null;
                    }
                };
                vm.chartObject.options.series[col - 1].color = '#CCCCCC';
            } else {
                vm.chartObject.view.columns[col] = col;
                vm.chartObject.options.series[col - 1].color = vm.chartObject.options.seriesDefault[col - 1].color;
            }
        } else {
            var filePath = encodeURI(vm.chartObject.data.rows[selectedItem.row].filePath);
            $http({
                method: 'GET',
                url: '/api/report/' + filePath + '/' + selectedItem.column
            }).then(function success(response) {
                vm.showTable = true;
                console.log(response.data);
                var rows = [];

                _.forEach(response.data, function (testCase) {
                    var row = {
                        c: [
                            { v: testCase.TestName },
                            { v: testCase.ComputerName },
                            { v: testCase.Message }
                        ],
                    };
                    rows.push(row);
                });

                vm.tableTitle = vm.chartObject.data.rows[selectedItem.row].c[0].v + ' ' + vm.chartObject.data.cols[selectedItem.column].label;
                vm.tableObject.data.rows = rows;
            }, function error(response) {
                console.log(response);
                vm.showTable = false;

            });
        }
    }

    var activate = function () {
        var lastNDays = vm.$storage.lastNDays;
        vm.showTable = true;
        $http({
            method: 'GET',
            url: '/api/report/' + lastNDays

        }).then(function success(response) {
            console.log(response.data);
            vm.showChart = true;
            var rows = [];

            _.forEach(response.data, function (testResult) {
                var row = {
                    c: [
                        { v: testResult.Date + "(" + testResult.Total + ")" },
                        { v: testResult.Passed },
                        { v: testResult.Failed },
                        { v: testResult.Inconclusive },
                        { v: testResult.NotExecuted },
                        { v: testResult.Pending },
                        { v: testResult.Aborted }
                    ],
                    filePath: testResult.FilePath
                };
                rows.push(row);
            });

            vm.chartObject.data.rows = rows;
        }, function error(response) {
            vm.showTable = false;
            vm.showChart = false;
            console.log(response);

        });
    }
    $scope.$watch("vm.$storage.lastNDays", function handleDaysChange() {
        console.log(vm.$storage.lastNDays);
        activate();
    });

    vm.tableObject = {};
    vm.tableObject.data = {
        "cols": [
            { id: "testName", label: "test name", type: "string" },
            { id: "computerName", label: "computer", type: "string" },
            { id: "message", label: "message", type: "string" }
        ],
        "rows": [
            {
                c: [
                    { v: "test1" },
                    { v: "Jan 11, 2016" },
                    { v: "test1 detail is here" }
                ]
            },
            {
                c: [
                    { v: "test2" },
                    { v: "Feb 12, 2015" },
                    { v: "test2 detail is here" }
                ]
            }
        ]
    };
    vm.tableObject.type = 'Table';
    vm.tableObject.options = {
        'title': 'this is my table',
        showRowNumber: true,
        width: '100%',
        height: '100%'
       
    };
    activate();
});

