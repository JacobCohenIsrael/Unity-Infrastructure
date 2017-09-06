using Infrastructure.Base.Application.Contracts;

namespace Infrastructure.Base.Application.Events
{
	public class ApplicationQuitEvent : Event.Event
	{
		protected IApplication App;
		
		public ApplicationQuitEvent(IApplication app)
		{
			App = app;
		}
	}
}