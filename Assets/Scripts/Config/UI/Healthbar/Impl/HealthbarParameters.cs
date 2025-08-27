using DG.Tweening;
using UnityEngine;

namespace Config.UI.Healthbar.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(HealthbarParameters), fileName = nameof(HealthbarParameters))]
    public class HealthbarParameters : ScriptableObject, IHealthbarParameters
    {
        [field: SerializeField]
        public float AnimationTime { get; private set; }
        
        [field: SerializeField]
        public Ease AnimationEase { get; private set; }
    }
}