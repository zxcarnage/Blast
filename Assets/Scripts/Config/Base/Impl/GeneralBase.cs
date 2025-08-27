using Config.Camera.Impl;
using Config.Enemy.EnemyBase.Impl;
using Config.Enemy.EnemySpawner.Impl;
using Config.Enemy.Impl;
using Config.Player.Impl;
using Config.UI.Healthbar.Impl;
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
        
        [field: SerializeField]
        public EnemySpawnerParameters EnemySpawnerParameters { get; private set; }

        [field: SerializeField]
        public PlayerShootingParameters PlayerShootingParameters { get; private set; }

        [field: SerializeField]
        public EnemyParameters EnemyParameters { get; private set; }

        [field: SerializeField]
        public HealthbarParameters HealthbarParameters { get; private set; }
        
        [field: SerializeField]
        public PlayerBasicParameters PlayerBasicParameters { get; private set; }
    }
}