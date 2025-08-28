using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    [System.Serializable]
    public struct Vector2IntRanged
    {
        [MinMaxSlider(0, 10, true)]
        public Vector2Int Value;
    }
}