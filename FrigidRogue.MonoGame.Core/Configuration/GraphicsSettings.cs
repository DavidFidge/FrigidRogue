using System.Xml.Serialization;

using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Configuration
{
    [Serializable]
    [XmlRoot("graphicsSettings")]
    public class GraphicsSettings : BaseConfigurationSectionHandler, IGraphicsSettings
    {
        [XmlElement("showBoundingBoxes")]
        public bool ShowBoundingBoxes { get; set; }
    }
}