using System;
using System.Collections.Generic;
using System.Linq;

using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics.Terrain;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics.Terrain
{
    public class HeightMapGenerator : IHeightMapGenerator
    {
        private readonly IRandom _random;
        private HeightMap _heightMap;

        public HeightMapGenerator(IRandom random)
        {
            _random = random;
        }

        public HeightMap HeightMap()
        {
            return _heightMap;
        }

        public HeightMapGenerator CreateHeightMap(int width, int length)
        {
            _heightMap = new HeightMap(width, length);
            return this;
        }

        public HeightMapGenerator Randomise(int patchX, int patchY)
        {
            for (var y = 0; y < patchY - 1; y++)
            {
                for (var x = 0; x < patchX - 1; x++)
                {

                    _heightMap[x, y] = _random.Next(0, 2);
                }
            }

            return this;
        }

        public HeightMapGenerator WithHills(float hillFrequency, int maxHillHeight)
        {

            if (hillFrequency == 0f || hillFrequency > 1f)
                throw new ArgumentException($"{nameof(hillFrequency)} must be > 0 or <= 1", nameof(hillFrequency));

            if (maxHillHeight == 0)
                throw new ArgumentException($"{nameof(maxHillHeight)} must be at least 1", nameof(maxHillHeight));

            while (_heightMap.ZeroAreaPercent() > 1f - hillFrequency )
            {
                var zeroHeights = _heightMap
                    .Select((h, i) => new { Index = i, Height = h })
                    .Where(h => h.Height == 0)
                    .Select(h => h.Index)
                    .ToArray();

                var nextPoint = zeroHeights[_random.Next(0, zeroHeights.Length)];

                var hillSizeVector = new Vector2(
                    _random.Next(10, 100) / 100f,
                    _random.Next(10, 100) / 100f
                    );

                Hill(
                    new Point(
                        nextPoint % _heightMap.Length,
                        nextPoint / _heightMap.Length
                    ),
                    hillSizeVector,
                    _random.Next(1, maxHillHeight)
                );
            }

            return this;
        }

        public HeightMapGenerator Hill(
            Vector2 relativeCentre,
            Vector2 relativeSize,
            int height,
            HeightMap.PatchMethod patchMethod = Terrain.HeightMap.PatchMethod.ReplaceIfHigher
        )
        {
            var centre = GetActualPointFromRelativeVector(relativeCentre);

            return Hill(centre, relativeSize, height, patchMethod);
        }

        public HeightMapGenerator Hill(
            Point centre,
            Vector2 relativeSize,
            int height,
            HeightMap.PatchMethod patchMethod = Terrain.HeightMap.PatchMethod.ReplaceIfHigher
            )
        {
            if (relativeSize.X > 1.0f || relativeSize.X < 0f)
                throw new ArgumentException("x must be between 0-1 inclusive", nameof(relativeSize));

            if (relativeSize.Y > 1.0f || relativeSize.Y < 0f)
                throw new ArgumentException("y must be between 0-1 inclusive", nameof(relativeSize));

            var hillEllipseRadius = new Point
            (
                (int) Math.Round(relativeSize.X * _heightMap.Width / 2f, MidpointRounding.AwayFromZero),
                (int) Math.Round(relativeSize.Y * _heightMap.Length / 2f, MidpointRounding.AwayFromZero)
            );

            var hillCentre = new Point(hillEllipseRadius.X + 1, hillEllipseRadius.Y + 1);

            var heightMapPatch = new HeightMap(hillEllipseRadius.X * 2 + 1, hillEllipseRadius.Y * 2 + 1);

            heightMapPatch[hillCentre.X, hillCentre.Y] = height;

            for (var y = 0; y < heightMapPatch.Length; y++)
            {
                for (var x = 0; x < heightMapPatch.Width; x++)
                {

                    var candidatePoint = new Point(x, y);

                    if (candidatePoint == hillCentre)
                        continue;

                    // See if the point is within the radius of ellipse
                    // Origin being point 0,0
                    var candidatePointRelativeToOrigin = candidatePoint - hillCentre;

                    var angle = Math.Atan2(candidatePointRelativeToOrigin.Y, candidatePointRelativeToOrigin.X);

                    var ellipseX = (hillEllipseRadius.X * hillEllipseRadius.Y) /
                        Math.Sqrt(Math.Pow(hillEllipseRadius.Y, 2) + Math.Pow(hillEllipseRadius.X, 2) * Math.Pow(Math.Tan(angle), 2));

                    if (angle < -Math.PI / 2 || angle > Math.PI / 2)
                        ellipseX = -ellipseX;

                    var ellipseY = (hillEllipseRadius.X * hillEllipseRadius.Y) /
                                   Math.Sqrt(Math.Pow(hillEllipseRadius.X, 2) + Math.Pow(hillEllipseRadius.Y, 2) / Math.Pow(Math.Tan(angle), 2));

                    if (angle < -Math.PI / 2 || angle > Math.PI / 2)
                        ellipseY = -ellipseY;

                    var ellipseLength = new Vector2((float) ellipseX, (float) ellipseY).Length();
                    var pointLength = candidatePointRelativeToOrigin.ToVector2().Length();

                    // if the point is further out than the point that lies on the ellipse then ignore this point
                    var ratio = pointLength / ellipseLength;

                    // if a point is on or beyond the ellipse circumference then it is beyond the range of the hill and thus ignored
                    if (ratio >= 1)
                        continue;

                    // Use Math.Abs on height so that valley depths are consistent with hill heights
                    var pointHeight = (int) Math.Ceiling((1 - ratio) * Math.Abs(height));

                    if (height < 0)
                        pointHeight = -pointHeight;

                    heightMapPatch[x, y] = pointHeight;
                }
            }

            _heightMap.Patch(heightMapPatch, new Point(centre.X - hillCentre.X, centre.Y - hillCentre.Y), patchMethod);

            return this;
        }

        private Point GetActualPointFromRelativeVector(Vector2 relativeVector)
        {
            if (relativeVector.X > 1.0f || relativeVector.X < 0f)
                throw new ArgumentException("x must be between 0-1 inclusive", nameof(relativeVector));

            if (relativeVector.Y > 1.0f || relativeVector.Y < 0f)
                throw new ArgumentException("y must be between 0-1 inclusive", nameof(relativeVector));


            var actualCentreX = (int)(relativeVector.X * (_heightMap.Width - 1));
            var actualCentreY = (int)(relativeVector.Y * (_heightMap.Length - 1));

            return new Point(actualCentreX, actualCentreY);
        }

        public HeightMapGenerator Ridge(
            Vector2 relativeStart,
            Vector2 relativeEnd,
            Vector2 relativeSize,
            int height
        )
        {
            var hillOptions = Terrain.HeightMap.PatchMethod.ReplaceIfHigher;

            if (height < 0)
                hillOptions = Terrain.HeightMap.PatchMethod.ReplaceIfLower;

            var hillPoints = new List<Point>();

            var start = GetActualPointFromRelativeVector(relativeStart);
            var end = GetActualPointFromRelativeVector(relativeEnd);

            var startVector = start.ToVector2();
            var endVector = end.ToVector2();

            var selectedPoint = start;

            while (selectedPoint != end)
            {
                hillPoints.Add(selectedPoint);

                selectedPoint = GetNextHillPoint(selectedPoint, startVector, endVector);
            }

            if (!hillPoints.Contains(end))
                hillPoints.Add(end);

            foreach (var hillPoint in hillPoints)
            {
                Hill(hillPoint, relativeSize, height, hillOptions);
            }

            return this;
        }

        public HeightMapGenerator DiamondSquare(
            int size,
            int minHeight,
            int maxHeight,
            IDiamondSquareHeightsReducer diamondSquareHeightsReducer = null)
        {
            var heightMapPatch = new DiamondSquare(_random)
                .Execute(size, minHeight, maxHeight)
                .HeightMap;

            _heightMap.Patch(heightMapPatch, new Point(0, 0));

            return this;
        }

        private Point GetNextHillPoint(
            Point currentPoint,
            Vector2 startVector,
            Vector2 endVector
        )
        {
            var points = currentPoint.SurroundingPoints();

            var currentVector = currentPoint.ToVector2();

            var lengthOfCurrentPointToEnd = (currentVector - endVector).Length();

            var shortestLengthStartToEnd = float.MaxValue;

            Point? shortestLengthStartToEndPoint = null;

            foreach (var point in points)
            {
                var pointVector = point.ToVector2();

                if (pointVector == endVector)
                    return point;

                var pointLengthToEnd = (pointVector - endVector).Length();

                if (pointLengthToEnd > lengthOfCurrentPointToEnd)
                    continue;

                var pointLengthToStart = (pointVector - startVector).Length();

                var pointLengthStartToEnd = pointLengthToStart + pointLengthToEnd;

                if (pointLengthStartToEnd < shortestLengthStartToEnd)
                {
                    shortestLengthStartToEnd = pointLengthStartToEnd;
                    shortestLengthStartToEndPoint = point;
                }
            }

            if (shortestLengthStartToEndPoint == null)
                throw new Exception("Could not find next hill point");

            return shortestLengthStartToEndPoint.Value;
        }
    }
}
