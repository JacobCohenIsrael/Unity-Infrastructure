using Newtonsoft.Json;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerJumpedToNodeEvent : Base.Event.Event
    {
        [JsonProperty("player")]
        public PlayerModel Player;
        
        [JsonProperty("node")]
        public NodeSpaceModel NodeSpace;
        
        public PlayerJumpedToNodeEvent(PlayerModel player, NodeSpaceModel nodeSpace)
        {
            Player = player;
            NodeSpace = nodeSpace;
        }
    }
}

