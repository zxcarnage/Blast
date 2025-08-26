using System.Collections.Generic;
using Config.Utils;
using Game.Utils.Enemy;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Config.Enemy.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(EnemyParameters), fileName = nameof(EnemyParameters))]
    public class EnemyParameters : SerializedScriptableObject, IEnemyParameters
    {
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Enemy type", ValueLabel = "Enemy parameters")]
        private Dictionary<EEnemyType, EnemyData> _enemyData;

        public IReadOnlyDictionary<EEnemyType, EnemyData> EnemyData => _enemyData;
    }
}