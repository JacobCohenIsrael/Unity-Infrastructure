﻿using Infrastructure.Core.Player;
using Infrastructure.Base.Service;
using Infrastructure.Base.Event;
using Infrastructure.Core.Network;
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
            eventManager = serviceManager.get<EventManager>() as EventManager;
            playerService = serviceManager.get<PlayerService>() as PlayerService;
            mainServer = serviceManager.get<MainServer>() as MainServer;
            mainServer.On("loginResponse", OnLogin);
        }

        public string LoginAsGuest()
        {
            var guid = Guid.NewGuid();
            playerService.LoginAsGuest(guid.ToString());
            return guid.ToString();
        }

        public void LoginAsGuest(string token)
        {
            playerService.LoginAsGuest(token);
        }

        public void Logout()
        {
            eventManager.DispatchEvent(new LogoutSuccessfulEvent());
        }


        public void OnLogin(SocketIOEvent e)
        {
            LoginSuccessfulEvent loginSuccessfulEvent = JsonConvert.DeserializeObject<LoginSuccessfulEvent>(e.data);
            eventManager.DispatchEvent(loginSuccessfulEvent);
        }
    }
}
