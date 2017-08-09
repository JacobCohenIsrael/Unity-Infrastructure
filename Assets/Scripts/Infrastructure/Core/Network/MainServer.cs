using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using Infrastructure.Base.Event;
using Infrastructure.Core.Network.Events;
using UnitySocketIO.Events;

namespace Infrastructure.Core.Network
{
    public class MainServer : IServiceProvider
    {
        public ISocketIO socketIO;

        public MainServer(ServiceManager serviceManager)
        {
        }

        public void SetSocketIO(ISocketIO socketIO)
        {
            this.socketIO = socketIO;
        }

        public void Connect()
        {
            socketIO.Connect();
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
            return socketIO.isConnected();
        }
    }
}

