using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "GameLogicSO/PlayerSO", fileName = "PlayerSO")]
    public class PlayerSo: ScriptableObject
    {
        public GameObject PlayerPrefab;
        public float Speed;
        public float NoiseIncrease;
        public float IncreaseInterval;
        public float NoiseReduction;
        public float ReductionInterval;
        public float MaxNoise;

    }
}