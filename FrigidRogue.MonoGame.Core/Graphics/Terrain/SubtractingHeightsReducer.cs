using FrigidRogue.MonoGame.Core.Interfaces.Graphics.Terrain;

namespace FrigidRogue.MonoGame.Core.Graphics.Terrain
{
    public class SubtractingHeightsReducer : IDiamondSquareHeightsReducer
    {
        private int _minHeightDeduction;
        private int _maxHeightDeduction;

        public decimal Scale { get; set; }

        public void Initialise(IDiamondSquare diamondSquare)
        {
            if (Scale == 0)
                Scale = 1;

            _maxHeightDeduction = diamondSquare.MaxHeight / (diamondSquare.NumberOfSteps * 2);
            _minHeightDeduction = diamondSquare.MinHeight / (diamondSquare.NumberOfSteps * 2);
        }

        public int ReduceMaxHeight(IDiamondSquare diamondSquare)
        {
            return diamondSquare.MaxHeight - (int)(_maxHeightDeduction * Scale);
        }

        public int ReduceMinHeight(IDiamondSquare diamondSquare)
        {
            return diamondSquare.MinHeight - (int)(_minHeightDeduction * Scale);
        }
    }
}