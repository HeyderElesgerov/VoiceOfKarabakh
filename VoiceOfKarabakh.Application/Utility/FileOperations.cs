using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VoiceOfKarabakh.Application.Utility
{
    public static class FileOperations
    {
        public static bool Upload(IFormFile formFile, string filePath)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }

                return true;
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }

            return false;
        }

        public static bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }

            return false;
        }

        public static string GenerateFilePath(string wwwrootFolder, string photosFolder, IFormFile formFile)
        {
            string uploadFolder = wwwrootFolder + "/" + photosFolder;
            string fileExtension = Path.GetExtension(formFile.FileName);
            string newFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = uploadFolder + "/" + newFileName;

            return filePath;
        }
    }
}
