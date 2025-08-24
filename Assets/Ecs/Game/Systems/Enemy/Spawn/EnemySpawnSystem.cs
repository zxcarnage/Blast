using System;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.Timer;
using Game.Services.Pool.Enemy;
using Game.Utils.Enemy;
using Scellecs.Morpeh;
using Utils.Providers.GameField;
using Random = UnityEngine.Random;

namespace Ecs.Game.Systems.Enemy.Spawn
{
    public class EnemySpawnSystem : ISystem
    {
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IEnemyPool _enemyPool;

        private Filter _expiredSpawnTimer;
        private Stash<TimerEndedComponent> _timerEndedStash;
        
        public World World { get; set; }

        public EnemySpawnSystem(
            IEnemyPool enemyPool,
            IGameFieldProvider gameFieldProvider
        )
        {
            _enemyPool = enemyPool;
            _gameFieldProvider = gameFieldProvider;
        }

        public void OnAwake()
        {
            _expiredSpawnTimer = World.Filter
                .With<TimerComponent>()
                .With<EnemySpawnTimerComponent>()
                .With<TimerEndedComponent>()
                .Build();
            
            _timerEndedStash = World.GetStash<TimerEndedComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var timer in _expiredSpawnTimer)
            {
                SpawnRandomed();
                PlayTimerAgain(timer);
            }

            return;

            void SpawnRandomed()
            {
                var spawnPoints = _gameFieldProvider.GameField.SpawnPoints;
                var randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                var randomEnemyType = Random.Range(1, Enum.GetValues(typeof(EEnemyType)).Length);
                var enemy = _enemyPool.SpawnEnemy((EEnemyType) randomEnemyType);
                enemy.transform.position = spawnPoints[randomSpawnPoint].transform.position;
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