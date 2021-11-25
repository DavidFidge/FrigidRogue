using FrigidRogue.MonoGame.Core.Interfaces.Graphics.Terrain;

namespace FrigidRogue.MonoGame.Core.Graphics.Terrain
{
    public class DividingHeightsReducer : IDiamondSquareHeightsReducer
    {
        public int Divisor { get; set; }

        public void Initialise(IDiamondSquare diamondSquare)
        {
            if (Divisor == 0)
                Divisor = 2;
        }

        public int ReduceMaxHeight(IDiamondSquare diamondSquare)
        {
            return diamondSquare.MaxHeight / Divisor;
        }

        public int ReduceMinHeight(IDiamondSquare diamondSquare)
        {
            return diamondSquare.MinHeight / Divisor;
        }
    }
}