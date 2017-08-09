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
            string json = JsonConvert.SerializeObject(body);
            return new JSONObject(json);
        }
    }
}