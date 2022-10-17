using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "GameLogicSO/EnemySO", fileName = "EnemySO")]
    public class EnemySO : ScriptableObject
    {
        public GameObject EnemyPrefab;
        public float Speed;
        public int EnemyAtTheLevel;
    }
}