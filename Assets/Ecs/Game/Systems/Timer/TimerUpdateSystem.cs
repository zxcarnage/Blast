using Config.Enemy.EnemySpawner;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.Timer;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Ecs.Game.Systems.Timer
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class TimerUpdateSystem : ISystem
    {
        private readonly IEnemySpawnerParameters _enemySpawnerParameters;
        public World World { get; set; }

        private Filter _timerFilter;
        private Stash<EnemySpawnTimerComponent> _spawnTimerStash;
        private Stash<TimerEndedComponent> _timerEndedStash;

        public TimerUpdateSystem(
            IEnemySpawnerParameters enemySpawnerParameters
        )
        {
            _enemySpawnerParameters = enemySpawnerParameters;
        }

        public void OnAwake()
        {
            _timerFilter = World.Filter
                .With<TimerComponent>()
                .With<EnemySpawnTimerComponent>()
                .Build();

            _spawnTimerStash = World.GetStash<EnemySpawnTimerComponent>();
            _timerEndedStash = World.GetStash<TimerEndedComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            UpdateEnemySpawnTimer(deltaTime * Time.timeScale);
        }

        private void UpdateEnemySpawnTimer(float deltaTime)
        {
            var targetTimer = 0f;
            foreach (var timerEntity in _timerFilter)
            {
                if(_timerEndedStash.Has(timerEntity))
                    continue;
                
                targetTimer = _spawnTimerStash.Get(timerEntity).Value + deltaTime;
                if (targetTimer >= _enemySpawnerParameters.Delay)
                {
                    _timerEndedStash.Set(timerEntity);
                    _spawnTimerStash.Set(timerEntity, new EnemySpawnTimerComponent() { Value = 0f });
                    continue;
                }
                _spawnTimerStash.Set(timerEntity, new EnemySpawnTimerComponent() { Value = targetTimer });
            }
        }

        public void Dispose()
        {
        }
    }
}