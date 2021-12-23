using System;
using System.Collections.Generic;
using System.Text;
using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class TurnDetails : ITurnNumber
    {
        public int TurnNumber { get; set; }
        public int SequenceNumber { get; set; }
    }
}
