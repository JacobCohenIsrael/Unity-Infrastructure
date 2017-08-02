using UnityEngine;
using System.Collections;
using Infrastructure.Base.Service;
using Infrastructure.Base.Event;
using Infrastructure.Base.Config;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Core.Network;
using UnitySocketIO.Events;
using UnitySocketIO;

namespace CWO
{
    public class Setup : MonoBehaviour
    {
        protected App application;
        protected MainServer mainServer;

        public SocketIOController socketIO;

        void Awake()
        {
            Debug.Log("Awaking Setup");
            Config config = new Config();
            application = App.getInstance();
            application.serviceManager.setConfig(config);
            UnityEngine.Application.runInBackground = true;
            mainServer = application.serviceManager.get<MainServer>() as MainServer;
            mainServer.SetSocketIO(socketIO);
        }

        void Start()
        {
            mainServer.Connect();
            mainServer.On("connect", this.OnConnect);
        }

        void OnConnect(SocketIOEvent e)
        {
            Debug.Log("Running Application");
            application.run();
        }
    }
}

