using SpriteSheetAnimationCycle = MonoGame.Extended.Sprites.SpriteSheetAnimationCycle;
using MonoGame.Extended.TextureAtlases;

namespace FrigidRogue.MonoGame.Core.Graphics.Sprites
{
    public class SpriteSheet
    {
        public SpriteSheet()
        {
            Cycles = new Dictionary<string, SpriteSheetAnimationCycle>();
        }

        public TextureAtlas TextureAtlas { get; set; }
        public Dictionary<string, SpriteSheetAnimationCycle> Cycles { get; set; }

        public SpriteSheetAnimation CreateAnimation(string name)
        {
            var cycle = Cycles[name];
            var keyFrames = cycle.Frames
                .Select(f => TextureAtlas[f.Index])
                .ToArray();

            return new SpriteSheetAnimation(name, keyFrames, cycle.FrameDuration, cycle.IsLooping, cycle.IsReversed);
        }
    }
}