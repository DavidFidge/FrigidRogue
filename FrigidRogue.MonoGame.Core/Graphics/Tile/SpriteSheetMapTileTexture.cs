using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads;

public class SpriteSheetMapTileTexture : IMapTileTexture
{
    private readonly AnimatedSprite _animatedSprite;
    private readonly float _opacity;

    public float Opacity => _opacity;
    public AnimatedSprite AnimatedSprite => _animatedSprite;

    // Further away = 1.0, closest = 0.0.  Only set if you are using sprite batch draw mode Texture.
    protected float _spriteBatchDrawDepth;

    public SpriteSheetMapTileTexture(
        AnimatedSprite animatedSprite,
        float opacity = 1f,
        float spriteBatchDrawDepth = 0)
    {
        _animatedSprite = animatedSprite;
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

        spriteBatch.Draw(_animatedSprite.TextureRegion.Texture, destinationRectangle, _animatedSprite.TextureRegion.Bounds, drawColour, 0f, Vector2.Zero, SpriteEffects.None, _spriteBatchDrawDepth);
    }

    public void Update(GameTime gameTime)
    {
        _animatedSprite.Update(gameTime);
    }

    public void Stop()
    {
        _animatedSprite.Controller.Stop();
    }

    public void Play()
    {
        _animatedSprite.Controller.Play();
    }
}