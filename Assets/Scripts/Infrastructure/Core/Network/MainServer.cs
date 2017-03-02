using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using SocketIO;
using System.Threading;
using Infrastructure.Base.Event;
using Infrastructure.Core.Network.Events;

namespace Infrastructure.Core.Network
{
    public class MainServer : IServiceProvider
    {
        public SocketIOComponent socketIO;

        public MainServer(ServiceManager serviceManager)
        {
            socketIO = new SocketIOComponent(serviceManager.getConfig().get("servers.main") as string);
        }

        public void Connect()
        {
            socketIO.Connect();
        }

        public void Update()
        {
            socketIO.Update();
        }

        public void Emit(string eventName, JSONObject json)
        {
            socketIO.Emit(eventName, json);
        }

        public void On(string eventName, System.Action<SocketIOEvent> callback)
        {
            socketIO.On(eventName, callback);
        }

        public bool isConnected()
        {
            return socketIO.IsConnected;
        }
    }
}

