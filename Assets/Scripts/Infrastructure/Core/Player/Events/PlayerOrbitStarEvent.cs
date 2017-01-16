using System;
using Infrastructure.Core.Star;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerOrbitStarEvent : Infrastructure.Base.Event.Event
    {
        public StarModel star;
        public PlayerOrbitStarEvent(StarModel star)
        {
            this.star = star;
        }
    }
}

