using System;
using System.Collections.Generic;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class LoadGameResult
    {
        public string ErrorMessage { get; set; }

        public byte[] Bytes { get; set; }

        public bool Success => String.IsNullOrEmpty(ErrorMessage);
        public bool Failure => !Success;
    }
}
