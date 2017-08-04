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
        }

        protected void OnExitLounge()
        {
            playerService.LeaveLounge(playerController.player);
        }
    }
}
