using Infrastructure.Core.Node;
using Newtonsoft.Json;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerJumpedToNodeEvent : Base.Event.Event
    {
        [JsonProperty("player")]
        public PlayerModel Player;
        
        [JsonProperty("node")]
        public NodeModel NodeSpace;
        
        public PlayerJumpedToNodeEvent(PlayerModel player, NodeModel nodeSpace)
        {
            Player = player;
            NodeSpace = nodeSpace;
        }
    }
}

