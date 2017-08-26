using UnityEngine.UI;
using Infrastructure.Core.Resource;
using Infrastructure.Core.Player;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Star.Events;
using Implementation;

namespace CWO.Market
{
    public class ResourceSlotController : InstantiatedMonoBehaviour
    {
        public Text nameText;
        public ResourceSlotModel resourceSlot;
        public Image resourceImage;
        public Image backgroundImage;
        public Button selectResource;
        public MarketMenuController marketMenuController;

        protected PlayerService playerService;
         
        void Start()
        {
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
            selectResource.onClick.AddListener(OnResourceSelected);
        }

        void OnResourceSelected()
        {
            marketMenuController.SetSelectedResourceSlot(this);
        }
    }
}

