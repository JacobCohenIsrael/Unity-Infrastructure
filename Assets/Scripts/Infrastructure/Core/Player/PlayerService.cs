using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using Infrastructure.Base.Event;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Star;
using Infrastructure.Core.Resource;
using Infrastructure.Core.Network;
using UnitySocketIO;
using UnityEngine;
using Newtonsoft.Json;
using UnitySocketIO.Events;

namespace Infrastructure.Core.Player
{
    public class PlayerService : IServiceProvider
    {
        StarService starService;
        PlayerAdapter playerAdapter;
        EventManager eventManager;
        MainServer mainServer;

        public PlayerService(ServiceManager serviceManager)
        {
            getDependencies(serviceManager);
            subscribeListeners();
        }

        protected void getDependencies(ServiceManager serviceManager)
        {
            starService = serviceManager.get<StarService>() as StarService;
            playerAdapter = serviceManager.get<PlayerAdapter>() as PlayerAdapter;
            eventManager = serviceManager.get<EventManager>() as EventManager;
            mainServer = serviceManager.get<MainServer>() as MainServer;
        }

        protected void subscribeListeners()
        {
            mainServer.On("playerBoughtResource", this.OnPlayerBoughtResource);
            mainServer.On("playerSoldResource", this.OnPlayerSoldResource);
        }
            
        public void LoginAsGuest(string sessionId)
        {
            playerAdapter.LoginAsGuest(sessionId);
        }

        public void jumpPlayerToStar(PlayerModel player, StarModel star)
        {
            float distance = starService.CalculateDistanceBetweenStars(starService.GetStarByName(player.currentNodeName), star);
            float engineJumpDistance = player.getActiveShip().shipStats[Infrastructure.Core.Ship.ShipStats.JumpDistance];
            if (distance > engineJumpDistance)
            {
                throw new UnityEngine.UnityException("Player Engines are not strong enough to jump there directly");

            }

            if (player.getActiveShip().currentEnergyAmount < 10)
            {
                throw new UnityEngine.UnityException("Insufficient Energy for a jump");
            }

            if (playerAdapter.jumpPlayerToStar(player, star))
            {
                player.currentNodeName = star.name;
                PlayerJumpedToStarEvent playerJumpedToStarEvent = new PlayerJumpedToStarEvent(player, star);
                eventManager.DispatchEvent<PlayerJumpedToStarEvent>(playerJumpedToStarEvent);
                OrbitPlayerOnStar(player, star);
            }

        }

        public void landPlayerOnStar(PlayerModel player)
        {
            if (playerAdapter.landPlayerOnStar(player))
            {
                PlayerLandOnStarEvent playerLandedOnStar = new PlayerLandOnStarEvent(player);
                eventManager.DispatchEvent<PlayerLandOnStarEvent>(playerLandedOnStar);
            }
        }

        public void departPlayerFromStar(PlayerModel player)
        {
            if (playerAdapter.departPlayerFromStar(player))
            {
                PlayerDepartFromStarEvent playerDepartFromStarEvent = new PlayerDepartFromStarEvent(player);
                eventManager.DispatchEvent<PlayerDepartFromStarEvent>(playerDepartFromStarEvent);
            }
        }

        public void openMarket(PlayerModel player)
        {
            StarModel star = starService.GetStarByName(player.currentNodeName);
            PlayerOpenedMarketEvent playerOpenedMarketEvent = new PlayerOpenedMarketEvent(player, star);
            eventManager.DispatchEvent<PlayerOpenedMarketEvent>(playerOpenedMarketEvent);
        }

        public void exitMarket(PlayerModel player)
        {
            PlayerExitMarketEvent playerExitMarketEvent = new PlayerExitMarketEvent(player);
            eventManager.DispatchEvent<PlayerExitMarketEvent>(playerExitMarketEvent);
        }

        public void OrbitPlayerOnStar(PlayerModel player, StarModel star)
        {
            PlayerOrbitStarEvent playerOrbitStarEvent = new PlayerOrbitStarEvent(star);
            eventManager.DispatchEvent<PlayerOrbitStarEvent>(playerOrbitStarEvent);
        }

        public void BuyResource(PlayerModel player, string resourceName)
        {
            StarModel star = starService.GetStarByName(player.currentNodeName);
            ResourceSlotModel resourceSlot = star.resourceList[resourceName];
            if (player.credits >= resourceSlot.buyPrice)
            {
                playerAdapter.BuyResource(player, resourceSlot);
            }
        }

        public void SellResource(PlayerModel player, string resourceName)
        {
            StarModel star = starService.GetStarByName(player.currentNodeName);
            ResourceSlotModel resourceSlot = star.resourceList[resourceName];
            playerAdapter.SellResource(player, resourceSlot);
        }

        protected void OnPlayerBoughtResource(SocketIOEvent e)
        {
            PlayerBoughtResourceEvent pbre = JsonConvert.DeserializeObject<PlayerBoughtResourceEvent>(e.data);
            eventManager.DispatchEvent<PlayerBoughtResourceEvent>(pbre);
        }

        protected void OnPlayerSoldResource(SocketIOEvent e)
        {
            PlayerSoldResourceEvent psre = JsonConvert.DeserializeObject<PlayerSoldResourceEvent>(e.data);
            eventManager.DispatchEvent<PlayerSoldResourceEvent>(psre);
        }
    }
}