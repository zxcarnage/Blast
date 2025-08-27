using Config.Enemy;
using Ecs.Game.Components;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.UI;
using Game.Services.Pool.Enemy;
using Game.Utils.Enemy;
using Game.Views.Enemy;
using Scellecs.Morpeh;
using UnityEngine;

namespace Game.Services.Factory.Enemy.Impl
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly World _world;
        private readonly IEnemyPool _enemyPool;
        private readonly IEnemyParameters _enemyParameters;

        public EnemyFactory(
            World world,
            IEnemyPool enemyPool,
            IEnemyParameters enemyParameters
        )
        {
            _world = world;
            _enemyPool = enemyPool;
            _enemyParameters = enemyParameters;
        }
        
        public EnemyView CreateEnemy(EEnemyType enemyType, Vector3 position)
        {
            var enemyView = _enemyPool.SpawnEnemy(enemyType);
            enemyView.transform.position = position;
            
            var enemyEntity = _world.CreateEntity();
            _world.GetStash<EnemyComponent>().Add(enemyEntity);
            _world.GetStash<TransformComponent>().Add(enemyEntity, new TransformComponent() { Value = enemyView.Transform });
            _world.GetStash<RigidbodyComponent>().Add(enemyEntity, new RigidbodyComponent() { Value = enemyView.Rigidbody });
            _world.GetStash<ColliderComponent>().Add(enemyEntity, new ColliderComponent() { Value = enemyView.Collider });
            _world.GetStash<HealthComponent>().Add(enemyEntity, new HealthComponent() { Value = _enemyParameters.EnemyData[enemyType].Health });
            _world.GetStash<EnemyLinkComponent>().Add(enemyEntity, new EnemyLinkComponent() { Value = enemyView });
            _world.GetStash<EnemyTypeComponent>().Add(enemyEntity, new EnemyTypeComponent() { Value = enemyType });
            _world.GetStash<HealthImageComponent>().Add(enemyEntity, new HealthImageComponent() { Value = enemyView.HealthbarImage });

            return enemyView;
        }
    }
}