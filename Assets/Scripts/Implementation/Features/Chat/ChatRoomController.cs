using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Infrastructure.Base.Application.Events;
using System;
using Infrastructure.Core.Player;
using CWO;

namespace Implementation.Features.Chat
{
    public class ChatRoomController : BaseFeature
    {
        public Button chatSendButton;
        public InputField chatInputField;

        protected PlayerService playerService;
        public PlayerController playerController;

        private void Start()
        {
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
        }
        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            chatSendButton.onClick.AddListener(() => { this.OnChatSendButtonClicked(); });
        }

        protected void OnChatSendButtonClicked()
        {
            if (chatInputField.text.Length > 0)
            {
                playerService.LoungeChatSent(playerController.player, chatInputField.text);
                chatInputField.text = "";
            }
            else
            {
                throw new UnityEngine.UnityException("Must enter text!");
            }
        }
    }
}
