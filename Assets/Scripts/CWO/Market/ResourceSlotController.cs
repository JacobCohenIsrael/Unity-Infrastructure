using System;
using UnityEngine;
using UnityEngine.UI;
using Infrastructure.Core.Resource;
using Implementation.Views.Screen;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player.Events;

namespace CWO.Market
{
    public class ResourceSlotController : BaseUIObject
    {
        public Text amountText;
        public Text nameText;
        public ResourceSlotModel resourceSlot;
        public Image resourceImage;
        public Button buyButton;
        public Button sellButton;
         
        void Start()
        {
            application.eventManager.AddListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
            application.eventManager.AddListener<PlayerExitMarketEvent>(this.OnPlayerExitMarket);
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
            Destroy (gameObject);
        }
    }
}

