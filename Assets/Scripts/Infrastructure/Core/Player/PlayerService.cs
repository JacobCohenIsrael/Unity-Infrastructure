using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using Infrastructure.Base.Event;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Star;

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
            float distance = starService.calculateDistanceBetweenStars(starService.getStarById(player.currentNodeId), star);
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
    }
}