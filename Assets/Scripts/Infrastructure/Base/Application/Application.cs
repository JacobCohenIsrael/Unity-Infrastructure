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
        private static Application localInstance;

        public static Application getInstance()
        {
            if (localInstance == null)
            {
                localInstance = new Application();
                localInstance.serviceManager = new ServiceManager();
                localInstance.eventManager = new EventManager();
            }
            return localInstance;
        }

        public void run()
        {
            ApplicationStartedEvent e = new ApplicationStartedEvent(this);
            eventManager.DispatchEvent(e);
        }
    }
}

