namespace FrigidRogue.MonoGame.Core.Graphics
{
    public interface ISelectable : IBaseSelectable
    {
        bool IsSelected { get; set; }
    }
}