using UnitySocketIO.Events;

namespace Infrastructure.Core.Network
{
    public interface ISocketIO
    {
        void Connect();

        void Emit(string eventName, JSONObject json);

        void On(string eventName, System.Action<SocketIOEvent> callback);

        bool isConnected();

        void Close();
    }
}