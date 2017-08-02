using System;
using System.Collections.Generic;
using Infrastructure.Core.Resource;
using Infrastructure.Base.Service;
using UnityEngine;
using Infrastructure.Core.Network;
using UnitySocketIO;
using Infrastructure.Core.Star.Events;
using Infrastructure.Base.Event;
using Newtonsoft.Json;
using UnitySocketIO.Events;
using Infrastructure.Core.Login.Events;

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
            //Debug.Log("LoginService: OnLogin() - Invoked");
            LoginSuccessfulEvent loginSuccessfulEvent = JsonConvert.DeserializeObject<LoginSuccessfulEvent>(e.data);
            this.starsList = loginSuccessfulEvent.starsList;
        }
    }
}

