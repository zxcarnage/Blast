using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using R3;
using Scellecs.Morpeh;
using UnityEngine;

namespace Ecs.Game.Systems.Input
{
    public class KeyboardInputSystem : ISystem
    {
        private readonly PlayerInputAction _inputAction;
        private readonly CompositeDisposable _disposables = new();

        private Filter _playerFilter;
        private Stash<MoveDirectionComponent> _moveDirectionStash;

        public World World { get; set; }

        public KeyboardInputSystem(PlayerInputAction inputAction)
        {
            _inputAction = inputAction;
        }

        public void OnAwake()
        {
            _playerFilter = World.Filter
                .With<PlayerComponent>()
                .Build();
            
            _moveDirectionStash = World.GetStash<MoveDirectionComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            var inputDirection = _inputAction.Keyboard.WASDMovement.ReadValue<Vector2>();
            inputDirection = Vector2.ClampMagnitude(inputDirection, 1f);

            foreach (var entity in _playerFilter)
            {
                _moveDirectionStash.Set(entity, new MoveDirectionComponent() { Value = inputDirection });
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}