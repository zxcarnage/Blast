using Config.Enemy.EnemySpawner;
using Game.Services.Pool.Enemy;
using Scellecs.Morpeh;

namespace Ecs.Game.Systems.Enemy.Spawn
{
    public class EnemySpawnSystem : ISystem
    {
        private readonly IEnemyPool _enemyPool;
        private readonly IEnemySpawnerParameters _enemySpawnerParameters;
        
        public World World { get; set; }

        public EnemySpawnSystem(
            IEnemyPool enemyPool,
            IEnemySpawnerParameters  enemySpawnerParameters
        )
        {
            _enemyPool = enemyPool;
            _enemySpawnerParameters = enemySpawnerParameters;
        }

        public void OnAwake()
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}