using System;

namespace UniSharp.Common.Extensions
{
    public static class RandomExtensions
    {
        public static string GetRandomDigits(this Random random, int lenght)
        {
            if (lenght <= 0)
                return string.Empty;

            var str = new char[lenght];

            for (int i = 0; i < str.Length; i++)
                str[i] = (char)(random.Next(0, 9) + '0');

            return new string(str);
        }
    }
}
