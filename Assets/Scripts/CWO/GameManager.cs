using UnityEngine;
using Infrastructure.Core.Login;
using Infrastructure.Core.Login.Events;
using Infrastructure.Base.Event;
using CWO.Login;
using CWO.Hud;
using CWO.Star;
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
            NodeSpace,
            StarMenu,
            MarketMenu,
            Lounge
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
            eventManager.AddListener<PlayerEnteredNodeSpaceEvent>(this.OnPlayerEnterNodeSpace);
            eventManager.AddListener<PlayerOpenedWorldMap>(this.OnPlayerOpenedWorldMap);
            eventManager.AddListener<PlayerLandOnStarEvent>(this.OnPlayerLandOnStar);
            eventManager.AddListener<PlayerDepartFromStarEvent>(this.OnPlayerExitStarMenu);
            eventManager.AddListener<PlayerEnteredMarketEvent>(this.OnPlayerEnteredMarket);
            eventManager.AddListener<PlayerExitMarketEvent>(this.OnPlayerExitMarket);
            eventManager.AddListener<PlayerEnteredLoungeEvent>(this.OnPlayerEnteredLounge);
            eventManager.AddListener<PlayerLeftLoungeEvent>(this.OnPlayerLeftLounge);
            eventManager.AddListener<PlayerJumpedToNodeEvent>(OnPlayerJumpedToNode);
        }

        void OnApplicationReady(ApplicationFinishedLoadingEvent e) 
        {
            ChangeState(GameState.EntryMenu);
            if ( PlayerPrefs.HasKey(LoginController.loginToken) )
            {
                Debug.Log("Session Id Found");
                LoginService loginService =  application.serviceManager.get<LoginService>() as LoginService;
                loginService.LoginAsGuest(PlayerPrefs.GetString(LoginController.loginToken));
            }
        }

        void EntryMenuState()
        {
            loginController.Show();
        }

        void WorldMapState()
        {
            worldMapController.Show();
        }

        void NodeSpaceState()
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

        void OnPlayerEnterNodeSpace(PlayerEnteredNodeSpaceEvent e)
        {
            ChangeState(GameState.NodeSpace);
        }

        void OnPlayerOpenedWorldMap(PlayerOpenedWorldMap e)
        {
            ChangeState(GameState.WorldMap);
        }
    
        void OnPlayerLandOnStar(PlayerLandOnStarEvent e)
        {
            ChangeState(GameState.StarMenu);
        }

        void OnPlayerExitStarMenu(PlayerDepartFromStarEvent e)
        {
            ChangeState(GameState.NodeSpace);
        }

        void OnPlayerEnteredMarket(PlayerEnteredMarketEvent e)
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
               
        private void OnPlayerJumpedToNode(PlayerJumpedToNodeEvent e)
        {
            ChangeState(GameState.NodeSpace);
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