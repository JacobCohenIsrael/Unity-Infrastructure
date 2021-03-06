﻿using UnityEngine;
using Infrastructure.Core.Player;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player.Events;
using UnityEngine.UI;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Base.Event;
using Newtonsoft.Json;

namespace CWO
{
    public class PlayerController : MonoBehaviour {

        public PlayerModel player;
        public Slider energyBar;
        public Text creditsText;
        public Text cargo;
        protected PlayerService playerService;
        protected App application;
        protected float lastRegen;
        protected bool playerLoaded;

        void Awake () 
        {
            application = App.getInstance();
            EventManager eventManager = application.eventManager;
            eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
            eventManager.AddListener<PlayerJumpedToStarEvent>(this.OnPlayJumpToStar);
            eventManager.AddListener<PlayerLandOnStarEvent>(this.OnPlayLandOnStar);
            eventManager.AddListener<PlayerDepartFromStarEvent>(this.OnPlayerDepartFromStar);
            eventManager.AddListener<PlayerBoughtResourceEvent>(this.OnPlayerBoughtResource);
            eventManager.AddListener<PlayerSoldResourceEvent>(this.OnPlayerSoldResource);
        }

        void Start()
        {
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
        }

        void Update()
        {
            if (application.hasStarted && playerLoaded)
            {
                ShipRegen();
                UpdateCredits();
                UpdateCargo();
            }
        }
            
        protected void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            player = e.player;
            playerLoaded = true;
        }

        protected void OnPlayJumpToStar(PlayerJumpedToStarEvent e)
        {
            player.currentNodeName = e.star.name;
        }

        protected void OnPlayLandOnStar(PlayerLandOnStarEvent e)
        {
            player.isLanded = e.player.isLanded;
        }

        protected void OnPlayerDepartFromStar(PlayerDepartFromStarEvent e)
        {
            player.isLanded = e.player.isLanded;
        }

        protected void ShipRegen()
        {
            ShipEnergyRegen();

        }

        protected void ShipEnergyRegen()
        {
            float energyRegen = player.getActiveShip().getMaxEnergyRegen();
            float energyCapacity = player.getActiveShip().getMaxEnergyCapacity();
            float newCurrentEnergyAmount = player.getActiveShip().currentEnergyAmount + energyRegen * Time.deltaTime;
            player.getActiveShip().currentEnergyAmount = (newCurrentEnergyAmount > energyCapacity) ? energyCapacity : newCurrentEnergyAmount;
            energyBar.maxValue = energyCapacity;
            energyBar.value = player.getActiveShip().currentEnergyAmount;
        }

        protected void UpdateCredits()
        {
            creditsText.text = "Credits: " + player.credits.ToString("C0");
        }

        protected void UpdateCargo()
        {
            cargo.text = "Cagro: " + JsonConvert.SerializeObject(player.getActiveShip().shipCargo);
        }

        protected void OnPlayerBoughtResource(PlayerBoughtResourceEvent e)
        {
            this.player.credits = e.player.credits;
        }

        protected void OnPlayerSoldResource(PlayerSoldResourceEvent e)
        {
            this.player.credits = e.player.credits;
        }
    }
}
   