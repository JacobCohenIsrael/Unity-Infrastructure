using UnityEngine;
using System.Collections;
using Infrastructure.Base.Service;
using Infrastructure.Base.Event;

namespace CWO
{
    public class Setup : MonoBehaviour
    {
        Infrastructure.Base.Application.Application application;
        ServiceManager serviceManager;
        EventManager eventManager;

        void Awake()
        {

        }

        void Start()
        {
            Debug.Log("Running Application");
            Infrastructure.Base.Application.Application.getInstance().run();
        }
    }
}

