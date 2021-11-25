using DavidFidge.MonoGame.Core.Graphics.Terrain;

namespace DavidFidge.MonoGame.Core.Interfaces.Graphics.Terrain
{
    public interface IDiamondSquare
    {
        IDiamondSquareHeightsReducer HeightsReducer { get; set; }
        int CurrentStep { get; }
        HeightMap HeightMap { get; }
        int MaxHeight { get; }
        int MinHeight { get; }
        int NumberOfSteps { get; }
        IDiamondSquare Execute(int heightMapSize, int minHeight, int maxHeight);
    }
}