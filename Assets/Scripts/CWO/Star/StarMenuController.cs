using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Implementation.Views.Screen;
using UnityEngine.UI;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Player;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Star;
using Infrastructure.Base.Application.Events;

namespace CWO.Star
{
    public class StarMenuController : BaseUIObject 
    {
        public Text welcomeText;
        public Button departButton;
        public Button hangerButton;
        public Button loungeButton;
        public Button marketButton;
        public PlayerController playerController;

        protected PlayerService playerService;

        private void Start () 
        {
            playerService = application.serviceManager.get <PlayerService>() as PlayerService;
            Hide();
        }

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            eventManager.AddListener<PlayerLandOnStarEvent>(OnPlayerLandOnStar);
            eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);
            departButton.onClick.AddListener(OnDepart);
            marketButton.onClick.AddListener(OpenMarket);
            loungeButton.onClick.AddListener(EnterLounge);
        }

        protected void OnDepart()
        {
            var player = playerController.player;
            if (null == player.getActiveShip())
            {
                throw new UnityException("Player must have an active ship");
            }
            playerService.DepartPlayerFromStar(player);
        }

        protected void OpenMarket()
        {
            var player = playerController.player;
            playerService.EnterMarket(player);
        }

        protected void EnterLounge()
        {
            var player = playerController.player;
            playerService.EnterLounge(player);
        }

        protected void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            welcomeText.text = "Welcome to " + e.Player.currentNodeName;
        }

        protected void OnPlayerLandOnStar(PlayerLandOnStarEvent e)
        {
            welcomeText.text = "Welcome to " + e.player.currentNodeName;
            ShouldShowHangerButton(e.player.hasHangerInNode(e.player.currentNodeName));
        }

        public void ShouldShowHangerButton(bool shouldShow)
        {
            hangerButton.gameObject.SetActive(shouldShow);
        }
        
    }
}

