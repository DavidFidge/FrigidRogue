using System.Diagnostics;
using System.Text;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class StringBuilderExtensions
    {
        public static void WriteToDebug(this StringBuilder stringBuilder)
        {
            foreach (var s in stringBuilder.ToString().Split(Environment.NewLine))
            {
                Debug.WriteLine(s);
            }
        }
    }
}
