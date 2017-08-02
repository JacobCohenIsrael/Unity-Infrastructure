using System.Collections.Generic;
using Infrastructure.Base.Model.Contracts;
using UnityEngine;
using Newtonsoft.Json;

namespace Infrastructure.Core.Network
{
    public class Message
    {
        public Dictionary<string, IModel> body;

        public Message()
        {
            body = new Dictionary<string, IModel>();
        }

        public JSONObject ToJson()
        {
            //string json = JsonConvert.SerializeObject(body);
            string json = "{\"player\":{\"id\":0,\"firstName\":null,\"lastName\":null,\"sessionId\":\"ce076e21-0b09-4e4e-bb88-5bf4ef44f6ec\",\"currentNodeName\":null,\"activeShipIndex\":0,\"ships\":null,\"isLanded\":false,\"homePlanetName\":null,\"credits\":0}}";
            Debug.Log("Message:ToJson() - Here's the json: " + json);
            return new JSONObject(json);
        }
    }
}