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
        public NodeSpaceController NodeSpaceController;
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

        private void Awake()
        {
            application = App.getInstance();
            eventManager = application.eventManager;
            eventManager.AddListener<ApplicationFinishedLoadingEvent>(OnApplicationReady);
            eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);
            eventManager.AddListener<LogoutSuccessfulEvent>(OnLogoutSuccessful);
            eventManager.AddListener<PlayerEnteredNodeSpaceEvent>(OnPlayerEnterNodeSpace);
            eventManager.AddListener<PlayerOpenedWorldMap>(OnPlayerOpenedWorldMap);
            eventManager.AddListener<PlayerLandOnStarEvent>(OnPlayerLandOnStar);
            eventManager.AddListener<PlayerDepartFromStarEvent>(OnPlayerExitStarMenu);
            eventManager.AddListener<PlayerEnteredMarketEvent>(OnPlayerEnteredMarket);
            eventManager.AddListener<PlayerExitMarketEvent>(OnPlayerExitMarket);
            eventManager.AddListener<PlayerEnteredLoungeEvent>(OnPlayerEnteredLounge);
            eventManager.AddListener<PlayerLeftLoungeEvent>(OnPlayerLeftLounge);
            eventManager.AddListener<PlayerJumpedToNodeEvent>(OnPlayerJumpedToNode);
        }

        private void OnApplicationReady(ApplicationFinishedLoadingEvent e) 
        {
            if ( PlayerPrefs.HasKey(LoginController.guestLoginToken) )
            {
                Debug.Log("Login Token Found");
                var loginService =  application.serviceManager.get<LoginService>() as LoginService;
                loginService.LoginAsGuest(PlayerPrefs.GetString(LoginController.guestLoginToken));
            }
            else
            {
                ChangeState(GameState.EntryMenu);
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
            NodeSpaceController.Show();
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
            if (e.Player.isLanded)
            {
                ChangeState(GameState.StarMenu);
            }
            else
            {
                ChangeState(GameState.NodeSpace);
            } 
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
            NodeSpaceController.Hide();
            starMenuController.Hide();
            marketMenuController.Hide();
            loungeController.Hide();
        }
    }
}