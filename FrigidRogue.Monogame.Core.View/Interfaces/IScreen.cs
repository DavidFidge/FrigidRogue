namespace FrigidRogue.MonoGame.Core.View.Interfaces
{
    public interface IScreen
    {
        bool IsInitialized { get; }
        bool IsVisible { get; }
        void Show();
        void Hide();
        void Initialize();
        void Update();
        void Draw();
    }
}