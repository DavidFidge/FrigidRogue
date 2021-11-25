using DavidFidge.MonoGame.Core.Components;
using DavidFidge.Monogame.Core.View.Interfaces;

using Entity = GeonBit.UI.Entities.Entity;

namespace DavidFidge.Monogame.Core.View
{
    public abstract class Screen : BaseComponent, IScreen
    {
        protected Screen(IView<Entity> primaryView)
        {
            PrimaryView = primaryView;
        }

        public bool IsInitialized { get; private set; }

        public bool IsVisible => GeonBit.UI.UserInterface.Active == UserInterface;

        protected GeonBit.UI.UserInterface UserInterface { get; set; }

        protected IView<Entity> PrimaryView { get; }

        public void Show()
        {
            if (!IsInitialized)
                Initialize();

            GeonBit.UI.UserInterface.Active = UserInterface;

            PrimaryView.Show();
        }

        public void Hide()
        {
            PrimaryView.Hide();
        }

        public void Initialize()
        {
            UserInterface = new GeonBit.UI.UserInterface();
            UserInterface.UseRenderTarget = true;
            UserInterface.IncludeCursorInRenderTarget = false;

            PrimaryView.Initialize();
            
            PrimaryView.RootPanel.AddRootPanelToGraph(UserInterface.Root);

            InitializeInternal();

            IsInitialized = true;
        }

        protected virtual void InitializeInternal()
        {
        }

        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
        }
    }
}