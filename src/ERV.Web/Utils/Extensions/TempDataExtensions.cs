using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace ERV.Web.Utils.Extensions
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            try
            {
                tempData[key] = JsonConvert.SerializeObject(value);
            }
            catch (Exception e) {}
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            try
            {
                object o;
                tempData.TryGetValue(key, out o);
                return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
