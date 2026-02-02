namespace PangTools.FreshUI.Renderer.Helpers;

public static class ColorHelper
{
    public static string ARGBtoRBGA(string hexColor)
    {
        if (hexColor.StartsWith("0x"))
        {
            hexColor = hexColor.Substring(2);
        }
        
        string alpha = hexColor.Substring(0, 2);

        return hexColor.Substring(2) + alpha;
    }
}