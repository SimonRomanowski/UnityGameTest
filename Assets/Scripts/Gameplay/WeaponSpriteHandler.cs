using De.Lasagevo.GameSystems;
using UnityEngine;

namespace De.Lasagevo.Gameplay
{

    public class WeaponSpriteHandler : MonoBehaviour
    {

        /// <summary>
        /// Player sprite addition for holding an assault rifle.
        /// </summary>
        public Sprite pistolSprite;

        /// <summary>
        /// Player sprite addition for holding a pistol.
        /// </summary>
        public Sprite assaultRifleSprite;

        /// <summary>
        /// Player object in the game. Set in Start().
        /// </summary>
        private GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            SetSprite();
        }

        /// <summary>
        /// Sets the sprite that is currently needed based on the equipped
        /// weapon.
        /// </summary>
        private void SetSprite()
        {
            var weaponType = GameState.GetCurrentWeapon().GetType();
            var spriteRenderer = GetComponent<SpriteRenderer>();
            // Switch on type does not exist in .NET 4.0....
            if (weaponType.Equals(typeof(Pistol)))
            {
                spriteRenderer.sprite = pistolSprite;
            }
            else if (weaponType.Equals(typeof(AssaultRifle)))
            {
                spriteRenderer.sprite = assaultRifleSprite;
            }
            else
            {
                // Type not recognized, default to pistol
                Debug.Log("Weapon type not recognized. Defaulting to Pistol.");
                spriteRenderer.sprite = pistolSprite;
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
        }
    }

}