using System.Diagnostics;
using UnityEngine;
using De.Lasagevo.GameSystems;

namespace De.Lasagevo.Gameplay
{

    public class PlayerAction : MonoBehaviour
    {

        /// <summary>
        /// Currently equipped weapon.
        /// </summary>
        private IWeapon weapon;

        /// <summary>
        /// Stopwatch keeping track of the time, the last shot was fired.
        /// </summary>
        private readonly Stopwatch lastShotStopwatch = new Stopwatch();

        /// <summary>
        /// The key the user needs to press in order to shoot.
        /// </summary>
        private const KeyCode ShootKey = KeyCode.Space;

        // Start is called before the first frame update
        void Start()
        {
            weapon = GameState.GetCurrentWeapon();
        }

        // Update is called once per frame
        void Update()
        {
            // Only act while the game is running
            if (GameState.GameIsRunning)
            {
                if (Input.GetKey(ShootKey) && ShotIsReady())
                {
                    // Restart the timer of the stopwatch
                    lastShotStopwatch.Restart();
                    SpawnBullet();
                }
            }
        }

        /// <summary>
        /// Returns true if and only if the current weapon is ready to shoot
        /// again.
        /// </summary>
        /// <returns>True, if the weapon can shoot again, false else.</returns>
        private bool ShotIsReady()
        {
            // Is this the first time?
            if (lastShotStopwatch.IsRunning)
            {
                return lastShotStopwatch.ElapsedMilliseconds 
                       > weapon.GetBulletDelay();
            }
            else
            {
                lastShotStopwatch.Start();
                return true;
            }
        }

        /// <summary>
        /// Spawns a bullet of the currently equipped weapon at the players
        /// position with the players rotation.
        /// </summary>
        private void SpawnBullet()
        {
            var currentRot = transform.rotation.eulerAngles.z;
            var rotation = Quaternion.Euler(0, 0, currentRot);
            Instantiate(weapon.GetBulletPrefab(),
                        transform.position,
                        rotation);
        }
    }

}