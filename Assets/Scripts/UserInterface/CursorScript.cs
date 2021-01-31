using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace De.Lasagevo.UserInterface
{

    public class CursorScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // Hide default cursor
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
    }

}