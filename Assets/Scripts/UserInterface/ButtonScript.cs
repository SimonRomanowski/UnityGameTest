using De.Lasagevo.GameSystems;
using UnityEngine;

namespace De.Lasagevo.UserInterface
{

    public class ButtonScript : MonoBehaviour
    {

        private void OnMouseDown()
        {
            GameState.StartGame();
            Destroy(gameObject);
        }
    }

}