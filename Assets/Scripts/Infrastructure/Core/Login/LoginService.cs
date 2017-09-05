using Infrastructure.Core.Player;
using Infrastructure.Base.Service;
using Infrastructure.Base.Event;
using Infrastructure.Core.Network;
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
            var loginSuccessfulEvent = JsonConvert.DeserializeObject<LoginSuccessfulEvent>(e.data);
            eventManager.DispatchEvent(loginSuccessfulEvent);
        }
    }
}
