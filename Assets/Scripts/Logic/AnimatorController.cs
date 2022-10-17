using UnityEngine;

namespace Logic
{
    public class AnimatorController: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int State1 = Animator.StringToHash("State");

        public void SetState(int state)
        {
            _animator.SetInteger(State1, state);
        }
    }
}