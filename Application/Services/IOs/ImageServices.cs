using System.Text;
using Microsoft.AspNetCore.Http;

namespace Application.Services.IOs;

public class ImageServices
{
    public static async Task ImportManyFile(string dirPath, ICollection<IFormFile> files)
    {
        foreach (var file in files)
            await ImportSingleFile(dirPath, file);
    }
    public static async Task ImportSingleFile(string dirPath, IFormFile file)
    {
        if (file.Length > 0 && (file.FileName.Contains(".jpg") || file.FileName.Contains(".png") || file.FileName.Contains(".bmp")))
        {
            var dirPathInfo = new DirectoryInfo(dirPath);
            if (!dirPathInfo.Exists) dirPathInfo.Create();
            var fullPath = Path.Combine(dirPathInfo.FullName, file.FileName);
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }

    private static string GetPathForWeb(DirectoryInfo dirInfo, string fileName)
    {
        var webPath = new StringBuilder($"/{fileName}");
        while (true) 
        {
            if (dirInfo.Name == "wwwroot") return webPath.ToString();
            webPath.Insert(0, $"/{dirInfo.Name}");
            dirInfo = dirInfo.Parent!;
        }
    }

    public static string? ExportFullPathImage(string dirPath)
    {
        var dirInfo = new DirectoryInfo(dirPath);
        string? fullPath = null;
            
        if (dirInfo.Exists) fullPath = Directory.GetFiles(dirInfo.FullName).FirstOrDefault();
        if (fullPath != null) fullPath = GetPathForWeb(dirInfo, Path.GetFileName(fullPath));
        return fullPath;
    }
    public static string[] ExportFullPathsImage(string dirPath)
    {
        var fullPaths = new List<string>();
        var dirInfo = new DirectoryInfo(dirPath);
        if (dirInfo.Exists)
            fullPaths.AddRange(
                Directory
                    .GetFiles(dirInfo.FullName)
                    .Where(f => f.Contains(".jpg") 
                                || f.Contains(".png") 
                                || f.Contains(".bmp"))
                    .Select(i => GetPathForWeb(dirInfo, Path.GetFileName(i))));
        return fullPaths.ToArray();
    }
    public static void RemovePathWithFiles(string dirPath)
    { 
        var dirInfo = new DirectoryInfo(dirPath);
        if (!dirInfo.Exists) return; 
        dirInfo.Delete();
    }
}
