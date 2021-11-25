using GeonBit.UI.Entities;

namespace DavidFidge.Monogame.Core.View.Extensions
{
    public static class LabelExtensions
    {
        public static Label H1Heading(this Label label)
        {
            label.Scale = 3f;
            return label;
        }

        public static Label H2Heading(this Label label)
        {
            label.Scale = 2f;
            return label;
        }

        public static Label H3Heading(this Label label)
        {
            label.Scale = 1.5f;
            return label;
        }

        public static Label H4Heading(this Label label)
        {
            label.Scale = 1.4f;
            return label;
        }

        public static Label H5Heading(this Label label)
        {
            label.Scale = 1.3f;
            return label;
        }

        public static Label H6Heading(this Label label)
        {
            label.Scale = 1.2f;
            return label;
        }

        public static Label DefaultFontSize(this Label label)
        {
            label.Scale = 1.2f;
            return label;
        }
    }
}
