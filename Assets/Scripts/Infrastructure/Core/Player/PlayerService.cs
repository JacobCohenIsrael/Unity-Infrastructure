using Infrastructure.Base.Service;
using Infrastructure.Base.Event;
using Infrastructure.Core.Player.Events;
using Infrastructure.Core.Resource;
using Infrastructure.Core.Network;
using Newtonsoft.Json;
using UnitySocketIO.Events;
using Infrastructure.Core.Chat;
using Infrastructure.Core.Chat.Events;
using Infrastructure.Core.Node;
using Infrastructure.Core.Notification.Events;
using Infrastructure.Core.Star;
using System;

namespace Infrastructure.Core.Player
{
    public class PlayerService : Base.Service.Contracts.IServiceProvider
    {
        protected NodeService nodeService;
        protected PlayerAdapter playerAdapter;
        protected EventManager eventManager;
        protected MainServer mainServer;

        public PlayerService(ServiceManager serviceManager)
        {
            getDependencies(serviceManager);
            subscribeListeners();
        }

        public void LeaveLounge(PlayerModel player)
        {
            if (playerAdapter.LeaveLounge(player))
            {

            }
        }

        protected void getDependencies(ServiceManager serviceManager)
        {
            nodeService = serviceManager.get <NodeService>() as NodeService;
            playerAdapter = serviceManager.get<PlayerAdapter>() as PlayerAdapter;
            eventManager = serviceManager.get<EventManager>() as EventManager;
            mainServer = serviceManager.get<MainServer>() as MainServer;
        }

        protected void subscribeListeners()
        {
            mainServer.On("playerDeparted", OnPlayerDepart);
            mainServer.On("playerLanded", OnPlayerLanded);
            mainServer.On("playerBoughtResource", OnPlayerBoughtResource);
            mainServer.On("playerSoldResource", OnPlayerSoldResource);
            mainServer.On("playerEnteredLounge", OnPlayerEnteredLounge);
            mainServer.On("playerLeftLounge", OnPlayerLeftLounge);
            mainServer.On("chatMessageReceived", OnChatMessageReceived);
            mainServer.On("notificationReceived", OnNotificationReceived);
            mainServer.On("playerEnteredMarket", OnPlayerEnteredMarket);
            mainServer.On("playerLeftMarket", OnPlayerLeftMarket);
            mainServer.On("playerJumpedToNode", OnPlayerJumpedToNode);
        }

        private void OnNotificationReceived(SocketIOEvent e)
        {
            var nre = JsonConvert.DeserializeObject<NotificationEvent>(e.data);
            eventManager.DispatchEvent(nre);
        }

        private void OnPlayerEnteredMarket(SocketIOEvent e)
        {
            var peme = JsonConvert.DeserializeObject<PlayerEnteredMarketEvent>(e.data);
            eventManager.DispatchEvent(peme);
        }

        private void OnPlayerLeftMarket(SocketIOEvent e)
        {
            var plme = JsonConvert.DeserializeObject<PlayerLeftMarketEvent>(e.data);
            eventManager.DispatchEvent(plme);
        }

        public void LoginAsGuest(string sessionId)
        {
            playerAdapter.LoginAsGuest(sessionId);
        }

        public void JumpPlayerToNode(PlayerModel player, NodeModel node)
        {
            playerAdapter.JumpPlayerToNode(player, node);
        }

        public void SendChat(PlayerModel player, string chatMessage, string chatRoomKey)
        {
            ChatMessageModel chatMessageModel = new ChatMessageModel { Message = chatMessage, RoomKey = chatRoomKey };
            playerAdapter.SendChat(player, chatMessageModel);
        }

        public void EnterLounge(PlayerModel player)
        {
            if (playerAdapter.EnterLounge(player))
            {
            }
        }

        public void LandPlayerOnStar(PlayerModel player)
        {
            playerAdapter.LandPlayerOnStar(player);
        }

        public void DepartPlayerFromStar(PlayerModel player)
        {
            if (playerAdapter.DepartPlayerFromStar(player))
            {
            }
        }

        public void EnterMarket(PlayerModel player)
        {
            playerAdapter.EnterMarket(player);
        }
            
        public void ExitMarket(PlayerModel player)
        {
            playerAdapter.LeaveMarket(player);
        }

        public void PlayerClosedWorldMap(PlayerModel player, NodeModel node)
        {
            PlayerEnteredNodeSpaceEvent playerEnteredNodeSpaceEvent = new PlayerEnteredNodeSpaceEvent(node);
            eventManager.DispatchEvent(playerEnteredNodeSpaceEvent);
        }

        public void BuyResource(PlayerModel player, ResourceSlotModel resourceSlot, int amount)
        {
            if (player.credits >= resourceSlot.BuyPrice)
            {
                playerAdapter.BuyResource(player, resourceSlot, amount);
            }
            else
            {
                eventManager.DispatchEvent(new NotificationEvent{ NotificationText = "Not Enough Credits"});
            }
        }

        public void SellResource(PlayerModel player, ResourceSlotModel resourceSlot, int amount)
        {
            playerAdapter.SellResource(player, resourceSlot, amount); 
        }

        protected void OnPlayerBoughtResource(SocketIOEvent e)
        {
            PlayerBoughtResourceEvent pbre = JsonConvert.DeserializeObject<PlayerBoughtResourceEvent>(e.data);
            eventManager.DispatchEvent(pbre);
        }

        protected void OnPlayerSoldResource(SocketIOEvent e)
        {
            PlayerSoldResourceEvent psre = JsonConvert.DeserializeObject<PlayerSoldResourceEvent>(e.data);
            eventManager.DispatchEvent(psre);
        }


        protected void OnPlayerEnteredLounge(SocketIOEvent e)
        {
            PlayerEnteredLoungeEvent pele = JsonConvert.DeserializeObject<PlayerEnteredLoungeEvent>(e.data);
            eventManager.DispatchEvent(pele);
        }

        protected void OnPlayerLeftLounge(SocketIOEvent e)
        {
            PlayerLeftLoungeEvent plle = JsonConvert.DeserializeObject<PlayerLeftLoungeEvent>(e.data);
            eventManager.DispatchEvent(plle);
        }

        protected void OnPlayerLanded(SocketIOEvent e)
        {
            PlayerLandOnStarEvent plose = JsonConvert.DeserializeObject<PlayerLandOnStarEvent>(e.data);
            eventManager.DispatchEvent(plose);
        }

        protected void OnPlayerDepart(SocketIOEvent e)
        {
            PlayerDepartFromStarEvent pde = JsonConvert.DeserializeObject<PlayerDepartFromStarEvent>(e.data);
            eventManager.DispatchEvent(pde);
        }

        protected void OnChatMessageReceived(SocketIOEvent e)
        {
            ChatMessageReceivedEvent cmre = JsonConvert.DeserializeObject<ChatMessageReceivedEvent>(e.data);
            eventManager.DispatchEvent(cmre);
        }
        
        protected void OnPlayerJumpedToNode(SocketIOEvent e)
        {
            var pjtne = JsonConvert.DeserializeObject<PlayerJumpedToNodeEvent>(e.data);
            eventManager.DispatchEvent(pjtne);
        }
    }
}