/* global angular */
angular.module("loyalty-report-app").controller("lrChartController", function () {
    var vm = this;

    vm.chartObject = {};
    vm.chartObject.data = {
        "cols": [
            { id: "week", label: "week", type: "string" },
            { id: "error", label: "error", type: "number" },
            { id: "success", label: "success", type: "number" },
            { id: "timeout", label: "timeout", type: "number" },
            { id: "inConclusive", label: "inConclusive", type: "number" }
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
                   { v: 20 },
                   { v: 22 },
                   { v: 25 },
                   { v: 26 },

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
        console.log(vm.chartObject.type);

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
                    { v: "test1 detail is here"}
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
});

