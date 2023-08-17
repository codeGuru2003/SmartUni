using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace SmartUni.Services
{
	public class CustomTempDataProvider : ITempDataProvider
	{
		public IDictionary<string, object> LoadTempData(HttpContext context)
		{
			var tempData = new Dictionary<string, object>();
			var value = context.Session.GetString("TempData");
			if (!string.IsNullOrEmpty(value))
			{
				tempData = JsonConvert.DeserializeObject<Dictionary<string, object>>(value);
			}
			return tempData;
		}

		public void SaveTempData(HttpContext context, IDictionary<string, object> values)
		{
			var value = JsonConvert.SerializeObject(values);
			context.Session.SetString("TempData", value);
		}
	}
}
