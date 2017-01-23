using System;
using System.Collections.Generic;
using Infrastructure.Core.Resource;
using Infrastructure.Base.Service;

namespace Infrastructure.Core.Star
{
    public class StarAdapter : Infrastructure.Base.Service.Contracts.IServiceProvider
    {
        protected Dictionary<int, StarModel> starsList;

        public StarAdapter(ServiceManager serviceManager)
        {
            starsList = new Dictionary<int, StarModel>();
            StarModel star = new StarModel();
            star.id = 1;
            star.coordX = -5f;
            star.coordY = 0f;
            star.name = "TestStar";
            star.resourceList.Add(Resource.Resources.Holmium, new ResourceSlotModel{ resouce = new ResourceModel{ id = 1, name = Resource.Resources.Holmium, image = "Holmium"}, amount = 50, buyPrice = 5, sellPrice = 4 });
            star.resourceList.Add(Resource.Resources.Cerium, new ResourceSlotModel{ resouce = new ResourceModel{ id = 1, name = Resource.Resources.Cerium, image = "Cerium"}, amount = 15, buyPrice = 50, sellPrice = 40 });
            star.resourceList.Add(Resource.Resources.Terbium, new ResourceSlotModel{ resouce = new ResourceModel{ id = 1, name = Resource.Resources.Terbium, image = "Terbium"}, amount = 25, buyPrice = 40, sellPrice = 30 });
            star.resourceList.Add(Resource.Resources.Europium, new ResourceSlotModel{ resouce = new ResourceModel{ id = 1, name = Resource.Resources.Europium, image = "Europium"}, amount = 10, buyPrice = 60, sellPrice = 50 });
            starsList.Add(star.id, star);

            StarModel star2 = new StarModel();
            star2.id = 2;
            star2.coordX = 5f;
            star2.coordY = 2f;
            star2.name = "TestStar2";
            star2.resourceList.Add(Resource.Resources.Holmium, new ResourceSlotModel{ resouce = new ResourceModel{ id = 1, name = Resource.Resources.Holmium, image = "Holmium"}, amount = 40, buyPrice = 15, sellPrice = 10 });
            star2.resourceList.Add(Resource.Resources.Cerium, new ResourceSlotModel{ resouce = new ResourceModel{ id = 1, name = Resource.Resources.Cerium, image = "Cerium"}, amount = 25, buyPrice = 30, sellPrice = 25 });
            star2.resourceList.Add(Resource.Resources.Terbium, new ResourceSlotModel{ resouce = new ResourceModel{ id = 1, name = Resource.Resources.Terbium, image = "Terbium"}, amount = 15, buyPrice = 50, sellPrice = 40 });
            star2.resourceList.Add(Resource.Resources.Europium, new ResourceSlotModel{ resouce = new ResourceModel{ id = 1, name = Resource.Resources.Europium, image = "Europium"}, amount = 5, buyPrice = 70, sellPrice = 50 });
            starsList.Add(star2.id, star2);

            StarModel star3 = new StarModel();
            star3.id = 3;
            star3.coordX = 0f;
            star3.coordY = 1f;
            star3.name = "TestStar3";
            starsList.Add(star3.id, star3);
        }


        public StarModel[] GetStarsList()
        {
            StarModel[] stars =  new StarModel[starsList.Count];
            starsList.Values.CopyTo(stars, 0);
            return stars;
        }

        public StarModel GetStarById(int id)
        {
            return starsList[id];
        }

    }
}

