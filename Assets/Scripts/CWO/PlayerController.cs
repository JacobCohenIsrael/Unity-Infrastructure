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
            float newCurrentEnergyAmount = player.getActiveShip().currentEnergyAmount + player.getActiveShip().shipStats[Infrastructure.Core.Ship.ShipStats.EnergyRegen] * Time.deltaTime;
            player.getActiveShip().currentEnergyAmount = (newCurrentEnergyAmount > player.getActiveShip().shipStats[Infrastructure.Core.Ship.ShipStats.EnergyCapacity]) ? player.getActiveShip().shipStats[Infrastructure.Core.Ship.ShipStats.EnergyCapacity] : newCurrentEnergyAmount;
            energyBar.maxValue = player.getActiveShip().shipStats[Infrastructure.Core.Ship.ShipStats.EnergyCapacity];
            energyBar.value = player.getActiveShip().currentEnergyAmount;
        }
            
        protected void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            player = e.player;
        }

        protected void OnPlayJumpToStar(PlayerJumpedToStarEvent e)
        {
            player.currentNodeId = e.star.id;
            player.getActiveShip().currentEnergyAmount -= 10;
        }

        protected void OnPlayLandOnStar(PlayerLandOnStarEvent e)
        {
            player.isLanded = true;
        }

        protected void OnPlayerDepartFromStar(PlayerDepartFromStarEvent e)
        {
            player.isLanded = false;
        }
    }
}
   