using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Core.Utilities.Helpers;

public class FileHelper
    {
        private static string _currentFileDirectory = Environment.CurrentDirectory + "\\wwwroot";
        private static string _folderName = "Images/";
        private static string _webRootPath =Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/");

        // public FileHelper(IHostingEnvironment _hostingEnvironment)
        // {
        //     WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        // }
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

            var randomGuid = Guid.NewGuid().ToString();

            
            var directory = Path.Combine(_webRootPath, _folderName);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            var fileDirectory = directory + randomGuid + type;

            CheckFileDirectoryExist(directory);
            CreateImageFile(fileDirectory, file);

            var fileAddressToBeSavedOnDatabase = randomGuid + type;
            return new SuccessResult(fileAddressToBeSavedOnDatabase.Replace("\\", "/"));
        }

        public static IResult Update(IFormFile file, string imagePath)
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
            var randomGuid = Guid.NewGuid().ToString();

            var directory = _currentFileDirectory + _folderName;
            var fileDirectory = directory + randomGuid + type;

            DeleteOldImageFile(imagePath.Replace("/", "\\"));
            CheckFileDirectoryExist(directory);
            CreateImageFile(fileDirectory, file);

            // Message of the result returns the ImagePath of added image.
            var fileAddressToBeSavedOnDatabase = _folderName + randomGuid + type;
            return new SuccessResult(fileAddressToBeSavedOnDatabase.Replace("\\", "/"));
        }


        public static IResult Delete(string path)
        {
            DeleteOldImageFile(path.Replace("/", "\\"));
            return new SuccessResult();
        }



        private static IResult CheckFileTypeValid(string type)
        {
            if (type == ".jpeg" || type == ".png" || type == ".jpg")
            {
                return new SuccessResult();
            }
            return new ErrorResult("File Type Is Wrong! It Has To Be ('.jpeg', '.png' or '.jpg')");
        }


        private static IResult CheckFileExists(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult("File Does Not Exist!");
        }

        private static void DeleteOldImageFile(string directory)
        {
            var fullDirectory = Environment.CurrentDirectory + "\\wwwroot" + directory;
            if (File.Exists(fullDirectory))
            {
                File.Delete(fullDirectory);
            }
        }

        private static void CheckFileDirectoryExist(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static void CreateImageFile(string directory, IFormFile file)
        {
            using (FileStream fileStream = File.Create(directory))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
        }
    }