using System;
using System.Collections.Generic;
using Infrastructure.Core.Resource;
using Infrastructure.Base.Service;
using UnityEngine;
using Infrastructure.Core.Network;
using SocketIO;
using Infrastructure.Core.Star.Events;
using Infrastructure.Base.Event;
using Newtonsoft.Json;

namespace Infrastructure.Core.Star
{
    public class StarAdapter : Infrastructure.Base.Service.Contracts.IServiceProvider
    {
        protected Dictionary<string, StarModel> starsList;
        protected MainServer mainServer;
        protected EventManager eventManager;

        public StarAdapter(ServiceManager serviceManager)
        {
            mainServer = serviceManager.get<MainServer>() as MainServer;
            eventManager = serviceManager.get<EventManager>() as EventManager;
            starsList = new Dictionary<string, StarModel>();
            mainServer.On("updateResourceAmount", this.OnUpdateResourceAmount);
            mainServer.On("updateStarsList", this.OnUpdateStarsList);
            mainServer.On("loginResponse", this.OnLogin);
            //StarModel star = new StarModel();
            //star.coordX = -5f;
            //star.coordY = 0f;
            //star.name = "TestStar";
            //star.resourceList.Add(Resource.Resources.Holmium, new ResourceSlotModel{ name = Resource.Resources.Holmium, amount = 50, buyPrice = 5, sellPrice = 4 });
            //star.resourceList.Add(Resource.Resources.Cerium, new ResourceSlotModel{ name = Resource.Resources.Cerium, amount = 15, buyPrice = 50, sellPrice = 40 });
            //star.resourceList.Add(Resource.Resources.Terbium, new ResourceSlotModel{ name = Resource.Resources.Terbium, amount = 25, buyPrice = 40, sellPrice = 30 });
            //star.resourceList.Add(Resource.Resources.Europium, new ResourceSlotModel{ name = Resource.Resources.Europium, amount = 10, buyPrice = 60, sellPrice = 50 });
            //starsList.Add(star.name, star);

            //StarModel star2 = new StarModel();
            //star2.coordX = 5f;
            //star2.coordY = 2f;
            //star2.name = "TestStar2";
            //star2.resourceList.Add(Resource.Resources.Holmium, new ResourceSlotModel{ name = Resource.Resources.Holmium, amount = 40, buyPrice = 15, sellPrice = 10 });
            //star2.resourceList.Add(Resource.Resources.Cerium, new ResourceSlotModel{ name = Resource.Resources.Cerium, amount = 25, buyPrice = 30, sellPrice = 25 });
            //star2.resourceList.Add(Resource.Resources.Terbium, new ResourceSlotModel{ name = Resource.Resources.Terbium, amount = 15, buyPrice = 50, sellPrice = 40 });
            //star2.resourceList.Add(Resource.Resources.Europium, new ResourceSlotModel{ name = Resource.Resources.Europium, amount = 5, buyPrice = 70, sellPrice = 50 });
            //starsList.Add(star2.name, star2);

            //StarModel star3 = new StarModel();
            //star3.coordX = 0f;
            //star3.coordY = 1f;
            //star3.name = "TestStar3";
            //star3.resourceList.Add(Resource.Resources.Holmium, new ResourceSlotModel{ name = Resource.Resources.Holmium, amount = 100, buyPrice = 3, sellPrice = 2 });
            //star3.resourceList.Add(Resource.Resources.Cerium, new ResourceSlotModel{ name = Resource.Resources.Cerium, amount = 2, buyPrice = 200, sellPrice = 150 });
            //star3.resourceList.Add(Resource.Resources.Terbium, new ResourceSlotModel{ name = Resource.Resources.Terbium, amount = 30, buyPrice = 50, sellPrice = 40 });
            //star3.resourceList.Add(Resource.Resources.Europium, new ResourceSlotModel{ name = Resource.Resources.Europium, amount = 1, buyPrice = 400, sellPrice = 350 });
            //starsList.Add(star3.name, star3);
        }


        public StarModel[] GetStarsList()
        {
            StarModel[] stars =  new StarModel[starsList.Count];
            starsList.Values.CopyTo(stars, 0);
            return stars;
        }

        public StarModel GetStarByName(string name)
        {
            return starsList[name];
        }

        protected void OnUpdateResourceAmount(SocketIOEvent e)
        {
            UpdateResourceAmountEvent updateResourceAmountEvent = JsonConvert.DeserializeObject<UpdateResourceAmountEvent>(e.data.ToString());
            starsList[updateResourceAmountEvent.starName].resourceList[updateResourceAmountEvent.resourceName].amount = updateResourceAmountEvent.newAmount;
            eventManager.DispatchEvent<UpdateResourceAmountEvent>(updateResourceAmountEvent);
        }

        protected void OnUpdateStarsList(SocketIOEvent e)
        {
            UpdateStarsListEvent updateStarsListEvent = JsonConvert.DeserializeObject<UpdateStarsListEvent>(e.data.ToString());
            this.starsList = updateStarsListEvent.starsList;
        }

        protected void OnLogin(SocketIOEvent e)
        {
            this.starsList = JsonConvert.DeserializeObject<Dictionary<string,StarModel>>(e.data.GetField("starsList").ToString());
        }
    }
}

