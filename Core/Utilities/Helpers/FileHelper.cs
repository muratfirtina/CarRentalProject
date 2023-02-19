using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Core.Utilities.Helpers;

public class FileHelper
    {
        public static IResult Add(IFormFile file)
        {
            var fileExist = CheckFileExists(file);
            if (!fileExist.Success)
            {
                return new ErrorResult(fileExist.Message);
            }
            
            var type = Path.GetExtension(file.FileName);
            var typeValid = CheckFileTypeValid(type);
            if (!typeValid.Success)
            {
                return new ErrorResult(typeValid.Message);
            }
            
            (string newPath, string path2) result = SaveFile(file);
            try
            {
                var sourcePath = Path.GetTempFileName();
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(sourcePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                File.Move(sourcePath, result.newPath);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }

            return new SuccessResult(Path.Combine(result.path2));
            
            
        }

        public static IResult Update(IFormFile file, string sourcePath)
        {
            var fileExist = CheckFileExists(file);
            if (!fileExist.Success)
            {
                return new ErrorResult(fileExist.Message);
            }
            
            var type = Path.GetExtension(file.FileName);
            var typeValid = CheckFileTypeValid(type);
            if (!typeValid.Success)
            {
                return new ErrorResult(typeValid.Message);
            }
            
            
            var result = SaveFile(file);
            try
            {
                if (sourcePath.Length > 0)
                {
                    using (var stream = new FileStream(result.newPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                File.Delete(sourcePath);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult(result.Path2);
        }
        public static IResult Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }

        private static (string newPath, string Path2) SaveFile(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExtension = fileInfo.Extension;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string newFileName = Guid.NewGuid().ToString("D") + fileExtension;
            string newPath = Path.Combine(path, newFileName);
            

            string path2 = Path.Combine(newFileName);
            return (newPath, path2);
        }
        
        private static IResult CheckFileExists(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult("File Does Not Exist!");
        }
        
        private static IResult CheckFileTypeValid(string type)
        {
            if (type == ".jpeg" || type == ".png" || type == ".JPG" || type == ".jpg" || type == ".JPEG" || type == ".PNG")
            {
                return new SuccessResult();
            }
            return new ErrorResult("File Type Is Wrong! It Has To Be ('.jpeg', '.png' or '.jpg')");
            
        }
        private static void DeleteOldImageFile(string directory)
        {
            var fullDirectory = Environment.CurrentDirectory + "\\wwwroot" + directory;
            if (File.Exists(fullDirectory))
            {
                File.Delete(fullDirectory);
            }
        }
        
    }