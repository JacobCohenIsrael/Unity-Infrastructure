using System.Collections;
using Implementation;
using UnityEngine;
using Infrastructure.Base.Config;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Core.Network;
using Infrastructure.Core.Notification.Events;
using UnityEditor;
using UnitySocketIO.Events;
using UnitySocketIO;

namespace CWO
{
    public class Setup : BaseMonoBehaviour
    {
        protected MainServer mainServer;

        public SocketIOController socketIO;

        private bool _isConnected;

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

        private void Update()
        {
            if (!application.HasStarted || _isConnected) return;
            StartCoroutine(QuitApplication(5));
            application.Quit();
        }
        
        private void Start()
        {
            mainServer.Connect();
            mainServer.On("connect", OnConnect);
            mainServer.On("close", OnDisconnect);
        }

        private void OnDisconnect(SocketIOEvent e)
        {
            _isConnected = false;
        }

        void OnConnect(SocketIOEvent e)
        {
            Debug.Log("Running Application");
            application.run();
            _isConnected = true;
        }

        private IEnumerator QuitApplication(float time)
        {            
            eventManager.DispatchEvent(new NotificationEvent{ NotificationText = "Connection Lost, closing application in " + time + " seconds", NotificationLength = time});
            yield return new WaitForSeconds(time);
            Debug.Log("Closing Application");
            Application.Quit();
        }
    }
}

