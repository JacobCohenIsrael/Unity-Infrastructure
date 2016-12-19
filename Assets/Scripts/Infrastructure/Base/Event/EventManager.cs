using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Base.Event.Contracts;

namespace Infrastructure.Base.Event
{
    public class EventManager : Contracts.IEventManager
    {
        protected Dictionary<Type, Delegate> eventTable;

        public EventManager()
        {
            eventTable = new Dictionary<Type, Delegate>();
        }

        public void AddListener<T>(Action<T> handler)
        {
            Delegate d;
            eventTable.TryGetValue(typeof(T), out d);
            eventTable[typeof(T)] = (Action<T>)d + handler;
        }

        public void RemoveListener<T>(Action<T> handler)
        {
            eventTable[typeof(T)] = (Action<T>)eventTable[typeof(T)] - handler;
        }

        public void DispatchEvent<T>(T e)
        {
            Delegate d;
            if (eventTable.TryGetValue(typeof(T), out d))
            {
                Action<T> callback = d as Action<T>;

                if (callback != null)
                {
                    callback(e);
                }
            }
        }
    }
}

