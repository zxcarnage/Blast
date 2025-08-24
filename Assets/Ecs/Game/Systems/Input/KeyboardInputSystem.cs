using Config.Camera;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Ecs.Utils;
using R3;
using Scellecs.Morpeh;
using UnityEngine;
using Utils.DebugUtil;
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
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
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

                var movementVector3 = new Vector3(movementInputDirection.x, 0f, movementInputDirection.y);
                _moveDirectionStash.Set(entity, new MoveDirectionComponent() { Value = movementVector3 });
            }

            void HandleLookInput(Entity entity)
            {
                var mouseInput = _inputAction.Keyboard.MouseMovement.ReadValue<Vector2>();
                var frameSensitivity = _cameraParameters.Sensitivity * deltaTime;
                var lookDelta = new Vector3(-mouseInput.y, mouseInput.x, 0f) * frameSensitivity;

                var currentRotation = _lookDirectionStash.Get(entity);
                var targetRotation = currentRotation.Value + lookDelta;
                var yClamp = Mathf.Clamp(targetRotation.x, _cameraParameters.MinMaxY.x , _cameraParameters.MinMaxY.y);
                targetRotation.x = yClamp;
                _lookDirectionStash.Set(entity, new LookDirectionComponent() { Value = targetRotation });
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}