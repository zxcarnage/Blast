using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.Player;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Ecs.Game.Systems.Enemy.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class EnemyRotationSystem : ISystem
    {
        private Filter _enemyFilter;
        private Filter _playerFilter;
        
        private Stash<TransformComponent> _transformStash;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _enemyFilter = World.Filter
                .With<EnemyComponent>()
                .Without<DeadComponent>()
                .Build();
            
            _playerFilter = World.Filter
                .With<PlayerComponent>()
                .Build();
            
            _transformStash = World.GetStash<TransformComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var enemyEntity in _enemyFilter)
            {
                var enemyTransform = _transformStash.Get(enemyEntity).Value;

                foreach (var playerEntity in _playerFilter)
                {
                    var playerTransform = _transformStash.Get(playerEntity).Value;
                    var directionToPlayer = playerTransform.position - enemyTransform.position;
                    directionToPlayer.y = 0;
                    enemyTransform.rotation = Quaternion.LookRotation(directionToPlayer.normalized,Vector3.up);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}