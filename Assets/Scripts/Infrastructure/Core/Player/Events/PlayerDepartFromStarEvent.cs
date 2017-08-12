using Newtonsoft.Json;
using System;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerDepartFromStarEvent
    {
        [JsonProperty("player")]
        public Player.PlayerModel Player;

        [JsonProperty("node")]
        public NodeSpaceModel NodeSpace;

        public PlayerDepartFromStarEvent(Player.PlayerModel player)
        {
            this.Player = player;
        }
    }
}

