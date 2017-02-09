using UnityEngine;
using System.Collections;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Base.Event;
using Infrastructure.Base.Application.Events;

namespace Implementation.Views.Screen
{
    public abstract class BaseUIObject : MonoBehaviour
    {
        protected App application = App.getInstance();

        protected EventManager eventManager = App.getInstance().eventManager;

        void Awake()
        {
            application.eventManager.AddListener<SubscribeEvent>(this.SubscribeToEvents);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        protected abstract void SubscribeToEvents(SubscribeEvent e);
    }
}
