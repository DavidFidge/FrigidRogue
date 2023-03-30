using FrigidRogue.TestInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using SpriteSheetAnimation = FrigidRogue.MonoGame.Core.Graphics.Sprites.SpriteSheetAnimation;

namespace FrigidRogue.MonoGame.Core.Tests.Graphics.Sprites;

[TestClass]
public class SpriteSheetAnimationTests : BaseGraphicsTest
{
    [DataTestMethod]
    [DataRow(0, 0)]
    [DataRow(0, 0.9f)]
    [DataRow(1, 1f)]
    [DataRow(1, 1.9f)]
    [DataRow(0, 2f)]
    [DataRow(0, 2.9f)]
    [DataRow(1, 3f)]
    public void Looping_Animation_Should_Return_Correct_Frame(int expectedTextureRegionIndex, float time)
    {
        // Arrange
        var textureRegion2D1 = new TextureRegion2D(null);
        var textureRegion2D2 = new TextureRegion2D(null);

        var textureRegions = new[] { textureRegion2D1, textureRegion2D2 };

        var spriteSheetAnimationData = new SpriteSheetAnimationData(
            new []{ 0, 1 },
            1,
            true,
            false
            );
        
        var spriteSheetAnimation = new SpriteSheetAnimation("Test", textureRegions, spriteSheetAnimationData);
        FakeStopwatchProvider.Elapsed = TimeSpan.FromSeconds(time);
        
        // Act
        spriteSheetAnimation.Play();
        spriteSheetAnimation.Update(FakeGameTimeService.GameTime);
        
        // Assert
        Assert.AreEqual(textureRegions[expectedTextureRegionIndex], spriteSheetAnimation.CurrentFrame);
        Assert.IsFalse(spriteSheetAnimation.IsComplete);
    }
}