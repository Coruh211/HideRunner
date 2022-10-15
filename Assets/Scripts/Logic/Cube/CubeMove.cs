using UnityEngine;

namespace Logic.Logic.Cube
{
    public class CubeMove : MonoBehaviour
    {
        private int _speed;
        private float _startPositionZ;
        private float _endPositionZ;

        public void SetParameters(int speed, int distance)
        {
            _speed = speed;
            _startPositionZ = transform.position.z;
            _endPositionZ = _startPositionZ + distance;
        }

        public void Update()
        {
            if (transform.position.z < _endPositionZ)
            {
                var position = transform.position;
                position = new Vector3(position.x, position.y ,position.z + _speed * Time.deltaTime);
                transform.position = position;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}