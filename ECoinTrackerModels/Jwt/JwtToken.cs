using System;
using System.Runtime.Serialization;

namespace ECoinTrackerModels.Jwt
{
	[DataContract]
	public class JwtToken
	{
		
		[DataMember]
		public DateTime ValidTo { get; set; }//=> _token.ValidTo;

		[DataMember]
		public string Value { get; set; }//=> new JwtSecurityTokenHandler().WriteToken(_token);
	}
}
