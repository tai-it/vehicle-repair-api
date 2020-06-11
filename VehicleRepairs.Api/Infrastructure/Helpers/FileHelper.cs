namespace VehicleRepairs.Api.Infrastructure.Helpers
{
    using Microsoft.AspNetCore.Http;
    using System.IO;

    public class FileHelper
    {
        public static string SaveFile(string folder, string fileName, IFormFile file)
        {
            var filePath = MapPath("/Files/" + folder);

            Directory.CreateDirectory(filePath);

            var returnPath = string.Empty;

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                returnPath = string.Format("/Files/{0}/{1}", folder, fileName);
            }

            return returnPath;
        }

        public static string GetFile(string folder, string fileName)
        {
            var filePath = MapPath("/Files/" + folder + "/" + fileName);
            return filePath;
        }

        public static void RemoveFile(string folder, string fileName)
        {
            var filePath = MapPath("/Files/" + folder + "/" + fileName);
            File.Delete(filePath);
        }

        public static string MapPath(string path, string basePath = null)
        {
            if (string.IsNullOrEmpty(basePath))
            {
                basePath = "wwwroot";
            }

            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(basePath, path);
        }
    }
}
