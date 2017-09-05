using System.Collections.Generic;
using UnityEngine;
using Implementation.Views.Screen;
using Infrastructure.Core.Player.Events;
using UnityEngine.UI;
using Infrastructure.Core.Player;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Market;

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

        private Dictionary<string, ResourceSlotController> _resourceSlots;

        private ResourceSlotController _selectedResourceSlotRef; 
        private PlayerService _playerService;

        [SerializeField]
        private Slider _buyAmountSlider;
        
        [SerializeField]
        private Slider _sellAmountSlider;
        
    	private void Start() 
        {
            Hide();
            DisableMarketButtons();
            _resourceSlots = new Dictionary<string, ResourceSlotController>();
    	}

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            eventManager.AddListener<LogoutSuccessfulEvent>(OnLogoutSuccessful);
            eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);
            eventManager.AddListener<PlayerBoughtResourceEvent>(OnPlayerBoughtResource);
            eventManager.AddListener<PlayerSoldResourceEvent>(OnPlayerSoldResource);
            eventManager.AddListener<PlayerEnteredMarketEvent>(OnMarketOpen);
            eventManager.AddListener<ResourcePriceChangedEvent>(OnResourcePriceChanged);
            exitButton.onClick.AddListener(OnExit);
            buyButton.onClick.AddListener(OnBuyResourceClicked);
            sellButton.onClick.AddListener(OnSellResourceClicked);
            _playerService = application.serviceManager.get<PlayerService>() as PlayerService;

            _buyAmountSlider.onValueChanged.AddListener(SetBuyButtonText);
            _sellAmountSlider.onValueChanged.AddListener(SetSellButtonText);
        }

        private void OnResourcePriceChanged(ResourcePriceChangedEvent e)
        {
            Debug.Log("price changed");
            _resourceSlots[e.Resource.Name].resourceSlot = e.Resource;
            SetBuyButtonText(1);
            SetSellButtonText(1);
        }

        private void OnPlayerSoldResource(PlayerSoldResourceEvent e)
        {
            RecalculateSliders();
        }

        private void OnPlayerBoughtResource(PlayerBoughtResourceEvent e)
        {
            RecalculateSliders();
        }

        private void RecalculateSliders()
        {
            _sellAmountSlider.value = 1;
            _buyAmountSlider.value = 1;
            SetBuyButtonText(_buyAmountSlider.value);
            SetSellButtonText(_sellAmountSlider.value);
        }
        
        private void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            ClearResourceList();
        }

        private void OnLogoutSuccessful(LogoutSuccessfulEvent e)
        {
            ClearResourceList();
        }

        protected void OnMarketOpen(PlayerEnteredMarketEvent e)
        {
            foreach (var resourceSlot in e.ResourceSlotList)
            {
                var texture = Resources.Load("Sprites/" + resourceSlot.Value.Name) as Texture2D;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                var instantiatedResourceSlot = Instantiate(resourceSlotPrefab, resourcesPanel);
                var resourceSlotController = instantiatedResourceSlot.GetComponent<ResourceSlotController>();
                resourceSlotController.resourceSlot = resourceSlot.Value;
                resourceSlotController.nameText.text = resourceSlot.Value.Name;
                resourceSlotController.resourceImage.color = Color.white;
                resourceSlotController.resourceImage.sprite = sprite;
                resourceSlotController.marketMenuController = this;
                _resourceSlots[resourceSlot.Value.Name] = resourceSlotController;
            }
        }

        protected void OnExit()
        {
            _playerService.ExitMarket(playerController.player);
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
            _playerService.BuyResource(playerController.player, _selectedResourceSlotRef.resourceSlot, (int)_buyAmountSlider.value);
        }

        protected void OnSellResourceClicked()
        {
            _playerService.SellResource(playerController.player, _selectedResourceSlotRef.resourceSlot, (int)_sellAmountSlider.value);
        }

        public void SetSelectedResourceSlot(ResourceSlotController resourceSlotRef)
        {
            resourceSlotRef.backgroundImage.color = Color.blue;
            
            if (_selectedResourceSlotRef != null)
            {
                _selectedResourceSlotRef.backgroundImage.color = Color.white;
            }
            if (_selectedResourceSlotRef == resourceSlotRef)
            {
                _selectedResourceSlotRef = null;
                DisableMarketButtons();
                return;
            }
            _selectedResourceSlotRef = resourceSlotRef;
            RecalculateSliders();
            EnableMarketButtons();
        }

        private void SetBuyButtonText(float buyAmount)
        {
            var player = playerController.player;
            var cargoCapacity = player.getActiveShip().GetCurrentCargo();
            int possibleResourceBuyAmount = player.credits / _selectedResourceSlotRef.resourceSlot.BuyPrice;
            int maxValue = Mathf.Min(cargoCapacity, possibleResourceBuyAmount);
            if (maxValue < 1)
            {
                buyButton.interactable = false;
                _buyAmountSlider.interactable = false;
                buyButton.GetComponentInChildren<Text>().text = "Sold for " + _selectedResourceSlotRef.resourceSlot.BuyPrice.ToString("C0");   
            }
            else
            {
                buyButton.interactable = true;
                _buyAmountSlider.interactable = true;
                _buyAmountSlider.maxValue = maxValue;
                buyButton.GetComponentInChildren<Text>().text = "Buy " + buyAmount  + " for " + _selectedResourceSlotRef.resourceSlot.BuyPrice.ToString("C0");    
            }  
        }
        
        private void SetSellButtonText(float sellAmount)
        {
            var player = playerController.player;
            int possibleResourceToSell = player.getActiveShip().GetResourceAmount(_selectedResourceSlotRef.resourceSlot.Name);
            if (possibleResourceToSell < 1)
            {
                sellButton.interactable = false;
                _sellAmountSlider.interactable = false;
                sellButton.GetComponentInChildren<Text>().text = "Bought for " + _selectedResourceSlotRef.resourceSlot.SellPrice.ToString("C0");  
            }
            else
            {
                sellButton.interactable = true;
                _sellAmountSlider.interactable = true;
                _sellAmountSlider.maxValue = possibleResourceToSell;
                sellButton.GetComponentInChildren<Text>().text = "Sell " + sellAmount  + " for " + (sellAmount * _selectedResourceSlotRef.resourceSlot.SellPrice).ToString("C0");    
            }  
        }

        private void EnableMarketButtons()
        {
            buyButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(true);
            _sellAmountSlider.gameObject.SetActive(true);
            _buyAmountSlider.gameObject.SetActive(true);
        }

        private void DisableMarketButtons()
        {
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
            _sellAmountSlider.gameObject.SetActive(false);
            _buyAmountSlider.gameObject.SetActive(false);
        }
    }
}
