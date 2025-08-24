using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.Timer;
using Game.Services.Pool.Enemy;
using Scellecs.Morpeh;

namespace Ecs.Game.Systems.Enemy.Spawn
{
    public class EnemySpawnSystem : ISystem
    {
        private readonly IEnemyPool _enemyPool;

        private Filter _expiredSpawnTimer;
        
        public World World { get; set; }

        public EnemySpawnSystem(
            IEnemyPool enemyPool
        )
        {
            _enemyPool = enemyPool;
        }

        public void OnAwake()
        {
            _expiredSpawnTimer = World.Filter
                .With<TimerComponent>()
                .With<EnemySpawnTimerComponent>()
                .With<TimerEndedComponent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var timer in _expiredSpawnTimer)
            {
                //Spawn enemy in spawn point
            }
        }

        public void Dispose()
        {
        }
    }
}