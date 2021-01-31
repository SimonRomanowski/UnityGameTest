using De.Lasagevo.GameSystems;
using De.Lasagevo.Utils;
using System.Diagnostics;
using UnityEngine;

namespace De.Lasagevo.Gameplay
{

    public class AssaultRifleBullet : MonoBehaviour
    {

        /// <summary>
        /// Units the bullet travels per second.
        /// </summary>
        private const float BulletSpeed = 10f;

        /// <summary>
        /// Stopwatch keeping track of this bullets lifetime.
        /// </summary>
        private readonly Stopwatch lifeStopwatch = new Stopwatch();

        /// <summary>
        /// Current velocity of the bullet;
        /// </summary>
        private Vector3 velocity = Vector3.zero;

        /// <summary>
        /// Life duration of a bullet in milliseconds.
        /// </summary>
        private static readonly long LifeDuration = 3000;

        /// <summary>
        /// Offset from player position to tip of the weapon.
        /// </summary>
        private static readonly Vector3 OffsetVector = 
            new Vector3(-0.6f, 0.15f, 0f);

        // Start is called before the first frame update
        void Start()
        {
            lifeStopwatch.Start();
            AdjustPosition();
            AdjustVelocity();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameState.GameIsRunning)
            {
                // Restart timer if it has been stopped
                if (!lifeStopwatch.IsRunning)
                {
                    lifeStopwatch.Start();
                }
                // Is the bullet supposed to be destroyed?
                if (lifeStopwatch.ElapsedMilliseconds < LifeDuration)
                {
                    // Move this bullet
                    transform.position += velocity * Time.deltaTime;
                }
                else
                {
                    GameObject.Destroy(gameObject);
                }
            }
            else // Game is paused
            {
                // Stop counting, if the game is paused
                lifeStopwatch.Stop();
            }
        }

        /// <summary>
        /// Adjusts the velocity of the bullet, based on its rotation.
        /// </summary>
        private void AdjustVelocity()
        {
            float angle = (transform.rotation.eulerAngles.z);
            var velocityVector =
                RotationAngles.RotateVector(Vector3.left, angle);
            velocity = velocityVector * BulletSpeed;
        }

        /// <summary>
        /// Adjusts the position of the bullet to be located at the tip of the
        /// gun instead of on the center of the player.
        /// </summary>
        private void AdjustPosition()
        {
            var positionOffset = OffsetVector;
            var currentRot = transform.rotation.eulerAngles.z;
            positionOffset =
                RotationAngles.RotateVector(positionOffset, currentRot);
            transform.position += positionOffset;
        }
    }

}