using UnityEngine;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Base.Event;
using Infrastructure.Base.Application.Events;

namespace Implementation
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        protected App application = App.getInstance();

        protected EventManager eventManager = App.getInstance().eventManager;

        void Awake()
        {
            application.eventManager.AddListener<SubscribeEvent>(this.SubscribeToEvents);
        }

        protected abstract void SubscribeToEvents(SubscribeEvent e);
    }
}
