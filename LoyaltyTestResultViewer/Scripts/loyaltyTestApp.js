angular.module("loyalty-report-app", ["ngRoute", "googlechart", "ngStorage"]).config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.
            when('/report/', {
                templateUrl: 'partials/lrChart.html',
                controller: 'lrChartController',
                controllerAs: 'vm'
            }).
            otherwise({
                redirectTo: '/report'
            });
    }]).value('googleChartApiConfig', {
        version: '1',
        optionalSettings: {
            packages: ['corechart', 'gauge']
        }
    });
