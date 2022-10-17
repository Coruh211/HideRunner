using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "GameLogicSO/Environment/FloorSO", fileName = "FloorSO")]
    public class FloorSo: ScriptableObject
    {
        public GameObject FloorPrefab;
    }
}