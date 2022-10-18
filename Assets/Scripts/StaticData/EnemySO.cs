using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "GameLogicSO/EnemySO", fileName = "EnemySO")]
    public class EnemySO : ScriptableObject
    {
        public GameObject EnemyPrefab;
        public float Speed;
        public int EnemyAtTheLevel;
        public int GoalsCount = 3;
        public float DistanceToChangeGoal = 0.1f;
        public float OffsetToCenterGoal = 0.5f;
    }
}