using UnityEngine;
using System.Collections;
using Infrastructure.Base.Service;
using Infrastructure.Base.Event;
using Infrastructure.Base.Config;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Core.Network;

namespace CWO
{
    public class Setup : MonoBehaviour
    {
        protected App application;

        void Awake()
        {
            Debug.Log("Awaking Setup");
            Config config = new Config();
            config.set("servers.main", "ws://127.0.0.1:4567/socket.io/?EIO=4&transport=websocket");
            application = App.getInstance();
            application.serviceManager.setConfig(config);
        }

        void Start()
        {
            Debug.Log("Running Application");
            NetworkService ns = application.serviceManager.get<NetworkService>() as NetworkService;
            ns.Connect();
            application.run();
        }
    }
}

