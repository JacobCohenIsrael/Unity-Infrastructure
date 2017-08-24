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
        public Text amountText;
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
            application.eventManager.AddListener<UpdateResourceAmountEvent>(OnUpdateResourceAmount);
            selectResource.onClick.AddListener(OnResourceSelected);
        }

        void OnResourceSelected()
        {
            marketMenuController.SetSelectedResourceSlot(this);
        }

        protected void OnUpdateResourceAmount(UpdateResourceAmountEvent e)
        {
            if (e.resourceName == resourceSlot.name)
            {
                resourceSlot.amount = e.newAmount;
                amountText.text = e.newAmount.ToString();
            }
        }

        private void OnDestroy()
        {
            application.eventManager.RemoveListener<UpdateResourceAmountEvent>(OnUpdateResourceAmount);
        }
    }
}

