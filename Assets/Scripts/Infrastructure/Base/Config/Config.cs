using System;
using System.Collections.Generic;
using Infrastructure.Base.Config.Contracts;

namespace Infrastructure.Base.Config
{
    public class Config : IConfig
    {
        Dictionary<string, object> dictonary;

        public Config()
        {
            dictonary = new Dictionary<string, object>();
        }

        public void set(string key, object value)
        {
            dictonary.Add(key, value);
        }

        public object get(string key)
        {
            if (dictonary.ContainsKey(key))
            {
                return dictonary[key];
            }
            return null;
        }
    }
}

