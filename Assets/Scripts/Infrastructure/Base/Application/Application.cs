using Infrastructure.Base.Application.Contracts;
using Infrastructure.Base.Service;
using Infrastructure.Base.Event;
using Infrastructure.Base.Application.Events;
using Infrastructure.Base.Config;


namespace Infrastructure.Base.Application
{
    public class Application : IApplication
    {
        public ServiceManager serviceManager;
        public EventManager eventManager;
        private static Application localInstance;

        public static Application getInstance()
        {
            if (localInstance == null)
            {
                localInstance = new Application();
                localInstance.serviceManager = new ServiceManager();
                localInstance.eventManager = new EventManager(localInstance.serviceManager);
                localInstance.serviceManager.set<EventManager>(localInstance.eventManager);
            }
            return localInstance;
        }

        public void run()
        {
            eventManager.DispatchEvent(new ApplicationStartedEvent(this));
            eventManager.DispatchEvent(new SetAppConfigEvent());
            eventManager.DispatchEvent(new SubscribeEvent());
            eventManager.DispatchEvent(new ApplicationFinishedLoadingEvent(this));
        }
    }
}

