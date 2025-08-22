using Config.Camera;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using R3;
using Scellecs.Morpeh;
using UnityEngine;
using Utils.Providers.GameField;

namespace Ecs.Game.Systems.Input
{
    public class KeyboardInputSystem : ISystem
    {
        private readonly PlayerInputAction _inputAction;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly ICameraParameters _cameraParameters;
        private readonly CompositeDisposable _disposables = new();

        private Filter _playerFilter;
        private Stash<MoveDirectionComponent> _moveDirectionStash;
        private Stash<LookDirectionComponent> _lookDirectionStash;

        public World World { get; set; }

        public KeyboardInputSystem(
            PlayerInputAction inputAction,
            IGameFieldProvider gameFieldProvider,
            ICameraParameters cameraParameters
        )
        {
            _inputAction = inputAction;
            _gameFieldProvider = gameFieldProvider;
            _cameraParameters = cameraParameters;
        }

        public void OnAwake()
        {
            _playerFilter = World.Filter
                .With<PlayerComponent>()
                .Build();
            
            _moveDirectionStash = World.GetStash<MoveDirectionComponent>();
            _lookDirectionStash = World.GetStash<LookDirectionComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _playerFilter)
            {
                HandleMovementInput(entity);
                HandleLookInput(entity);
            }

            return;

            void HandleMovementInput(Entity entity)
            {
                var movementInputDirection = Vector2.ClampMagnitude(_inputAction.Keyboard.WASDMovement.ReadValue<Vector2>(), 1f);
                _moveDirectionStash.Set(entity, new MoveDirectionComponent() { Value = movementInputDirection });
                
            }

            void HandleLookInput(Entity entity)
            {
                var mouseInput = _inputAction.Keyboard.MouseMovement.ReadValue<Vector2>();
                var sensitivity = _cameraParameters.Sensitivity;
                var lookDelta = new Vector3(mouseInput.x, -mouseInput.y, 0f) * sensitivity;
                var currentRotation = _lookDirectionStash.Get(entity);
                var targetRotation = currentRotation.Value + lookDelta;
                var yClamp = Mathf.Clamp(targetRotation.y, _cameraParameters.MinMaxY.x , _cameraParameters.MinMaxY.y);
                targetRotation.y = yClamp;
                _lookDirectionStash.Set(entity, new LookDirectionComponent() { Value = targetRotation });
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}