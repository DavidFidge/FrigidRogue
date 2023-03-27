using FrigidRogue.MonoGame.Core.Interfaces.Services;
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

    public SpriteSheetMapTileTexture(SpriteSheetAnimation spriteSheetAnimation, float opacity = 1f)
    {
        _spriteSheetAnimation = spriteSheetAnimation;
        _opacity = opacity;
    }

    public virtual void SpriteBatchDraw(SpriteBatch spriteBatch, Rectangle destinationRectangle, float? opacityOverride = null)
    {
        var drawColour = Color.White;

        if (opacityOverride is < 1f)
            drawColour.A = (byte)(opacityOverride * byte.MaxValue);
        else
            drawColour.A = (byte)(_opacity * byte.MaxValue);

        spriteBatch.Draw(_spriteSheetAnimation.CurrentFrame.Texture, destinationRectangle, _spriteSheetAnimation.CurrentFrame.Bounds, drawColour, 0f, Vector2.Zero, SpriteEffects.None, 0);  // TODO - likely we don't need layers anymore due to use of texture atlases combined with known ordering of tile draws //_spriteBatchDrawDepth);
    }

    public void Update(IGameTimeService gameTimeService)
    {
        _spriteSheetAnimation.Update(gameTimeService.GameTime);
    }
}