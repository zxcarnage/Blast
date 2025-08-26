using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Scellecs.Morpeh;

namespace Ecs.Game.Systems.Enemy.Shooting
{
    public class EnemyDamageHandlerSystem : ISystem
    {
        private Filter _enemyWithHitsFilter;
        
        private Stash<HealthComponent> _healthStash;
        private Stash<HitComponent> _hitStash;
        private Stash<DeadComponent> _deadStash;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _enemyWithHitsFilter = World.Filter
                .With<EnemyComponent>()
                .With<HitComponent>()
                .Build();

            _healthStash = World.GetStash<HealthComponent>();
            _deadStash = World.GetStash<DeadComponent>();
            _hitStash = World.GetStash<HitComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var enemyEntity in _enemyWithHitsFilter)
            {
                var currentHealth = _healthStash.Get(enemyEntity).Value;
                var hitValue = _hitStash.Get(enemyEntity).Value;
                var targetHealth = currentHealth - hitValue;

                _hitStash.Remove(enemyEntity);
                
                if (targetHealth <= 0)
                {
                    _deadStash.Add(enemyEntity);
                    continue;                    
                }

                _healthStash.Set(enemyEntity, new HealthComponent { Value = targetHealth });
            }
        }

        public void Dispose()
        {
        }
    }
}