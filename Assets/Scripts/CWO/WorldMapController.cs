using System.Collections.Generic;
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
		private GameObject _nodePrefab;
		
		[SerializeField]
        private Transform _nodeSpawn;

		[SerializeField]
		private Camera _mainCamera;

		private NodeService _nodeService;

		private Dictionary<string, GameObject> _nodesPrefabsReference;
		
		private void Start()
		{
			_nodeService = application.serviceManager.get<NodeService>() as NodeService;
			_nodesPrefabsReference = new Dictionary<string, GameObject>();
			Hide ();
		}

		protected override void SubscribeToEvents(SubscribeEvent e)
        {
	        eventManager.AddListener<LoginSuccessfulEvent>(onLoginSuccessful);
	        eventManager.AddListener<PlayerOpenedWorldMap>(OnPlayerJump);
	        eventManager.AddListener<LogoutSuccessfulEvent>(OnLogoutSuccessful);
        }

		private void OnLogoutSuccessful(LogoutSuccessfulEvent e)
		{
			foreach (Transform child in _nodeSpawn)
			{
				Destroy(child.gameObject);
			}
		}

		private void OnPlayerJump(PlayerOpenedWorldMap e)
		{
			var playerNode = _nodeService.GetNodeByName(e.Player.currentNodeName);
			setCameraPosition(playerNode.coordX, playerNode.coordY);
			if (playerNode.ConnectedNodes != null)
			{
				foreach (string connectedNodeName in playerNode.ConnectedNodes)
				{
					DrawLineBetweenTwoNodes(_nodeService.GetNodeByName(connectedNodeName), playerNode);
				}
			}
		}

		private void setCameraPosition(float coordX, float coordY)
		{
			var newPos = new Vector3(coordX, coordY, _mainCamera.transform.position.z);
			_mainCamera.transform.position = newPos;
		}

		private void DrawLineBetweenTwoNodes(NodeModel connectedNode, NodeModel currentNode)
		{
			var connectedNodePrefab = _nodesPrefabsReference[connectedNode.name];
			var lineRenderer = connectedNodePrefab.GetComponent<LineRenderer>();
			lineRenderer.enabled = true;
			lineRenderer.SetPosition(0, new Vector3(connectedNode.coordX, connectedNode.coordY, connectedNodePrefab.transform.position.z));
			lineRenderer.SetPosition(1, new Vector3(currentNode.coordX, currentNode.coordY, connectedNodePrefab.transform.position.z));
			connectedNodePrefab.GetComponent<NodeController>().ReachableIndicator.enabled = true;
		}

		private void onLoginSuccessful(LoginSuccessfulEvent e)
		{
			foreach(var entry in e.NodesCoords)
			{
                var texture = UnityEngine.Resources.Load("Sprites/Nodes/" + entry.Value.Sprite) as Texture2D;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                var nodePosition = new Vector3(entry.Value.coordX, entry.Value.coordY, 0);
				var instantiatedPrefab = Instantiate(_nodePrefab, _nodeSpawn);
                instantiatedPrefab.GetComponent<SpriteRenderer>().sprite = sprite;
				var nodeController = instantiatedPrefab.GetComponent<NodeController>();
				nodeController.transform.position = nodePosition;
				nodeController.Node = entry.Value;
				_nodesPrefabsReference[entry.Value.name] = instantiatedPrefab;
			}
		}
	}
}