using System.Collections.Generic;
using Infrastructure.Base.Service;
using Infrastructure.Core.Network;
using Infrastructure.Core.Star.Events;
using Infrastructure.Base.Event;
using Newtonsoft.Json;
using UnitySocketIO.Events;

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
            UpdateResourceAmountEvent updateResourceAmountEvent = JsonConvert.DeserializeObject<UpdateResourceAmountEvent>(e.data);
            eventManager.DispatchEvent<UpdateResourceAmountEvent>(updateResourceAmountEvent);
        }

        protected void OnUpdateStarsList(SocketIOEvent e)
        {
            UpdateStarsListEvent updateStarsListEvent = JsonConvert.DeserializeObject<UpdateStarsListEvent>(e.data.ToString());
            this.starsList = updateStarsListEvent.starsList;
        }
    }
}

