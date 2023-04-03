using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class LoadGameDetails : IHeaderSaveData
    {
        public string Filename { get; set; }
        public DateTime DateTime { get; set; }
        public string LoadGameDetail { get; set; }

        protected bool Equals(LoadGameDetails other)
        {
            return Filename == other.Filename;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LoadGameDetails)obj);
        }

        public override int GetHashCode()
        {
            return Filename.GetHashCode();
        }
    }
}
