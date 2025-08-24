using System;
using Config.Enemy.EnemySpawner;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.SpawnPoint;
using Ecs.Game.Components.Timer;
using Game.Services.OverlapService;
using Game.Services.Pool.Enemy;
using Game.Utils.Enemy;
using Scellecs.Morpeh;
using Utils.DebugUtil;
using Utils.Layer;
using Utils.Providers.GameField;
using Random = UnityEngine.Random;

namespace Ecs.Game.Systems.Enemy.Spawn
{
    public class EnemySpawnSystem : ISystem
    {
        private readonly IEnemyPool _enemyPool;
        private readonly IOverlapService _overlapService;
        private readonly IEnemySpawnerParameters _enemySpawnerParameters;

        private Filter _expiredSpawnTimer;
        private Filter _spawnPointFilter;
        
        private Stash<TimerEndedComponent> _timerEndedStash;
        private Stash<TransformComponent> _transformStash;
        
        public World World { get; set; }

        public EnemySpawnSystem(
            IEnemyPool enemyPool,
            IOverlapService overlapService,
            IEnemySpawnerParameters enemySpawnerParameters
        )
        {
            _enemyPool = enemyPool;
            _overlapService = overlapService;
            _enemySpawnerParameters = enemySpawnerParameters;
        }

        public void OnAwake()
        {
            _expiredSpawnTimer = World.Filter
                .With<TimerComponent>()
                .With<EnemySpawnTimerComponent>()
                .With<TimerEndedComponent>()
                .Build();
            
            _spawnPointFilter = World.Filter
                .With<SpawnPointComponent>()
                .Build();
            
            _transformStash = World.GetStash<TransformComponent>();
            _timerEndedStash = World.GetStash<TimerEndedComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var timer in _expiredSpawnTimer)
            {
                if(TrySpawnRandomed())
                    PlayTimerAgain(timer);
            }

            return;

            bool TrySpawnRandomed()
            {
                var spawnPointCount = _spawnPointFilter.GetLengthSlow();

                for (var i = 0; i < spawnPointCount; i++)
                {
                    var spawnPointEntity = _spawnPointFilter.GetEntity(i);
                    var spawnPointTransform = _transformStash.Get(spawnPointEntity);
                    var isEnemyThere = _overlapService.CheckOverlapSphere(
                        spawnPointTransform.Value.position,
                        _enemySpawnerParameters.EnemyCheckRadius,
                        LayerMask.Enemy | LayerMask.Player);

                    if (isEnemyThere)
                        continue;
                    
                    var randomSpawnPointIndex = Random.Range(0, spawnPointCount);
                    var randomEnemyType = Random.Range(1, Enum.GetValues(typeof(EEnemyType)).Length);
                    var enemy = _enemyPool.SpawnEnemy((EEnemyType) randomEnemyType);
                    enemy.transform.position = spawnPointTransform.Value.position;

                    return true;
                }

                return false;
            }

            void PlayTimerAgain(Entity timer)
            {
                _timerEndedStash.Remove(timer);
            }
        }

        public void Dispose()
        {
        }
    }
}