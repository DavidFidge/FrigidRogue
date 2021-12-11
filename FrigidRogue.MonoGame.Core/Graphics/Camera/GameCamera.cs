using System;
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

        public CameraMovement GameUpdateContinuousMovement { get; set; }

        public GameCamera(IGameProvider gameProvider) : base(gameProvider)
        {
            Reset();
        }

        public override void Reset()
        {
            _cameraPosition = new Vector3(0f, 0f, 100f);
            _cameraRotation = Quaternion.Identity;
            SetViewMatrix();
        }

        public override void Update()
        {
            Move(GameUpdateContinuousMovement, _moveSpeed);
            Rotate(GameUpdateContinuousMovement, _rotateSpeed);
            SetViewMatrix();
        }

        public void Move(CameraMovement cameraMovement, float moveMagnitude)
        {
            var movementVector = new Vector3();

            if (cameraMovement.HasFlag(CameraMovement.PanLeft))
                movementVector.X -= moveMagnitude;

            if (cameraMovement.HasFlag(CameraMovement.PanRight))
                movementVector.X += moveMagnitude;

            if (cameraMovement.HasFlag(CameraMovement.PanUp))
                movementVector.Y += moveMagnitude;

            if (cameraMovement.HasFlag(CameraMovement.PanDown))
                movementVector.Y -= moveMagnitude;

            if (cameraMovement.HasFlag(CameraMovement.Forward))
                movementVector.Z -= moveMagnitude;

            if (cameraMovement.HasFlag(CameraMovement.Backward))
                movementVector.Z += moveMagnitude;

            var rotatedVector = Vector3.Transform(movementVector, _cameraRotation);

            ChangeTranslationRelative(rotatedVector);
        }

        public void Rotate(CameraMovement cameraMovement, float rotateMagnitude)
        {
            var upDownRotation = 0f;
            var leftRightRotation = 0f;

            if (cameraMovement.HasFlag(CameraMovement.RotateUp))
                upDownRotation -= rotateMagnitude;

            if (cameraMovement.HasFlag(CameraMovement.RotateDown))
                upDownRotation += rotateMagnitude;

            if (cameraMovement.HasFlag(CameraMovement.RotateLeft))
                leftRightRotation += rotateMagnitude;

            if (cameraMovement.HasFlag(CameraMovement.RotateRight))
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
            if (magnitude == 0)
                return;

            Move(
                magnitude > 0 ? CameraMovement.Forward : CameraMovement.Backward,
                Math.Abs(magnitude) * _zoomSpeed
            );
        }

        public Task<Unit> Handle(ZoomViewRequest request, CancellationToken cancellationToken)
        {
            Zoom(request.Difference);
            return Unit.Task;
        }

        public Task<Unit> Handle(MoveViewRequest request, CancellationToken cancellationToken)
        {
            GameUpdateContinuousMovement = request.CameraMovementFlags;

            return Unit.Task;
        }

        public Task<Unit> Handle(RotateViewRequest request, CancellationToken cancellationToken)
        {
            if (request.XRotation > float.Epsilon)
                Rotate(CameraMovement.RotateDown, request.XRotation);
            else if (request.XRotation < float.Epsilon)
                Rotate(CameraMovement.RotateUp, -request.XRotation);

            if (request.ZRotation > float.Epsilon)
                Rotate(CameraMovement.RotateLeft, request.ZRotation);
            else if (request.ZRotation < float.Epsilon)
                Rotate(CameraMovement.RotateRight, -request.ZRotation);

            return Unit.Task;
        }
    }
}