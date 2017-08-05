using System.Collections;
using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;
using Infrastructure.Core.Star;
using Infrastructure.Core.Resource;
using UnitySocketIO;
using Infrastructure.Core.Network;
using System.Threading;
using UnityEngine;
using System;
using Infrastructure.Core.Chat;

namespace Infrastructure.Core.Player
{
    public class PlayerAdapter : Base.Service.Contracts.IServiceProvider
    {
        protected MainServer mainServer;

        public PlayerAdapter(ServiceManager serviceManager)
        {
            mainServer = serviceManager.get<MainServer>() as MainServer;
        }

        public void LoginAsGuest(string sessionId)
        {
            Message msg = new Message();
            msg.body.Add("player", new PlayerModel(){sessionId = sessionId});
            mainServer.Emit("login", msg.ToJson());
        }

        public bool JumpPlayerToStar(PlayerModel player, StarModel star)
        {
            if (player.currentNodeName == star.name)
            {
                return false;
            }
            Message msg = new Message();
            msg.body.Add("player", player);
            msg.body.Add("star", star);
            mainServer.Emit("jumpPlayerToStar", msg.ToJson());
            return true;
        }

        public bool LandPlayerOnStar(PlayerModel player)
        {
            mainServer.Emit("landPlayerOnStar", new JSONObject(JsonUtility.ToJson(player)));
            return true;
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

        public bool BuyResource(PlayerModel player, ResourceSlotModel resourceSlot)
        {
            if (resourceSlot.amount > 0)
            {
                if (player.getActiveShip().AddResource(resourceSlot.name, 1))
                {
                    Message msg = new Message();
                    msg.body.Add("player", player);
                    msg.body.Add("resource", new BuyResourceModel(){name = resourceSlot.name, amount = 1});
                    mainServer.Emit("playerBuyResource", msg.ToJson());
                    return true;
                }
            }
            return false;
        }

        public bool SellResource(PlayerModel player, ResourceSlotModel resourceSlot)
        {
            if (player.getActiveShip().RemoveResource(resourceSlot.name, 1))
            {
                Message msg = new Message();
                msg.body.Add("player", player);
                msg.body.Add("resource", new SellResourceModel() { name = resourceSlot.name, amount = 1 });
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

        public void LoungeChatSent(PlayerModel player, string chatMessage)
        {
            Message msg = new Message();
            msg.body.Add("player", player);
            msg.body.Add("message", new ChatMessageModel { Message = chatMessage });
            mainServer.Emit("loungeChatSent", msg.ToJson());
        }
    }
}

