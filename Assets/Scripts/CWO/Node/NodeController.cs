using Implementation;
using Implementation.Views.Screen;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Player;
using Infrastructure.Core.Player.Events;
using UnityEngine;

namespace CWO.Node
{
	public class NodeController : InstantiatedMonoBehaviour
	{
		public Infrastructure.Core.Node.NodeModel Node;
		
		public SpriteRenderer ReachableIndicator;
		
		private PlayerController _playerController;
		private PlayerService _playerService;
		
		[SerializeField]
		private Behaviour _halo;

		void Update () {
			// Code for OnMouseDown in the iPhone. Unquote to test.
			RaycastHit hit = new RaycastHit();
			for (int i = 0; i < Input.touchCount; ++i)
				if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
					// Construct a ray from the current touch coordinates
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
					if (Physics.Raycast(ray, out hit))
						hit.transform.gameObject.SendMessage("OnMouseUp");
				}
		}

		private void Start()
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");	
			_playerController = player.GetComponent<PlayerController>();
			_playerService = application.serviceManager.get<PlayerService>() as PlayerService;
			eventManager.AddListener<PlayerJumpedToNodeEvent>(OnPlayerJumpedToNode);
			_halo.enabled = _playerController.player.currentNodeName == Node.name;
		}

		private void OnPlayerJumpedToNode(PlayerJumpedToNodeEvent e)
		{
			_halo.enabled = Node.name == e.node.name;
			GetComponent<LineRenderer>().enabled = false;
			ReachableIndicator.enabled = false;
		}

		private void OnMouseUp()
		{
			var player = _playerController.player;
			if (Node.name == player.currentNodeName)
			{
				_playerService.PlayerClosedWorldMap(player, Node);
			}
			else
			{
				_playerService.JumpPlayerToNode(player, Node);
			}
		}
	}
}

