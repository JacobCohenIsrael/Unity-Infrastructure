using Infrastructure.Base.Application.Events;

namespace Implementation
{
    public abstract class HierarchyMonoBehaviour : BaseMonoBehaviour
    {
        protected bool _hasAwaken;
        private void Awake()
        {
            if (!_hasAwaken)
            {
                application.eventManager.AddListener<SubscribeEvent>(SubscribeToEvents);
                _hasAwaken = true;
            }

        }
        protected abstract void SubscribeToEvents(SubscribeEvent e);
    }
}

