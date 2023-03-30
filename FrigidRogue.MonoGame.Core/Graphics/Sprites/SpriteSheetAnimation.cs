using Animation = MonoGame.Extended.Sprites.Animation;
using SpriteSheetAnimationData = MonoGame.Extended.Sprites.SpriteSheetAnimationData;
using MonoGame.Extended.TextureAtlases;

namespace FrigidRogue.MonoGame.Core.Graphics.Sprites
{
    public class SpriteSheetAnimation : Animation
    {
        public const float DefaultFrameDuration = 0.2f;

        public SpriteSheetAnimation(string name, TextureAtlas textureAtlas, float frameDuration = DefaultFrameDuration,
            bool isLooping = true, bool isReversed = false)
            : this(name, textureAtlas.Regions.ToArray(), frameDuration, isLooping, isReversed)
        {
        }

        public SpriteSheetAnimation(string name, TextureRegion2D[] keyFrames, float frameDuration = DefaultFrameDuration,
            bool isLooping = true, bool isReversed = false)
            : base(null, false)
        {
            Name = name;
            KeyFrames = keyFrames;
            FrameDuration = frameDuration;
            IsLooping = isLooping;
            IsReversed = isReversed;
            CurrentFrameIndex = IsReversed ? KeyFrames.Length - 1 : 0;
        }

        public SpriteSheetAnimation(string name, TextureRegion2D[] keyFrames, SpriteSheetAnimationData data)
            : this(name, keyFrames, data.FrameDuration, data.IsLooping, data.IsReversed)
        {
        }

        public string Name { get; }
        public TextureRegion2D[] KeyFrames { get; }
        public float FrameDuration { get; set; }
        public bool IsLooping { get; set; }
        public bool IsReversed { get; set; }

        public float AnimationDuration => KeyFrames.Length*FrameDuration;

        public TextureRegion2D CurrentFrame => KeyFrames[CurrentFrameIndex];
        public int CurrentFrameIndex { get; private set; }

        public float FramesPerSecond
        {
            get => 1.0f/FrameDuration;
            set => FrameDuration = value/1.0f;
        }

        public Action OnCompleted { get; set; }

        // This method will not be called if IsComplete or IsPaused
        protected override bool OnUpdate(float deltaTime)
        {
            var isComplete = false;
            
            if (IsLooping)
            {
                CurrentFrameIndex = Math.Min(KeyFrames.Length - 1, (int)(CurrentTime % AnimationDuration));
            }
            else
            {
                CurrentFrameIndex = Math.Min(KeyFrames.Length - 1, (int)(CurrentTime / AnimationDuration));
                if (CurrentFrameIndex == KeyFrames.Length - 1)
                    isComplete = true;
            }

            if (IsReversed)
            {
                CurrentFrameIndex = Math.Abs(CurrentFrameIndex - KeyFrames.Length - 1);
            }

            return isComplete;
        }
    }
}