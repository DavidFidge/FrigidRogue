﻿using FrigidRogue.MonoGame.Core.Components.MapPointChoiceRules;
using GoRogue.GameFramework;
using GoRogue.Random;

using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using ShaiRandom.Generators;
using Point = SadRogue.Primitives.Point;
using Rectangle = SadRogue.Primitives.Rectangle;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class MapExtensions
    {
        public static Point RandomPosition(this Map map)
        {
            return GlobalRandom.DefaultRNG.RandomPosition(map);
        }

        public static Point RandomPositionFromRules(this Map map, List<MapPointChoiceRule> rules, Point startingPoint)
        {
            if (startingPoint == Point.None) 
                startingPoint = GlobalRandom.DefaultRNG.RandomPosition(map);

            if (!rules.Any())
                return startingPoint;
            
            var points = new List<Point> { startingPoint };
            uint radius = 0;
            var lastRectangle = Rectangle.Empty;

            while (true)
            {
                foreach (var point in points)
                {
                    // does not need to be initialised as rules is guaranteed to have items
                    var ruleIsValid = false;
                    
                    foreach (var rule in rules)
                    {
                        ruleIsValid = rule.IsValid(point);
                        
                        if (!ruleIsValid)
                            break;
                    }
                    
                    if (ruleIsValid)
                        return point;   
                }

                radius++;

                var rectangle = map.RectangleForRadiusAndPoint(radius, startingPoint);

                if (rectangle == lastRectangle)
                    break;

                points = rectangle.PerimeterPositions().ToList();
                lastRectangle = rectangle;
            }

            return Point.None;
        }
        
        public static Rectangle RectangleForRadiusAndPoint(this Map map, uint radius, Point point)
        {
            var rectangle = Rectangle.WithRadius(point, (int)radius, (int)radius);

            rectangle = Rectangle.GetIntersection(rectangle, map.Bounds());

            return rectangle;
        }
        
        public static Rectangle RectangleCoveringPoints(this Map map, params Point[] points)
        {
            if (points == null || points.Length == 0)
            {
                return new Rectangle(
                    new Point(0, 0),
                    new Point(map.Width - 1, map.Height - 1)
                );
            }

            var rectangle = RectangleExtensions.CoveringPoints(points);

            rectangle = Rectangle.GetIntersection(rectangle, map.Bounds());
            
            return rectangle;
        }

        public static IEnumerable<Point> CircleCoveringPoints(this Map map, Point point, int radius)
        {
            return Radius.Circle.PositionsInRadius(point, radius, map.Bounds());
        }
    }
}
