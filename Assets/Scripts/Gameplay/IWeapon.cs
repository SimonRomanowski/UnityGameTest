using UnityEngine;

namespace De.Lasagevo.Gameplay
{
    interface IWeapon
    {

        /// <summary>
        /// Returns bullet delay in miliseconds.
        /// </summary>
        int GetBulletDelay();

        /// <summary>
        /// Returns the damge of a bullet per hit.
        /// </summary>
        int GetDamage();

        /// <summary>
        /// Returns the prefab to generate the bullet object in the game.
        /// </summary>
        /// <returns>The weapons bullets prefab.</returns>
        GameObject GetBulletPrefab();
    }

    public class Pistol : IWeapon
    {

        /// <summary>
        /// Bullet delay in milliseconds.
        /// </summary>
        private const int BulletDelay = 1000;

        /// <summary>
        /// Damage per shot.
        /// </summary>
        private const int Damage = 10;

        /// <summary>
        /// Prefab to generate the bullet object in the game.
        /// </summary>
        private GameObject bulletPrefab;

        public int GetBulletDelay()
        {
            return BulletDelay;
        }

        public GameObject GetBulletPrefab()
        {
            if(bulletPrefab != null)
            {
                return bulletPrefab;
            }
            else
            {
                bulletPrefab = WeaponAssets.Instance.pistolBulletPrefab;
                return bulletPrefab;
            }
        }

        public int GetDamage()
        {
            return Damage;
        }
    }

    public class AssaultRifle : IWeapon
    {

        /// <summary>
        /// Bullet delay in milliseconds.
        /// </summary>
        private const int BulletDelay = 200;

        /// <summary>
        /// Damage per shot.
        /// </summary>
        private const int Damage = 2;

        /// <summary>
        /// Prefab to generate the bullet object in the game.
        /// </summary>
        private GameObject bulletPrefab;

        public int GetBulletDelay()
        {
            return BulletDelay;
        }

        public GameObject GetBulletPrefab()
        {
            if (bulletPrefab != null)
            {
                return bulletPrefab;
            }
            else
            {
                bulletPrefab = WeaponAssets.Instance.assaultRifleBulletPrefab;
                return bulletPrefab;
            }
        }

        public int GetDamage()
        {
            return Damage;
        }
    }
}
