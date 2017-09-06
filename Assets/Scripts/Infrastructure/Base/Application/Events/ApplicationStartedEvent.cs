using Infrastructure.Base.Application.Contracts;

namespace Infrastructure.Base.Application.Events
{
    public class ApplicationStartedEvent : Event.Event
    {
        protected IApplication App;

        public ApplicationStartedEvent(IApplication app)
        {
            App = app;
        }
    }
}

