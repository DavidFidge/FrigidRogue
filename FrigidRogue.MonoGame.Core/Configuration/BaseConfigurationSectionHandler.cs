using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace FrigidRogue.MonoGame.Core.Configuration
{
    [Serializable]
    public class BaseConfigurationSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var serialiser = new XmlSerializer(typeof(GraphicsSettings));

            var deserialisedSetting = serialiser.Deserialize(new XmlNodeReader(section.ParentNode)) as GraphicsSettings;

            return deserialisedSetting;
        }

        public static T Load<T>()
        {
            var typeName = typeof(T).Name;

            var camelCaseTypeName = typeName.First().ToString().ToLower() + typeName.Substring(1);

            var setting = (T)ConfigurationManager.GetSection(camelCaseTypeName);

            return setting;
        }
    }
}