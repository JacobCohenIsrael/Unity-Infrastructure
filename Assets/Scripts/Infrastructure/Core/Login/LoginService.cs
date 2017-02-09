using Infrastructure.Core.Player;
using Infrastructure.Base.Service;
using Infrastructure.Core.Login;
using Infrastructure.Base.Event;
using Infrastructure.Core.Network;
using SocketIO;

namespace Infrastructure.Core.Login
{
    public class LoginService : Base.Service.Contracts.IServiceProvider
    {
        protected EventManager eventManager;
        protected PlayerService playerService;

        public LoginService(ServiceManager serviceManager)
        {
            this.eventManager = serviceManager.get<EventManager>() as EventManager;
            this.playerService = serviceManager.get<PlayerService>() as PlayerService;
        }

        public PlayerModel LoginAsGuest()
        {
            PlayerModel player = playerService.getPlayerById(123);
            player.sessionId = "test";
            eventManager.DispatchEvent<Events.LoginSuccessfulEvent>(new Events.LoginSuccessfulEvent(player));
            return player;
        }

        public PlayerModel LoginAsGuest(string sessionId)
        {
            PlayerModel player = playerService.getPlayerById(123);
            eventManager.DispatchEvent<Events.LoginSuccessfulEvent>(new Events.LoginSuccessfulEvent(player));
            return player;
        }

        public void Logout()
        {
            eventManager.DispatchEvent<Events.LogoutSuccessfulEvent>(new Events.LogoutSuccessfulEvent());
        }
    }
}
