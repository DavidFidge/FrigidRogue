using System.Threading;
using FrigidRogue.MonoGame.Core.Components.Mediator;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Messages;
using FrigidRogue.MonoGame.Core.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Camera
{
    public class GameCamera :
        IGameCamera,
        IRequestHandler<MoveViewContinousRequest>,
        IRequestHandler<RotateViewRequest>,
        IRequestHandler<ZoomViewRequest>,
        IRequestHandler<MoveViewRequest>
    {
        private Quaternion _cameraRotation;
        private readonly IGameProvider _gameProvider;
        private GraphicsDevice GraphicsDevice => _gameProvider?.Game.GraphicsDevice ?? _nullGameProviderGraphicsDevice;
        private readonly GraphicsDevice _nullGameProviderGraphicsDevice;

        private const int ProjectionAngle = 90;
        private readonly float _nearClippingPlane = 0.5f;
        private readonly float _farClippingPlane = 10000f;
        private RenderResolution _renderResolution;

        private Vector3 _cameraPosition;

        public float ContinuousMoveSensitivity { get; set; } = 1f;
        public float ContinuousRotateSensitivity { get; set; } = 1f;
        public float ZoomSensitivity { get; set; } = 1f;
        public float MoveSensitivity { get; set;  } = 1f;

        public Matrix View { get; protected set; }
        public Matrix Projection { get; private set; }
        public CameraMovementType ContinuousCameraMovementType { get; set; }

        public RenderResolution RenderResolution
        {
            get => _renderResolution;
            set
            {
                _renderResolution = value;
                RecalculateProjectionMatrix();
            }
        }

        public GameCamera(IGameProvider gameProvider)
        {
            _gameProvider = gameProvider;
            Reset();
        }

        public GameCamera(GraphicsDevice nullGameProviderGraphicsDevice)
        {
            _nullGameProviderGraphicsDevice = nullGameProviderGraphicsDevice;
            Reset();
        }

        public void Initialise()
        {
            Reset();
            RecalculateProjectionMatrix();
        }

        public void Reset()
        {
            _cameraPosition = Vector3.Zero;
            _cameraRotation = Quaternion.Identity;
            SetViewMatrix();
        }

        public void Update()
        {
            if (ContinuousMoveSensitivity > 0)
                Move(ContinuousCameraMovementType, ContinuousMoveSensitivity);

            if (ContinuousRotateSensitivity > 0)
                Rotate(ContinuousCameraMovementType, ContinuousRotateSensitivity);
            
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

            ChangeTranslationRelative(Vector3.Transform(movementVector, _cameraRotation));
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

            _cameraRotation *= additionalRotation;
        }

        private void SetViewMatrix()
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

        public void Zoom(float magnitude)
        {
            if (magnitude == 0 && ZoomSensitivity > 0)
                return;

            Move(
                magnitude > 0 ? CameraMovementType.Forward : CameraMovementType.Backward,
                Math.Abs(magnitude) * ZoomSensitivity
            );
        }

        public void Handle(ZoomViewRequest request)
        {
            Zoom(request.Difference);
        }

        public void Handle(RotateViewRequest request)
        {
            if (request.XRotation > float.Epsilon)
                Rotate(CameraMovementType.RotateDown, request.XRotation);
            else if (request.XRotation < float.Epsilon)
                Rotate(CameraMovementType.RotateUp, -request.XRotation);

            if (request.ZRotation > float.Epsilon)
                Rotate(CameraMovementType.RotateLeft, request.ZRotation);
            else if (request.ZRotation < float.Epsilon)
                Rotate(CameraMovementType.RotateRight, -request.ZRotation);
        }

        public void Handle(MoveViewContinousRequest request)
        {
            ContinuousCameraMovementType = request.CameraMovementTypeFlags;
        }

        public void Handle(MoveViewRequest request)
        {
            if (request.MoveX > 0)
                Move(CameraMovementType.PanLeft, request.MoveX * MoveSensitivity);
            else if (request.MoveX < 0)
                Move(CameraMovementType.PanRight, -request.MoveX * MoveSensitivity);

            if (request.MoveY > 0)
                Move(CameraMovementType.PanUp, request.MoveY * MoveSensitivity);
            else if (request.MoveY < 0)
                Move(CameraMovementType.PanDown, -request.MoveY * MoveSensitivity);

            if (request.MoveZ != 0)
                Zoom((int)(request.MoveZ * ZoomSensitivity));
        }

        public void RecalculateProjectionMatrix()
        {
            var aspectRatio = _renderResolution == null
                ? GraphicsDevice.Viewport.AspectRatio
                : (float)_renderResolution.Width / _renderResolution.Height;

            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(ProjectionAngle),
                aspectRatio,
                _nearClippingPlane,
                _farClippingPlane
            );
        }

        public Ray GetPointerRay(int x, int y, bool normalised = true)
        {
            var nearScreenPoint = new Vector3(x, y, 0);
            var farScreenPoint = new Vector3(x, y, 1);

            var near3DPoint = GraphicsDevice.Viewport.Unproject(nearScreenPoint, Projection, View, Matrix.Identity);
            var far3DPoint = GraphicsDevice.Viewport.Unproject(farScreenPoint, Projection, View, Matrix.Identity);

            var pointerRayDirection = far3DPoint - near3DPoint;

            if (normalised)
                pointerRayDirection.Normalize();

            return new Ray(near3DPoint, pointerRayDirection);
        }

        public void ChangeTranslationRelative(Vector3 translationDelta)
        {
            _cameraPosition += translationDelta;
        }
    }
}