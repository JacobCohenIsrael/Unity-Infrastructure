using UnityEngine;
using System.Collections;
using Infrastructure.Base.Service;
using Infrastructure.Base.Service.Contracts;

namespace Infrastructure.Core.Player
{
    public class PlayerAdapter : IServiceProvider
    {
        public PlayerAdapter(ServiceManager serviceManager)
        {
            Debug.Log(" I am PlayerAdapter!");
        }

        public int getById(int id)
        {
            return 123;
        }
    }
}

