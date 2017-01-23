using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Implementation.Views.Screen;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player;
using Infrastructure.Core.Player.Events;

namespace CWO.Star
{
	public class StarController : BaseUIObject
	{
		public Infrastructure.Core.Star.StarModel star;

        public PlayerController playerController;

        protected PlayerService playerService;
	
		void Start()
		{
			application.eventManager.AddListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent("PlayerController") as PlayerController;
		}

        void Update()
        {
            if (playerController.player.currentNodeId == star.id)
            {
                ((Behaviour)gameObject.GetComponent("Halo")).enabled = true;
            }
            else
            {
                ((Behaviour)gameObject.GetComponent("Halo")).enabled = false;
            }
        }

		void OnMouseUp()
		{
            if (PlayerPrefs.HasKey("playerId"))
            {
                int playerId = PlayerPrefs.GetInt("PlayerId");
                PlayerModel player = playerService.getPlayerById(playerId);
                if (star.id == player.currentNodeId)
                {
                    playerService.OrbitPlayerOnStar(player, star);
                }
                else
                {
                    playerService.jumpPlayerToStar(player, star);
                }
            }
		}

		void OnLogoutSuccessful(LogoutSuccessfulEvent e)
		{
			Debug.Log("Logout Successful, Destroying Star");
            application.eventManager.RemoveListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
			Destroy (gameObject);
		}
	}
}

