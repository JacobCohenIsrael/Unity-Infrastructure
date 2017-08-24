using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Implementation.Views.Screen;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Resource;
using UnityEngine.UI;
using Infrastructure.Core.Player;
using Infrastructure.Base.Application.Events;

namespace CWO.Market 
{
    public class MarketMenuController : BaseUIObject 
    {
        public GameObject resourceSlotPrefab;
        public Transform resourcesPanel;
        public Button exitButton;
        public Button buyButton;
        public Button sellButton;
        public PlayerController playerController;

        protected ResourceSlotController selectedResourceSlotRef; 
        protected PlayerService playerService;

    	void Start() 
        {
            Hide();
            DisableMarketButtons();
    	}

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            application.eventManager.AddListener<PlayerEnteredMarketEvent>(OnMarketOpen);
            exitButton.onClick.AddListener(OnExit);
            buyButton.onClick.AddListener(OnBuyResourceClicked);
            sellButton.onClick.AddListener(OnSellResourceClicked);
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
        }

        protected void OnMarketOpen(PlayerEnteredMarketEvent e)
        {
            foreach (KeyValuePair<string, ResourceSlotModel> resourceSlot in e.ResourceSlotList)
            {
                Texture2D texture = UnityEngine.Resources.Load("Sprites/" + resourceSlot.Value.name) as Texture2D;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                GameObject instantiatedResourceSlot = Instantiate(resourceSlotPrefab, resourcesPanel) as GameObject;
                ResourceSlotController resourceSlotController = instantiatedResourceSlot.GetComponent<ResourceSlotController>();
                resourceSlotController.resourceSlot = resourceSlot.Value;
                resourceSlotController.nameText.text = resourceSlot.Value.name;
                resourceSlotController.amountText.text = resourceSlot.Value.amount.ToString();
                resourceSlotController.resourceImage.color = Color.white;
                resourceSlotController.resourceImage.sprite = sprite;
                resourceSlotController.marketMenuController = this;
            }
        }

        protected void OnExit()
        {
            playerService.ExitMarket(playerController.player);
            DisableMarketButtons();
        }


        protected void OnBuyResourceClicked()
        {
            playerService.BuyResource(playerController.player, selectedResourceSlotRef.resourceSlot);
        }

        protected void OnSellResourceClicked()
        {
            playerService.SellResource(playerController.player, selectedResourceSlotRef.resourceSlot);
        }

        public void SetSelectedResourceSlot(ResourceSlotController resourceSlotRef)
        {
            resourceSlotRef.backgroundImage.color = Color.blue;
            if (selectedResourceSlotRef != null)
            {
                selectedResourceSlotRef.backgroundImage.color = Color.white;
            }
            if (selectedResourceSlotRef == resourceSlotRef)
            {
                selectedResourceSlotRef = null;
                DisableMarketButtons();
                return;
            }
            selectedResourceSlotRef = resourceSlotRef;
            buyButton.GetComponentInChildren<Text>().text = "Buy for " + resourceSlotRef.resourceSlot.buyPrice.ToString("C0");
            sellButton.GetComponentInChildren<Text>().text = "Sell for " + resourceSlotRef.resourceSlot.sellPrice.ToString("C0");
            EnableMarketButtons();

        }

        private void EnableMarketButtons()
        {
            buyButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(true);
        }

        private void DisableMarketButtons()
        {
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
        }
    }
}
