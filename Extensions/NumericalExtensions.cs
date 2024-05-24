namespace UniSharp.Common.Extensions
{
    public static class NumericalExtensions
    {
        public const int MEGA_BYTE = 1048576;
        public const int KILO_BYTE = 1024;

        /// <summary>
        /// Multiplies this number by 1024 * 1024
        /// </summary>
        /// <param name="num">this number</param>
        /// <returns></returns>
        public static int MegaByte(this int num)
        {
            return num * MEGA_BYTE;
        }

        /// <summary>
        /// Multiplies this number by 1024
        /// </summary>
        /// <param name="num">this number</param>
        /// <returns></returns>
        public static int KiloByte(this int num)
        {
            return num * KILO_BYTE;
        }
    }
}