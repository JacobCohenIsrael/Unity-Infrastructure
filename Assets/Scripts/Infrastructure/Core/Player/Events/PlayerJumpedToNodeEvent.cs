using Infrastructure.Core.Node;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerJumpedToNodeEvent : Base.Event.Event
    {
        public PlayerModel player;
        public NodeModel node;
        public PlayerJumpedToNodeEvent(PlayerModel player, NodeModel node)
        {
            this.player = player;
            this.node = node;
        }
    }
}

