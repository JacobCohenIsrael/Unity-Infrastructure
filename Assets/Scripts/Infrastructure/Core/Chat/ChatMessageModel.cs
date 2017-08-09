using Infrastructure.Core.Ship;
using Newtonsoft.Json;
using System;

namespace Infrastructure.Core.Chat
{
    [Serializable]
    public class ChatMessageModel : Infrastructure.Base.Model.Model
    {
        [JsonProperty("message")]
        public string Message;

        [JsonProperty("roomKey")]
        public string RoomKey;
    }
}