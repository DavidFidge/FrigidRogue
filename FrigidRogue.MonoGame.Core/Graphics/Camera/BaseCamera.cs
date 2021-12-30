using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;
using FrigidRogue.MonoGame.Core.Services;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics.Camera
{
    public abstract class BaseCamera : ICamera
    {
        private readonly IGameProvider _gameProvider;
        private const int ProjectionAngle = 90;

        private float _nearClippingPlane = 0.5f;
        private float _farClippingPlane = 10000f;

        protected Vector3 _cameraPosition;

        public float MoveSensitivity { get; set;  } = 1f;
        public float ZoomSensitivity { get; set; } = 1f;
        public float RotateSensitivity { get; set; } = 1f;

        public Matrix View { get; protected set; }
        public Matrix Projection { get; private set; }

        private RenderResolution _renderResolution;

        public RenderResolution RenderResolution
        {
            get => _renderResolution;
            set
            {
                _renderResolution = value;
                RecalculateProjectionMatrix();
            }
        }

        protected BaseCamera(IGameProvider gameProvider)
        {
            _gameProvider = gameProvider;
        }

        public abstract void Update();
        public abstract void Reset();

        public void Initialise()
        {
            Reset();
            RecalculateProjectionMatrix();
        }

        public void RecalculateProjectionMatrix()
        {
            var aspectRatio = _renderResolution == null
                ? _gameProvider.Game.GraphicsDevice.Viewport.AspectRatio
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

            var near3DPoint = _gameProvider.Game.GraphicsDevice.Viewport.Unproject(nearScreenPoint, Projection, View, Matrix.Identity);
            var far3DPoint = _gameProvider.Game.GraphicsDevice.Viewport.Unproject(farScreenPoint, Projection, View, Matrix.Identity);

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