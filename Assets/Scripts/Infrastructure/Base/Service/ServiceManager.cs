using System.Collections;
using Infrastructure.Base.Service;
using System;
using System.Collections.Generic;
using Infrastructure.Base.Config;

namespace Infrastructure.Base.Service
{
    public class ServiceManager : Contracts.IServiceManager
    {
        Dictionary<Type, Contracts.IServiceProvider> dictonary;

        public ServiceManager()
        {
            dictonary = new Dictionary<Type, Contracts.IServiceProvider>();
        }

        public Contracts.IServiceProvider get<T>() where T : Contracts.IServiceProvider
        {
            if (!dictonary.ContainsKey(typeof(T)))
            {
                var service = (T)Activator.CreateInstance(typeof(T), this);
                dictonary[typeof(T)] = service;
            } 

            return dictonary[typeof(T)];
        }

        public void set<T>(T service) where T : Contracts.IServiceProvider
        {
            dictonary[typeof(T)] = service;
        }
    }
}
