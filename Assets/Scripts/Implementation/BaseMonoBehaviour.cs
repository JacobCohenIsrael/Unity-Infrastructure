using UnityEngine;
using App = Infrastructure.Base.Application.Application;
using Infrastructure.Base.Event;

namespace Implementation
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        protected App application = App.getInstance();

        protected EventManager eventManager = App.getInstance().eventManager;
    }
}