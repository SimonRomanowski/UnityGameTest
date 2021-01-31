using De.Lasagevo.Gameplay;
using De.Lasagevo.GameSystems;
using De.Lasagevo.Utils;
using System.Diagnostics;
using UnityEngine;

namespace De.Lasagevo.Enemies
{

    public class EnemyPyromancerBehaviour : MonoBehaviour
    {

        /// <summary>
        /// Player object in the game. Initialized in Start().
        /// </summary>
        private GameObject player;

        /// <summary>
        /// Value representing this units health.
        /// </summary>
        private int health = 100;

        /// <summary>
        /// Bullet prefab set in UnityEditor.
        /// </summary>
        public GameObject bulletPrefab;

        /// <summary>
        /// Stopwatch keeping track of the time the last shot was fired.
        /// </summary>
        private readonly Stopwatch lastShotStopwatch = new Stopwatch();

        /// <summary>
        /// Shot delay in milliseconds.
        /// </summary>
        private const int ShotDelay = 1000;

        /// <summary>
        /// Maximum amount of degrees turned per second.
        /// </summary>
        private const float RotationCap = 360f;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (GameState.GameIsRunning)
            {
                RotateSprite();
                Attack();
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("PistolBullet"))
            {
                TakeDamage(new Pistol().GetDamage());
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("AssaultRifleBullet"))
            {
                TakeDamage(new AssaultRifle().GetDamage());
                Destroy(collision.gameObject);
            }
        }

        /// <summary>
        /// Deal damage to this unit. If this units health drops below 0, it
        /// will be removed from the game.
        /// </summary>
        /// <param name="amount">The amount of damage to deal.</param>
        private void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                KillThisUnit();
            }
        }

        /// <summary>
        /// Remove this GameObject from this game.
        /// </summary>
        private void KillThisUnit()
        {
            Destroy(gameObject);
        }

        private void Attack()
        {
            if (ShotIsReady())
            {
                lastShotStopwatch.Restart();
                SpawnBullet();
            }
        }

        private void SpawnBullet()
        {
            var currentRot = transform.rotation.eulerAngles.z;
            var rotation = Quaternion.Euler(0, 0, currentRot);
            Instantiate(bulletPrefab,
                        transform.position,
                        rotation);
        }

        private bool ShotIsReady()
        {
            if (lastShotStopwatch.IsRunning)
            {
                return lastShotStopwatch.ElapsedMilliseconds > ShotDelay;
            }
            else
            {
                // Don't fire on the first tick
                lastShotStopwatch.Start();
                return false;
            }
        }

        private void RotateSprite()
        {
            var playerPosition = player.transform.position;
            var newRotationAngle =
                RotationAngles.CalculateRotationAngle(transform.position,
                                                      playerPosition);
            // Cap rotation
            var currentRotation = transform.rotation.eulerAngles.z;
            newRotationAngle =
                RotationAngles.CapRotationAngle(currentRotation,
                                                newRotationAngle,
                                                RotationCap * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newRotationAngle);
        }
    }

}