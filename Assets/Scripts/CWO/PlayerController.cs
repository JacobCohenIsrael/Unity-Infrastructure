﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infrastructure.Core.Player;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player.Events;
using UnityEngine.UI;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Base.Event;

namespace CWO
{
    public class PlayerController : MonoBehaviour {

        public PlayerModel player;
        public Slider energyBar;
        public Text creditsText;
        protected PlayerService playerService;
        protected App application;
        protected float lastRegen;

        void Awake () 
        {
            application = App.getInstance();
            EventManager eventManager = application.eventManager;
            eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
            eventManager.AddListener<PlayerJumpedToStarEvent>(this.OnPlayJumpToStar);
            eventManager.AddListener<PlayerLandOnStarEvent>(this.OnPlayLandOnStar);
            eventManager.AddListener<PlayerDepartFromStarEvent>(this.OnPlayerDepartFromStar);
        }

        void Start()
        {
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
        }

        void Update()
        {
            if (PlayerPrefs.HasKey("playerId"))
            {
                ShipRegen();
                UpdateCredits();
            }
        }
            
        protected void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            player = e.player;
        }

        protected void OnPlayJumpToStar(PlayerJumpedToStarEvent e)
        {
            player.currentNodeId = e.star.id;
        }

        protected void OnPlayLandOnStar(PlayerLandOnStarEvent e)
        {
//            player = e.player;
        }

        protected void OnPlayerDepartFromStar(PlayerDepartFromStarEvent e)
        {
//            player = e.player;
        }

        protected void ShipRegen()
        {
            ShipEnergyRegen();

        }

        protected void ShipEnergyRegen()
        {
            float energyRegen = player.getActiveShip().shipStats[Infrastructure.Core.Ship.ShipStats.EnergyRegen];
            float energyCapacity = player.getActiveShip().shipStats[Infrastructure.Core.Ship.ShipStats.EnergyCapacity];
            float newCurrentEnergyAmount = player.getActiveShip().currentEnergyAmount + energyRegen * Time.deltaTime;
            player.getActiveShip().currentEnergyAmount = (newCurrentEnergyAmount > energyCapacity) ? energyCapacity : newCurrentEnergyAmount;
            energyBar.maxValue = energyCapacity;
            energyBar.value = player.getActiveShip().currentEnergyAmount;
        }

        protected void UpdateCredits()
        {
            creditsText.text = "Credits: " + player.credits.ToString("C0");
        }
    }
}
   