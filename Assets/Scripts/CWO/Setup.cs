using Implementation;
using UnityEngine;
using Infrastructure.Base.Config;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Core.Network;
using UnitySocketIO.Events;
using UnitySocketIO;

namespace CWO
{
    public class Setup : BaseMonoBehaviour
    {
        protected MainServer mainServer;

        public SocketIOController socketIO;

        private void Awake()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Debug.Log("Awaking Setup");
            var config = new Config();
            application.serviceManager.setConfig(config);
            Application.runInBackground = true;
            socketIO.Init();
            mainServer = application.serviceManager.get<MainServer>() as MainServer;
            mainServer.SetSocketIO(socketIO);
        }

        private void Start()
        {
            mainServer.Connect();
            mainServer.On("connect", OnConnect);
            mainServer.On("disconnect", OnDisconnect);
        }

        private void OnDisconnect(SocketIOEvent e)
        {
            Application.Quit();
        }

        void OnConnect(SocketIOEvent e)
        {
            Debug.Log("Running Application");
            application.run();
        }
    }
}

