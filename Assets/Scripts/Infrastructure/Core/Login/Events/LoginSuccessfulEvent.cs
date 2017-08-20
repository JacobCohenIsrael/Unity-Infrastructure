using System.Collections.Generic;
using Infrastructure.Core.Node;
using Infrastructure.Core.Player;
using Newtonsoft.Json;

namespace Infrastructure.Core.Login.Events
{
    public class LoginSuccessfulEvent : Base.Event.Event
    {
        [JsonProperty("player")]
        private PlayerModel _player;

        public PlayerModel Player 
        {
            get { return _player; }
            set { _player = value; }
        }

        [JsonProperty("node")]
        public NodeModel Node;

        [JsonProperty("nodesCoords")]
        public Dictionary<string, NodeModel> NodesCoords;
    }
}

