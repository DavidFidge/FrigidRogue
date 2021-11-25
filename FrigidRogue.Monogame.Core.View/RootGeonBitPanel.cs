using FrigidRogue.MonoGame.Core.View.Extensions;
using FrigidRogue.MonoGame.Core.View.Interfaces;

using GeonBit.UI.Entities;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    public class RootGeonBitPanel : IRootPanel<IEntity>
    {
        private Panel _panel;

        public bool Visible
        {
            get => _panel.Visible;
            set => _panel.Visible = value;
        }

        public bool IsMouseInRootPanelEmptySpace => _panel != null && _panel.State == EntityState.Default;
        
        public void Initialize(string panelIdentifier)
        {
            _panel = new Panel(new Vector2(0, 0), PanelSkin.None)
                .NoPadding()
                .Hidden();

            _panel.Identifier = panelIdentifier;
        }

        public void AddChild(IEntity child)
        {
            _panel.AddChild((Entity)child);
        }

        public void AddChild(IRootPanel<IEntity> child)
        {
            child.AddRootPanelToGraph(_panel);
        }

        public void AddAsChildOf(IPanel panel)
        {
            panel.AddChild(_panel);
        }

        public void AddRootPanelToGraph(IEntity root)
        {
            root.AddChild(_panel);
        }
    }
}