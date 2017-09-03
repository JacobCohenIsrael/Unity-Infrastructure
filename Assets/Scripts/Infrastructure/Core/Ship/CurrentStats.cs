using Newtonsoft.Json;

namespace Infrastructure.Core.Ship
{
	public class CurrentStats
	{
		[JsonProperty("hull")]
		public int Hull;
		
		[JsonProperty("shield")]
		public int Shield;
		
		[JsonProperty("energy")]
		public int Energy;
		
		[JsonProperty("cargo")]
		public int Cargo;
	}
}