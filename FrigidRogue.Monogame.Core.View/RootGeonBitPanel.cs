using System.Linq;
using FrigidRogue.MonoGame.Core.View.Extensions;
using FrigidRogue.MonoGame.Core.View.Interfaces;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

using Entity = GeonBit.UI.Entities.Entity;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    public class RootGeonBitPanel : IRootPanel<Entity>
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

        public void AddChild(Entity child)
        {
            _panel.AddChild(child);
        }

        public void ClearParent()
        {
            _panel.RemoveFromParent();
        }

        public void AddChild(IRootPanel<Entity> child)
        {
            child.AddRootPanelToGraph(_panel);
        }

        public bool HasChild(IRootPanel<Entity> child)
        {
            return child.IsChildOf(_panel);
        }

        public bool IsChildOf(Entity root)
        {
            return root.Children.Any(c => c == _panel);
        }

        public void AddAsChildOf(Panel panel)
        {
            panel.AddChild(_panel);
        }

        public void AddRootPanelToGraph(Entity root)
        {
            root.AddChild(_panel);
        }
    }
}