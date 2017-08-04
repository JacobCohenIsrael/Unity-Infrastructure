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
using System;

namespace Infrastructure.Core.Player
{
    public class PlayerService : Base.Service.Contracts.IServiceProvider
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

        public void LeaveLounge(PlayerModel player)
        {
            if (playerAdapter.LeaveLounge(player))
            {

            }
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
            mainServer.On("playerDeparted", this.OnPlayerDepart);
            mainServer.On("playerLanded", this.OnPlayerLanded);
            mainServer.On("playerBoughtResource", this.OnPlayerBoughtResource);
            mainServer.On("playerSoldResource", this.OnPlayerSoldResource);
            mainServer.On("playerEnteredLounge", this.OnPlayerEnteredLounge);
            mainServer.On("playerLeftLounge", this.OnPlayerLeftLounge);
        }

        public void LoginAsGuest(string sessionId)
        {
            playerAdapter.LoginAsGuest(sessionId);
        }

        public void jumpPlayerToStar(PlayerModel player, StarModel star)
        {
            float distance = starService.CalculateDistanceBetweenStars(starService.GetStarByName(player.currentNodeName), star);
            float engineJumpDistance = player.getActiveShip().getShipJumpDistance();
            if (distance > engineJumpDistance)
            {
                throw new UnityEngine.UnityException("Player Engines are not strong enough to jump there directly");
            }

            if (player.getActiveShip().currentEnergyAmount < 10)
            {
                throw new UnityEngine.UnityException("Insufficient Energy for a jump");
            }

            if (playerAdapter.JumpPlayerToStar(player, star))
            {
                player.currentNodeName = star.name;
                PlayerJumpedToStarEvent playerJumpedToStarEvent = new PlayerJumpedToStarEvent(player, star);
                eventManager.DispatchEvent<PlayerJumpedToStarEvent>(playerJumpedToStarEvent);
                OrbitPlayerOnStar(player, star);
            }

        }

        public void EnterLounge(PlayerModel player)
        {
            if (playerAdapter.EnterLounge(player))
            {
            }
        }

        public void LandPlayerOnStar(PlayerModel player)
        {
            if (playerAdapter.LandPlayerOnStar(player))
            {
            }
        }

        public void DepartPlayerFromStar(PlayerModel player)
        {
            if (playerAdapter.DepartPlayerFromStar(player))
            {
            }
        }

        public void OpenMarket(PlayerModel player)
        {
            StarModel star = starService.GetStarByName(player.currentNodeName);
            PlayerOpenedMarketEvent playerOpenedMarketEvent = new PlayerOpenedMarketEvent(player, star);
            eventManager.DispatchEvent<PlayerOpenedMarketEvent>(playerOpenedMarketEvent);
        }

        public void ExitMarket(PlayerModel player)
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


        protected void OnPlayerEnteredLounge(SocketIOEvent e)
        {
            PlayerEnteredLoungeEvent pele = JsonConvert.DeserializeObject<PlayerEnteredLoungeEvent>(e.data);
            eventManager.DispatchEvent<PlayerEnteredLoungeEvent>(pele);
        }

        protected void OnPlayerLeftLounge(SocketIOEvent e)
        {
            PlayerLeftLoungeEvent plle = JsonConvert.DeserializeObject<PlayerLeftLoungeEvent>(e.data);
            eventManager.DispatchEvent<PlayerLeftLoungeEvent>(plle);
        }

        protected void OnPlayerLanded(SocketIOEvent e)
        {
            PlayerLandOnStarEvent plose = JsonConvert.DeserializeObject<PlayerLandOnStarEvent>(e.data);
            eventManager.DispatchEvent<PlayerLandOnStarEvent>(plose);
        }

        protected void OnPlayerDepart(SocketIOEvent e)
        {
            PlayerDepartFromStarEvent pde = JsonConvert.DeserializeObject<PlayerDepartFromStarEvent>(e.data);
            eventManager.DispatchEvent<PlayerDepartFromStarEvent>(pde);
        }
    }
}