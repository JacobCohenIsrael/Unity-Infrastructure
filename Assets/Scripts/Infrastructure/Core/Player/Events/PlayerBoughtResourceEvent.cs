using Newtonsoft.Json;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerBoughtResourceEvent
    {
        [JsonProperty("player")]
        public PlayerModel Player;
    }
}