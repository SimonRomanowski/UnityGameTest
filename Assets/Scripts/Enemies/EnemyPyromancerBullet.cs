using De.Lasagevo.Gameplay;
using De.Lasagevo.GameSystems;
using De.Lasagevo.Utils;
using System.Diagnostics;
using UnityEngine;

namespace De.Lasagevo.Enemies
{
    class EnemyPyromancerBullet : MonoBehaviour
    {

        /// <summary>
        /// Life duration of a bullet in milliseconds.
        /// </summary>
        private static readonly long LifeDuration = 3000;

        /// <summary>
        /// Stopwatch keeping track of this bullets lifetime.
        /// </summary>
        private readonly Stopwatch lifeStopwatch = new Stopwatch();

        /// <summary>
        /// Units the bullet travels per second.
        /// </summary>
        private const float BulletSpeed = 7f;

        /// <summary>
        /// Current velocity of the bullet;
        /// </summary>
        private Vector3 velocity = Vector3.zero;

        /// <summary>
        /// Offset from player position to tip of the weapon.
        /// </summary>
        private readonly Vector3 OffsetVector =
            new Vector3(-0.4f, 0.2f, 0f);

        /// <summary>
        /// Damage a bullet deals to the player on hit.
        /// </summary>
        private const int Damage = 3;

        // Start is called before the first frame update
        void Start()
        {
            // Adjust transform
            AdjustPosition();
            AdjustVelocity();
            AdjustRotation();
            // Start counter
            lifeStopwatch.Start();
        }

        private void AdjustRotation()
        {
            var towards = GameObject.FindGameObjectWithTag("Player")
                          .transform.position;
            var newAngle =
                RotationAngles.CalculateRotationAngle(transform.position,
                                                      towards);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
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

        void OnCollisionEnter2D(Collision2D collision)
        {
            var collidingObject = collision.gameObject;
            if (collidingObject.CompareTag("Player"))
            {
                // Deal damage, then destroy this GameObject
                PlayerStats.TakeDamage(Damage);
                Destroy(gameObject);
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
