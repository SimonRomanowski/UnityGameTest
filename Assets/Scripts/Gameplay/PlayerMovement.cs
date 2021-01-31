using De.Lasagevo.GameSystems;
using UnityEngine;
using De.Lasagevo.Utils;
using UnityEngine.UI;

namespace De.Lasagevo.Gameplay
{

    public class PlayerMovement : MonoBehaviour
    {

        /// <summary>
        /// Maximumum angle of a rotation per second in degrees.
        /// </summary>
        private const float RotationCap = 360f;

        /// <summary>
        /// Players movement speed.
        /// </summary>
        private const float MovementSpeed = 5f;

        public new Rigidbody2D rigidbody;

        // Update is called once per frame
        void Update()
        {
            // Only move while the game is running.
            if (GameState.GameIsRunning)
            {
                RotateSprite();
                MoveSprite();
            }
        }

        private void MoveSprite()
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            // Square for faster acceleration and faster deceleration
            verticalInput *= Mathf.Abs(verticalInput);
            horizontalInput *= Mathf.Abs(horizontalInput);
            var velocity = new Vector3(horizontalInput, verticalInput, 0);
            velocity *= MovementSpeed;
            rigidbody.velocity = velocity;
        }

        /// <summary>
        /// Rotates the sprite to point towards the cursor.
        /// </summary>
        private void RotateSprite()
        {
            var mousePosition = Camera.main
                                .ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            var rotationAngle =
                RotationAngles.CalculateRotationAngle(transform.position,
                                                      mousePosition);
            // Cap the performed rotation
            var currentAngle = transform.rotation.eulerAngles.z;
            var cappedAngle =
                RotationAngles.CapRotationAngle(currentAngle,
                                                rotationAngle,
                                                RotationCap * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, cappedAngle);
        }
    }

}