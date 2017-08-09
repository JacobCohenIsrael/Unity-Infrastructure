using System;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Player.Events
{
    public class PlayerSentChatEvent
    {
        public Player.PlayerModel player;
        public string chatMessage;

        public PlayerSentChatEvent(Player.PlayerModel player, string chatMessage)
        {
            this.player = player;
            this.chatMessage = chatMessage;
        }
    }
}

