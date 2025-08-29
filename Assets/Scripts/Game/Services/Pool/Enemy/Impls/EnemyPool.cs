using System.Collections.Generic;
using Config.Enemy.EnemyBase;
using Game.Utils.Enemy;
using Game.Views.Enemy;
using VContainer;

namespace Game.Services.Pool.Enemy.Impls
{
    public class EnemyPool : AObjectPool<EEnemyType, EnemyView>, IEnemyPool
    {
        private readonly IEnemyBase _enemyBase;
        protected override IReadOnlyDictionary<EEnemyType, IReadOnlyList<EnemyView>> PoolObjectVariants => _enemyBase.EnemiesVariations;

        public EnemyPool(
            IObjectResolver container,
            IEnemyBase enemyBase
        ) : base(container)
        {
            _enemyBase = enemyBase;
            
            CreateSubPools();
        }

        public EnemyView SpawnEnemy(EEnemyType enemyType)
        {
            return SpawnObject(enemyType);
        }

        public void DespawnEnemy(EEnemyType enemyType, EnemyView enemyView)
        {
            DespawnObject(enemyType, enemyView);
        }
    }
}