using System;

using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class TurnDetails : ITurnNumber, ICloneable
    {
        public int TurnNumber { get; set; }
        public int SequenceNumber { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
