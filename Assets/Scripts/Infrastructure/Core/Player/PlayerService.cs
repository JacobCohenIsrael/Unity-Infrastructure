using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using Infrastructure.Base.Event;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Star;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Player
{
    public class PlayerService : IServiceProvider
    {
        StarService starService;
        PlayerAdapter playerAdapter;
        EventManager eventManager;

        public PlayerService(ServiceManager serviceManager)
        {
            starService = serviceManager.get<StarService>() as StarService;
            playerAdapter = serviceManager.get<PlayerAdapter>() as PlayerAdapter;
            eventManager = serviceManager.get<EventManager>() as EventManager;
        }

        public PlayerModel getPlayerById(int playerId)
        {
            return playerAdapter.getById(playerId);
        }

        public void jumpPlayerToStar(PlayerModel player, StarModel star)
        {
            float distance = starService.CalculateDistanceBetweenStars(starService.GetStarById(player.currentNodeId), star);
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
            if (playerAdapter.landPlayerOnStar(player))
            {
                PlayerDepartFromStarEvent playerDepartFromStarEvent = new PlayerDepartFromStarEvent(player);
                eventManager.DispatchEvent<PlayerDepartFromStarEvent>(playerDepartFromStarEvent);
            }
        }

        public void openMarket(PlayerModel player)
        {
            StarModel star = starService.GetStarById(player.currentNodeId);
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

        public void BuyResource(PlayerModel player, ResourceModel resource)
        {
            StarModel star = starService.GetStarById(player.currentNodeId);
            ResourceSlotModel resourceSlot = star.resourceList[resource.name];
            if (player.credits >= resourceSlot.buyPrice)
            {
                if (playerAdapter.BuyResource(player, resourceSlot))
                {
                    PlayerBoughtResourceEvent playerBoughtResourceEvent = new PlayerBoughtResourceEvent(player, resourceSlot);
                    eventManager.DispatchEvent<PlayerBoughtResourceEvent>(playerBoughtResourceEvent);
                }
            }
        }

        public void SellResource(PlayerModel player, ResourceModel resource)
        {
            StarModel star = starService.GetStarById(player.currentNodeId);
            ResourceSlotModel resourceSlot = star.resourceList[resource.name];
            if (playerAdapter.SellResource(player, resourceSlot))
            {
                PlayerSoldResourceEvent playerSoldResourceEvent = new PlayerSoldResourceEvent(player, resourceSlot);
                eventManager.DispatchEvent<PlayerSoldResourceEvent>(playerSoldResourceEvent);
            }
        }
    }
}