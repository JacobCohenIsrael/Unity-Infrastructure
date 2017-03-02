using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Implementation.Views.Screen;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Player;
using Infrastructure.Core.Player.Events;
using Infrastructure.Base.Application.Events;

namespace CWO.Star
{
	public class StarController : BaseUIObject
	{
		public Infrastructure.Core.Star.StarModel star;

        public PlayerController playerController;

        protected PlayerService playerService;
	
        void Awake()
        {
            eventManager.AddListener<LogoutSuccessfulEvent>(this.OnLogoutSuccessful);
        }

		void Start()
		{
            playerService = application.serviceManager.get<PlayerService>() as PlayerService;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent("PlayerController") as PlayerController;
		}

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
        }

        void Update()
        {
            if (playerController.player.currentNodeName == star.name)
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
            PlayerModel player = playerController.player;
            if (star.name == player.currentNodeName)
            {
                playerService.OrbitPlayerOnStar(player, star);
            }
            else
            {
                playerService.jumpPlayerToStar(player, star);
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

