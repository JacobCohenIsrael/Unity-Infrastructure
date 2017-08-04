﻿using UnityEngine;
using System.Collections;
using Infrastructure.Core.Login;
using Infrastructure.Core.Login.Events;
using Infrastructure.Base.Event;
using CWO.Login;
using CWO.Hud;
using CWO.Star;
using Infrastructure.Core.Star;
using Infrastructure.Core.Player.Events;
using CWO.Market;
using Infrastructure.Base.Application.Events;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Core.Network;


namespace CWO
{
    public class GameManager : MonoBehaviour {

        public LoginController loginController;
        public HudController hudController;
        public WorldMapController worldMapController;
        public StarScreenController starScreenController;
        public StarMenuController starMenuController;
        public MarketMenuController marketMenuController;
        public LoungeController loungeController;

        public GameState currentState;
        public enum GameState
        {
            EntryMenu,
            WorldMap,
            StarOrbit,
            StarMenu,
            MarketMenu,
            Lounge,
        }

        protected App application;
        protected EventManager eventManager;
        protected MainServer mainServer;

        void Awake()
        {
            application = App.getInstance();
            eventManager = application.eventManager;
            eventManager.AddListener<ApplicationFinishedLoadingEvent>(this.OnApplicationReady);
            eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
            eventManager.AddListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
            eventManager.AddListener<PlayerOrbitStarEvent>(this.OnPlayerOrbitStar);
            eventManager.AddListener<PlayerJumpEvent>(this.OnPlayerJump);
            eventManager.AddListener<PlayerLandOnStarEvent>(this.OnPlayerLandOnStar);
            eventManager.AddListener<PlayerDepartFromStarEvent>(this.OnPlayerExitStarMenu);
            eventManager.AddListener<PlayerOpenedMarketEvent>(this.OnPlayerOpenMarket);
            eventManager.AddListener<PlayerExitMarketEvent>(this.OnPlayerExitMarket);
            eventManager.AddListener<PlayerEnteredLoungeEvent>(this.OnPlayerEnteredLounge);
            eventManager.AddListener<PlayerLeftLoungeEvent>(this.OnPlayerLeftLounge);
        }

        void OnApplicationReady(ApplicationFinishedLoadingEvent e) 
        {
            ChangeState(GameState.EntryMenu);
            if ( PlayerPrefs.HasKey(LoginController.loginSessionId) )
            {
                Debug.Log("Session Id Found");
                LoginService loginService =  application.serviceManager.get<LoginService>() as LoginService;
                loginService.LoginAsGuest(PlayerPrefs.GetString(LoginController.loginSessionId));
            }
        }

        void EntryMenuState()
        {
            loginController.Show();
        }

        void WorldMapState()
        {
            hudController.Show();
            worldMapController.Show();
        }

        void StarOrbitState()
        {
            hudController.Show();
            starScreenController.Show();
        }

        public void StarMenuState()
        {
            hudController.Show();
            starMenuController.Show();
        }

        void MarketMenuState()
        {
            hudController.Show();
            marketMenuController.Show();
        }

        void LoungeState()
        {
            hudController.Show();
            loungeController.Show();
        }

        public void ChangeState(GameState newState)
        {
            HideAll();
            currentState = newState;
            StartCoroutine(newState.ToString() + "State", currentState);
        }

        void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            ChangeState(GameState.StarMenu);
        }

        void OnLogoutSuccessful(LogoutSuccessfulEvent e)
        {
            ChangeState(GameState.EntryMenu);
        }

        void OnPlayerOrbitStar(PlayerOrbitStarEvent e)
        {
            ChangeState(GameState.StarOrbit);
        }

        void OnPlayerJump(PlayerJumpEvent e)
        {
            ChangeState(GameState.WorldMap);
        }
    
        void OnPlayerLandOnStar(PlayerLandOnStarEvent e)
        {
            ChangeState(GameState.StarMenu);
        }

        void OnPlayerExitStarMenu(PlayerDepartFromStarEvent e)
        {
            ChangeState(GameState.StarOrbit);
        }

        void OnPlayerOpenMarket(PlayerOpenedMarketEvent e)
        {
            ChangeState(GameState.MarketMenu);
        }

        void OnPlayerExitMarket(PlayerExitMarketEvent e)
        {
            ChangeState(GameState.StarMenu);
        }

        void OnPlayerEnteredLounge(PlayerEnteredLoungeEvent e)
        {
            ChangeState(GameState.Lounge);
        }

        void OnPlayerLeftLounge(PlayerLeftLoungeEvent e)
        {
            ChangeState(GameState.StarMenu);
        }

        void HideAll()
        {
            loginController.Hide();
            worldMapController.Hide();
            hudController.Hide();
            starScreenController.Hide();
            starMenuController.Hide();
            marketMenuController.Hide();
            loungeController.Hide();
        }
    }
}