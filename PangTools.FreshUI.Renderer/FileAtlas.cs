using SixLabors.ImageSharp;

namespace PangTools.FreshUI.Renderer;

public class FileAtlas
{
    protected string[] FileNames;

    public FileAtlas(string[] fileNames)
    {
        FileNames = fileNames;
    }

    public bool HasFile(string fileName)
    {
        return FileNames.FirstOrDefault(file => file.Contains(fileName)) != null;
    }

    public string? GetFullPath(string fileName)
    {
        string fullPath = FileNames.FirstOrDefault(file => file.ToLower().Contains(fileName.ToLower()));

        if (fullPath != null)
        {
            return fullPath;
        }

        return null;
    }

    public Image? GetImage(string fileName)
    {
        string fullPath = FileNames.FirstOrDefault(file => file.ToLower().Contains(fileName.ToLower()));

        if (fullPath != null)
        {
            return Image.Load(fullPath);
        }

        return null;
    }
}