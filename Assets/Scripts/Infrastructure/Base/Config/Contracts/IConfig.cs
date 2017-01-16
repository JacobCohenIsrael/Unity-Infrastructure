using UnityEngine;
using System.Collections;

namespace Infrastructure.Base.Config.Contracts
{
    public interface IConfig
    {
        object get(string key);
        void set(string key, object value);
    }
}