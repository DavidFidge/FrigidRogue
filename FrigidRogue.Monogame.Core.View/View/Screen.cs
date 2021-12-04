using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.View.Interfaces;

using Entity = GeonBit.UI.Entities.Entity;

namespace FrigidRogue.MonoGame.Core.View
{
    public abstract class Screen : BaseComponent, IScreen
    {
        // Property injected
        public IUserInterface UserInterface { get; set; }
        public bool IsInitialized { get; private set; }
        public bool IsVisible => UserInterface.IsActive(_screen);

        protected IView<Entity> PrimaryView { get; }

        private GeonBit.UI.UserInterface _screen;

        protected Screen(IView<Entity> primaryView)
        {
            PrimaryView = primaryView;
        }

        public void Show()
        {
            if (!IsInitialized)
                Initialize();

            UserInterface.SetActive(_screen);

            PrimaryView.Show();
        }

        public void Hide()
        {
            PrimaryView.Hide();
        }

        public void Initialize()
        {
            _screen = UserInterface.Create();

            PrimaryView.Initialize();
            
            PrimaryView.RootPanel.AddRootPanelToGraph(_screen.Root);

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