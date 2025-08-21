using Config.Player;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Scellecs.Morpeh;
using UnityEngine;

namespace Ecs.Game.Systems.Movement
{
    public class PlayerMovementSystem : IFixedSystem
    {
        private readonly IPlayerMovementParameters _playerMovementParameters;

        private Filter _playerFilter;
        private Stash<MoveDirectionComponent> _moveDirectionStash;
        private Stash<RigidbodyComponent> _rigidbodyStash;
        private Stash<TransformComponent> _transformStash;
        
        public World World { get; set; }

        public PlayerMovementSystem(
            IPlayerMovementParameters playerMovementParameters    
        )
        {
            _playerMovementParameters = playerMovementParameters;
        }

        public void OnAwake()
        {
            _moveDirectionStash = World.GetStash<MoveDirectionComponent>();
            _rigidbodyStash = World.GetStash<RigidbodyComponent>();
            _transformStash = World.GetStash<TransformComponent>();
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
                
                var worldDirection = transform.Value.TransformDirection(direction.Value);
                var targetMovementDirection =
                    new Vector3(worldDirection.x, rigidbody.Value.velocity.y, worldDirection.y);
                //rigidbody.Value.AddForce(targetMovementDirection * _playerMovementParameters.Speed, ForceMode.VelocityChange);
                rigidbody.Value.velocity = targetMovementDirection * _playerMovementParameters.Speed;
            }
        }

        public void Dispose()
        {
        }
    }
}