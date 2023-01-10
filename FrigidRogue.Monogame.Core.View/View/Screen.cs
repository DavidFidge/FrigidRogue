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
        public bool IsVisible => UserInterface.IsActive(ScreenUserInterface);

        protected IView<Entity> PrimaryView { get; }
        public GeonBit.UI.UserInterface ScreenUserInterface => _screen;

        private GeonBit.UI.UserInterface _screen;

        protected Screen(IView<Entity> primaryView)
        {
            PrimaryView = primaryView;
        }

        public void Show()
        {
            if (!IsInitialized)
                Initialize();

            UserInterface.SetActive(this);

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
            
            PrimaryView.RootPanel.AddRootPanelToGraph(ScreenUserInterface.Root);

            IsInitialized = true;
        }

        public virtual void Update()
        {
            PrimaryView.Update();
        }

        public virtual void Draw()
        {
            PrimaryView.Draw();
        }
    }
}