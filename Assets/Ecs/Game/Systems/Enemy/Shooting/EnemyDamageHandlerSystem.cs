using Config.Enemy;
using Config.UI.Healthbar;
using DG.Tweening;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.UI;
using Scellecs.Morpeh;

namespace Ecs.Game.Systems.Enemy.Shooting
{
    public class EnemyDamageHandlerSystem : ISystem
    {
        private readonly IEnemyParameters _enemyParameters;
        private readonly IHealthbarParameters _healthbarParameters;

        private Filter _enemyWithHitsFilter;
        
        private Stash<HealthComponent> _healthStash;
        private Stash<HitComponent> _hitStash;
        private Stash<DeadComponent> _deadStash;
        private Stash<HealthImageComponent> _healthImageStash;
        private Stash<EnemyTypeComponent> _enemyTypeStash;
        
        public World World { get; set; }

        public EnemyDamageHandlerSystem(
            IEnemyParameters enemyParameters,
            IHealthbarParameters healthbarParameters
        )
        {
            _enemyParameters = enemyParameters;
            _healthbarParameters = healthbarParameters;
        }

        public void OnAwake()
        {
            _enemyWithHitsFilter = World.Filter
                .With<EnemyComponent>()
                .With<HitComponent>()
                .Build();

            _healthStash = World.GetStash<HealthComponent>();
            _deadStash = World.GetStash<DeadComponent>();
            _hitStash = World.GetStash<HitComponent>();
            _enemyTypeStash = World.GetStash<EnemyTypeComponent>();
            _healthImageStash = World.GetStash<HealthImageComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var enemyEntity in _enemyWithHitsFilter)
            {
                var currentHealth = _healthStash.Get(enemyEntity).Value;
                var hitValue = _hitStash.Get(enemyEntity).Value;
                var targetHealth = currentHealth - hitValue;
                var enemyType = _enemyTypeStash.Get(enemyEntity).Value;
                var enemyMaxHealth = _enemyParameters.EnemyData[enemyType].Health;
                var healthbarImage = _healthImageStash.Get(enemyEntity);

                var tween = healthbarImage.Value
                    .DOFillAmount(targetHealth / enemyMaxHealth, _healthbarParameters.AnimationTime)
                    .SetEase(_healthbarParameters.AnimationEase);

                _hitStash.Remove(enemyEntity);
                
                if (targetHealth <= 0)
                {
                    tween?.Kill();
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