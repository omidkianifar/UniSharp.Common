using System;
using System.Collections.Generic;

namespace UniSharp.Common
{
    public static class Ensure
    {
        public static void ArgumentNotNull(object value, string paramName, string customMessage = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, customMessage);
            }
        }

        public static void ReferenceNotNull(object value, string paramName)
        {
            if (value is null)
            {
                throw new NullReferenceException(paramName);
            }
        }

        public static void ArgumentInRange(int value, int min, int max, string paramName)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value should be between {min} and {max}.");
            }
        }

        public static void ArgumentNotNegative(int value, string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value should be posetive.");
            }
        }

        public static void ArgumentNotNegative(float value, string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value should be posetive.");
            }
        }

        public static void ArgumentNotNegative(double value, string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value should be posetive.");
            }
        }

        public static void ArgumentNotNegative(long value, string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value should be posetive.");
            }
        }

        public static void ArgumentIsMatch(string param1Name, string param2Name, int param1Value, int param2Value)
        {
            if (param1Value != param2Value)
            {
                throw new ArgumentException($"{param1Name} not match with {param2Name}. values:{param1Value},{param2Value}.");
            }
        }

        public static void KeyExists<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, string dictionaryName, string keyName)
        {
            if (!dictionary.ContainsKey(key))
            {
                throw new KeyNotFoundException($"The key:{key} with name '{keyName}' was not found in the dictionary:{dictionaryName}.");
            }
        }

        public static void CollectionNotEmpty<T>(ICollection<T> collection, string paramName)
        {
            if (collection == null || collection.Count == 0)
            {
                throw new ArgumentException($"The collection '{paramName}' cannot be null or empty.", paramName);
            }
        }

        public static void IsValidEnumValue<TEnum>(TEnum value, string paramName) where TEnum : struct, Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                throw new ArgumentException($"The value '{value}' is not a valid enum value for type '{typeof(TEnum).Name}'.", paramName);
            }
        }

        public static void IsOfType<TExpected>(object value, string paramName)
        {
            if (value is not TExpected)
            {
                throw new ArgumentException($"The parameter '{paramName}' is not of the expected type '{typeof(TExpected).Name}'.", paramName);
            }
        }
    }
}
