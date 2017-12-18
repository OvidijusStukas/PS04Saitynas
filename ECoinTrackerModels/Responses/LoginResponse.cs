using System.Runtime.Serialization;
using ECoinTrackerModels.Jwt;

namespace ECoinTrackerModels.Responses
{
	[DataContract]
    public class LoginResponse
    {
		[DataMember]
		public JwtToken Token { get; set; }

		[DataMember]
		public int AccountId { get; set; }

		[DataMember]
		public string AccountFullname { get; set; }
    }
}
