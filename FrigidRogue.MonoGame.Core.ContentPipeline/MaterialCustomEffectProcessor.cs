using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace FrigidRogue.MonoGame.Core.ContentPipeline
{
    [ContentProcessor(DisplayName =  "FrigidRogue.MonoGame.Core.ContentPipeline.MaterialCustomEffectProcessor")]
    public class MaterialCustomEffectProcessor : MaterialProcessor
    {
        public override MaterialContent Process(
            MaterialContent materialContent,
            ContentProcessorContext context)
        {
            var material = new EffectMaterialContent();
            
            var effectDirectory = Path.GetDirectoryName(materialContent.Identity.SourceFilename);
            
            // go back out of models and into effects directory
            effectDirectory = Path.Combine(effectDirectory, "..\\Effects");

            var effectFile = Path.Combine(effectDirectory, (string)context.Parameters["CustomEffectFilename"]);

            material.Effect = new ExternalReference<EffectContent>(effectFile);

            // this will only add the first texture in the input textures for this material
            if (materialContent.Textures != null)
            {
                foreach (string key in materialContent.Textures.Keys)
                {
                    material.Textures.Add("tex0", materialContent.Textures[key]);
                    break;
                }
            }

            return context.Convert<MaterialContent, MaterialContent>(material, "MaterialProcessor");
        }
    }
}