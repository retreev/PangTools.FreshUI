using SixLabors.ImageSharp;

namespace PangTools.FreshUI.Renderer.Helpers;

public class DimensionHelper
{
    public static Point? ParsePoint(string text)
    {
        int[] point = TextToIntArray(text);

        if (point.Length != 2)
        {
            return null;
        }
        
        return new Point(point[0], point[1]);
    }

    public static Size? ParseSize(string text)
    {
        int[] size = TextToIntArray(text);

        if (size.Length != 2)
        {
            return null;
        }

        return new Size(size[0], size[1]);
    }

    public static RectangleF? ParseRectangle(string text)
    {
        int[]? rect = TextToIntArray(text);

        if (rect == null || rect.Length != 4)
        {
            return null;
        }

        return new RectangleF(
            rect[0], 
            rect[1], 
            rect[2] - rect[0], 
            rect[3] - rect[1]);
    }

    public static int[]? TextToIntArray(string? text)
    {
        return text?.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(Int32.Parse).ToArray();
    }
}