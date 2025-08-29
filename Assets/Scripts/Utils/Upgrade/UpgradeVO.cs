using UnityEngine;

namespace Utils.Upgrade
{
    [System.Serializable]
    public struct UpgradeVO
    {
        [field: SerializeField]
        public int MaxLevel { get; private set; }
        
        [field: SerializeField]
        public float IncreaseDelta { get; private set; }
    }
}