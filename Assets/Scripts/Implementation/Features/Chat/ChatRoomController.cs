using UnityEngine;
using UnityEngine.UI;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Player;
using CWO;
using Infrastructure.Core.Chat.Events;

namespace Implementation.Features.Chat
{
    public class ChatRoomController : BaseFeature
    {
        public Button chatSendButton;
        public InputField chatInputField;
        public Text chatTextField;

        public string chatRoomKey;

        protected PlayerService playerService;
        public PlayerController playerController;

        private void Start()
        {
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
        }
        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            chatSendButton.onClick.AddListener(OnChatSendButtonClicked);
            eventManager.AddListener<ChatMessageReceivedEvent>(OnChatMessageReceived);
        }

        protected void OnChatSendButtonClicked()
        {
            if (chatInputField.text.Length > 0)
            {
                playerService.SendChat(playerController.player, chatInputField.text, chatRoomKey);
                chatInputField.text = "";
            }
            else
            {
                throw new UnityException("Must enter text!");
            }
        }

        protected void OnChatMessageReceived(ChatMessageReceivedEvent e)
        {
            chatTextField.text += e.PlayerName + ": " + e.ChatMessage + "\n\r";
        }
    }
}
