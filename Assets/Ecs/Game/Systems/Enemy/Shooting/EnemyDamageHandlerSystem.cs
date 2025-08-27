using Config.Enemy;
using Config.UI.Healthbar;
using DG.Tweening;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.UI;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Game.Systems.Enemy.Shooting
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyDamageHandlerSystem : ISystem
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
                var enemyType = _enemyTypeStash.Get(enemyEntity).Value;
                var healthbarImage = _healthImageStash.Get(enemyEntity);
                var targetHealth = currentHealth - hitValue;
                var enemyMaxHealth = _enemyParameters.EnemyData[enemyType].Health;

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