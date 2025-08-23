using Config.Camera.Impl;
using Config.Enemy.EnemyBase.Impl;
using Config.Player.Impl;
using UnityEngine;

namespace Config.Base.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(GeneralBase), fileName = nameof(GeneralBase))]
    public class GeneralBase : ScriptableObject, IGeneralBase
    {
        [field: SerializeField]
        public PrefabsBase PrefabsBase { get; private set; }
        
        [field: SerializeField]
        public PlayerMovementParameters PlayerMovementParameters { get; private set; }

        [field: SerializeField]
        public CameraParameters CameraParameters { get; private set; }

        [field: SerializeField]
        public EnemyBase EnemyBase { get; private set; }
    }
}