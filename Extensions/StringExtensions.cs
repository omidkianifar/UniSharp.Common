using Newtonsoft.Json;
using System.Text;

namespace UniSharp.Common.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static T FromJson<T>(this string str, JsonConverter<T> customJsonConveter = null)
        {
            try
            {
                if (customJsonConveter == null)
                    return JsonConvert.DeserializeObject<T>(str);

                return JsonConvert.DeserializeObject<T>(str, customJsonConveter);
            }
            catch
            {
                return default;
            }
        }
    }
}

