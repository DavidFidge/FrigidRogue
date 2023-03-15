using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.View.Extensions;

public static class ColorExtensions
{
    /// <summary>
    /// Create a color with an alpha component
    /// </summary>
    /// <param name="color">Color</param>
    /// <param name="opacity">0 = transparent, 1 = opaque</param>
    /// <returns></returns>
    public static Color WithTransparency(this Color color, float opacity)
    {
        return new Color(color, opacity);
    }
    
    /// <summary>
    /// https://shawnhargreaves.com/blog/premultiplied-alpha.html
    /// </summary>
    /// <param name="color"></param>
    /// <param name="opacity"></param>
    /// <returns></returns>
    public static Color WithTransparencyPremultiplied(this Color color, float opacity)
    {
        return (color * opacity).WithTransparency(opacity);
    }
}