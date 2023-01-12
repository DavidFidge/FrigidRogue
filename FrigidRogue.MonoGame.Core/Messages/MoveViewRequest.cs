using FrigidRogue.MonoGame.Core.Graphics.Camera;

using MediatR;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class MoveViewRequest : IRequest
    {
        public float MoveX { get; }

        public float MoveY { get; }

        public int MoveZ { get; }

        public MoveViewRequest(float moveX, float moveY, int moveZ = 0)
        {
            MoveX = moveX;
            MoveY = moveY;
            MoveZ = moveZ;
        }
    }
}