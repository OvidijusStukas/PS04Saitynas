using System.Runtime.Serialization;

namespace ECoinTrackerModels.Requests
{
	[DataContract]
    public class LoginRequest
    {
		[DataMember]
		public string Username { get; set; }

		[DataMember]
		public string Password { get; set; }
    }
}
