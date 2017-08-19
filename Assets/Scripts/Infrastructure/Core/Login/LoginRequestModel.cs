using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Infrastructure.Base.Model.Contracts;

public class LoginRequestModel : IModel
{  
    public string Token
    {
        get
        {
            return m_token;
        }
        set
        {
            m_token = value;
        }
    }

    [JsonProperty("token")]
    private string m_token;
}
