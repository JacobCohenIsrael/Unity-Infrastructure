using System.Collections;
using Infrastructure.Base.Application.Contracts;
using Infrastructure.Base.Event;

namespace Infrastructure.Base.Application.Events
{
    public class ApplicationFinishedLoadingEvent
    {
        protected IApplication app;

        public ApplicationFinishedLoadingEvent(IApplication app)
        {
            this.app = app;
        }
    }
}

