using UnityEngine;

namespace De.Lasagevo.Utils
{

    public class RotationAngles
    {

        /// <summary>
        /// Calculates the 2D rotation angle between the two
        /// vectors in degrees.
        /// Example: you are standing on (1, 0, 0) and you want to rotate
        /// towards (0, 1, 0). Facing left is interpreted as a 0° angle,
        /// and facing down is a 90° angle. This means that to face to
        /// (0, 1, 0) you need to turn to 315°, which is the value this
        /// function returns.
        /// </summary>
        /// <param name="fromVec">Vector to rotate from.</param>
        /// <param name="toVec">Vector to rotate towards.</param>
        /// <returns>2D rotation angle between the two
        /// vectors in degrees</returns>
        public static float CalculateRotationAngle(Vector3 fromVec, 
                                                   Vector3 toVec)
        {
            var diffX = fromVec.x - toVec.x;
            var diffY = fromVec.y - toVec.y;
            var angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
            AdjustAngle(ref angle);
            return angle;
        }

        /// <summary>
        /// Caps the rotation from 'currentAngle' by 'rotationAngle' 
        /// to a maximum of 'cap'.
        /// </summary>
        /// <param name="rotatingFrom">Current angle in degrees.</param>
        /// <param name="rotatingTo">Angle that is being rotated to in
        ///     degrees.</param>
        /// <param name="cap">Maximum turning angle in degrees.</param>
        /// <returns>The new angle in degrees.</returns>
        public static float CapRotationAngle(float rotatingFrom,
                                             float rotatingTo,
                                             float cap)
        {
            AdjustAngle(ref rotatingFrom);
            AdjustAngle(ref rotatingTo);
            // Calculate angle that rotates clockwise
            // from 'current' to 'rotation'
            float clockwiseRotation = rotatingFrom - rotatingTo;
            AdjustAngle(ref clockwiseRotation);
            // Is the rotation within the rotation cap?
            if (clockwiseRotation <= cap)
            {
                return rotatingTo;
            }
            else if (360 - clockwiseRotation <= cap)
            {
                return rotatingTo;
            }
            else
            {
                // Is it faster to rotate clockwise?
                if (clockwiseRotation >= 180)
                {
                    return rotatingFrom + cap;
                }
                else
                {
                    return rotatingFrom - cap;
                }
            }
        }

        /// <summary>
        /// Returns 'angle mod 360' with the mathematical modulo operator.
        /// </summary>
        /// <param name="angle">The angle to adjust.</param>
        public static void AdjustAngle(ref float angle)
        {
            angle = ((angle % 360) + 360) % 360;
        }

        /// <summary>
        /// Rotates the vector by the given angle (in degrees).
        /// </summary>
        /// <param name="vector">The vector to rotate</param>
        /// <param name="angle">The angle to rotate by in degrees</param>
        /// <returns>The rotated vector</returns>
        public static Vector3 RotateVector(Vector3 vector, float angle)
        {
            // Interpret angle as radians for trig functions
            angle *= Mathf.Deg2Rad;
            // Add current angle
            angle += Mathf.Atan2(vector.y, vector.x);
            // Return a new vector rotated by the angle and stretched to the
            // original length
            return new Vector3(Mathf.Cos(angle) * vector.magnitude,
                               Mathf.Sin(angle) * vector.magnitude,
                               0);
        }

        public static Vector3 RotateVector(Vector3 vector, Quaternion angle)
        {
            return RotateVector(vector, angle.eulerAngles.z);
        }

    }

}