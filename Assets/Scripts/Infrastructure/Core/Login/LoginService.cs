using Infrastructure.Core.Player;
using Infrastructure.Base.Service;
using Infrastructure.Core.Login;
using Infrastructure.Base.Event;
using Infrastructure.Core.Network;
using UnitySocketIO;
using UnityEngine;
using System;
using System.Collections.Generic;
using Infrastructure.Core.Star;
using Newtonsoft.Json;
using UnitySocketIO.Events;
using Infrastructure.Core.Login.Events;

namespace Infrastructure.Core.Login
{
    public class LoginService : Base.Service.Contracts.IServiceProvider
    {
        protected EventManager eventManager;
        protected PlayerService playerService;
        protected MainServer mainServer;

        public LoginService(ServiceManager serviceManager)
        {
            this.eventManager = serviceManager.get<EventManager>() as EventManager;
            this.playerService = serviceManager.get<PlayerService>() as PlayerService;
            mainServer = serviceManager.get<MainServer>() as MainServer;
            mainServer.On("loginResponse", this.OnLogin);
        }

        public void LoginAsGuest()
        {
            Guid g = Guid.NewGuid();
            playerService.LoginAsGuest(g.ToString());
        }

        public void LoginAsGuest(string sessionId)
        {
            playerService.LoginAsGuest(sessionId);
        }

        public void Logout()
        {
            eventManager.DispatchEvent<Events.LogoutSuccessfulEvent>(new Events.LogoutSuccessfulEvent());
        }


        public void OnLogin(SocketIOEvent e)
        {
            //Debug.Log("LoginService: OnLogin() - Invoked");
            LoginSuccessfulEvent loginSuccessfulEvent = JsonConvert.DeserializeObject<LoginSuccessfulEvent>(e.data);
            PlayerModel player = loginSuccessfulEvent.player;
            Dictionary<string, StarModel> starsList = loginSuccessfulEvent.starsList;
            eventManager.DispatchEvent<Events.LoginSuccessfulEvent>(new Events.LoginSuccessfulEvent(player, starsList));
        }
    }
}
