using System;
using System.Collections.Generic;

namespace UniSharp.Common.Extensions
{
    public static class DictionaryExtension
    {

        public static T GetValue<T>(this Dictionary<string, object> data, string key)
        {
            try
            {
                if (data.ContainsKey(key))
                    return (T)data[key];
                else throw new Exception(string.Format("key:{0} not found", key));
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AddOrUpdate<T, G>(this IDictionary<T, G> dictionary, T key, G value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }
    }
}
