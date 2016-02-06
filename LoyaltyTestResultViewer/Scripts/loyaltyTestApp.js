angular.module("loyalty-report-app", ["ngRoute", "googlechart"]).config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.
            when('/report/', {
                templateUrl: 'partials/lrChart.html',
                controller: 'lrChartController'
            }).
            otherwise({
                redirectTo: '/generic'
            });
    }]).value('googleChartApiConfig', {
        version: '1',
        optionalSettings: {
            packages: ['corechart', 'gauge']
        }
    });
