using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IFormFileExtensions
{
    internal static class Helpers
    {
        internal static async Task<byte[]> GetBytesAsync(this Stream value)
        {
            using (var memoryStream = new MemoryStream())
            {
                await value.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        internal static Stream GetStream(this byte[] value)
        {
            return new MemoryStream(value);
        }
        internal static string GetHeaderInfo(IFormFile image)
        {
            using (BinaryReader reader = new BinaryReader(image.OpenReadStream()))
            {
                byte[] buffer = new byte[8];
                reader.Read(buffer, 0, buffer.Length);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in buffer)
                    sb.Append(b.ToString("X2"));
                return sb.ToString().ToUpper();
            }
        }
    }
}
