using Ecs.Game.Components;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.Player;
using Game.Services.Pool.Enemy;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Game.Systems.Character
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyDeathSystem : ISystem
    {
        private readonly IEnemyPool _enemyPool;

        private Filter _deadEnemyFilter;
        private Filter _playerFilter;
        
        private Stash<EnemyLinkComponent> _linkStash;
        private Stash<EnemyTypeComponent> _enemyTypeStash;
        private Stash<PlayerSkillpointComponent> _skillpointStash;

        public World World { get; set; }
        
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
            
            _playerFilter = World.Filter
                .With<PlayerComponent>()
                .Build();
            
            _linkStash = World.GetStash<EnemyLinkComponent>();
            _enemyTypeStash = World.GetStash<EnemyTypeComponent>();
            _skillpointStash = World.GetStash<PlayerSkillpointComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var deadEnemy in _deadEnemyFilter)
            {
                var enemyView = _linkStash.Get(deadEnemy).Value;
                var enemyType = _enemyTypeStash.Get(deadEnemy).Value;

                AddSkillPoint();
                
                _enemyPool.DespawnEnemy(enemyType, enemyView);
                World.RemoveEntity(deadEnemy);
            }

            return;

            void AddSkillPoint()
            {
                foreach (var playerEntity in _playerFilter)
                {
                    var currentSkillpoints = _skillpointStash.Get(playerEntity).Value;
                    
                    _skillpointStash.Set(playerEntity, new PlayerSkillpointComponent() { Value = currentSkillpoints + 1 });
                }
            }
        }

        public void Dispose()
        {
        }
    }
}