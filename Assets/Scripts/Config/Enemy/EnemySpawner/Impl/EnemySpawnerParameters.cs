using UnityEngine;

namespace Config.Enemy.EnemySpawner.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(EnemySpawnerParameters), fileName = nameof(EnemySpawnerParameters))]
    public class EnemySpawnerParameters : ScriptableObject, IEnemySpawnerParameters
    {
        [field: SerializeField]
        public float Delay { get; private set; }

        [field: SerializeField]
        public float EnemyCheckRadius { get; private set; }
    }
}