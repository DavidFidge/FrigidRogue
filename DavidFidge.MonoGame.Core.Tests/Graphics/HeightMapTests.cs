using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using DavidFidge.MonoGame.Core.Graphics.Terrain;
using DavidFidge.MonoGame.Core.Tests.Services;
using DavidFidge.TestInfrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.Tests.Graphics
{
    [TestClass]
    public class HeightMapTests : BaseTest
    {
        [TestMethod]
        public void HeightMap_Constructor_Should_Create_HeightMap_With_Supplied_Dimensions()
        {
            // Act
            var heightMap = new HeightMap(10, 11);

            // Assert
            Assert.AreEqual(10, heightMap.Width);
            Assert.AreEqual(11, heightMap.Length);
        }

        [TestMethod]
        public void Area_Should_Give_Area()
        {
            // Arrange
            var heightMap = new HeightMap(5, 4);

            // Assert
            Assert.AreEqual(20, heightMap.Area);
        }

        [TestMethod]
        public void ZeroAreaPercent_Should_Give_Area_Covered_By_ZeroValues()
        {
            // Arrange
            var heightMap = new HeightMap(5, 4);

            heightMap.FromArray(
                new int[5 * 4]
                {
                    1, 1, 1, 1, 1,
                    0, 0, 0, 3, 3,
                    -1, -1, -1, -1, -1,
                    2, 2, 2, 0, 0
                });

            // Act
            var result = heightMap.ZeroAreaPercent();

            // Assert
            Assert.AreEqual(0.25d, result);
        }

        [TestMethod]
        public void Patch_Should_Patch_In_Full()
        {
            // Arrange
            var heightMap = new HeightMap(5, 4);

            var heightMapPatch = new HeightMap(3, 2)
                .FromArray(new int[3 * 2]
                    {
                        1, 2, 3,
                        4, 5, 6
                    });

            var expectedMap = new int[5 * 4]
            {
                0, 0, 0, 0, 0,
                0, 0, 1, 2, 3,
                0, 0, 4, 5, 6,
                0, 0, 0, 0, 0,
            };

            // Act
            heightMap.Patch(heightMapPatch, new Point(2, 1));

            // Assert
            CollectionAssert.AreEquivalent(expectedMap, heightMap.ToArray());
        }

        [TestMethod]
        public void Patch_Should_Patch_Partial_Right()
        {
            // Arrange
            var heightMap = new HeightMap(5, 4);

            var heightMapPatch = new HeightMap(3, 2)
                .FromArray(new int[3 * 2]
                {
                    1, 2, 3,
                    4, 5, 6
                });

            var expectedMap = new int[5 * 4]
            {
                0, 0, 0, 0, 0,
                0, 0, 0, 1, 2,
                0, 0, 0, 4, 5,
                0, 0, 0, 0, 0,
            };

            // Act
            heightMap.Patch(heightMapPatch, new Point(3, 1));

            // Assert
            CollectionAssert.AreEquivalent(expectedMap, heightMap.ToArray());
        }

        [TestMethod]
        public void Patch_Should_Patch_Partial_Left()
        {
            // Arrange
            var heightMap = new HeightMap(5, 4);

            var heightMapPatch = new HeightMap(3, 2)
                .FromArray(new int[3 * 2]
                {
                    1, 2, 3,
                    4, 5, 6
                });

            var expectedMap = new int[5 * 4]
            {
                0, 0, 0, 0, 0,
                2, 3, 0, 0, 0,
                5, 6, 0, 0, 0,
                0, 0, 0, 0, 0,
            };

            // Act
            heightMap.Patch(heightMapPatch, new Point(-1, 1));

            // Assert
            CollectionAssert.AreEquivalent(expectedMap, heightMap.ToArray());
        }

        [TestMethod]
        public void Patch_Should_Patch_Partial_Top()
        {
            // Arrange
            var heightMap = new HeightMap(5, 4);

            var heightMapPatch = new HeightMap(3, 2)
                .FromArray(new int[3 * 2]
                {
                    1, 2, 3,
                    4, 5, 6
                });

            var expectedMap = new int[5 * 4]
            {
                0, 0, 4, 5, 6,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
            };

            // Act
            heightMap.Patch(heightMapPatch, new Point(2, -1));

            // Assert
            CollectionAssert.AreEquivalent(expectedMap, heightMap.ToArray());
        }

        [TestMethod]
        public void Patch_Should_Patch_Partial_Bottom()
        {
            // Arrange
            var heightMap = new HeightMap(5, 4);

            var heightMapPatch = new HeightMap(3, 2)
                .FromArray(new int[3 * 2]
                {
                    1, 2, 3,
                    4, 5, 6
                });

            var expectedMap = new int[5 * 4]
            {
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 1, 2, 3,
            };

            // Act
            heightMap.Patch(heightMapPatch, new Point(2, 3));

            // Assert
            CollectionAssert.AreEquivalent(expectedMap, heightMap.ToArray());
        }

        [TestMethod]
        public void Patch_Should_Patch_When_Patch_Oversize()
        {
            // Arrange
            var heightMap = new HeightMap(5, 4);

            var heightMapPatch = new HeightMap(7, 6)
                .FromArray(new int[7 * 6]
                {
                    1, 2, 3, 4, 5, 6, 7,
                    8, 9, 10, 11, 12, 13, 14,
                    15, 16, 17, 18, 19, 20, 21,
                    22, 23, 24, 25, 26, 27, 28,
                    29, 30, 31, 32, 33, 34, 35,
                    36, 37, 38, 39, 40, 41, 42,
                });

            var expectedMap = new int[5 * 4]
            {
                9, 10, 11, 12, 13,
                16, 17, 18, 19, 20,
                23, 24, 25, 26, 27,
                30, 31, 32, 33, 34
            };

            // Act
            heightMap.Patch(heightMapPatch, new Point(-1, -1));

            // Assert
            CollectionAssert.AreEquivalent(expectedMap, heightMap.ToArray());
        }

        [TestMethod]
        public void Patch_Should_Not_Patch_When_Totally_Outside()
        {
            // Arrange
            var heightMap = new HeightMap(5, 4);

            var heightMapPatch = new HeightMap(3, 2)
                .FromArray(new int[3 * 2]
                {
                    1, 2, 3,
                    4, 5, 6
                });

            var expectedMap = new int[5 * 4]
            {
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
            };

            // Act
            heightMap.Patch(heightMapPatch, new Point(5, 0));

            // Assert
            CollectionAssert.AreEquivalent(expectedMap, heightMap.ToArray());
        }

        [TestMethod]
        public void Import_Should_Import_HeightMap_From_File()
        {
            // Arrange
            var heightMap = new HeightMap(3, 2)
                .FromArray(new int[3 * 2]
                {
                    1, 2, 3,
                    4, 5, 6
                });

            var tempFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}.csv";

            heightMap.Export("Test", tempFileName);

            var filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Test",
                tempFileName);

            // Act
            var result = HeightMap.Import(filePath);

            // Assert
            var expectedMap = new int[3 * 2]
            {
                1, 2, 3,
                4, 5, 6
            };

            CollectionAssert.AreEquivalent(expectedMap, result.ToArray());
        }

        [TestMethod]
        public void Export_Should_Export_HeightMap_To_File()
        {
            // Arrange
            var heightMap = new HeightMap(3, 2)
                .FromArray(new int[3 * 2]
                {
                    1, 2, 3,
                    4, 5, 6
                });

            var tempFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}.csv";

            // Act
            heightMap.Export("Test", tempFileName);

            // Assert
            var filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Test",
                tempFileName);

            var stringList = new List<string>();

            using (var streamReader = new StreamReader(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    stringList.Add(streamReader.ReadLine());
                }
            }

            Assert.AreEqual(2, stringList.Count);
            Assert.AreEqual("1,2,3", stringList[0]);
            Assert.AreEqual("4,5,6", stringList[1]);
        }

        [TestMethod]
        public void GetExactHeight_Should_Get_Height_Within_Lower_Triangle_With_A_Higher_Upper_Triangle()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 1,
                    1, 2
                });

            // Act
            var result = heightMap.GetExactHeightAt(0.5f, 0.25f);

            // Assert
            Assert.AreEqual(1f, result);
        }

        [TestMethod]
        public void GetExactHeight_Should_Get_Height_Within_Upper_Triangle_With_A_Higher_Upper_Triangle()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 1,
                    1, 2
                });

            // Act
            var result = heightMap.GetExactHeightAt(0.5f, 0.75f);

            // Assert
            Assert.AreEqual(1.25f, result);
        }

        [TestMethod]
        public void GetExactHeight_Should_Get_Height_On_Top_Boundary_With_A_Higher_Upper_Triangle()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 1,
                    1, 2
                });

            // Act
            var result = heightMap.GetExactHeightAt(0.5f, 1);

            // Assert
            Assert.AreEqual(1.5f, result);
        }

        [TestMethod]
        public void GetExactHeight_Should_Get_Height_On_Bottom_Boundary_With_A_Higher_Upper_Triangle()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 1,
                    1, 2
                });

            // Act
            var result = heightMap.GetExactHeightAt(0.5f, 0);

            // Assert
            Assert.AreEqual(1, result);
        }


        [TestMethod]
        public void GetExactHeight_Should_Get_Height_Within_Lower_Triangle_With_A_Lower_Upper_Triangle()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    2, 1,
                    1, 1
                });

            // Act
            var result = heightMap.GetExactHeightAt(0.5f, 0.25f);

            // Assert
            Assert.AreEqual(1.25f, result);
        }

        [TestMethod]
        public void GetExactHeight_Should_Get_Height_Within_Upper_Triangle_With_A_Lower_Upper_Triangle()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    2, 1,
                    1, 1
                });

            // Act
            var result = heightMap.GetExactHeightAt(0.5f, 0.75f);

            // Assert
            Assert.AreEqual(1f, result);
        }

        [TestMethod]
        public void GetExactHeight_Should_Get_Height_On_Top_Boundary_With_A_Lower_Upper_Triangle()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    2, 1,
                    1, 1
                });

            // Act
            var result = heightMap.GetExactHeightAt(0.5f, 1);

            // Assert
            Assert.AreEqual(1f, result);
        }

        [TestMethod]
        public void GetExactHeight_Should_Get_Height_On_Bottom_Boundary_With_A_Lower_Upper_Triangle()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    2, 1,
                    1, 1
                });

            // Act
            var result = heightMap.GetExactHeightAt(0.5f, 0);

            // Assert
            Assert.AreEqual(1.5f, result);
        }

        [TestMethod]
        public void GetExactHeight_Should_Get_Height_In_Middle()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 2,
                    3, 4
                });

            // Act
            var result = heightMap.GetExactHeightAt(0.5f, 0.5f);

            // Assert
            Assert.AreEqual(2.5f, result);
        }

        [TestMethod]
        public void Linear_Search_For_Length_2_HeightMap_Should_Return_Ray_Containing_Intersection_Point_In_First_Half()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 2,
                    3, 4
                });

            var position = new Vector3(0, 0, 2);
            var direction = new Vector3(0, 1, 0);

            var ray = new Ray(position, direction);

            // Act
            var result = heightMap.LinearSearch(ray, 2);

            // Assert
            var expectedPosition = new Vector3(0, 0f, 2);
            var expectedDirection = new Vector3(0, 0.5f, 0);

            Assert.That.AreEquivalent(expectedPosition, result.Value.Position);
            Assert.That.AreEquivalent(expectedDirection, result.Value.Direction);
        }

        [TestMethod]
        public void Linear_Search_For_Length_2_HeightMap_Should_Return_Ray_Containing_Intersection_Point_In_Last_Half()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 2,
                    3, 4
                });

            var position = new Vector3(0, 0, 2.5f);
            var direction = new Vector3(0, 1, 0);

            var ray = new Ray(position, direction);

            // Act
            var result = heightMap.LinearSearch(ray, 2);

            // Assert
            var expectedPosition = new Vector3(0, 0.5f, 2.5f);
            var expectedDirection = new Vector3(0, 0.5f, 0);

            Assert.That.AreEquivalent(expectedPosition, result.Value.Position);
            Assert.That.AreEquivalent(expectedDirection, result.Value.Direction);
        }

        [TestMethod]
        public void Linear_Search_Should_Return_Null_Ray_If_No_Collision()
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 2,
                    3, 4
                });

            var position = new Vector3(0, 0, 4f);
            var direction = new Vector3(0, 1, 0);

            var ray = new Ray(position, direction);

            // Act
            var result = heightMap.LinearSearch(ray, 2);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Linear_Search_For_Length_5_HeightMap_With_2_Possible_Intersections_Should_Return_Ray_For_First_Intersection()
        {
            // Arrange
            var heightMap = new HeightMap(5, 5)
                .FromArray(new int[5 * 5]
                {
                    1, 0, 0, 0, 0,
                    3, 0, 0, 0, 0,
                    2, 0, 0, 0, 0,
                    4, 0, 0, 0, 0,
                    1, 0, 0, 0, 0
                });

            var position = new Vector3(0, 0, 2.5f);
            var direction = new Vector3(0, 10, 0);

            var ray = new Ray(position, direction);

            // Act
            var result = heightMap.LinearSearch(ray, 8);

            // Assert
            var expectedPosition = new Vector3(0, 0.5f, 2.5f);
            var expectedDirection = new Vector3(0, 0.5f, 0);

            Assert.That.AreEquivalent(expectedPosition, result.Value.Position);
            Assert.That.AreEquivalent(expectedDirection, result.Value.Direction);
        }

        [TestMethod]
        public void Linear_Search_For_Length_5_HeightMap_With_Diagonal_Ray_Returns_Position_At_Furthest_Corner()
        {
            // Arrange
            var heightMap = new HeightMap(5, 5)
                .FromArray(new int[5 * 5]
                {
                    0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0,
                    0, 0, 0, 0, 1
                });

            var position = new Vector3(0, 0, 1f);
            var direction = new Vector3(1, 1, 0);

            var ray = new Ray(position, direction);

            // Act
            var result = heightMap.LinearSearch(ray, 8);

            // Assert
            var expectedPosition = new Vector3(3.5f, 3.5f, 1f);
            var expectedDirection = new Vector3(0.5f, 0.5f, 0);

            Assert.That.AreEquivalent(expectedPosition, result.Value.Position);
            Assert.That.AreEquivalent(expectedDirection, result.Value.Direction);
        }

        [TestMethod]
        [DataRow(0.01f)]
        [DataRow(0.0001f)]
        public void Binary_Search_Should_Find_Exact_Height_For_Point_In_Second_Half(float accuracy)
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 2,
                    3, 4
                });

            var position = new Vector3(0, 0, 2.5f);
            var direction = new Vector3(0, 1, 0);

            var ray = new Ray(position, direction);

            // Act
            var result = heightMap.BinarySearch(ray, accuracy);

            // Assert
            var expectedPosition = new Vector3(0, 0.75f, 2.5f);

            Assert.AreEqual(expectedPosition.X, result.X);
            Assert.AreEqual(expectedPosition.Z, result.Z);

            var height = heightMap.GetExactHeightAt(result.X, result.Y);

            // the exact height should be slightly under i.e. the point
            // found should slightly hover above if no exact match found
            Assert.IsTrue(height <= 2.5f);
            Assert.IsTrue(2.5f - height <= accuracy);
        }

        [TestMethod]
        [DataRow(0.01f)]
        [DataRow(0.0001f)]
        public void Binary_Search_Should_Find_Exact_Height_For_Point_In_First_Half(float accuracy)
        {
            // Arrange
            var heightMap = new HeightMap(2, 2)
                .FromArray(new int[2 * 2]
                {
                    1, 2,
                    3, 4
                });

            var position = new Vector3(0, 0, 1.5f);
            var direction = new Vector3(0, 1, 0);

            var ray = new Ray(position, direction);

            // Act
            var result = heightMap.BinarySearch(ray, accuracy);

            // Assert
            var expectedPosition = new Vector3(0, 0.25f, 1.5f);

            Assert.AreEqual(expectedPosition.X, result.X);
            Assert.AreEqual(expectedPosition.Z, result.Z);

            var height = heightMap.GetExactHeightAt(result.X, result.Y);

            // the exact height should be slightly under i.e. the point
            // found should slightly hover above if no exact match found
            Assert.IsTrue(height <= expectedPosition.Z);
            Assert.IsTrue(expectedPosition.Z - height <= accuracy);
        }
    }
}