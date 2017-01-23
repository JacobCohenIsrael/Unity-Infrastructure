using System.Collections;
using System.Collections.Generic;
using Infrastructure.Core.Resource;

namespace Infrastructure.Core.Star
{
	public class StarModel : Infrastructure.Base.Model.Model
	{
		public int id;
		public string name;
		public float coordX;
		public float coordY;

        public Dictionary<Resources, ResourceSlotModel> resourceList;

        public StarModel()
        {
            resourceList = new Dictionary<Resources, ResourceSlotModel>();
        }
	}
}