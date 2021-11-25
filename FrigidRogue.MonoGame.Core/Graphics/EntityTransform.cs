using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public class EntityTransform : ITransform
    {
        public EntityTransform(Matrix matrix)
        {
            Scale = matrix.Scale();
            Translation = matrix.Translation;
            Rotation = Quaternion.CreateFromRotationMatrix(matrix);

            UpdateTransform();
        }

        public EntityTransform()
        {
            Scale = Vector3.One;
            Translation = Vector3.Zero;
            Rotation = Quaternion.Identity;
            UpdateTransform();
        }

        private void UpdateTransform()
        {
            Transform = ScaleMatrix * Matrix.CreateFromQuaternion(Rotation) * TranslationMatrix;
        }

        public void ChangeTranslationRelative(Vector3 translationDelta)
        {
            Translation = Vector3.Add(Translation, translationDelta);
            UpdateTransform();
        }

        public void ChangeTranslation(Vector3 translation)
        {
            Translation = translation;
            UpdateTransform();
        }

        public void ChangeScaleRelative(Vector3 scaleDelta)
        {
            Scale = Vector3.Add(Scale, scaleDelta);
            UpdateTransform();
        }

        public void ChangeScale(Vector3 scale)
        {
            Scale = scale;
            UpdateTransform();
        }

        public void ChangeRotationRelative(Quaternion quaternion)
        {
            Rotation = Quaternion.Add(Rotation, quaternion);
            UpdateTransform();
        }

        public void ChangeRotation(Quaternion quaternion)
        {
            Rotation = quaternion;
            UpdateTransform();
        }

        public Vector3 Translation { get; private set; }
        public Matrix TranslationMatrix => Matrix.CreateTranslation(Translation);
        public Vector3 Scale { get; private set; }
        public Matrix ScaleMatrix => Matrix.CreateScale(Scale);
        public Quaternion Rotation { get; private set; }
        public Matrix Transform { get;  private set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}