using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using SocketIO;
using System.Threading;
using Infrastructure.Base.Event;
using Infrastructure.Core.Network.Events;

namespace Infrastructure.Core.Network
{
    public class NetworkService : IServiceProvider
    {
        SocketIOComponent socketIO;
        protected Thread socketThread;
        EventManager eventManager;

        public NetworkService(ServiceManager serviceManager)
        {
            socketIO = new SocketIOComponent(serviceManager.getConfig().get("servers.main") as string);
            eventManager = serviceManager.get<EventManager>() as EventManager;
        }

        public void Connect()
        {
            socketIO.Connect();
            socketThread = new Thread(socketIO.Update);
            socketThread.Start();
        }

        public void Emit(string eventName, JSONObject json)
        {
            socketIO.Emit(eventName, json);
        }

        public void On(string eventName, System.Action<SocketIOEvent> callback)
        {
            socketIO.On(eventName, callback);
        }
    }
}

