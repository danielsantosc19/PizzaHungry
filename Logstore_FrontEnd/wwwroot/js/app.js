angular.module('app', ['ngMaterial', 'ngMessages', 'material.svgAssetsCache'])

    .controller('HungryPizzaController', ['$scope', '$locale', '$http', '$timeout', '$q','$mdDialog',
        function ($scope, $locale, $http, $timeout, $q, $mdDialog) {
            var alert;
            $scope.newOrder = false;
            $scope.currentOrder = undefined;
            $scope.flavorHide = {};
            $scope.showFlavor = false;            

            $http({
                method: 'GET',
                url: '/api/flavors'
            }).then(function successCallback(data) {
                $scope.flavors = data.data;

            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });

            
            $scope.loadAll = function () {
                var allStates = 'Alabama, Alaska, Arizona, Arkansas, California, Colorado, Connecticut, Delaware,\
              Florida, Georgia, Hawaii, Idaho, Illinois, Indiana, Iowa, Kansas, Kentucky, Louisiana,\
              Maine, Maryland, Massachusetts, Michigan, Minnesota, Mississippi, Missouri, Montana,\
              Nebraska, Nevada, New Hampshire, New Jersey, New Mexico, New York, North Carolina,\
              North Dakota, Ohio, Oklahoma, Oregon, Pennsylvania, Rhode Island, South Carolina,\
              South Dakota, Tennessee, Texas, Utah, Vermont, Virginia, Washington, West Virginia,\
              Wisconsin, Wyoming';

                return allStates.split(/, +/g).map(function (state) {
                    return {
                        value: state.toLowerCase(),
                        display: state
                    };
                });
            }

            $scope.searchClient = function (searchText) {
                return $http
                    .get('/api/clients?name=' + searchText)
                    .then(function (data) {
                        // Map the response object to the data object.
                        return data.data;
                    });
            };
            
            function createFilterFor(query) {
                var lowercaseQuery = query.toLowerCase();

                return function filterFn(state) {
                    return (state.value.indexOf(lowercaseQuery) === 0);
                };

            }
            
            $scope.searchText = null;

            $scope.addItem = function () {
                $scope.showFlavor = true;
                $scope.currentItem = { "Flavor": "", "Flavor2": "" };
            }

            $scope.addOrder = function (ev) {
                $scope.newOrder = true;                
                $scope.currentOrder = { "Items": [] };
            }

            $scope.cancelOrder = function (ev) {
                $scope.newOrder = false;
                $scope.showFlavor = false;
                $scope.currentOrder = undefined;
                $scope.flavorHide = {};
                $scope.currentItem = {};
            }

            $scope.addFlavor = function (flavor) {

                if ($scope.currentItem.Flavor == "") {
                    $scope.currentItem.Flavor = flavor;

                    var confirm = $mdDialog.confirm()
                        .title('Segundo sabor?')
                        .textContent('Você pode escolher outro sabor a pizza,deseja fazer isso?')
                        .ariaLabel('Lucky day')
                        .ok('Sim, por favor')
                        .cancel('Não, quero somente um sabor');

                    $mdDialog.show(confirm).then(function () {                        
                        $scope.flavorHide = flavor;
                    }, function () {
                            $scope.showFlavor = false;
                            $scope.flavorHide = {};
                            $scope.currentOrder.Items.push($scope.currentItem);

                    });
                }

                else {
                    $scope.currentItem.Flavor2 = flavor;
                    $scope.showFlavor = false;
                    $scope.flavorHide = {};
                    $scope.currentOrder.Items.push($scope.currentItem);
                }

            }

            $scope.checkout = function () {
                                
                var confirm = $mdDialog.confirm()
                    .title('Finalizar o pedido')
                    .textContent('Deseja finalizar o pedido?')
                    .ariaLabel('Lucky day')
                    .ok('Sim, estou com muita fome')
                    .cancel('Não, preciso alterar algumas coisas');

                $mdDialog.show(confirm).then(function () {
                    $scope.createOrder();
                }, function () {

                });

            }

            $scope.createOrder = function () {

                var data = {
                    clientId: $scope.selectedClient?.id ?? undefined,
                    name: $scope.searchText,
                    address: $scope.selectedClient?.address ?? '',
                    items: []
                }

                for (var i = 0; i < $scope.currentOrder.Items.length; i++) {
                    var item = $scope.currentOrder.Items[i];
                    data.items.push({ Flavor: item.Flavor.id, Flavor2: item.Flavor2.id  }  )
                }
                $http({
                    method: 'POST',
                    url: 'api/orders',
                    data:data,
                    headers: { 'Content-Type': 'application/json' }
                }).then(
                    function (response) {

                        $scope.newOrder = false;
                        $scope.showFlavor = false;
                        $scope.currentOrder = undefined;
                        $scope.flavorHide = {};
                        $scope.currentItem = {};

                        alert = $mdDialog.alert({
                            title: 'Sucesso',
                            textContent: 'Pedido a caminho!',
                            ok: 'Fechar'
                        });

                        $mdDialog
                            .show(alert)
                            .finally(function () {
                                alert = undefined;
                            });

                        console.log("Status Code= " + response.status + ", Status Text= " + response.statusText);
                    },
                    function (response) {                        
                        alert = $mdDialog.alert({
                            title: 'Ops',
                            textContent: 'Pedido a caminho!',
                            ok: 'Fechar'
                        });

                        $mdDialog
                            .show(alert)
                            .finally(function () {
                                alert = undefined;
                            });
                        console.log("Status Code= " + response.status + ", Status Text= " + response.statusText);
                    });
            }

            $scope.getOrderHistory = function (){

                var clientId = $scope.selectedClient.id;

                $http({
                    method: 'GET',
                    url: '/api/clients/' + clientId + '/orders'
                }).then(function successCallback(data) {
                    $scope.orderHistory = data.data;

                }, function errorCallback(response) {
                        alert('ops');
                });
            }


    }]);