using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.Player;
using Game.Services.OverlapService;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Utils;
using Utils.DebugUtil;
using LayerMask = Utils.Layer.LayerMask;

namespace Ecs.Game.Systems.Shooting
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ShootingSystem : ISystem
    {
        private readonly IOverlapService _overlapService;
        public World World { get; set; }

        private Filter _playerShootingFilter;
        private Filter _playerHeadFilter;
        private Filter _enemyFilter;

        private Stash<TransformComponent> _transformStash;
        private Stash<HitComponent> _hitStash;
        private Stash<ShootComponent> _shootStash;
        private Stash<DamageComponent> _damageStash;

        public ShootingSystem(
            IOverlapService overlapService
        )
        {
            _overlapService = overlapService;
        }

        public void OnAwake()
        {
            _playerShootingFilter = World.Filter
                .With<PlayerComponent>()
                .With<ShootComponent>()
                .Build();
            
            _playerHeadFilter = World.Filter
                .With<PlayerHeadComponent>()
                .Build();

            _enemyFilter = World.Filter
                .With<EnemyComponent>()
                .Build();
            
            
            _transformStash = World.GetStash<TransformComponent>();
            _hitStash = World.GetStash<HitComponent>();
            _shootStash = World.GetStash<ShootComponent>();
            _damageStash = World.GetStash<DamageComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var playerEntity in _playerShootingFilter)
            {
                foreach (var playerHead in _playerHeadFilter)
                {
                    var headTransform = _transformStash.Get(playerHead).Value;
                    var raycastFrom = headTransform.position;
                    var raycastTo = headTransform.forward;
                    var raycastHit = _overlapService.GetRaycastHit(
                        raycastFrom, 
                        raycastTo, 
                        float.MaxValue, 
                        LayerMask.Enemy
                    );

                    if (raycastHit.collider == null)
                        continue;
                    
                    var enemyEntity = FindEntityByPosition(raycastHit.collider.transform.position);
                    
                    if(enemyEntity == null)
                        continue;

                    var damage = _damageStash.Get(playerEntity).Value;
                                 
                    _hitStash.Set(enemyEntity!.Value, new HitComponent { Value = damage });
                }
                
                _shootStash.Remove(playerEntity);
            }
        }

        private Entity? FindEntityByPosition(Vector3 position)
        {
            foreach (var enemyEntity in _enemyFilter)
            {
                var enemyTransform = _transformStash.Get(enemyEntity);

                if (Vector3.Distance(enemyTransform.Value.position, position) <= ConstValues.ENTITY_SEARCH_RADIUS)
                    return enemyEntity;
            }

            DebugUtility.Log($"No entity found with position {position}", UtilsColors.ErrorColor);
            
            return null;
        }

        public void Dispose()
        {
        }
    }
}