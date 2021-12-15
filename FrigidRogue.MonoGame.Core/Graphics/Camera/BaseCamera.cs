using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics.Camera
{
    public abstract class BaseCamera : ICamera
    {
        private readonly IGameProvider _gameProvider;
        public const int ProjectionAngle = 90;

        private float _nearClippingPlane = 0.5f;
        private float _farClippingPlane = 10000f;

        protected float _viewportWidth;
        protected float _viewportHeight;
        protected Vector3 _cameraPosition;

        public float MoveSensitivity { get; set;  } = 1f;
        public float ZoomSensitivity { get; set; } = 1f;
        public float RotateSensitivity { get; set; } = 1f;

        public Matrix View { get; protected set; }
        public Matrix Projection { get; private set; }

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
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(ProjectionAngle),
                _gameProvider.Game.GraphicsDevice.Viewport.AspectRatio,
                _nearClippingPlane,
                _farClippingPlane
            );

            _viewportHeight = _gameProvider.Game.GraphicsDevice.Viewport.Height;
            _viewportWidth = _gameProvider.Game.GraphicsDevice.Viewport.Width;
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