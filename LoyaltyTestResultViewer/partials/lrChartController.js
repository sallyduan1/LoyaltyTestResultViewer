/* global angular */
angular.module("loyalty-report-app").controller("lrChartController", function () {
    var vm = this;
    vm.showTable = false;

    vm.chartObject = {};
    vm.tableTitle = '';
    vm.chartObject.data = {
        "cols": [
            { id: "week", label: "week", type: "string" },
            { id: "error", label: "error", type: "number" },
            { id: "success", label: "success", type: "number" },
            { id: "timeout", label: "timeout", type: "number" },
            { id: "inConclusive", label: "inConclusive", type: "number" },

        ],
        "rows": [
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
                        v: 26 ,
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
    vm.chartObject.type = 'ColumnChart';
    vm.chartObject.options = {
        'title': 'How Much Pizza I Ate Last Night',
        series: {
            0: { color: 'red' },
            1: { color: 'green' },
            2: { color: 'brown' },
            3: { color: 'blue' },
        }
    };
    vm.selectHandler = function (selectedItem) {
        vm.showTable = true;
        var detail = vm.chartObject.data.rows[selectedItem.row].c[selectedItem.column].detail;
        vm.tableObject.data.rows = detail;
        vm.tableTitle = vm.chartObject.data.rows[selectedItem.row].c[0].v + ' ' + vm.chartObject.data.cols[selectedItem.column].label;
    }


    vm.tableObject = {};
    vm.tableObject.data = {
        "cols": [
            { id: "testName", label: "testName", type: "string" },
            { id: "time", label: "time", type: "string" },
            { id: "detail", label: "detail", type: "string" }
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
});

