using System;
using System.Xml.Serialization;

using DavidFidge.MonoGame.Core.Interfaces.Services;

namespace DavidFidge.MonoGame.Core.Configuration
{
    [Serializable]
    [XmlRoot("graphicsSettings")]
    public class GraphicsSettings : BaseConfigurationSectionHandler, IGraphicsSettings
    {
        [XmlElement("showBoundingBoxes")]
        public bool ShowBoundingBoxes { get; set; }
    }
}