using Infrastructure.Base.Application.Contracts;
using Infrastructure.Base.Service;
using Infrastructure.Base.Event;
using Infrastructure.Base.Application.Events;


namespace Infrastructure.Base.Application
{
    public class Application : IApplication
    {
        public ServiceManager serviceManager;
        public EventManager eventManager;
        private static Application _localInstance;
        public bool HasStarted;

        public static Application getInstance()
        {
            if (_localInstance == null)
            {
                _localInstance = new Application();
                _localInstance.serviceManager = new ServiceManager();
                _localInstance.eventManager = new EventManager(_localInstance.serviceManager);
                _localInstance.serviceManager.set(_localInstance.eventManager);
            }
            return _localInstance;
        }

        public void run()
        {
            if (!HasStarted)
            {
                eventManager.DispatchEvent(new ApplicationStartedEvent(this));
                eventManager.DispatchEvent(new SetAppConfigEvent());
                eventManager.DispatchEvent(new SubscribeEvent());
            }
            eventManager.DispatchEvent(new ApplicationFinishedLoadingEvent(this));
            HasStarted = true;
        }

        public void Quit()
        {
            eventManager.DispatchEvent(new ApplicationQuitEvent(this));
            HasStarted = false;
        }
    }
}

