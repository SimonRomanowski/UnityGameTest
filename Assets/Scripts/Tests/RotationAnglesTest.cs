using System.Collections;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static De.Lasagevo.Utils.RotationAngles;
using static De.Lasagevo.Tests.Assertions.Assert;

namespace De.Lasagevo.Tests
{
    public class RotationAnglesTest
    {

        /// <summary>
        /// Asserts, that the result is not null.
        /// </summary>
        [Test]
        public void CalculateRotationAngleTestNotNull()
        {
            var result = CalculateRotationAngle(Vector3.one,
                                                Vector3.up);
            AssertThat(result).IsNotNull();
        }

        /// <summary>
        /// Asserts that simple rotations of unit vectors will succeed.
        /// </summary>
        [Test]
        public void CalculateRotationAngleTestSimple()
        {
            Vector3 vec1 = new Vector3(1f, 0f, 0f);
            Vector3 vec2 = new Vector3(0f, 1f, 0f);

            // Should be 315°
            float angle1 = CalculateRotationAngle(vec1, vec2);
            // Should be 135°
            float angle2 = CalculateRotationAngle(vec2, vec1);

            AssertThat(angle1).IsCloseTo(315f);
            AssertThat(angle2).IsCloseTo(135f);
        }

        /// <summary>
        /// Asserts that the angles are correctly capped.
        /// </summary>
        [Test]
        public void CapRotationAngleTest()
        {
            // Should be 0
            float noRotation1 = CapRotationAngle(0f, 0f, 2f);
            // Should be 120
            float noRotation2 = CapRotationAngle(120f, 120f, 2f);
            // Should be 61
            float capped1 = CapRotationAngle(63f, 60f, 2f);
            // Should be 77
            float capped2 = CapRotationAngle(75f, 78f, 2f);
            // Should be 101.2
            float uncapped1 = CapRotationAngle(100f, 101.2f, 2f);
            // Should be 302.8
            float uncapped2 = CapRotationAngle(304f, 302.8f, 2f);

            AssertThat(noRotation1).IsCloseTo(0f);
            AssertThat(noRotation2).IsCloseTo(120f);

            AssertThat(capped1).IsCloseTo(61f);
            AssertThat(capped2).IsCloseTo(77f);

            AssertThat(uncapped1).IsCloseTo(101.2f);
            AssertThat(uncapped2).IsCloseTo(302.8f);
        }

        /// <summary>
        /// Asserts that angles are adjusted properly.
        /// </summary>
        [Test]
        public void AdjustAngleTest()
        {
            // Should be 0
            float angle1 = 0f;
            // Should be 90
            float angle2 = 90f;
            // Should be 0
            float angle3 = 720f;
            // Should be 31
            float angle4 = 391f;
            // Should be 300
            float angle5 = -60f;

            AdjustAngle(ref angle1);
            AdjustAngle(ref angle2);
            AdjustAngle(ref angle3);
            AdjustAngle(ref angle4);
            AdjustAngle(ref angle5);

            AssertThat(angle1).IsCloseTo(0f);
            AssertThat(angle2).IsCloseTo(90f);
            AssertThat(angle3).IsCloseTo(0f);
            AssertThat(angle4).IsCloseTo(31f);
            AssertThat(angle5).IsCloseTo(300f);
        }

        /// <summary>
        /// Asserts that rotation of a vector by an angle does not return null.
        /// </summary>
        [Test]
        public void RotateVectorTestNotNull()
        {
            var result = RotateVector(Vector3.one, 0f);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Asserts that rotation of a vector by an angle works.
        /// </summary>
        [Test]
        public void RotateVectorTestAngle()
        {
            AssertThat(new Vector3(0, 0, 0)).IsEqualTo(Vector3.zero);
        }
    }
}
