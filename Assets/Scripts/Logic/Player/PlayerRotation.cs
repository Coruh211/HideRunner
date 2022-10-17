using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        private Vector2 _rotateDirection;

        public void OnMove(InputAction.CallbackContext context)
        {
            _rotateDirection = context.ReadValue<Vector2>();
        }

        public void Update()
        {
            Rotate();            
        }
        
        private void Rotate()
        {
            if (_rotateDirection == Vector2.up)
            {
                transform.eulerAngles = new Vector3(0,0 ,0);
            }
            else if (_rotateDirection == Vector2.down)
            {
                transform.eulerAngles = new Vector3(0,180 ,0);
            }
            
            if (_rotateDirection == Vector2.left)
            {
                transform.eulerAngles = new Vector3(0,-90 ,0);
            }
            else if (_rotateDirection == Vector2.right)
            {
                transform.eulerAngles = new Vector3(0, 90,0);
            }

        }
    }
}