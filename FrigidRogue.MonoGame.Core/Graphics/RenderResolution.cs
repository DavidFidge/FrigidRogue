using System.Collections.Generic;
using System.Linq;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class RenderResolution
    {
        public static IList<RenderResolution> RenderResolutions = new List<RenderResolution>
        {
            new RenderResolution(1280, 720, "High Definition"),
            new RenderResolution(1920, 1080, "Full HD"),
            new RenderResolution(2560, 1440, "Quad HD"),
            new RenderResolution(3840, 2160, "Ultra-High Definition"),
            new RenderResolution(7680, 4320, "8K")
        };

        public static RenderResolution Default => RenderResolutions.Single(r => r.Height == 1080);

        public int Width { get; }
        public int Height { get; }
        public string Name { get; }

        public RenderResolution(int width, int height, string name)
        {
            Width = width;
            Height = height;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Height}p";
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is RenderResolution renderResolution)
                return Equals(renderResolution);

            return false;
        }

        public bool Equals(RenderResolution renderResolution)
        {
            return Height == renderResolution.Height;
        }

        public override int GetHashCode()
        {
            return Height.GetHashCode();
        }
    }
}