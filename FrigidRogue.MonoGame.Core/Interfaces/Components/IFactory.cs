namespace FrigidRogue.MonoGame.Core.Components
{
    public interface IFactory<T>
    {
        T Create();
        void Release(T mapTileEntity);
    }
}