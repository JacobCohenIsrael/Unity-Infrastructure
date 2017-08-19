using Infrastructure.Core.Player;
using Infrastructure.Core.Star;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Infrastructure.Core.Login.Events
{
    public class LoginSuccessfulEvent : Infrastructure.Base.Event.Event
    {
        [JsonProperty("player")]
        private PlayerModel m_player;

        public PlayerModel Player {
            get { return this.m_player; }
            set { this.m_player = value; }
        }
    }
}

