using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace IFormFileExtensions
{   
    /// <summary>
    /// Represents the basic IFormFile operations.
    /// </summary>
    public static class BasicExtensions
    {
        /// <summary>
        /// Read All the content of a file, works with ".txt" file.
        /// </summary>
        /// <param name="Value">Represents the Current Value of IFormFile</param>
        /// <returns>A String contains the content of the current file.</returns>
        public static async Task<string> ReadAllTextAsync(this IFormFile Value)
        {
            return await new StreamReader(Value?.OpenReadStream()).ReadToEndAsync();
        }
        /// <summary>
        /// Read All the content of a collection of files, works with ".txt" files.
        /// </summary>
        /// <param name="fileCollection">Represents the Current Value of IFormFileCollection</param>
        /// <returns>List of Strings each contains the content of the its current file.</returns>
        public static async Task<IEnumerable<string>> ReadAllTextsAsync(this IFormFileCollection fileCollection)
        {
            ICollection<string> ReadTexts = new List<string>(fileCollection?.Count ?? 0);
            if (ReadTexts.Count == 0) return ReadTexts;
            foreach(IFormFile file in fileCollection)  ReadTexts.Add(await ReadAllTextAsync(file));
            return ReadTexts;
        }
        /// <summary>
        /// Converts the file into Byte Arrays
        /// </summary>
        /// <param name="file">Represents the current IFormFile</param>
        /// <returns>Byte Array represents the current file</returns>
        public static async Task<byte[]> ConvertFileToBytesAsync(this IFormFile file)
        {
            using (MemoryStream Stream = new MemoryStream())
            {
                // Open the image as a stream and copy it into Stream object
                await file?.OpenReadStream()?.CopyToAsync(Stream);
                // Convert the stream to Byte array.
                return Stream.ToArray();
            }
        }
        /// <summary>
        /// Converts the files into list of Byte Arrays
        /// </summary>
        /// <param name="files">Represents the current IFormFileCollection</param>
        /// <returns>List of Byte Array represents its current file</returns>
        public static async Task<IEnumerable<byte[]>> ConvertFilesToBytesAsync(this IFormFileCollection files)
        {
            List<byte[]> ByteImages = new List<byte[]>(files.Count);
            foreach (var file in files) ByteImages.Add(await ConvertFileToBytesAsync(file));
            return ByteImages;
        }

        /// <summary>
        /// Store the current file in the server, for later use
        /// </summary>
        /// <param name="file">the file that you want to store in the server</param>
        /// <param name="directoryPath">the directory path, that you want to store in it.</param>
        /// <returns>Task of storing the file in the server</returns>
        /// <example>
        /// await StoreFileToServerFolderAsync(file,"images/files");
        /// </example>
        public static async Task StoreFileToServerFolderAsync(this IFormFile file, string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
            await file?.CopyToAsync(new FileStream($"{directoryPath}/{file.FileName}", FileMode.Create));
        }

        /// <summary>
        /// Store the current files in the server, for later use
        /// </summary>
        /// <param name="files">the files that you want to store in the server</param>
        /// <param name="directoryPath">the directory path, that you want to store in it.</param>
        /// <returns>Task of storing the files in the server</returns>
        /// <example>
        /// await StoreFilesToServerFolderAsync(files,"images/files");
        /// </example>
        public static async Task StoreFilesToServerFolderAsync(this IFormFileCollection files, string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
            foreach (var file in files)
            {
                await file?.CopyToAsync(new FileStream($"{directoryPath}/{file.FileName}", FileMode.Create));
            }
        }
    }
}
