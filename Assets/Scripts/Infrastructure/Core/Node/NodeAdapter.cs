using System.Collections.Generic;
using Infrastructure.Base.Service;
using Infrastructure.Core.Network;
using Infrastructure.Core.Node;

namespace Infrastructure.Core.Star
{
    public class NodeAdapter : Base.Service.Contracts.IServiceProvider
    {
        private Dictionary<string, NodeModel> nodeList;
        private MainServer _mainServer;

        public NodeAdapter(ServiceManager serviceManager)
        {
            _mainServer = serviceManager.get<MainServer>() as MainServer;
        }
    }
}

