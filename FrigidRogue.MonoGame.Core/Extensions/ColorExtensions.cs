using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Example usage FromUInt(0xFFEE11)
        /// </summary>
        /// <param name="colourInt"></param>
        /// <returns></returns>
        public static Color FromUInt(int colourInt)
        {
            var colour = System.Drawing.Color.FromArgb(colourInt);
            return new Color(colour.R, colour.G, colour.B, colour.A);
        }

        public static Color FromName(string colorName)
        {
            System.Drawing.Color systemColor = System.Drawing.Color.FromName(colorName);
            return new Color(systemColor.R, systemColor.G, systemColor.B, systemColor.A);
        }
    }
}
