using Newtonsoft.Json;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerSoldResourceEvent
    {
        [JsonProperty("player")]
        public PlayerModel Player;
    }
}

