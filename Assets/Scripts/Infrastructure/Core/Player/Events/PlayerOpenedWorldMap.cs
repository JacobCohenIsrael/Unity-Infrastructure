namespace Infrastructure.Core.Player.Events
{
    public class PlayerOpenedWorldMap : Base.Event.Event
    {
        public PlayerModel Player;
        public PlayerOpenedWorldMap(PlayerModel player)
        {
            Player = player;
        }
    }
}

