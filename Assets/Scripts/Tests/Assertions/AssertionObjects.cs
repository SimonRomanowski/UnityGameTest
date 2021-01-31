using NUnit.Framework;
using System;
using UnityEngine;

namespace De.Lasagevo.Tests.Assertions
{

    public class AssertionObject<T>
    {
        protected T value;

        public AssertionObject(T value)
        {
            this.value = value;
        }

        public void IsEqualTo(T other)
        {
            if (!value.Equals(other))
            {
                string message = StandardErrorMessage(value,
                                                      "equal to",
                                                      other);
                throw new AssertionException(message);
            }
        }

        /// <summary>
        /// Asserts that the assertion parameter is null.
        /// </summary>
        public void IsNull()
        {
            if (value != null)
            {
                string message = StandardErrorMessage(value, "", "null");
                throw new AssertionException(message);
            }
        }

        public void IsNotNull()
        {
            if(value == null)
            {
                string message =
                    "Expected:\n\t" + value + "\n" +
                    "to be not be null, but it was.";
                throw new AssertionException(message);
            }
        }

        /// <summary>
        /// <para>Returns a stirng of the form:</para>
        /// <para>Expected:</para> 
        /// <para>'actual'</para>
        /// <para>to be 'adjective':</para>
        /// <para>'expected'</para>
        /// <para>but it was not.</para>
        /// </summary>
        /// <param name="actual">Actual value</param>
        /// <param name="adjective">Adjective to connect expected and actual
        /// </param>
        /// <param name="expected">Expected value</param>
        /// <returns></returns>
        protected static string StandardErrorMessage(object actual,
                                            string adjective,
                                            object expected)
        {
            return "Expected:\n\t" + actual + "\n" +
                   "to be " + adjective + ":\n\t" + expected + "\n" +
                   "but it was not.";
        }


    }

    public class AssertionObjectFloat : AssertionObject<float>
    {

        /// <summary>
        /// Acceptable relative difference of two float values to be considered
        /// 'close to another'.
        /// </summary>
        private const float RelativeTolerance = 0.00001f;

        /// <summary>
        /// Creates an assertion object of type float with 
        /// special methods.
        /// </summary>
        /// <param name="value">Assertion parameter</param>
        public AssertionObjectFloat(float value)
            : base(value)
        {
            this.value = value;
        }

        /// <summary>
        /// Asserts that the assertion parameter is close to 'other', where
        /// close is defined as within 0.001% of the assertion parameter.
        /// </summary>
        /// <param name="other">The value to check</param>
        public void IsCloseTo(float other)
        {
            float relativeDifference = Mathf.Abs(value - other) / value;
            if (relativeDifference > RelativeTolerance)
            {
                string message =
                    StandardErrorMessage(value, "close to", other) + "\n" +
                    "Relative difference was\n\t" + relativeDifference + "\n" +
                    "which is greater than the tolerated difference " +
                    RelativeTolerance;
                throw new AssertionException(message);
            }
        }
    }

}
