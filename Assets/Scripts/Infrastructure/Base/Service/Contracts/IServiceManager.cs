using UnityEngine;
using System.Collections;

namespace Infrastructure.Base.Service.Contracts
{
    public interface IServiceManager
    {
        Infrastructure.Base.Service.Contracts.IServiceProvider get<T>() where T : Infrastructure.Base.Service.Contracts.IServiceProvider;
    }
}
