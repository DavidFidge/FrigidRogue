using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace FrigidRogue.MonoGame.Core.ContentPipeline
{
    [ContentProcessor(DisplayName = "FrigidRogue.MonoGame.Core.ContentPipeline.ModelCustomProcessor")]
    public class ModelCustomProcessor : ModelProcessor
    {
        // This is set in the "properties" window when clicking on the model file.
        public string CustomEffectFilename { get; set; }

        protected override MaterialContent ConvertMaterial(MaterialContent material, ContentProcessorContext context)
        {
            // This function is called multiple times for each material in the model.  The parameter only needs to be added the first time
            if (string.IsNullOrEmpty(CustomEffectFilename))
            {
                return context.Convert<MaterialContent, MaterialContent>(material, "MaterialProcessor");
            }

            if (!context.Parameters.ContainsKey("CustomEffectFilename"))
                context.Parameters.Add("CustomEffectFilename", CustomEffectFilename);

            return context.Convert<MaterialContent, MaterialContent>(material, "MaterialCustomEffectProcessor");
        }

        public override ModelContent Process(NodeContent nodeInput, ContentProcessorContext context)
        {
            var model = base.Process(nodeInput, context);

            var vertices = new List<Vector3>();
            vertices = AddVerticesToList(nodeInput, vertices);

            var boundingBox = BoundingBox.CreateFromPoints(vertices);
            var boundingSphere = BoundingSphere.CreateFromPoints(vertices);

            var tagObject = new TagObject(boundingBox, boundingSphere);

            model.Tag = tagObject;

            return model;
        }

        private List<Vector3> AddVerticesToList(NodeContent nodeContent, List<Vector3> vertices)
        {
            if (nodeContent is MeshContent meshContent)
            {
                var absTransform = meshContent.AbsoluteTransform;

                foreach (var geometryContent in meshContent.Geometry)
                {
                    foreach (var vertex in geometryContent.Vertices.Positions)
                    {
                        var transform = Vector3.Transform(vertex, absTransform);
                        vertices.Add(transform);
                    }
                }
            }

            foreach (var child in nodeContent.Children)
                vertices = AddVerticesToList(child, vertices);

            return vertices;
        }
    }
}