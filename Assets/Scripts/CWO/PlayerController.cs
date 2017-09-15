﻿using Implementation;
using UnityEngine;
using Infrastructure.Core.Player;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player.Events;
using UnityEngine.UI;

namespace CWO
{
    public class PlayerController : BaseMonoBehaviour {

        public PlayerModel player;
        public Slider EnergyBar;
        public bool PlayerLoaded;

        private float lastRegenTime;

        private void Awake ()
        {
            eventManager.AddListener<PlayerBoughtResourceEvent>(OnPlayerBoughtResource);
            eventManager.AddListener<PlayerSoldResourceEvent>(OnPlayerSoldResource);
            eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);
            eventManager.AddListener<PlayerJumpedToNodeEvent>(OnPlayJumpToNode);
            eventManager.AddListener<PlayerLandOnStarEvent>(OnPlayLandOnStar);
            eventManager.AddListener<PlayerDepartFromStarEvent>(OnPlayerDepartFromStar);
        }

        private void Start()
        {
            lastRegenTime = Time.time;
        }

        private void Update()
        {
            if (application.HasStarted && PlayerLoaded)
            {
                ShipRegen();
            }
        }
        
        protected void ShipRegen()
        {
            ShipEnergyRegen();

        }

        protected void ShipEnergyRegen()
        {
            int energyRegen = player.getActiveShip().GetMaxEnergyRegen();
            int energyCapacity = player.getActiveShip().GetMaxEnergyCapacity();
            int energyAddition = (int)(energyRegen * (Time.time - lastRegenTime));
            if (energyAddition > 0)
            {
                lastRegenTime = Time.time;
                int newCurrentEnergyAmount = player.getActiveShip().GetCurrentEnergy() + energyAddition;
                player.getActiveShip().SetCurrentEnergy(newCurrentEnergyAmount > energyCapacity ? energyCapacity : newCurrentEnergyAmount);
                EnergyBar.maxValue = energyCapacity;
                EnergyBar.value = player.getActiveShip().GetCurrentEnergy();
            }
        }
        
        protected void OnPlayerBoughtResource(PlayerBoughtResourceEvent e)
        {
            player = e.Player;
        }

        protected void OnPlayerSoldResource(PlayerSoldResourceEvent e)
        {
            player = e.Player;
        }
            
        protected void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            player = e.Player;
            PlayerLoaded = true;
        }

        protected void OnPlayJumpToNode(PlayerJumpedToNodeEvent e)
        {
            player = e.Player;
        }

        protected void OnPlayLandOnStar(PlayerLandOnStarEvent e)
        {
            player.isLanded = e.player.isLanded;
        }

        protected void OnPlayerDepartFromStar(PlayerDepartFromStarEvent e)
        {
            player.isLanded = e.Player.isLanded;
        }
    }
}
   