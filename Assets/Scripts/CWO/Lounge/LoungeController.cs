using Implementation.Views.Screen;
using Infrastructure.Base.Application.Events;
using Infrastructure.Base.Event;
using Infrastructure.Core.Player;
using UnityEngine.UI;

namespace CWO.Market 
{
    public class LoungeController : BaseUIObject 
    {
        public Button exitButton;

        public Button chatSendButton;

        public InputField chatInputField; 

        public PlayerController playerController;
        protected PlayerService playerService;

    	void Start() 
        {
            Hide();
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
        }

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            exitButton.onClick.AddListener(() => { this.OnExitLounge(); });
            chatSendButton.onClick.AddListener(() => { this.OnChatSendButtonClicked(); });
        }

        protected void OnExitLounge()
        {
            playerService.LeaveLounge(playerController.player);
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
