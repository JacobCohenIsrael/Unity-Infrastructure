using Infrastructure.Base.Service;
using UnityEngine;

namespace Infrastructure.Base.Connection
{
    public abstract class ServerService : Base.Service.Contracts.IServiceProvider
    {

        private WWW www;

        public ServerService(ServiceManager serviceManager)
        {
            string url = serviceManager.getConfig().get("server.url") as string;
            www = new WWW(url);
        }


        //public IHttpResponse Send<IHttpRequest>(IHttpRequest request)
        //{
        //}
    }
}