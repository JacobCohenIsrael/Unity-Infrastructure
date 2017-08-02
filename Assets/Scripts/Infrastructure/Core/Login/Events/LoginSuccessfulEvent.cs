using Infrastructure.Core.Player;
using Infrastructure.Core.Star;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Infrastructure.Core.Login.Events
{
    public class LoginSuccessfulEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;

        [JsonProperty("success")]
        public bool isSuccessful;

        public Dictionary<string, StarModel> starsList;

        public LoginSuccessfulEvent(PlayerModel player, Dictionary<string, StarModel> starsList)
        {
            this.player = player;
            this.starsList = starsList;
        }   
    }
}

