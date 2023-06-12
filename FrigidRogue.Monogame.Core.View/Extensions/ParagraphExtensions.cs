using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.View.Extensions
{
    public static class ParagraphExtensions
    {
        public static Paragraph NoWrap(this Paragraph paragraph)
        {
            paragraph.WrapWords = false;

            return paragraph;
        }
        
        public static Paragraph Wrap(this Paragraph paragraph)
        {
            paragraph.WrapWords = true;

            return paragraph;
        }
        
        public static Rectangle GetTextDestRect(this Paragraph paragraph)
        {
            paragraph.CalcTextActualRectWithWrap();
            var paragraphTextRect = paragraph.GetActualDestRect();
            return paragraphTextRect;
        }
        
        public static Paragraph RecalculateWidth(this Paragraph paragraph)
        {
            paragraph.CalcTextActualRectWithWrap();
            var paragraphTextRect = paragraph.GetActualDestRect();
            paragraph.Width(paragraphTextRect.Width);

            return paragraph;
        }
        
        public static Paragraph RecalculateWidth(this Paragraph paragraph, int extraPadding)
        {
            paragraph.RecalculateWidth();
            paragraph.Width(paragraph.Size.X + extraPadding);

            return paragraph;
        }
    }
}
