using UnityEngine;

namespace StarterPack.Gameplay.StarterPack
{
    public class CameraController : Singleton<CameraController>
    {
        [SerializeField] private Transform Target;
        [SerializeField] private float Speed = 10f;
        [SerializeField] private Vector3 Offset;
        
        private void Start() => 
            EventManager.OnStartGame.Subscribe(SetTarget);

        private void SetTarget() => 
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        
        private void LateUpdate()
        {
            if (!Target)
                return;

            transform.position = Vector3.Lerp(transform.position, Target.position + Offset, Time.deltaTime * Speed);
        }
    }
}
