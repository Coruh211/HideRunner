using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "GameLogicSO/Environment/ExitSO", fileName = "ExitSO")]
    public class ExitSo: ScriptableObject
    {
        public GameObject ExitPrefab;
    }
}