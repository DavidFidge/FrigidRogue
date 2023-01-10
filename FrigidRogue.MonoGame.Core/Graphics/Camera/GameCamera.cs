using System.Threading;
using System.Threading.Tasks;

using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Messages;

using MediatR;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics.Camera
{
    public class GameCamera : BaseCamera,
        IGameCamera,
        IRequestHandler<MoveViewRequest>,
        IRequestHandler<RotateViewRequest>,
        IRequestHandler<ZoomViewRequest>
    {
        private Quaternion _cameraRotation;

        public CameraMovementType ContinuousCameraMovementType { get; set; }

        public GameCamera(IGameProvider gameProvider) : base(gameProvider)
        {
            Reset();
        }

        public override void Reset()
        {
            _cameraPosition = Vector3.Zero;
            _cameraRotation = Quaternion.Identity;
            SetViewMatrix();
        }

        public override void Update()
        {
            if (MoveSensitivity > 0)
                Move(ContinuousCameraMovementType, MoveSensitivity);

            if (RotateSensitivity > 0)
                Rotate(ContinuousCameraMovementType, RotateSensitivity);

            SetViewMatrix();
        }

        public void Move(CameraMovementType cameraMovementType, float moveMagnitude)
        {
            var movementVector = new Vector3();

            if (cameraMovementType.HasFlag(CameraMovementType.PanLeft))
                movementVector.X -= moveMagnitude;

            if (cameraMovementType.HasFlag(CameraMovementType.PanRight))
                movementVector.X += moveMagnitude;

            if (cameraMovementType.HasFlag(CameraMovementType.PanUp))
                movementVector.Y += moveMagnitude;

            if (cameraMovementType.HasFlag(CameraMovementType.PanDown))
                movementVector.Y -= moveMagnitude;

            if (cameraMovementType.HasFlag(CameraMovementType.Forward))
                movementVector.Z -= moveMagnitude;

            if (cameraMovementType.HasFlag(CameraMovementType.Backward))
                movementVector.Z += moveMagnitude;

            var rotatedVector = Vector3.Transform(movementVector, _cameraRotation);

            ChangeTranslationRelative(rotatedVector);
        }

        public void Rotate(CameraMovementType cameraMovementType, float rotateMagnitude)
        {
            var upDownRotation = 0f;
            var leftRightRotation = 0f;

            if (cameraMovementType.HasFlag(CameraMovementType.RotateUp))
                upDownRotation -= rotateMagnitude;

            if (cameraMovementType.HasFlag(CameraMovementType.RotateDown))
                upDownRotation += rotateMagnitude;

            if (cameraMovementType.HasFlag(CameraMovementType.RotateLeft))
                leftRightRotation += rotateMagnitude;

            if (cameraMovementType.HasFlag(CameraMovementType.RotateRight))
                leftRightRotation -= rotateMagnitude;

            var additionalRotation = Quaternion.CreateFromAxisAngle(Vector3.Up, upDownRotation) * Quaternion.CreateFromAxisAngle(Vector3.Right, leftRightRotation);

            _cameraRotation = _cameraRotation * additionalRotation;
        }

        protected void SetViewMatrix()
        {
            var cameraRotatedTarget = Vector3.Transform(Vector3.Forward, _cameraRotation);
            var cameraFinalTarget = _cameraPosition + cameraRotatedTarget;
            var cameraRotatedUpVector = Vector3.Transform(Vector3.Up, _cameraRotation);

            View = Matrix.CreateLookAt(
                _cameraPosition,
                cameraFinalTarget,
                cameraRotatedUpVector
            );
        }

        public void Zoom(int magnitude)
        {
            if (magnitude == 0 && ZoomSensitivity > 0)
                return;

            Move(
                magnitude > 0 ? CameraMovementType.Forward : CameraMovementType.Backward,
                Math.Abs(magnitude) * ZoomSensitivity
            );
        }

        public Task<Unit> Handle(ZoomViewRequest request, CancellationToken cancellationToken)
        {
            Zoom(request.Difference);
            return Unit.Task;
        }

        public Task<Unit> Handle(MoveViewRequest request, CancellationToken cancellationToken)
        {
            ContinuousCameraMovementType = request.CameraMovementTypeFlags;

            return Unit.Task;
        }

        public Task<Unit> Handle(RotateViewRequest request, CancellationToken cancellationToken)
        {
            if (request.XRotation > float.Epsilon)
                Rotate(CameraMovementType.RotateDown, request.XRotation);
            else if (request.XRotation < float.Epsilon)
                Rotate(CameraMovementType.RotateUp, -request.XRotation);

            if (request.ZRotation > float.Epsilon)
                Rotate(CameraMovementType.RotateLeft, request.ZRotation);
            else if (request.ZRotation < float.Epsilon)
                Rotate(CameraMovementType.RotateRight, -request.ZRotation);

            return Unit.Task;
        }
    }
}