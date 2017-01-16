using Infrastructure.Core.Player;
namespace Infrastructure.Core.Login.Events
{
    public class LoginSuccessfulEvent : Infrastructure.Base.Event.Event
    {
        public PlayerModel player;

        public LoginSuccessfulEvent(PlayerModel player)
        {
            this.player = player;
        }
    }
}

