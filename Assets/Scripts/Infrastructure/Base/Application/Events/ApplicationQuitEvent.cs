using Infrastructure.Base.Application.Contracts;

namespace Infrastructure.Base.Application.Events
{
	public class ApplicationQuitEvent : Event.Event
	{
		protected IApplication app;
		
		public ApplicationQuitEvent(IApplication app)
		{
			this.app = app;
		}
	}
}