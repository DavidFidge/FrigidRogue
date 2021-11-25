using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FrigidRogue.MonoGame.Core.ContentPipeline
{
    public class TagObjectTypeReader : ContentTypeReader<TagObject>
    {
        protected override TagObject Read(ContentReader contentReader, TagObject tagObject)
        {
            var boundingBox = contentReader.ReadObject<BoundingBox>();
            var boundingSphere = contentReader.ReadObject<BoundingSphere>();

            if (tagObject == null)
                tagObject = new TagObject(boundingBox, boundingSphere);
            else
                tagObject.BoundingBox = boundingBox;

            return tagObject;
        }
    }
}