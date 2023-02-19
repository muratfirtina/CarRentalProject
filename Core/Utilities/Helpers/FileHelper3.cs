using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using SkiaSharp;

namespace Core.Utilities.Helpers;

public class FileHelper3 //frontendden gelen imajın pixel boyutunu azaltıyor.
{
        private static string _currentFileDirectory = Environment.CurrentDirectory + "\\wwwroot";
        private static string _folderName = "Images/";
        private static string _webRootPath =Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/");

        
        public static IResult Add(IFormFile file, int maxWidth, int maxHeight)
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
            
            var extension = Path.GetExtension(file.FileName);
            var guid = Guid.NewGuid().ToString();
            var fileName = $"{guid}{extension}";
            var thumbnailFilePath = Path.Combine(_webRootPath, _folderName, fileName);

            using (var stream = new FileStream(thumbnailFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            ResizeImage(thumbnailFilePath, maxWidth, maxHeight);

            var imagePath = Path.Combine(fileName);
            
            return new SuccessResult(imagePath);
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
        
        private static void ResizeImage(string filePath, int maxWidth, int maxHeight)
        {
            using (var original = SKBitmap.Decode(filePath))
            {
                var scaled = original.Resize(new SKImageInfo(maxWidth, maxHeight), SKFilterQuality.Medium);
                var image = SKImage.FromBitmap(scaled);
        
                // Dosya adı ve kayıt yolu oluşturma
                var fileName = Path.GetFileName(filePath);
                var path = Path.Combine(Path.GetDirectoryName(filePath), fileName);
                
                // Resmi dosyaya kaydetme
                using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 100))
                using (var stream = File.OpenWrite(path))
                {
                    data.SaveTo(stream);
                }
                    
                
                
            }
        }



        public static IResult Delete(string path)
        {
            DeleteOldImageFile(path.Replace("/", "\\"));
            return new SuccessResult();
        }



        private static IResult CheckFileTypeValid(string type)
        {
            if (type == ".jpeg" || type == ".png" || type == ".JPG" || type == ".jpg" || type == ".JPEG" || type == ".PNG")
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