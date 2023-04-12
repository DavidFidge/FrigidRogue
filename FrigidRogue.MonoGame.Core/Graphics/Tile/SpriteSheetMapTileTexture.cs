using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads;

public class SpriteSheetMapTileTexture : IMapTileTexture
{
    private readonly SpriteSheetAnimation _spriteSheetAnimation;
    private readonly float _opacity;

    public float Opacity => _opacity;
    public SpriteSheetAnimation SpriteSheetAnimation => _spriteSheetAnimation;

    // Further away = 1.0, closest = 0.0.  Only set if you are using sprite batch draw mode Texture.
    protected float _spriteBatchDrawDepth;

    public SpriteSheetMapTileTexture(
        SpriteSheetAnimation spriteSheetAnimation,
        float opacity = 1f,
        float spriteBatchDrawDepth = 0)
    {
        _spriteSheetAnimation = spriteSheetAnimation;
        _opacity = opacity;
        _spriteBatchDrawDepth = spriteBatchDrawDepth;
    }

    public virtual void SpriteBatchDraw(SpriteBatch spriteBatch, Rectangle destinationRectangle, float? opacityOverride = null)
    {
        var drawColour = Color.White;

        if (opacityOverride is < 1f)
            drawColour.A = (byte)(opacityOverride * byte.MaxValue);
        else
            drawColour.A = (byte)(_opacity * byte.MaxValue);

        spriteBatch.Draw(_spriteSheetAnimation.CurrentFrame.Texture, destinationRectangle, _spriteSheetAnimation.CurrentFrame.Bounds, drawColour, 0f, Vector2.Zero, SpriteEffects.None, _spriteBatchDrawDepth);
    }

    public void Update(GameTime gameTime)
    {
        _spriteSheetAnimation.Update(gameTime);
    }

    public void Stop()
    {
        _spriteSheetAnimation.Stop();
    }

    public void Play()
    {
        _spriteSheetAnimation.Play();
    }
}