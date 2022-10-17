using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "GameLogicSO/GeneratorSO", fileName = "GeneratorSO")]
    public class GeneratorSo : ScriptableObject
    {
        public int FieldRows;
        public int FieldColums;
        public float PlacementThreshold = .1f;
    }
}