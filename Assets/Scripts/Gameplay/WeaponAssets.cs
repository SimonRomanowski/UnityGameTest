using UnityEngine;

namespace De.Lasagevo.Gameplay
{

    public class WeaponAssets : MonoBehaviour
    {

        /// <summary>
        /// Prefab for a pistol bullet. Set in UnityEditor.
        /// </summary>
        public GameObject pistolBulletPrefab;

        /// <summary>
        /// Prefab for an assault rifle bullet. Set in UnityEditor.
        /// </summary>
        public GameObject assaultRifleBulletPrefab;

        /// <summary>
        /// Instance of this class to reference from anywhere. The instance
        /// is set in Unitys Start()-method, therefore will be null until
        /// this step is finished. Always check for null.
        /// </summary>
        public static WeaponAssets Instance;

        // Start is called before the first frame update
        public void Start()
        {
            Instance = this;
        }

    }
}
