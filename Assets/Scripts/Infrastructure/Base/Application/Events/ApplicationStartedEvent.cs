using System.Collections;
using Infrastructure.Base.Application.Contracts;
using Infrastructure.Base.Event;

namespace Infrastructure.Base.Application.Events
{
    public class ApplicationStartedEvent : Infrastructure.Base.Event.Event
    {
        protected IApplication app;

        public ApplicationStartedEvent(IApplication app)
        {
            this.app = app;
        }
    }
}

