using System.Collections.Generic;
using Game.Utils.Enemy;
using Game.Views.Enemy;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Config.Enemy.EnemyBase.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(EnemyBase), fileName = nameof(EnemyBase))]
    
    public class EnemyBase : SerializedScriptableObject, IEnemyBase
    {
#if UNITY_EDITOR
        [Title("Enemy Variations")] 
        [OdinSerialize]
        [HideReferenceObjectPicker] 
        [Searchable]
        private Dictionary<EEnemyType, SerializedEnemyTypeInfos> _enemyMatrices;
#endif
        
        [OdinSerialize]
        [HideInInspector]
        private Dictionary<EEnemyType, IReadOnlyList<EnemyView>> _enemiesVariations;

        private Dictionary<EEnemyType, EnemyTypeInfo> _enemyInfos;

        public IReadOnlyDictionary<EEnemyType, IReadOnlyList<EnemyView>> EnemiesVariations => _enemiesVariations;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            _enemyInfos = new Dictionary<EEnemyType, EnemyTypeInfo>();

            foreach (var pair in _enemyMatrices)
            {
                _enemyInfos[pair.Key] = pair.Value;
            }

            _enemiesVariations = new Dictionary<EEnemyType, IReadOnlyList<EnemyView>>();

            foreach (var pair in _enemyInfos)
            {
                _enemiesVariations[pair.Key] = pair.Value;
            }
        }
#endif
    }
}