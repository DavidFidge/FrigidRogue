namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class ArrayExtensions
    {
        public static void CopyInto<T>(this T[] array, T[] destination, int sourceWidth, int destWidth)
        {
            var destHeight = destination.Length / destWidth;
            var sourceHeight = array.Length / sourceWidth;
            
            var copyLength = Math.Min(destWidth, sourceWidth);
            var copyHeight = Math.Min(destHeight, sourceHeight);
            
            for (var y = 0; y < copyHeight; y++)
            {
                var sourceIndexStart = y * sourceWidth;
                var destIndexStart = y * destWidth;
                
                Array.Copy(array, sourceIndexStart, destination, destIndexStart, copyLength);
            }
        }
    }
}
