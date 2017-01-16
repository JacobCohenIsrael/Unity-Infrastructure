using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using UnityEngine;

namespace Infrastructure.Core.Star
{
    public class StarService : IServiceProvider
	{
		StarModel[] starsList;

		public StarService(ServiceManager serviceManager)
		{
			StarModel star = new StarModel ();
			star.id = 1;
			star.coordX = -5f;
			star.coordY = 0f;
			star.name = "TestStar";

			StarModel star2 = new StarModel ();
			star2.id = 2;
			star2.coordX = 5f;
			star2.coordY = 2f;
			star2.name = "TestStar2";

            StarModel star3 = new StarModel ();
            star3.id = 3;
            star3.coordX = 0f;
            star3.coordY = 1f;
            star3.name = "TestStar3";


			starsList = new StarModel[3] { star, star2, star3 };
		}

		public StarModel[] getStarsList()
		{
			return starsList;
		}

        public StarModel getStarById(int id)
        {
            return starsList[id-1];
        }

        public float calculateDistanceBetweenStars(StarModel a, StarModel b)
        {
            return (Mathf.Sqrt(Mathf.Pow(Mathf.Abs(a.coordX - b.coordX), 2) + Mathf.Pow(Mathf.Abs(a.coordY - b.coordY), 2) ));
        }
	}
}