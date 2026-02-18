using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class ColorExtensions
    {
        public static Color ToXna(this SadRogue.Primitives.Color c)
        {
            return new Color(c.R, c.G, c.B, c.A);
        }
    }
}
