using System;
using UnityEngine;
using UnityEngine.UI;
using Infrastructure.Core.Resource;
using Implementation.Views.Screen;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Player;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Star.Events;

namespace CWO.Market
{
    public class ResourceSlotController : BaseUIObject
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
            application.eventManager.AddListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
            application.eventManager.AddListener<PlayerExitMarketEvent>(this.OnPlayerExitMarket);
            application.eventManager.AddListener<UpdateResourceAmountEvent>(this.OnUpdateResourceAmount);
            selectResource.onClick.AddListener(() => { this.OnResourceSelected(); });
        }

        protected override void SubscribeToEvents(SubscribeEvent e)
        {

        }

        void OnResourceSelected()
        {
            marketMenuController.SetSelectedResourceSlot(this);
        }
            
        void OnLogoutSuccessful(LogoutSuccessfulEvent e)
        {
            Debug.Log("Logout Successful, Destroying Resource Slot");
            SelfDestruct();
        }

        void OnPlayerExitMarket(PlayerExitMarketEvent e)
        {
            Debug.Log("Exiting Market, Destroying Resource Slot");
            SelfDestruct();
        }

        protected void SelfDestruct()
        {
            application.eventManager.RemoveListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
            application.eventManager.RemoveListener<PlayerExitMarketEvent>(this.OnPlayerExitMarket);
            application.eventManager.RemoveListener<UpdateResourceAmountEvent>(this.OnUpdateResourceAmount);
            Destroy (gameObject);
        }

        protected void OnUpdateResourceAmount(UpdateResourceAmountEvent e)
        {
            if (e.resourceName == resourceSlot.name)
            {
                resourceSlot.amount = e.newAmount;
                amountText.text = e.newAmount.ToString();
            }
        }
    }
}

