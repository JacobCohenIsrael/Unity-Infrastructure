using Implementation;
using UnityEngine;
using Infrastructure.Core.Player;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player.Events;
using UnityEngine.UI;
using App = Infrastructure.Base.Application.Application;

namespace CWO
{
    public class PlayerController : BaseMonoBehaviour {

        public PlayerModel player;
        public Slider EnergyBar;
        public bool PlayerLoaded;
        private PlayerService _playerService;

        private void Awake () 
        {
            eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);
            eventManager.AddListener<PlayerJumpedToNodeEvent>(OnPlayJumpToNode);
            eventManager.AddListener<PlayerLandOnStarEvent>(OnPlayLandOnStar);
            eventManager.AddListener<PlayerDepartFromStarEvent>(OnPlayerDepartFromStar);
        }

        private void Start()
        {
            _playerService = application.serviceManager.get<PlayerService>() as PlayerService;
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
            float energyRegen = player.getActiveShip().getMaxEnergyRegen();
            float energyCapacity = player.getActiveShip().getMaxEnergyCapacity();
            float newCurrentEnergyAmount = player.getActiveShip().currentEnergyAmount + energyRegen * Time.deltaTime;
            player.getActiveShip().currentEnergyAmount = (newCurrentEnergyAmount > energyCapacity) ? energyCapacity : newCurrentEnergyAmount;
            EnergyBar.maxValue = energyCapacity;
            EnergyBar.value = player.getActiveShip().currentEnergyAmount;
        }
        
        protected void OnPlayerBoughtResource(PlayerBoughtResourceEvent e)
        {
            player.credits = e.player.credits;
        }

        protected void OnPlayerSoldResource(PlayerSoldResourceEvent e)
        {
            player.credits = e.player.credits;
        }
            
        protected void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            player = e.Player;
            PlayerLoaded = true;
        }

        protected void OnPlayJumpToNode(PlayerJumpedToNodeEvent e)
        {
            player.currentNodeName = e.NodeSpace.name;
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
   