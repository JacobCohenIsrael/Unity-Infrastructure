using System;
using UnityEngine;
using UnityEngine.UI;
using Infrastructure.Core.Resource;
using Implementation.Views.Screen;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Player;
using Infrastructure.Base.Application.Events;

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
            application.eventManager.AddListener<PlayerBoughtResourceEvent>(this.OnPlayerBoughtResource);
            application.eventManager.AddListener<PlayerSoldResourceEvent>(this.OnPlayerSoldResource);
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
            application.eventManager.RemoveListener<PlayerBoughtResourceEvent>(this.OnPlayerBoughtResource);
            application.eventManager.RemoveListener<PlayerSoldResourceEvent>(this.OnPlayerSoldResource);
            Destroy (gameObject);
        }

        protected void OnPlayerBoughtResource(PlayerBoughtResourceEvent e)
        {
            if (e.resource.name == resourceSlot.resouce.name.ToString())
            {
                resourceSlot.amount -= e.resource.amount;
                amountText.text = resourceSlot.amount.ToString();
            }
        }

        protected void OnPlayerSoldResource(PlayerSoldResourceEvent e)
        {
            if (e.resource.name == resourceSlot.resouce.name.ToString())
            {
                resourceSlot.amount += e.resource.amount;
                amountText.text = resourceSlot.amount.ToString();
            }
        }
    }
}

