﻿using Newtonsoft.Json;

// ReSharper disable CheckNamespace
namespace RestSharp.Deserializers
// ReSharper restore CheckNamespace
{
	public class DynamicJsonDeserializer : IDeserializer
	{
		public string RootElement { get; set; }
		public string Namespace { get; set; }
		public string DateFormat { get; set; }

		T IDeserializer.Deserialize<T>(IRestResponse response)
		{
			return JsonConvert.DeserializeObject<dynamic>(response.Content);
		}
	}
}