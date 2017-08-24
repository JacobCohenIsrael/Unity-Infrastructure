using Infrastructure.Base.Application.Events;
using App = Infrastructure.Base.Application.Application;

namespace Implementation
{
    public abstract class HierarchyMonoBehaviour : BaseMonoBehaviour
    {
        private void Awake()
        {
            application.eventManager.AddListener<SubscribeEvent>(SubscribeToEvents);
        }

        protected abstract void SubscribeToEvents(SubscribeEvent e);
    }
}

