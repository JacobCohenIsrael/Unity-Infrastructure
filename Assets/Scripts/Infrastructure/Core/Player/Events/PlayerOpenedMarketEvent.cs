using Infrastructure.Core.Star;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerOpenedMarketEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;
        public StarModel star;
        public PlayerOpenedMarketEvent(PlayerModel player, StarModel star)
        {
            this.player = player;
            this.star = star;
        }
    }
}

