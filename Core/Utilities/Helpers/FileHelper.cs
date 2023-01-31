using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers;

public class FileHelper
{
    public static string Add(IFormFile file)
    {
        var result = newPath(file);
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
            return exception.Message;
        }

        return result.Path2;
    }

    public static string Update(string sourcePath, IFormFile file)
    {
        var result = newPath(file);
        try
        {
            if (file.Length >0)
            {
                using (var stream = new FileStream(sourcePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            File.Delete(sourcePath);
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
        return result.Path2;
    }

    public IResult Delete(string path)
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
    
    private static (string newPath, string Path2) newPath(IFormFile file)
    {
        FileInfo fileInfo = new FileInfo(file.FileName);
        string fileExtansion = fileInfo.Extension;
        

        string path = Environment.CurrentDirectory + @"\wwwroot\images";
        var newPath = Guid.NewGuid().ToString("N") + fileExtansion;

        string result = $@"{path}\{newPath}";
        return (result, $"\\images\\{newPath}");
    }
    
    private static bool IsValidImage(IFormFile file)
    {
        string[] allowedTypes = { "image/jpeg", "image/png", "image/jpg" };

        if (Array.IndexOf(allowedTypes, file.ContentType) < 0)
        {
            return false;
        }

        return true;
    }
}