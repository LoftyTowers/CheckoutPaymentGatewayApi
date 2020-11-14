using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GatewayLoadTest
{
	/// <summary>
	/// From http://west-wind.com/weblog/posts/442969.aspx
	/// </summary>
	public static class JsonExtensions
	{
		/// <summary>
		/// Serializes a type to Json. Note the type must be marked Serializable
		/// or include a DataContract attribute. 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToJsonString(object value)
		{
			var ser = new DataContractJsonSerializer(value.GetType());
			string json;
			using (var ms = new MemoryStream())
			{
				ser.WriteObject(ms, value);
				json = Encoding.Default.GetString(ms.ToArray());
			}
			// AJL - Ensure that single quotes are escaped, and that the incorrectly \u000x are coded as \\u0009
			return json.Replace("'", @"\'").Replace("\\u0", "\\\\u0");
		}

		/// <summary>
		/// Extension method on object that serializes the value to Json. 
		/// Note the type must be marked Serializable or include a DataContract attribute. 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToJson(this object value)
		{
			return ToJsonString(value);
		}

		/// <summary>
		/// Deserializes a json string into a specific type.
		/// Note that the type specified must be serializable.
		/// </summary>
		/// <param name="json"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static object FromJsonString(string json, Type type)
		{
			object value = null;
			using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
			{
				DataContractJsonSerializer ser = new DataContractJsonSerializer(type);
				value = ser.ReadObject(ms);
			}
			return value;
		}

		/// <summary>
		/// Extension method to string that deserializes a json string 
		/// into a specific type.
		/// Note that the type specified must be serializable.
		/// </summary>
		/// <param name="json"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static object FromJson(this string json, Type type)
		{
			return FromJsonString(json, type);
		}

		/// <summary>
		/// Extension method to string that deserializes a json string 
		/// into a specific type.
		/// Note that the type specified must be serializable.
		/// </summary>
		/// <param name="json"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static T FromJson<T>(this string json)
		{
			if (string.IsNullOrWhiteSpace(json))
				return default(T);

			return (T)FromJsonString(json, typeof(T));
		}
	}
}
