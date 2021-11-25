using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.Graphics
{
    public interface ISelectable : IBaseSelectable
    {
        bool IsSelected { get; set; }
    }
}