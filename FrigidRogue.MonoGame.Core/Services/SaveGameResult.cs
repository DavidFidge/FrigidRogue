namespace FrigidRogue.MonoGame.Core.Services
{
    public class SaveGameResult
    {
        public bool RequiresOverwrite { get; set; }

        public string ErrorMessage { get; set; }

        public static SaveGameResult Success = new SaveGameResult();
        public static SaveGameResult Overwrite = new SaveGameResult { RequiresOverwrite = true };

        protected bool Equals(SaveGameResult other)
        {
            return RequiresOverwrite == other.RequiresOverwrite && ErrorMessage == other.ErrorMessage;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SaveGameResult)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RequiresOverwrite, ErrorMessage);
        }
    }
}
