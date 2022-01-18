namespace FrigidRogue.MonoGame.Core.Components
{
    public class RandomNumberSaveData
    {
        public bool BytesAvailable { get; set; }
        public ulong X { get; set; }
        public ulong Y { get; set; }
        public uint BitBuffer { get; set; }
        public int BitCount { get; set; }
        public uint Seed { get; set; }
    }
}
