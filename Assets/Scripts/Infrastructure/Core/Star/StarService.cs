using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;

namespace Infrastructure.Core.Star
{
    public class StarService : IServiceProvider
	{
        private StarAdapter _starAdapter;

		public StarService(ServiceManager serviceManager)
		{
            _starAdapter = serviceManager.get<StarAdapter>() as StarAdapter;
		}
	}
}