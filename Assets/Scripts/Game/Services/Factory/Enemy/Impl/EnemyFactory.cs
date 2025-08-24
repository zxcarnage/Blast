using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
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

        public EnemyFactory(
            World world,
            IEnemyPool enemyPool
        )
        {
            _world = world;
            _enemyPool = enemyPool;
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

            return enemyView;
        }
    }
}