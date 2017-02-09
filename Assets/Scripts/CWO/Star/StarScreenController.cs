using UnityEngine;
using Infrastructure.Core.Login.Events;
using Implementation.Views.Screen;
using UnityEngine.UI;
using Infrastructure.Core.Login;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Player;
using Infrastructure.Base.Application.Events;

namespace CWO.Star
{
    public class StarScreenController : BaseUIObject {

        public Button jumpButton;
        public Button landButton;

        protected PlayerService playerService;

        void Start()
        {
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
            Hide();
        }

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
            eventManager.AddListener<PlayerOrbitStarEvent>(this.OnPlayerEnterStar);
            jumpButton.onClick.AddListener(() => { this.OnJump(); });
            landButton.onClick.AddListener(() => { this.OnLand(); });
        }

        void OnPlayerEnterStar(PlayerOrbitStarEvent e)
        {
//            Debug.Log("Player entered star, showing star screen");
//            Debug.Log("Welcome to " + e.star.name + " star");
        }

        void OnJump()
        {
            if (PlayerPrefs.HasKey("playerId"))
            {
                int playerId = PlayerPrefs.GetInt("PlayerId");
                PlayerModel player = playerService.getPlayerById(playerId);
                if (null == player.getActiveShip())
                {
                    throw new UnityException("Player must have a ship to jump");
                }
                if (player.getActiveShip().currentHullAmount > 0)
                {
                    PlayerJumpEvent playerJumpEvent = new PlayerJumpEvent(player);
                    application.eventManager.DispatchEvent<PlayerJumpEvent>(playerJumpEvent);
                }
                else
                {
                    throw new UnityException("Player ship must be repaired");
                }
            }
        }

        void OnLand()
        {
            if (PlayerPrefs.HasKey("playerId"))
            {
                int playerId = PlayerPrefs.GetInt("PlayerId");
                playerService.landPlayerOnStar(playerService.getPlayerById(playerId));
            }
        }


    } 
}