using System.Runtime.Serialization;

namespace ECoinTrackerModels.Models
{
	[DataContract]
	public class CurrencyPairModel
	{
		[DataMember]
		public string SymbolFrom { get; set; }

		[DataMember]
		public string SymbolTo { get; set; }

		[DataMember]
		public string LastPrice { get; set; }
	}
}
