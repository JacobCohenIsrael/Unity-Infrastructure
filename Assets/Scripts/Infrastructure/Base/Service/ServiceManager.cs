using System.Collections;
using Infrastructure.Base.Service;
using System;
using System.Collections.Generic;
using Configuration = Infrastructure.Base.Config.Config;

namespace Infrastructure.Base.Service
{
    public class ServiceManager : Contracts.IServiceManager
    {
        Dictionary<Type, Contracts.IServiceProvider> dictonary;

        Configuration config;

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

        public void setConfig(Configuration config)
        {
            this.config = config;
        }

        public Configuration getConfig()
        {
            return config;
        }
    }
}
