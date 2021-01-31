using NUnit.Framework;
using System;
using UnityEngine;

namespace De.Lasagevo.Tests.Assertions
{
    public class Assert
    {
        /// <summary>
        /// Generate an assertion object of type float with special methods.
        /// </summary>
        /// <param name="f">The assertion parameter</param>
        /// <returns>An assertion object that does assertions on 'f'</returns>
        public static AssertionObjectFloat AssertThat(float f)
        {
            return new AssertionObjectFloat(f);
        }

        /// <summary>
        /// Generate an assertion object of type T.
        /// </summary>
        /// <param name="value">The assertion parameter</param>
        /// <returns>An assertion object that does assertions on 'value'
        /// </returns>
        public static AssertionObject<T> AssertThat<T>(T value)
        {
            return new AssertionObject<T>(value);
        }
    }

}
