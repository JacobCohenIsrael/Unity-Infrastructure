using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infrastructure.Core.Player;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player.Events;
using UnityEngine.UI;

namespace CWO
{
    public class PlayerController : MonoBehaviour {

        public PlayerModel player;
        public Slider energyBar;
        protected PlayerService playerService;

        protected float lastRegen;

        void Start () 
        {
            Infrastructure.Base.Application.Application application = Infrastructure.Base.Application.Application.getInstance();
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
            application.eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
            application.eventManager.AddListener<PlayerJumpedToStarEvent>(this.OnPlayJumpToStar);
            application.eventManager.AddListener<PlayerLandOnStarEvent>(this.OnPlayLandOnStar);
            application.eventManager.AddListener<PlayerDepartFromStarEvent>(this.OnPlayerDepartFromStar);
        }

        void Update()
        {
            shipRegen();
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

        protected void shipRegen()
        {
            shipEnergyRegen();

        }

        protected void shipEnergyRegen()
        {
            float energyRegen = player.getActiveShip().shipStats[Infrastructure.Core.Ship.ShipStats.EnergyRegen];
            float energyCapacity = player.getActiveShip().shipStats[Infrastructure.Core.Ship.ShipStats.EnergyCapacity];
            float newCurrentEnergyAmount = player.getActiveShip().currentEnergyAmount + energyRegen * Time.deltaTime;
            player.getActiveShip().currentEnergyAmount = (newCurrentEnergyAmount > energyCapacity) ? energyCapacity : newCurrentEnergyAmount;
            energyBar.maxValue = energyCapacity;
            energyBar.value = player.getActiveShip().currentEnergyAmount;
        }
    }
}
   