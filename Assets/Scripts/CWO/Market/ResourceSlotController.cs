using System;
using UnityEngine;
using UnityEngine.UI;
using Infrastructure.Core.Resource;
using Implementation.Views.Screen;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Player;

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

        protected PlayerService playerService;
         
        void Start()
        {
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
            application.eventManager.AddListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
            application.eventManager.AddListener<PlayerExitMarketEvent>(this.OnPlayerExitMarket);
            application.eventManager.AddListener<PlayerBoughtResourceEvent>(this.OnPlayerBoughtResource);
            application.eventManager.AddListener<PlayerSoldResourceEvent>(this.OnPlayerSoldResource);
            buyButton.onClick.AddListener(() => { this.OnBuyResourceClicked(); });
            sellButton.onClick.AddListener(() => { this.OnSellResourceClicked(); });
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

        protected void OnBuyResourceClicked()
        {
            if (PlayerPrefs.HasKey("playerId"))
            {
                int playerId = PlayerPrefs.GetInt("PlayerId");
                PlayerModel player = playerService.getPlayerById(playerId);
                playerService.BuyResource(player, resourceSlot.resouce);
            }
        }

        protected void OnSellResourceClicked()
        {
            if (PlayerPrefs.HasKey("playerId"))
            {
                int playerId = PlayerPrefs.GetInt("PlayerId");
                PlayerModel player = playerService.getPlayerById(playerId);
                playerService.SellResource(player, resourceSlot.resouce);
            }
        }

        protected void OnPlayerBoughtResource(PlayerBoughtResourceEvent e)
        {
            if (e.resouceSlot.resouce == resourceSlot.resouce)
            {
                amountText.text = resourceSlot.amount.ToString();
            }
        }

        protected void OnPlayerSoldResource(PlayerSoldResourceEvent e)
        {
            if (e.resouceSlot.resouce == resourceSlot.resouce)
            {
                amountText.text = resourceSlot.amount.ToString();
            }
        }
    }
}

