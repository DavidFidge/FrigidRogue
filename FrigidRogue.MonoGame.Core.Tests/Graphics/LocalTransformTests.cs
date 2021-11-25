using System;

using FrigidRogue.MonoGame.Core.Graphics;
using FrigidRogue.MonoGame.Core.Tests.Services;
using FrigidRogue.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Tests.Graphics
{
    [TestClass]
    public class LocalTransformTests : BaseTest
    {
        private EntityTransform _entityTransform;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            _entityTransform = new EntityTransform();
        }

        [TestMethod]
        public void World_Should_Return_Identity_Matrix_From_Constructor()
        {
            // Act
            var result = _entityTransform.Transform;

            // Assert
            Assert.That.AreEquivalent(Matrix.Identity, result);
        }

        [TestMethod]
        public void ChangeTranslation_Should_Return_Translated_Matrix()
        {
            // Arrange
            var translation = new Vector3(2f, 3f, 4f);

            // Act
            _entityTransform.ChangeTranslation(translation);

            // Assert
            Assert.That.AreEquivalent(Matrix.CreateTranslation(translation), _entityTransform.Transform);
        }

        [TestMethod]
        public void ChangeTranslationRelative_Should_Return_Matrix_Translated_From_Last_Translation()
        {
            // Arrange
            var translation = new Vector3(2f, 3f, 4f);
            _entityTransform.ChangeTranslation(translation);

            var relativeTranslation = new Vector3(1f, 2f, 3f);

            // Act
            _entityTransform.ChangeTranslationRelative(relativeTranslation);

            // Assert
            Assert.That.AreEquivalent(
                Matrix.CreateTranslation(translation + relativeTranslation),
                _entityTransform.Transform
                );
        }

        [TestMethod]
        public void ChangeTranslationRelative_Should_Return_Matrix_Translated_From_Last_Translation_Before_Rotation_And_Scale()
        {
            // Arrange
            var translation = new Vector3(2f, 3f, 4f);
            var scale = new Vector3(5f, 6f, 7f);
            var rotation = Quaternion.CreateFromAxisAngle(new Vector3(0.1f, 0.2f, 0.3f), 0);
            _entityTransform.ChangeTranslation(translation);
            _entityTransform.ChangeRotation(rotation);
            _entityTransform.ChangeScale(scale);

            var relativeTranslation = new Vector3(1f, 2f, 3f);

            // Act
            _entityTransform.ChangeTranslationRelative(relativeTranslation);

            // Assert
            Assert.That.AreEquivalent(
                Matrix.CreateScale(scale)
                * Matrix.CreateFromQuaternion(rotation) 
                * Matrix.CreateTranslation(translation + relativeTranslation),
                _entityTransform.Transform
            );
        }

        [TestMethod]
        public void ChangeScale_Should_Return_Scaled_Matrix()
        {
            // Arrange
            var scale = new Vector3(2f, 3f, 4f);

            // Act
            _entityTransform.ChangeScale(scale);

            // Assert
            Assert.That.AreEquivalent(Matrix.CreateScale(scale), _entityTransform.Transform);
        }

        [TestMethod]
        public void ChangeScale_Should_Return_Matrix_Scaled_Relative_To_Current_Scale()
        {
            // Arrange
            var scale = new Vector3(2f, 3f, 4f);
            _entityTransform.ChangeScale(scale);

            var relativeScale = new Vector3(1f, 2f, 3f);

            // Act
            _entityTransform.ChangeScaleRelative(relativeScale);

            // Assert
            Assert.That.AreEquivalent(
                Matrix.CreateScale(scale + relativeScale),
                _entityTransform.Transform
            );
        }

        [TestMethod]
        public void ChangeScaleRelative_Should_Return_Matrix_Scaled_From_Last_Scale_Before_Rotation_And_Translation()
        {
            // Arrange
            var translation = new Vector3(2f, 3f, 4f);
            var scale = new Vector3(5f, 6f, 7f);
            var rotation = Quaternion.CreateFromAxisAngle(new Vector3(0.1f, 0.2f, 0.3f), 0);

            _entityTransform.ChangeTranslation(translation);
            _entityTransform.ChangeRotation(rotation);
            _entityTransform.ChangeScale(scale);

            var relativeScale = new Vector3(1f, 2f, 3f);

            // Act
            _entityTransform.ChangeScaleRelative(relativeScale);

            // Assert
            Assert.That.AreEquivalent(
                Matrix.CreateScale(scale + relativeScale)
                * Matrix.CreateFromQuaternion(rotation)
                * Matrix.CreateTranslation(translation),
                _entityTransform.Transform
            );
        }

        [TestMethod]
        public void ChangeRotation_Should_Return_Rotated_Matrix()
        {
            // Arrange
            var rotation = Quaternion.CreateFromAxisAngle(new Vector3(0.1f, 0.2f, 0f), 0);

            // Act
            _entityTransform.ChangeRotation(rotation);

            // Assert
            Assert.That.AreEquivalent(
                Matrix.CreateRotationX(rotation.X)
                * Matrix.CreateRotationY(rotation.Y),
                _entityTransform.Transform);
        }

        [TestMethod]
        public void ChangeRotation_Should_Return_Matrix_Rotated_Relative_To_Current_Rotation_Via_Mulitplying_With_Existing()
        {
            // Arrange
            var rotation = Quaternion.CreateFromAxisAngle(new Vector3(0.1f, 0.2f, 0), 0);
            var rotation2 = Quaternion.CreateFromAxisAngle(new Vector3(0.1f, 0.2f, 0), 1f);

            _entityTransform.ChangeRotation(rotation);

            var relativeRotation = new Vector3(0.4f, 0.5f, 0.6f);

            // Act
            _entityTransform.ChangeRotationRelative(rotation2);

            // Assert
            Assert.AreEqual(_entityTransform.Rotation, Quaternion.Add(rotation, rotation2));
        }

        [TestMethod]
        public void ChangeRotationRelative_Should_Return_Matrix_Rotated_From_Last_Rotation_Before_Scale_Translation()
        {
            // Arrange
            var translation = new Vector3(2f, 3f, 4f);
            var scale = new Vector3(5f, 6f, 7f);
            var rotation = Quaternion.CreateFromAxisAngle(new Vector3(0.1f, 0.2f, 0), 0);
            var rotationRelative = Quaternion.CreateFromAxisAngle(new Vector3(0.1f, 0.2f, 0), 1f);

            _entityTransform.ChangeTranslation(translation);
            _entityTransform.ChangeRotation(rotation);
            _entityTransform.ChangeScale(scale);

            // Act
            _entityTransform.ChangeRotationRelative(rotationRelative);

            // Assert
            var rotationMatrix = Matrix.CreateFromQuaternion(Quaternion.Add(rotation, rotationRelative));

            Assert.That.AreEquivalent(
                Matrix.CreateScale(scale)
                * rotationMatrix
                * Matrix.CreateTranslation(translation),
                _entityTransform.Transform
            );
        }

        [TestMethod]
        public void ChangeRotationRelative_Rotating_Relative_Then_Reversing_RotationRelative_Should_Go_Back_To_Identity()
        {
            // Act
            _entityTransform.ChangeRotationRelative(Quaternion.CreateFromAxisAngle(Vector3.UnitX, 1));
            _entityTransform.ChangeRotationRelative(Quaternion.CreateFromAxisAngle(Vector3.UnitY, 1));
            _entityTransform.ChangeRotationRelative(Quaternion.CreateFromAxisAngle(Vector3.UnitZ, 1));

            Assert.That.AreNotEquivalent(
                Matrix.Identity,
                _entityTransform.Transform);

            _entityTransform.ChangeRotationRelative(Quaternion.CreateFromAxisAngle(Vector3.UnitZ, -1));
            _entityTransform.ChangeRotationRelative(Quaternion.CreateFromAxisAngle(Vector3.UnitY, -1));
            _entityTransform.ChangeRotationRelative(Quaternion.CreateFromAxisAngle(Vector3.UnitX, -1));

            // Assert
            Assert.That.AreEquivalent(Matrix.Identity, _entityTransform.Transform);
        }
    }
}