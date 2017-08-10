using Infrastructure.Core.Star;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerJumpedToStarEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;
        public StarModel star;
        public PlayerJumpedToStarEvent(PlayerModel player, StarModel star)
        {
            this.player = player;
            this.star = star;
        }
    }
}

