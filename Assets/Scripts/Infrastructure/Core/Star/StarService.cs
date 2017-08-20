using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;

namespace Infrastructure.Core.Star
{
    public class StarService : IServiceProvider
	{
        StarAdapter starAdapter;

		public StarService(ServiceManager serviceManager)
		{
            starAdapter = serviceManager.get<StarAdapter>() as StarAdapter;
		}
	}
}