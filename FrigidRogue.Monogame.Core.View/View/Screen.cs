using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.View.Interfaces;
using GeonBit.UI.Entities;

using IGeonBitUserInterface = GeonBit.UI.IUserInterface;

namespace FrigidRogue.MonoGame.Core.View
{
    public abstract class Screen : BaseComponent, IScreen
    {
        private readonly IGeonBitUserInterface _screen;
        private readonly IUserInterface _userInterface;

        protected Screen(IView<IEntity> primaryView, IGeonBitUserInterface screen, IUserInterface userInterface)
        {
            _screen = screen;
            _userInterface = userInterface;
            PrimaryView = primaryView;
        }

        public bool IsInitialized { get; private set; }

        public bool IsVisible => _userInterface.IsActive(_screen);

        protected IUserInterface UserInterface { get; set; }

        protected IView<IEntity> PrimaryView { get; }

        public void Show()
        {
            if (!IsInitialized)
                Initialize();

            _userInterface.SetActive(_screen);

            PrimaryView.Show();
        }

        public void Hide()
        {
            PrimaryView.Hide();
        }

        public void Initialize()
        {
            _screen.UseRenderTarget = true;
            _screen.IncludeCursorInRenderTarget = false;

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