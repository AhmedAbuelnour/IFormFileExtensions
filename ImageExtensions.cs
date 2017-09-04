using Microsoft.AspNetCore.Http;

namespace IFormFileExtensions
{
    /// <summary>
    /// Provide The ability to work with images easily
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        ///  Gets the current image format 
        /// </summary>
        /// <param name="image">The current Image</param>
        /// <returns>The Type of the current image</returns>
        public static ImageFormat GetImageFormat(this IFormFile image)
        {
            string headerCode = Helpers.GetHeaderInfo(image);
            if (headerCode.StartsWith("FFD8FFE0"))
                return ImageFormat.JPG;
            else if (headerCode.StartsWith("49492A"))
                return ImageFormat.TIFF;
            else if (headerCode.StartsWith("424D"))
                return ImageFormat.BMP;
            else if (headerCode.StartsWith("474946"))
                return ImageFormat.GIF;
            else if (headerCode.StartsWith("89504E470D0A1A0A"))
                return ImageFormat.PNG;
            else
                return ImageFormat.Unknown;
        }
        /// <summary>
        /// Represents the Image types
        /// </summary>
        public enum ImageFormat
        {
            JPG, PNG, GIF, BMP, TIFF, Unknown
        }
    }
}
