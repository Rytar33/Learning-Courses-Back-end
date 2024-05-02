using System.Text;

namespace Application.Services.IOs;

public static class FileLocalStorageServices
{
    public static async Task ImportManyFile(string dirPath, ICollection<IFormFile> files)
    {
        foreach (var file in files)
            await ImportSingleFile(dirPath, file);
    }
    public static async Task ImportSingleFile(string dirPath, IFormFile file)
    {
        if (file.Length > 0)
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

    public static string? ExportFullPathFile(string dirPath)
    {
        var dirInfo = new DirectoryInfo(dirPath);
        string? fullPath = null;
            
        if (dirInfo.Exists) fullPath = Directory.GetFiles(dirInfo.FullName).FirstOrDefault();
        if (fullPath != null) fullPath = GetPathForWeb(dirInfo, Path.GetFileName(fullPath));
        return fullPath;
    }
    public static string[] ExportFullPathsFile(string dirPath)
    {
        var fullPaths = new List<string>();
        var dirInfo = new DirectoryInfo(dirPath);
        if (dirInfo.Exists)
            fullPaths.AddRange(
                Directory
                    .GetFiles(dirInfo.FullName)
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
