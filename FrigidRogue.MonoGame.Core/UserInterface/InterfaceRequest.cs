using System.Reflection;
using MediatR;

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