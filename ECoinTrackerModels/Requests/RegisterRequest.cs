using System.Runtime.Serialization;

namespace ECoinTrackerModels.Requests
{
	[DataContract]
    public class RegisterRequest
    {
		[DataMember]
		public string FullName { get; set; }

		[DataMember]
		public string Username { get; set; }

		[DataMember]
		public string Password { get; set; }
    }
}
