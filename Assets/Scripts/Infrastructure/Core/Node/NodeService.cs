using Infrastructure.Base.Service;
using Infrastructure.Core.Network;
using Infrastructure.Base.Event;
using UnitySocketIO.Events;
using Infrastructure.Core.Player.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Infrastructure.Core.Login.Events;
using UnityEngine;

namespace Infrastructure.Core.Node
{
    public class NodeService : Base.Service.Contracts.IServiceProvider
    {
        private readonly MainServer _mainServer;
        private readonly EventManager _eventManager;
        private Dictionary<string, NodeModel> _nodesCoords;
        public NodeService(ServiceManager serviceManager)
		{
            _eventManager = serviceManager.get<EventManager>() as EventManager;
            _mainServer = serviceManager.get<MainServer>() as MainServer;
            subscribeListeners();
        }

        protected void subscribeListeners()
        {
            _mainServer.On("shipEnteredNode", OnShipEnteredNode);
            _mainServer.On("shipLeftNode", OnShipLeftNode);
            _eventManager.AddListener<LoginSuccessfulEvent>(OnLoginSuccessful);
        }

        private void OnLoginSuccessful(LoginSuccessfulEvent e)
        {
            _nodesCoords = e.WorldMap;
        }

        private void OnShipLeftNode(SocketIOEvent e)
        {
            var slne = JsonConvert.DeserializeObject<ShipLeftNodeEvent>(e.data);
            _eventManager.DispatchEvent(slne);
        }

        private void OnShipEnteredNode(SocketIOEvent e)
        {
            var sene = JsonConvert.DeserializeObject<ShipEnteredNodeEvent>(e.data);
            _eventManager.DispatchEvent(sene);
        }

        public NodeModel GetNodeByName(string name)
        {
            return _nodesCoords[name];
        }
        
        public float CalculateDistanceBetweenNodes(NodeModel from, NodeModel to)
        {
            return Mathf.Round(Mathf.Sqrt(Mathf.Pow(Mathf.Abs(from.coordX - to.coordX), 2) + Mathf.Pow(Mathf.Abs(from.coordY - to.coordY), 2) ));
        }
    }
}