﻿using System.Collections;
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
        protected StarService starService;

        void Start () 
        {
            playerService = application.serviceManager.get <PlayerService>() as PlayerService;
            starService = application.serviceManager.get <StarService>() as StarService;
            Hide();
        }

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            eventManager.AddListener<PlayerLandOnStarEvent>(this.OnPlayerLandOnStar);
            eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
            departButton.onClick.AddListener(() => { this.OnDepart(); });
            marketButton.onClick.AddListener(() => { this.OpenMarket(); });
            loungeButton.onClick.AddListener(() => { this.EnterLounge(); });
        }

        protected void OnDepart()
        {
            PlayerModel player = playerController.player;
            if (null == player.getActiveShip())
            {
                throw new UnityException("Player must have an active ship");
            }
            playerService.DepartPlayerFromStar(player);
        }

        protected void OpenMarket()
        {
            PlayerModel player = playerController.player;
            playerService.EnterMarket(player);
        }

        protected void EnterLounge()
        {
            PlayerModel player = playerController.player;
            playerService.EnterLounge(player);
        }

        protected void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            welcomeText.text = "Welcome to " + e.Player.currentNodeName;
        }

        protected void OnPlayerLandOnStar(PlayerLandOnStarEvent e)
        {
            welcomeText.text = "Welcome to " + e.player.currentNodeName;
            if (e.player.homePlanetName == e.player.currentNodeName)
            {
                hangerButton.gameObject.SetActive(true);
            }
            else
            {
                hangerButton.gameObject.SetActive(false);
            }
        }
    }
}

