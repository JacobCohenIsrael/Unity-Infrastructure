﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Implementation.Views.Screen;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Resource;
using UnityEngine.UI;
using Infrastructure.Core.Player;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Login.Events;
using System;

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
            eventManager.AddListener<LogoutSuccessfulEvent>(OnLogoutSuccessful);
            eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);
        }

        private void OnLoginSuccessful(LoginSuccessfulEvent obj)
        {
            ClearResourceList();
        }

        private void OnLogoutSuccessful(LogoutSuccessfulEvent e)
        {
            ClearResourceList();
        }

        protected void OnMarketOpen(PlayerEnteredMarketEvent e)
        {
            Debug.Log("Marked opened");
            foreach (KeyValuePair<string, ResourceSlotModel> resourceSlot in e.ResourceSlotList)
            {
                var texture = UnityEngine.Resources.Load("Sprites/" + resourceSlot.Value.Name) as Texture2D;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                var instantiatedResourceSlot = Instantiate(resourceSlotPrefab, resourcesPanel);
                var resourceSlotController = instantiatedResourceSlot.GetComponent<ResourceSlotController>();
                resourceSlotController.resourceSlot = resourceSlot.Value;
                resourceSlotController.nameText.text = resourceSlot.Value.Name;
                resourceSlotController.resourceImage.color = Color.white;
                resourceSlotController.resourceImage.sprite = sprite;
                resourceSlotController.marketMenuController = this;
            }
        }

        protected void OnExit()
        {
            playerService.ExitMarket(playerController.player);
            ClearResourceList();
        }

        private void ClearResourceList()
        {
            foreach (Transform child in resourcesPanel)
            {
                Destroy(child.gameObject);
                DisableMarketButtons();
            }
        }


        protected void OnBuyResourceClicked()
        {
            playerService.BuyResource(playerController.player, selectedResourceSlotRef.resourceSlot, 1);
        }

        protected void OnSellResourceClicked()
        {
            playerService.SellResource(playerController.player, selectedResourceSlotRef.resourceSlot, 1);
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
            buyButton.GetComponentInChildren<Text>().text = "Buy for " + resourceSlotRef.resourceSlot.BuyPrice.ToString("C0");
            sellButton.GetComponentInChildren<Text>().text = "Sell for " + resourceSlotRef.resourceSlot.SellPrice.ToString("C0");
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
