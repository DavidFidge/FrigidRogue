namespace FrigidRogue.MonoGame.Core.Interfaces.Graphics.Terrain
{
    public interface IDiamondSquareHeightsReducer
    {
        void Initialise(IDiamondSquare diamondSquare);
        int ReduceMaxHeight(IDiamondSquare diamondSquare);
        int ReduceMinHeight(IDiamondSquare diamondSquare);
    }
}