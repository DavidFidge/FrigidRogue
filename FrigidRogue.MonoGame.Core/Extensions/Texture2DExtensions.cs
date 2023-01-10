using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class Texture2DExtensions
    {
        public static void ExportToPng(this Texture2D texture, string filenameWithoutExtension)
        {
            using (var fileStream = File.OpenWrite($"{filenameWithoutExtension}.png"))
            {
                texture.SaveAsPng(fileStream, texture.Width, texture.Height);
                
                fileStream.Close();
            }
            
        }
        
        public static void ExportToJpeg(this Texture2D texture, string filenameWithoutExtension)
        {
            using (var fileStream = File.OpenWrite($"{filenameWithoutExtension}.jpg"))
            {
                texture.SaveAsJpeg(fileStream, texture.Width, texture.Height);
                
                fileStream.Close();
            }        
        }
    }
}
