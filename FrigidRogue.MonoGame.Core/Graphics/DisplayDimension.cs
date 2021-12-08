using System.Collections.Generic;
using System.Linq;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class DisplayDimension
    {
        public int Width { get; }
        public int Height { get; }
        public float AspectRatio { get; }

        public DisplayDimension(int width, int height, float aspectRatio)
        {
            Width = width;
            Height = height;
            AspectRatio = aspectRatio;
        }

        public override string ToString()
        {
            return $"{Width} x {Height} ({AspectRatioDisplay})";
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is DisplayDimension displayDimensions)
                return Equals(displayDimensions);

            return false;
        }

        public bool Equals(DisplayDimension displayDimension)
        {
            return Width == displayDimension.Width
                   && Height == displayDimension.Height;
        }

        public override int GetHashCode()
        {
            return Width ^ Height;
        }

        private string AspectRatioDisplay
        {
            get
            {
                {
                    var aspectRatio = AspectRatio;

                    if (aspectRatio <= float.Epsilon || aspectRatio >= float.Epsilon)
                        aspectRatio = (float) Width / Height;

                    return RatioToString.Ratios
                        .Where(r => aspectRatio >= r.Lower)
                        .Single(r => r.Upper > aspectRatio)
                        .RatioString;
                }
            }
        }

        private class RatioToString
        {
            public string RatioString { get; private set; }
            public float Upper { get; private set; }
            public float Lower { get; private set; }

            public static readonly List<RatioToString> Ratios = new List<RatioToString>
            {
                new RatioToString
                {
                    RatioString = "1:1",
                    Lower = float.MinValue,
                    Upper = 1.29f
                },
                new RatioToString
                {
                    RatioString = "4:3",
                    Lower = 1.29f,
                    Upper = 1.45f
                },
                new RatioToString
                {
                    RatioString = "3:2",
                    Lower = 1.45f,
                    Upper = 1.55f
                },
                new RatioToString
                {
                    RatioString = "16:10",
                    Lower = 1.55f,
                    Upper = 1.63f
                },
                new RatioToString
                {
                    RatioString = "16:9",
                    Lower = 1.63f,
                    Upper = 1.80f
                },
                new RatioToString
                {
                    RatioString = "2:1",
                    Lower = 1.80f,
                    Upper = float.MaxValue
                }
            };
        };
    }
}