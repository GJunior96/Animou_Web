using Newtonsoft.Json;
using System.Web;

namespace Animou.App.Extensions
{
    public static class WebHelper
    {
        public static T Deserialize<T>(this string value, T _)
        {
            if (string.IsNullOrEmpty(value)) return default;

            return JsonConvert.DeserializeObject<T>(HttpUtility.HtmlDecode(value));
        }

        public static string Serialize(this object value)
        {
            if (value == null) return null;

            return HttpUtility.HtmlEncode(JsonConvert.SerializeObject(value,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
        }
    }
}
