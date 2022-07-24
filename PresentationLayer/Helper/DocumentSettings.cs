using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace PresentationLayer.Helper
{
    public class DocumentSettings
    {
        public static string Upload(IFormFile File , string FolderName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/", FolderName);

            var fileName = Guid.NewGuid()+Path.GetFileName(File.Name);

            var filePath = Path.Combine(FolderPath, fileName);

            using var fs = new FileStream(filePath, FileMode.Create);

            File.CopyTo(fs);

            return fileName;
        }


        public static void Delete(string fileName, string FolderName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/", FolderName , fileName);
            if(File.Exists(FilePath))
                File.Delete(FilePath);

            
        }
    }
}
