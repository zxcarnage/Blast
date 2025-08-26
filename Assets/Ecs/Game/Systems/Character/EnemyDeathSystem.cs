using Ecs.Game.Components;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Game.Services.Pool.Enemy;
using Scellecs.Morpeh;

namespace Ecs.Game.Systems.Character
{
    public class EnemyDeathSystem : ISystem
    {
        private readonly IEnemyPool _enemyPool;
        
        private Stash<EnemyLinkComponent> _linkStash;
        private Stash<EnemyTypeComponent> _enemyTypeStash;
        
        public World World { get; set; }

        private Filter _deadEnemyFilter;

        public EnemyDeathSystem(
            IEnemyPool enemyPool
        )
        {
            _enemyPool = enemyPool;
        }

        public void OnAwake()
        {
            _deadEnemyFilter = World.Filter
                .With<EnemyComponent>()
                .With<DeadComponent>()
                .Build();
            
            _linkStash = World.GetStash<EnemyLinkComponent>();
            _enemyTypeStash = World.GetStash<EnemyTypeComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var deadEnemy in _deadEnemyFilter)
            {
                var enemyView = _linkStash.Get(deadEnemy).Value;
                var enemyType = _enemyTypeStash.Get(deadEnemy).Value;
                
                _enemyPool.DespawnEnemy(enemyType, enemyView);
                World.RemoveEntity(deadEnemy);
            }
        }

        public void Dispose()
        {
        }
    }
}