using UnityEngine;
using Implementation.Views.Screen;
using UnityEngine.UI;
using Infrastructure.Core.Login;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Player;
using Infrastructure.Core.Player.Events;
using Newtonsoft.Json;

namespace CWO.Hud
{
    public class HudController : BaseUIObject 
    {
        [SerializeField]
        private Button _logoutButton;
        [SerializeField]
        private Text _creditsText;
        [SerializeField]
        private Text _cargo;
        [SerializeField]
        private Text _cargoCapacity;

        void Start ()
        {
            Hide();
        }

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            _logoutButton.onClick.AddListener(OnLogout);
            eventManager.AddListener<PlayerBoughtResourceEvent>(OnPlayerBoughtResource);
            eventManager.AddListener<PlayerSoldResourceEvent>(OnPlayerSoldResource);
        }

        private void OnPlayerBoughtResource(PlayerBoughtResourceEvent e)
        {
            UpdateCargo(e.player);
            UpdateCargoCapacity(e.player);
            UpdateCargo(e.player);
        }

        private void OnPlayerSoldResource(PlayerSoldResourceEvent e)
        {
            UpdateCargo(e.player);
            UpdateCargoCapacity(e.player);
            UpdateCargo(e.player);
        }

        protected void UpdateCredits(PlayerModel player)
        {
            _creditsText.text = "Credits: " + player.credits.ToString("C0");
        }

        protected void UpdateCargo(PlayerModel player)
        {
            _cargo.text = "Cagro:\n" + JsonConvert.SerializeObject(player.getActiveShip().shipCargo).Replace("{","").Replace("}", "").Replace(",", "\n\r");
        }

        private void UpdateCargoCapacity(PlayerModel player)
        {
            _cargoCapacity.text = "Cargo Capacity: " + player.getActiveShip().GetShipCargoHold() + "/" + player.getActiveShip().GetShipCargoCapacity();
        }

        public void OnLogout()
        {
            LoginService loginService = application.serviceManager.get<LoginService>() as LoginService;
            loginService.Logout();
        }
    }
}

