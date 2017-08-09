using Newtonsoft.Json;
using System;

namespace Infrastructure.Core.Chat.Events
{
    [Serializable]
    public class ChatMessageReceivedEvent
    {
        [JsonProperty("senderId")]
        public int PlayerId;

        [JsonProperty("senderName")]
        public string PlayerName;

        [JsonProperty("receivedMessage")]
        public string ChatMessage;

        public ChatMessageReceivedEvent(int playerId, string playerName,  string chatMessage)
        {
            this.PlayerId = playerId;
            this.PlayerName = playerName;
            this.ChatMessage = chatMessage;
        }
    }
}

