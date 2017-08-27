using Newtonsoft.Json;
using Infrastructure.Core.Node;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerDepartFromStarEvent
    {
        [JsonProperty("player")]
        public PlayerModel Player;

        [JsonProperty("node")]
        public NodeModel NodeSpace;
    }
}

