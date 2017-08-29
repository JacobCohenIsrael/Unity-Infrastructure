using UnityEngine;
using System;
using UnitySocketIO.SocketIO;
using UnitySocketIO.Events;
using Infrastructure.Core.Network;

namespace UnitySocketIO {
    public class SocketIOController : MonoBehaviour, ISocketIO {
        
        public SocketIOSettings settings;

        BaseSocketIO socketIO;

        public string SocketID { get { return socketIO.SocketID; } }

        public void Init() {
            if(Application.platform == RuntimePlatform.WebGLPlayer) {
                Debug.Log("Using WebGL SocketIO");
                socketIO = gameObject.AddComponent<WebGLSocketIO>();
            }
            else {
                Debug.Log("Using Native SocketIO");
                socketIO = gameObject.AddComponent<NativeSocketIO>();
            }
            
            socketIO.Init(settings);
        }

        public void Connect() {
            socketIO.Connect();
        }

        public void Close() {
            socketIO.Close();
        }

        public void Emit(string e) {
            socketIO.Emit(e);
        }
        public void Emit(string e, Action<string> action) {
            socketIO.Emit(e, action);
        }
        public void Emit(string e, string data) {
            socketIO.Emit(e, data);
        }
        public void Emit(string e, string data, Action<string> action) {
            socketIO.Emit(e, data, action);
        }

        public void On(string e, Action<SocketIOEvent> callback) {
            socketIO.On(e, callback);
        }
        public void Off(string e, Action<SocketIOEvent> callback) {
            socketIO.Off(e, callback);
        }

        public void Emit(string eventName, JSONObject json)
        {
            socketIO.Emit(eventName, json.ToString());
        }

        public bool isConnected()
        {
            return socketIO.isActiveAndEnabled;
        }
    }
}