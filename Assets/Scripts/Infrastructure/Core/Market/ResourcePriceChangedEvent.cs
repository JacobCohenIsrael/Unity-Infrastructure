using Infrastructure.Core.Resource;
using Newtonsoft.Json;

namespace Infrastructure.Core.Market
{
	public class ResourcePriceChangedEvent : Base.Event.Event
	{
		[JsonProperty("resource")]
		public ResourceSlotModel Resource;
	}
}