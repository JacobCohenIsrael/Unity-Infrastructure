using Infrastructure.Base.Service;
using Infrastructure.Core.Resource;
using Infrastructure.Core.Network;
using UnityEngine;
using Infrastructure.Core.Chat;
using Infrastructure.Core.Node;
using Infrastructure.Core.Star;

namespace Infrastructure.Core.Player
{
    public class PlayerAdapter : Base.Service.Contracts.IServiceProvider
    {
        protected MainServer mainServer;
        protected PlayerModel player;

        public PlayerAdapter(ServiceManager serviceManager)
        {
            mainServer = serviceManager.get<MainServer>() as MainServer;
        }

        public void LoginAsGuest(string token)
        {
            Message msg = new Message();
            msg.body.Add("request", new LoginRequestModel { Token = token });
            mainServer.Emit("login", msg.ToJson());
        }

        public void JumpPlayerToNode(PlayerModel player, NodeModel node)
        {
            if (player.currentNodeName != node.name)
            {
                Message msg = new Message();
                msg.body.Add("player", player);
                msg.body.Add("node", node);
                mainServer.Emit("jumpPlayerToNode", msg.ToJson());
            }
        }

        public void LandPlayerOnStar(PlayerModel player)
        {
            Message msg = new Message();
            msg.body.Add("player", player);
            mainServer.Emit("landPlayerOnStar", msg.ToJson());
        }

        public bool DepartPlayerFromStar(PlayerModel player)
        {
            if (player.getActiveShip() == null)
            {
                return false;
            }
            Message msg = new Message();
            msg.body.Add("player", player);
            mainServer.Emit("departPlayerFromStar", msg.ToJson());
            return true;
        }

        public void BuyResource(PlayerModel player, ResourceSlotModel resourceSlot)
        {
            if (player.getActiveShip().AddResource(resourceSlot.name, 1))
            {
                Message msg = new Message();
                msg.body.Add("player", player);
                msg.body.Add("resource", new BuyResourceModel(){name = resourceSlot.name, amount = 1});
                mainServer.Emit("playerBuyResource", msg.ToJson());
            }
        }

        public bool SellResource(PlayerModel player, ResourceSlotModel resourceSlot)
        {
            if (player.getActiveShip().RemoveResource(resourceSlot.name, 1))
            {
                Message msg = new Message();
                msg.body.Add("player", player);
                msg.body.Add("resource", new SellResourceModel { name = resourceSlot.name, amount = 1 });
                mainServer.Emit("playerSellResource", msg.ToJson());
                return true;
            }
            return false;
        }

        public bool EnterLounge(PlayerModel player)
        {
            Message msg = new Message();
            msg.body.Add("player", player);
            mainServer.Emit("playerEnteredLounge", msg.ToJson());
            return true;
        }

        public bool LeaveLounge(PlayerModel player)
        {
            Message msg = new Message();
            msg.body.Add("player", player);
            mainServer.Emit("playerLeftLounge", msg.ToJson());
            return true;
        }

        public void SendChat(PlayerModel player, ChatMessageModel chatMessage)
        {
            Message msg = new Message();
            msg.body.Add("player", player);
            msg.body.Add("message", chatMessage);
            mainServer.Emit("chatSent", msg.ToJson());
        }

        public void EnterMarket(PlayerModel player)
        {
            Message msg = new Message();
            msg.body.Add("player", player);
            mainServer.Emit("playerEnterMarket", msg.ToJson());
        }
    }
}

