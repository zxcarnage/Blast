using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Ecs.Game.Systems.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerMovementSystem : IFixedSystem
    {
        private Filter _playerFilter;
        private Stash<MoveDirectionComponent> _moveDirectionStash;
        private Stash<RigidbodyComponent> _rigidbodyStash;
        private Stash<TransformComponent> _transformStash;
        private Stash<SpeedComponent> _speedStash;
        
        public World World { get; set; }
        
        public void OnAwake()
        {
            _moveDirectionStash = World.GetStash<MoveDirectionComponent>();
            _rigidbodyStash = World.GetStash<RigidbodyComponent>();
            _transformStash = World.GetStash<TransformComponent>();
            _speedStash = World.GetStash<SpeedComponent>();
            
            _playerFilter = World.Filter
                .With<PlayerComponent>()
                .With<MoveDirectionComponent>()
                .With<RigidbodyComponent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _playerFilter)
            {
                HandleMovement(entity);
            }

            return;

            void HandleMovement(Entity player)
            {
                ref var direction = ref _moveDirectionStash.Get(player);
                ref var rigidbody = ref _rigidbodyStash.Get(player);
                ref var transform = ref _transformStash.Get(player);
                
                var localDirection = new Vector3(direction.Value.x, 0f, direction.Value.z);
                var worldDirection = transform.Value.TransformDirection(localDirection);
                var currentYVelocity = rigidbody.Value.velocity.y;
                var speed = _speedStash.Get(player).Value;
                var targetVelocity = worldDirection * speed;
                targetVelocity.y = currentYVelocity;
                
                rigidbody.Value.velocity = targetVelocity;
            }
        }

        public void Dispose()
        {
        }
    }
}