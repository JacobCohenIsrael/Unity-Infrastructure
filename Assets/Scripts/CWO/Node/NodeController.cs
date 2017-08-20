using Implementation.Views.Screen;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Player;
using UnityEngine;

namespace CWO.Node
{
	public class NodeController : BaseUIObject
	{
		public PlayerController PlayerController;
		public Infrastructure.Core.Node.NodeModel Node;
		private PlayerService _playerService;

		private void Start()
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");	
			PlayerController = player.GetComponent<PlayerController>();
			_playerService = application.serviceManager.get<PlayerService>() as PlayerService;
		}
		
        protected override void SubscribeToEvents(SubscribeEvent e)
        {
	        
        }

		private void Update()
		{
			((Behaviour)gameObject.GetComponent("Halo")).enabled = PlayerController.player.currentNodeName == Node.name;
		}

		private void OnMouseUp()
		{
			var player = PlayerController.player;
			if (Node.name == player.currentNodeName)
			{
				_playerService.OrbitPlayerOnStar(player, Node);
			}
			else
			{
				_playerService.JumpPlayerToNode(player, Node);
			}
		}
	}
}

