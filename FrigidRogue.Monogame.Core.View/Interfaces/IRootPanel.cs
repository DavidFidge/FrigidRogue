using GeonBit.UI.Entities;

namespace FrigidRogue.MonoGame.Core.View.Interfaces
{
    public interface IRootPanel<T>
    {
        bool Visible { get; set; }
        bool IsMouseInRootPanelEmptySpace { get; }
        void Initialize(string panelIdentifier);
        void AddChild(T child);
        void AddChild(IRootPanel<T> child);
        bool HasChild(IRootPanel<T> child);
        bool IsChildOf(Entity root);
        void AddRootPanelToGraph(T root);
        void AddAsChildOf(Panel panel);
        void ClearParent();
    }
}