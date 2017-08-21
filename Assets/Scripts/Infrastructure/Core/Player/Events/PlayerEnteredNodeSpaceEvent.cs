using Infrastructure.Core.Node;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerEnteredNodeSpaceEvent : Base.Event.Event
    {
        public NodeModel node;
        public PlayerEnteredNodeSpaceEvent(NodeModel node)
        {
            this.node = node;
        }
    }
}

