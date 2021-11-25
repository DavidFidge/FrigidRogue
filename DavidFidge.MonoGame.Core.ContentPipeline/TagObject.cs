using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.ContentPipeline
{
    public class TagObject
    {
        public BoundingBox BoundingBox { get; set; }
        public BoundingSphere BoundingSphere { get; set; }

        public TagObject(BoundingBox boundingBox, BoundingSphere boundingSphere)
        {
            BoundingBox = boundingBox;
            BoundingSphere = boundingSphere;
        }
    }
}