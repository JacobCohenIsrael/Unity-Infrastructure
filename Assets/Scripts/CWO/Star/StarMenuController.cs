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
        public Button marketButton;

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
        }

        protected void OnDepart()
        {
            if (PlayerPrefs.HasKey("playerId"))
            {
                int playerId = PlayerPrefs.GetInt("PlayerId");
                PlayerModel player = playerService.getPlayerById(playerId);
                if (null == player.getActiveShip())
                {
                    throw new UnityException("Player must have an active ship");
                }
                playerService.departPlayerFromStar(player);
            }
        }

        protected void OpenMarket()
        {
            if (PlayerPrefs.HasKey("playerId"))
            {
                int playerId = PlayerPrefs.GetInt("PlayerId");
                PlayerModel player = playerService.getPlayerById(playerId);
                playerService.openMarket(player);
            }
        }

        protected void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            StarService starService = application.serviceManager.get<StarService>() as StarService;
            StarModel currentStar = starService.GetStarById(e.player.currentNodeId);
            welcomeText.text = "Welcome to " + currentStar.name;
        }

        protected void OnPlayerLandOnStar(PlayerLandOnStarEvent e)
        {
            StarModel star = starService.GetStarById(e.player.currentNodeId);
            welcomeText.text = "Welcome to " + star.name;
            if (e.player.homePlanetId == star.id)
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

