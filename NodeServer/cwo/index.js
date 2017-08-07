var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);
var idCounter = 2;
var players = {};
var playersDb = {
    "1ec6c968-63af-408b-9ce2-931631c0bbed" : {
        id : 1,
        currentNodeName : "TestStar",
        isLanded : true,
        homePlanetId : "TestStar",
        credits : 10,
        activeShipIndex : 0,
        sessionId : "1ec6c968-63af-408b-9ce2-931631c0bbed",
        ships : [
            {
                id : 1,
                currentHullAmount: 1,
                currentShieldAmount : 1,
                currentEnergyAmount : 1,
                shipCargo : {},
				shipParts : [
					{
						"name": "BasicEngine",
						"partStats" : {
							"hull": 50,
							"jumpDistance" : 10
						}
					},
					{
						"name": "BasicCargo",
						"partStats" : {
							"cargoCapacity": 50
						}
					},
					{
						"name": "BasicGenerator",
						"partStats" : {
							"energyRegen": 2,
							"energyCapacity": 10
						}
					}
				]
            }
        ]
    }
};

var starsDb = {
    "TestStar" : {
        "name" : "TestStar",
        "coordX" : -5.0,
        "coordY" : 0.0,
        "resourceList" : 
		{
			"Holmium" : { "name" : "Holmium", "amount" : 50, "buyPrice" : 5, "sellPrice" : 4 },
			"Cerium" : { "name" : "Cerium", "amount" : 25, "buyPrice" : 15, "sellPrice" : 8 }
		}
    },
    "TestStar2" : {
        "name" : "TestStar2",
        "coordX" : 5.0,
        "coordY" : 2.0,
        "resourceList" : {"Holmium" : { "name" : "Holmium", "amount" : 50, "buyPrice" : 5, "sellPrice" : 4 }}
    },
    "TestStar3" : {
        "name" : "TestStar3",
        "coordX" : 0.0,
        "coordY" : 1.0,
        "resourceList" : {"Holmium" : { "name" : "Holmium", "amount" : 40, "buyPrice" : 15, "sellPrice" : 10 }}
    }
}

//setInterval(logStuff, 5000);
server.listen(3000);
console.log('CWO Server is listening on port 3000');
io.on('connection', function(socket) {
    console.log('Connectin Established');
    socket.emit('connectionResponse', {'success' : true });
    socket.on('login', function(data) {
        console.log("Player with session " + data.player.sessionId + " is trying to login", data.player);
        if (!playersDb.hasOwnProperty(data.player.sessionId))
        {
            playersDb[data.player.sessionId] = {
                id : idCounter,
				currentNodeName : "TestStar",
				isLanded : true,
				homePlanetId : "TestStar",
				credits : 10,
				activeShipIndex : 0,
				sessionId : "1ec6c968-63af-408b-9ce2-931631c0bbed",
				ships : [
					{
						id : 1,
						currentHullAmount: 1,
						currentShieldAmount : 1,
						currentEnergyAmount : 1,
						shipCargo : {},
						shipParts : [
							{
								"name": "BasicEngine",
								"partStats" : {
									"hull": 50,
									"jumpDistance" : 10
								}
							},
							{
								"name": "BasicCargo",
								"partStats" : {
									"cargoCapacity": 50
								}
							},
							{
								"name": "BasicGenerator",
								"partStats" : {
									"energyRegen": 2,
									"energyCapacity": 10
								}
							}
						]
					}
				]
            };
            idCounter++;
        }
        var player = playersDb[data.player.sessionId];
		player.ships[0].cachedShipStats = {
			"hull" : 50,
			"cargoCapacity": 50,
			"jumpDistance": 10,
			"energyRegen": 2,
			"energyCapacity": 10
		};
        players[player.id] = player;
        socket.emit('loginResponse', {'success' : true, player : player, starsList : starsDb });
    });

    socket.on('landPlayerOnStar', function(data) {
        console.log("Landing player " + data.id + " On Star");
        players[data.id].isLanded = true;
        socket.emit('playerLanded', {'success' : true, player : players[data.id] });
    });

    socket.on('departPlayerFromStar', function(data) {
        console.log("Departing player " + data.player.id + " From Star");
        players[data.player.id].isLanded = false;
        socket.emit('playerDeparted', {'success' : true, player : players[data.player.id] });
    });

	socket.on('playerEnteredLounge', function(data) {
        console.log("Player " + data.player.id + " Entered Lounge");
		socket.join('lounge' + data.player.currentNodeName);
        socket.emit('playerEnteredLounge', {'success' : true, player : players[data.id] });
    });

	socket.on('playerLeftLounge', function(data) {
        console.log("Player " + data.player.id + " Left Lounge");
		socket.leave('lounge' + data.player.currentNodeName);
        socket.emit('playerLeftLounge', {'success' : true, player : players[data.id] });
    });

	socket.on('loungeChatSent', (data) => {
        console.log("Player " + data.player.id + " Sent Chat", data);
    });

    socket.on('playerBuyResource', function(data) {
        console.log("Player is buying resource ", data);
        if (!starsDb[data.player.currentNodeName].resourceList.hasOwnProperty([data.resource.name])) {
            console.log("This star does not contain this resource");
            return;
        }
        if (starsDb[data.player.currentNodeName].resourceList[data.resource.name].amount < data.resource.amount) {
            console.log("This star does not have this much resources to sell");
            return;
        }
        if (players[data.player.id].credits < starsDb[data.player.currentNodeName].resourceList[data.resource.name].buyPrice) {
            console.log("Player does not have enough credits to purchase this resource");
            return;
        }

        starsDb[data.player.currentNodeName].resourceList[data.resource.name].amount -= data.resource.amount;
        players[data.player.id].credits -= starsDb[data.player.currentNodeName].resourceList[data.resource.name].buyPrice;
        if (!players[data.player.id].ships[players[data.player.id].activeShipIndex].shipCargo.hasOwnProperty(data.resource.name)) {
            players[data.player.id].ships[players[data.player.id].activeShipIndex].shipCargo[data.resource.name] = 0;
        }
        players[data.player.id].ships[players[data.player.id].activeShipIndex].shipCargo[data.resource.name] += data.resource.amount;
        socket.emit('playerBoughtResource', {'success' : true, player : players[data.player.id]});
        io.sockets.emit('updateResourceAmount', {
            'success' : true,
            starName: data.player.currentNodeName,
            resourceName: data.resource.name,
            newAmount : starsDb[data.player.currentNodeName].resourceList[data.resource.name].amount });
    });

    socket.on('playerSellResource', function(data) {
        console.log("Player is selling resource ", data);
        if (!players[data.player.id].ships[players[data.player.id].activeShipIndex].shipCargo.hasOwnProperty([data.resource.name])) {
            console.log("Player does not have this resource");
            return;
        }
        if (!players[data.player.id].ships[players[data.player.id].activeShipIndex].shipCargo[data.resource.name] >= data.resource.amount) {
            console.log("Player does not have that amount of resource to sell");
            console.log("Player Cargo: ", players[data.player.id].ships[players[data.player.id].activeShipIndex].shipCargo);
            return;
        }

        starsDb[data.player.currentNodeName].resourceList[data.resource.name].amount += data.resource.amount;
        players[data.player.id].credits += starsDb[data.player.currentNodeName].resourceList[data.resource.name].sellPrice;
        players[data.player.id].ships[players[data.player.id].activeShipIndex].shipCargo[data.resource.name] -= data.resource.amount;
        socket.emit('playerSoldResource', {'success' : true, player : players[data.player.id]});
        io.sockets.emit('updateResourceAmount', {
            'success' : true,
            starName: data.player.currentNodeName,
            resourceName: data.resource.name,
            newAmount : starsDb[data.player.currentNodeName].resourceList[data.resource.name].amount
        });
    });

    socket.on('jumpPlayerToStar', function(data) {
        console.log(data);
        console.log("Jumping player " + data.player.id + " From Star " + data.player.currentNodeName + " To Star " + data.star.name);
        if (players[data.player.id].currentNodeName != data.star.name )
        {
            players[data.player.id].currentNodeName = data.star.name;
        }
        else
        {
            console.log("WTF player is trying to jump to the star he's at?");
        }
        socket.emit('playerJumped', {'success' : true, player : players[data.player.id] });
    });

    socket.on('test', function(data) {
        console.log("Test: ", data);
    });

});

function logStuff()
{
    console.log("**************************** Players ****************************");
    for (var playerId in players)
    {
        console.log("**************************** Player " + playerId + " ****************************");
        console.log(players[playerId]);
    }

    console.log("**************************** Stars ****************************");
    for (var starName in starsDb)
    {
        console.log("**************************** Star " + starName + " ****************************");
        console.log(starsDb[starName]);
    }
}
