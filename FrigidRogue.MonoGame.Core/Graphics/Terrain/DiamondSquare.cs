using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics.Terrain;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics.Terrain
{
    public class DiamondSquare : IDiamondSquare
    {
        private readonly IRandom _random;
        private Point _midPoint;
        private List<Square> _squares;

        public IDiamondSquareHeightsReducer HeightsReducer { get; set; }
        public int CurrentStep { get; private set; }
        public HeightMap HeightMap { get; private set; }
        public int MaxHeight { get; private set; }
        public int MinHeight { get; private set; }
        public int NumberOfSteps { get; private set; }

        public DiamondSquare(IRandom random)
        {
            _random = random;

            if (HeightsReducer == null)
                HeightsReducer = new DividingHeightsReducer();
        }

        public IDiamondSquare Execute(int heightMapSize, int minHeight, int maxHeight)
        {
            MaxHeight = maxHeight;
            MinHeight = minHeight;

            _squares = new List<Square>();

            if (Math.Log(heightMapSize, 2) % (int)Math.Log(heightMapSize, 2) > 0.0001d)
            {
                throw new Exception("Diamond square currently only supports square height maps with size equal to square root 2.  If your height map is a different size then generate a diamond square map bigger and patch your height map with a portion of it");
            }

            if (heightMapSize < 2)
                throw new Exception("Height map size must be at least 4");

            NumberOfSteps = (int) Math.Log(heightMapSize, 2);
            CurrentStep = NumberOfSteps;

            HeightMap = new HeightMap(heightMapSize + 1, heightMapSize + 1);

            _midPoint = new Point(HeightMap.Width / 2, HeightMap.Width / 2);

            HeightMap[_midPoint.X, _midPoint.Y] = MaxHeight;

            _squares.Add(
                new Square(
                    new Point(0, 0),
                    new Point(0, heightMapSize),
                    new Point(heightMapSize, 0),
                    new Point(heightMapSize, heightMapSize)
                ));

            HeightsReducer.Initialise(this);

            while (CurrentStep > 0)
            {
                ExecuteNextDiamondSquareStep();
                CurrentStep--;
            }

            return this;
        }

        private void ExecuteNextDiamondSquareStep()
        {
            var newSquares = new List<Square>();
            var squareStepPoints = new HashSet<Point>();

            var diamondPointWidth = 0;

            foreach (var squarePart in _squares)
            {
                var midPoint = squarePart.Midpoint;

                var heightRanges = squarePart.Points
                    .Select(squarePoint => HeightMap[squarePoint.X, squarePoint.Y])
                    .ToList();

                SetHeight(midPoint, heightRanges);

                BuildNewSquares(midPoint, squarePart, newSquares, squareStepPoints);

                if (diamondPointWidth == 0)
                    diamondPointWidth = midPoint.X - squarePart.LeftX;
            }

            if (CurrentStep != NumberOfSteps)
            {
                ReduceHeightLimits();
            }

            foreach (var squareStepPoint in squareStepPoints)
            {
                var heightRanges = squareStepPoint
                    .PointsOutwardsFrom(diamondPointWidth, 0, HeightMap.Width - 1, 0, HeightMap.Length - 1)
                    .Select(squarePoint => HeightMap[squarePoint.X, squarePoint.Y])
                    .ToList();

                SetHeight(squareStepPoint, heightRanges);
            }

            ReduceHeightLimits();

            _squares = newSquares;
        }

        private void ReduceHeightLimits()
        {
            MaxHeight = HeightsReducer.ReduceMaxHeight(this);
            MinHeight = HeightsReducer.ReduceMinHeight(this);

            if (MaxHeight < MinHeight)
                throw new Exception("MaxHeight cannot be less than MinHeight");
        }

        private void SetHeight(Point squareStepPoint, List<int> surroundingHeights)
        {
            if (squareStepPoint != _midPoint)
            {
                HeightMap[squareStepPoint.X, squareStepPoint.Y] = (int)surroundingHeights.Average() + _random.Next(MinHeight, MaxHeight);
            }
        }

        private void BuildNewSquares(Point midPoint, Square currentSquare, List<Square> newSquares, HashSet<Point> squareStepPoints)
        {
            // Top Left quadrant
            var diamondPoint1 = new Point(currentSquare.LeftX, midPoint.Y);
            var diamondPoint2 = new Point(midPoint.X, currentSquare.TopY);

            var newSquare = new Square(
                midPoint,
                new Point(currentSquare.LeftX, currentSquare.TopY),
                diamondPoint1,
                diamondPoint2
            );

            newSquares.Add(newSquare);
            squareStepPoints.Add(diamondPoint1);
            squareStepPoints.Add(diamondPoint2);

            // Top Right quadrant
            diamondPoint1 = new Point(currentSquare.RightX, midPoint.Y);
            diamondPoint2 = new Point(midPoint.X, currentSquare.TopY);

            newSquare = new Square(
                midPoint,
                new Point(currentSquare.RightX, currentSquare.TopY),
                diamondPoint1,
                diamondPoint2
            );

            newSquares.Add(newSquare);
            squareStepPoints.Add(diamondPoint1);
            squareStepPoints.Add(diamondPoint2);

            // Bottom left quadrant
            diamondPoint1 = new Point(currentSquare.LeftX, midPoint.Y);
            diamondPoint2 = new Point(midPoint.X, currentSquare.BottomY);

            newSquare = new Square(
                midPoint,
                new Point(currentSquare.LeftX, currentSquare.BottomY),
                diamondPoint1,
                diamondPoint2
            );

            newSquares.Add(newSquare);
            squareStepPoints.Add(diamondPoint1);
            squareStepPoints.Add(diamondPoint2);

            // Bottom right quadrant
            diamondPoint1 = new Point(currentSquare.RightX, midPoint.Y);
            diamondPoint2 = new Point(midPoint.X, currentSquare.BottomY);

            newSquare = new Square(
                midPoint,
                new Point(currentSquare.RightX, currentSquare.BottomY),
                diamondPoint1,
                diamondPoint2
            );

            newSquares.Add(newSquare);
            squareStepPoints.Add(diamondPoint1);
            squareStepPoints.Add(diamondPoint2);
        }

        public class Square
        {
            public Square(Point point1, Point point2, Point point3, Point point4)
            {
                Points = new List<Point> { point1, point2, point3, point4 };
            }

            public List<Point> Points { get; set; }

            public int TopY => Points.Max(p => p.Y);
            public int BottomY => Points.Min(p => p.Y);
            public int LeftX => Points.Min(p => p.X);
            public int RightX => Points.Max(p => p.X);

            public Point Midpoint => Points.GetMidpoint();

            public int Width => RightX - LeftX;
            public int Height => TopY - BottomY;
        }
    }
}
