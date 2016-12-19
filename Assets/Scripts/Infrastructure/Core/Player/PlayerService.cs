using UnityEngine;
using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;

namespace Infrastructure.Core.Player
{
    public class PlayerService : IServiceProvider
    {
        PlayerAdapter playerAdapter;

        public PlayerService(ServiceManager serviceManager)
        {
            playerAdapter = serviceManager.get<PlayerAdapter>() as PlayerAdapter;
            Debug.Log("I am player service!");
        }

        public int getPlayerById(int playerId)
        {
            return playerAdapter.getById(playerId);
        }
    }
}