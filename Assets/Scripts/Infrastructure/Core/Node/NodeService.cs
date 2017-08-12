using Infrastructure.Base.Service;
using Infrastructure.Core.Network;
using Infrastructure.Base.Event;
using UnitySocketIO.Events;
using Infrastructure.Core.Player.Events;
using Newtonsoft.Json;
using System;

namespace Infrastructure.Core.Node
{
    public class NodeService : Base.Service.Contracts.IServiceProvider
    {
        MainServer mainServer;
        EventManager eventManager;

        public NodeService(ServiceManager serviceManager)
		{
            eventManager = serviceManager.get<EventManager>() as EventManager;
            mainServer = serviceManager.get<MainServer>() as MainServer;
            subscribeListeners();
        }

        protected void subscribeListeners()
        {
            mainServer.On("shipEnteredNode", this.OnShipEnteredNode);
            mainServer.On("shipLeftNode", this.OnShipLeftNode);
        }

        private void OnShipLeftNode(SocketIOEvent e)
        {
            ShipLeftNodeEvent slne = JsonConvert.DeserializeObject<ShipLeftNodeEvent>(e.data);
            eventManager.DispatchEvent<ShipLeftNodeEvent>(slne);
        }

        private void OnShipEnteredNode(SocketIOEvent e)
        {
            ShipEnteredNodeEvent sene = JsonConvert.DeserializeObject<ShipEnteredNodeEvent>(e.data);
            eventManager.DispatchEvent<ShipEnteredNodeEvent>(sene);
        }
    }
}