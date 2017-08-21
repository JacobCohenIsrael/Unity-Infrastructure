using CWO.Node;
using CWO.Star;
using UnityEngine;
using Implementation.Views.Screen;
using Infrastructure.Base.Application.Events;
using Infrastructure.Base.Service;
using Infrastructure.Core.Login.Events;
using Infrastructure.Core.Node;
using Infrastructure.Core.Player.Events;


namespace CWO
{
	public class WorldMapController : BaseUIObject 
	{
		[SerializeField]
		private GameObject _starPrefab;
		
		[SerializeField]
		private GameObject _nodePrefab;
		
		[SerializeField]
        private Transform _nodeSpawn;

		[SerializeField]
		private Camera _mainCamera;

		private NodeService _nodeService;
		
		private void Start()
		{
			_nodeService = application.serviceManager.get<NodeService>() as NodeService;
			Hide ();
		}

		protected override void SubscribeToEvents(SubscribeEvent e)
        {
	        eventManager.AddListener<LoginSuccessfulEvent>(onLoginSuccessful);
	        eventManager.AddListener<PlayerJumpEvent>(onPlayerJump);
        }

		private void onPlayerJump(PlayerJumpEvent e)
		{
			var coordX = _nodeService.GetNodeByName(e.player.currentNodeName).coordX;
			var coordY = _nodeService.GetNodeByName(e.player.currentNodeName).coordY;
			var newPos = new Vector3(coordX, coordY, _mainCamera.transform.position.z);
			_mainCamera.transform.position = newPos;
		}

		private void onLoginSuccessful(LoginSuccessfulEvent e)
		{
			foreach(var entry in e.NodesCoords)
			{
				var nodePosition = new Vector3(entry.Value.coordX, entry.Value.coordY, 0);
				var instantiatedPrefab = Instantiate(entry.Value.HasStar() ? _starPrefab : _nodePrefab, _nodeSpawn);
				var nodeController = instantiatedPrefab.GetComponent<NodeController>();
				nodeController.transform.position = nodePosition;
				nodeController.Node = entry.Value;
			}
		}
	}
}