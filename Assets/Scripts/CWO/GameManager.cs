using UnityEngine;
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

namespace CWO
{
    public class GameManager : MonoBehaviour {

        public LoginController loginController;
        public HudController hudController;
        public WorldMapController worldMapController;
        public StarScreenController starScreenController;
        public StarMenuController starMenuController;
        public MarketMenuController marketMenuController;

        public GameState currentState;
        public enum GameState
        {
            EntryMenu,
            WorldMap,
            StarOrbit,
            StarMenu,
            MarketMenu,
        }

        protected Infrastructure.Base.Application.Application application;
        protected EventManager eventManager;

        void Start () 
        {
//            Debug.Log("Game Manager Starting");
            ChangeState(GameState.EntryMenu);
            application = Infrastructure.Base.Application.Application.getInstance();
            eventManager = application.eventManager;
            application.eventManager.AddListener<LoginSuccessfulEvent>(this.OnLoginSuccessful);
            application.eventManager.AddListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
            application.eventManager.AddListener<PlayerOrbitStarEvent>(this.OnPlayerOrbitStar);
            application.eventManager.AddListener<PlayerJumpEvent>(this.OnPlayerJump);
            application.eventManager.AddListener<PlayerLandOnStarEvent>(this.OnPlayerLandOnStar);
            application.eventManager.AddListener<PlayerDepartFromStarEvent>(this.OnPlayerExitStarMenu);
            application.eventManager.AddListener<PlayerOpenedMarketEvent>(this.OnPlayerOpenMarket);
            application.eventManager.AddListener<PlayerExitMarketEvent>(this.OnPlayerExitMarket);

            if ( PlayerPrefs.HasKey("sessionId") )
            {
//                Debug.Log("Session Id Found");
                LoginService loginService =  Infrastructure.Base.Application.Application.getInstance().serviceManager.get<LoginService>() as LoginService;
                loginService.LoginAsGuest(PlayerPrefs.GetString("sessionId"));
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

        void StarMenuState()
        {
            hudController.Show();
            starMenuController.Show();
        }

        void MarketMenuState()
        {
            hudController.Show();
            marketMenuController.Show();
        }

        public void ChangeState(GameState newState)
        {
            HideAll();
            currentState = newState;
            StartCoroutine(newState.ToString() + "State", currentState);
        }

        void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
//            Debug.Log("Login Successful, Changing to Star Menu state");
            PlayerPrefs.SetString("sessionId", e.player.sessionId);
            PlayerPrefs.SetInt("playerId", e.player.id);
            PlayerPrefs.Save();
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

        void HideAll()
        {
            loginController.Hide();
            worldMapController.Hide();
            hudController.Hide();
            starScreenController.Hide();
            starMenuController.Hide();
            marketMenuController.Hide();
        }
    }
}