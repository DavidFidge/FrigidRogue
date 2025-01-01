using System.Reflection;
using FrigidRogue.MonoGame.Core.Components.Mediator;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    public class InterfaceRequest<T> : IRequest
    {
        public InterfaceRequest(PropertyInfo propertyInfo, object value)
        {
            PropertyInfo = propertyInfo;
            Value = value;
        }

        public PropertyInfo PropertyInfo { get; }
        public object Value { get; }
    }
}