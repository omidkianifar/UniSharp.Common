using System.IO;

namespace UniSharp.Common.Extensions
{
    public static class FileStreamExtensions
    {
        public static byte[] ToArray(this FileStream fs)
        {
            var ms = new MemoryStream();
            fs.CopyTo(ms);
            return ms.ToArray();
        }
    }
}