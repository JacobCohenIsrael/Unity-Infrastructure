using Infrastructure.Core.Player;
using Infrastructure.Base.Service;
using Infrastructure.Core.Login;

namespace Infrastructure.Core.Login
{
    public class LoginService : Base.Service.Contracts.IServiceProvider
    {
        public LoginService(ServiceManager serviceManager)
        {
        }

        public PlayerModel LoginAsGuest()
        {
            PlayerModel player = new PlayerModel();
            player.sessionId = "test";
            Infrastructure.Base.Application.Application.getInstance().eventManager.DispatchEvent<Events.LoginSuccessfulEvent>(new Events.LoginSuccessfulEvent(player));
            return player;
        }

        public PlayerModel LoginAsGuest(string sessionId)
        {
            PlayerModel player = new PlayerModel();
            Infrastructure.Base.Application.Application.getInstance().eventManager.DispatchEvent<Events.LoginSuccessfulEvent>(new Events.LoginSuccessfulEvent(player));
            return player;
        }

        public void Logout()
        {
            Infrastructure.Base.Application.Application.getInstance().eventManager.DispatchEvent<Events.LogoutSuccessfulEvent>(new Events.LogoutSuccessfulEvent());
        }
    }
}
