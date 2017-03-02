//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using SocketIO;
//
//namespace CWO
//{
//    public class NetworkManager : MonoBehaviour {
//    	
//        public SocketIOComponent socketio;
//    	void Start () 
//        {
//            socketio.On("connect", OnConnect);
//    	}
//    	
//    	// Update is called once per frame
//    	void Update () 
//        {
//    		
//    	}
//
//        void OnConnect(SocketIOEvent e)
//        {
//            socketio.Emit("shoot");
//        }
//    }
//}