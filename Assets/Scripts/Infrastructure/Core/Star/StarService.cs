using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using UnityEngine;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Star
{
    public class StarService : IServiceProvider
	{
        StarAdapter starAdapter;

		public StarService(ServiceManager serviceManager)
		{
            starAdapter = serviceManager.get<StarAdapter>() as StarAdapter;
		}

		public StarModel[] GetStarsList()
		{
            return starAdapter.GetStarsList();
		}

        public StarModel GetStarByName(string name)
        {
            return starAdapter.GetStarByName(name);
        }

        public float CalculateDistanceBetweenStars(StarModel a, StarModel b)
        {
            return Mathf.Round((Mathf.Sqrt(Mathf.Pow(Mathf.Abs(a.coordX - b.coordX), 2) + Mathf.Pow(Mathf.Abs(a.coordY - b.coordY), 2) )));
        }
	}
}