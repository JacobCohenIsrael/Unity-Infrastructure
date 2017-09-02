using System;
using Infrastructure.Core.Player;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerLeftMarketEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel Player;
    }
}

