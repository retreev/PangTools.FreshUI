namespace PangTools.FreshUI.Renderer.Extensions;

public static class StringExtensions
{
    public static bool EndsWith(this string value, params string[] values)
    {
        return values.Any(value.EndsWith);
    }
}