using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyVision : MonoBehaviour 
    {
        private const string TargetTag = "Player";
        
        [SerializeField] private EnemyMove EnemyMove;
        [SerializeField] private int Rays = 6;
        [SerializeField] private int Distance = 15;
        [SerializeField] private float Angle = 20;
        [SerializeField] private Vector3 Offset;
        
        private Transform _target;

        private void Start() 
        {
            _target = GameObject.FindGameObjectWithTag(TargetTag).transform;
        }

        private bool GetRaycast(Vector3 dir)
        {
            bool result = false;
            RaycastHit hit = new RaycastHit();
            Vector3 pos = transform.position + Offset;
            if (Physics.Raycast (pos, dir, out hit, Distance))
            {
                if(hit.transform == _target)
                {
                    result = true;
                    Debug.DrawLine(pos, hit.point, Color.green);
                }
                else
                {
                    Debug.DrawLine(pos, hit.point, Color.blue);
                }
            }
            else
            {
                Debug.DrawRay(pos, dir * Distance, Color.red);
            }
            return result;
        }
	
        private bool RayToScan() 
        {
            bool result = false;
            bool a = false;
            bool b = false;
            float j = 0;
            for (int i = 0; i < Rays; i++)
            {
                var x = Mathf.Sin(j);
                var y = Mathf.Cos(j);

                j += Angle * Mathf.Deg2Rad / Rays;

                Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
                if(GetRaycast(dir)) a = true;

                if(x != 0) 
                {
                    dir = transform.TransformDirection(new Vector3(-x, 0, y));
                    if(GetRaycast(dir)) b = true;
                }
            }
	
            if(a || b) result = true;
            return result;
        }

        private void Update ()
        {	
            if(Vector3.Distance(transform.position, _target.position) < Distance)
            {
                if(RayToScan())
                {
                    EnemyMove.StartChasing();
                }
            }
        }
    }
}
