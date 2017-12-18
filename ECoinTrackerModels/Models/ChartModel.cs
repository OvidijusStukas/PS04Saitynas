using System.Runtime.Serialization;

namespace ECoinTrackerModels.Models
{
	[DataContract]
    public class ChartModel
    {
		[DataMember]
		public string Timestamp { get; set; }

		[DataMember]
		public string Price { get; set; }
    }
}
