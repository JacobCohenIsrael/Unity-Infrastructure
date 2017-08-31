using Infrastructure.Base.Application.Events;
using App = Infrastructure.Base.Application.Application;

namespace Implementation
{
    public abstract class HierarchyMonoBehaviour : BaseMonoBehaviour
    {
        protected bool _hasAwaken;
        private void Awake()
        {
            application.eventManager.AddListener<SubscribeEvent>(SubscribeToEvents);
            _hasAwaken = true;
        }

        protected abstract void SubscribeToEvents(SubscribeEvent e);
    }
}

