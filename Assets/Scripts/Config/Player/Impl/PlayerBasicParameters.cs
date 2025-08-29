using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Config.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerBasicParameters), fileName = nameof(PlayerBasicParameters))]
    public class PlayerBasicParameters : ScriptableObject, IPlayerBasicParameters
    {
        private const string ANIMATIONS_FOLDOUT = "Animations";
        
        [field: SerializeField]
        public int Health { get; private set; }
        
        [field: SerializeField]
        [field: FoldoutGroup(ANIMATIONS_FOLDOUT)]
        public float HealthbarAnimationDuration { get; private set; }

        [field: SerializeField]
        [field: FoldoutGroup(ANIMATIONS_FOLDOUT)]
        public Ease HealthbarAnimationEase { get; private set; }
    }
}