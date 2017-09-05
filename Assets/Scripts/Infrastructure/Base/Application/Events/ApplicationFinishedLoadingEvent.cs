using Infrastructure.Base.Application.Contracts;

namespace Infrastructure.Base.Application.Events
{
    public class ApplicationFinishedLoadingEvent : Event.Event
    {
        protected IApplication app;

        public ApplicationFinishedLoadingEvent(IApplication app)
        {
            this.app = app;
        }
    }
}

