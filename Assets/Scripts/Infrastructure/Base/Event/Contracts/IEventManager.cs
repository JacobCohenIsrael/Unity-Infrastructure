using System;
using System.Collections;
using Infrastructure.Base.Event.Contracts;

namespace Infrastructure.Base.Event.Contracts
{
    public interface IEventManager
    {
        void DispatchEvent<T>(T e);
        void AddListener<T>(Action<T> onApplicationStarted);
    }
}
