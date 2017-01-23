using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Implementation.Views.Screen;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Resource;
using UnityEngine.UI;
using Infrastructure.Core.Player;

namespace CWO.Market 
{
    public class MarketMenuController : BaseUIObject 
    {
        public GameObject resourceSlotPrefab;
        public Transform resourcesPanel;
        public Button exitButton;


        protected PlayerService playerService;

    	void Start () 
        {
            application.eventManager.AddListener<PlayerOpenedMarketEvent>(this.OnMarketOpen);
            exitButton.onClick.AddListener(() => { this.OnExit(); });
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
            Hide();
    	}

        protected void OnMarketOpen(PlayerOpenedMarketEvent e)
        {
            foreach (KeyValuePair<Infrastructure.Core.Resource.Resources, ResourceSlotModel> resourceSlot in e.star.resourceList)
            {
                Texture2D texture = UnityEngine.Resources.Load("Sprites/" + resourceSlot.Value.resouce.image) as Texture2D;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                GameObject instantiatedResourceSlot = Instantiate(resourceSlotPrefab, resourcesPanel) as GameObject;
                ResourceSlotController resourceSlotController = instantiatedResourceSlot.GetComponent<ResourceSlotController>();
                resourceSlotController.resourceSlot = resourceSlot.Value;
                resourceSlotController.nameText.text = resourceSlot.Value.resouce.name.ToString();
                resourceSlotController.amountText.text = resourceSlot.Value.amount.ToString();
                resourceSlotController.buyButton.GetComponentInChildren<Text>().text = "Buy for " + resourceSlot.Value.buyPrice.ToString("C0");
                resourceSlotController.sellButton.GetComponentInChildren<Text>().text = "Sell for " + resourceSlot.Value.sellPrice.ToString("C0");
                resourceSlotController.resourceImage.color = Color.white;
                resourceSlotController.resourceImage.sprite = sprite;

            }
        }

        protected void OnExit()
        {
            if (PlayerPrefs.HasKey("playerId"))
            {
                int playerId = PlayerPrefs.GetInt("PlayerId");
                PlayerModel player = playerService.getPlayerById(playerId);
                playerService.exitMarket(player);
            }
        }
    }
}
