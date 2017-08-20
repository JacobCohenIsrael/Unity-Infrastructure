using CWO.Node;
using CWO.Star;
using UnityEngine;
using Implementation.Views.Screen;
using Infrastructure.Base.Application.Events;
using Infrastructure.Core.Login.Events;


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

		private void Start() 
		{
			Hide ();
		}

        protected override void SubscribeToEvents(SubscribeEvent e)
        {
	        eventManager.AddListener<LoginSuccessfulEvent>(this.onLoginSuccessful);
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