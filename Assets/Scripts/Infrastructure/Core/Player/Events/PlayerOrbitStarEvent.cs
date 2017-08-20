using Infrastructure.Core.Node;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerOrbitStarEvent : Base.Event.Event
    {
        public NodeModel node;
        public PlayerOrbitStarEvent(NodeModel node)
        {
            this.node = node;
        }
    }
}

