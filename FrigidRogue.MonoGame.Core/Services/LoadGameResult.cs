using System;
using System.Collections.Generic;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class LoadGameResult
    {
        public string ErrorMessage { get; set; }

        public static LoadGameResult Success = new LoadGameResult();

        protected bool Equals(LoadGameResult other)
        {
            return ErrorMessage == other.ErrorMessage;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LoadGameResult)obj);
        }

        public override int GetHashCode()
        {
            return ErrorMessage.GetHashCode();
        }
    }
}
