using Config.Camera;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Ecs.Game.Components.UI;
using R3;
using ReactiveInputSystem;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Ecs.Game.Systems.Input
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class KeyboardInputSystem : ISystem
    {
        private readonly PlayerInputAction _inputAction;
        private readonly ICameraParameters _cameraParameters;
        private readonly CompositeDisposable _disposables = new();

        private Filter _playerFilter;
        private Filter _upgradeMenuFilter;
        
        private Stash<MoveDirectionComponent> _moveDirectionStash;
        private Stash<LookDirectionComponent> _lookDirectionStash;
        private Stash<UpgradeMenuComponent> _upgradeMenuStash;
        private Stash<ShootComponent> _shootStash;

        public World World { get; set; }

        public KeyboardInputSystem(
            PlayerInputAction inputAction,
            ICameraParameters cameraParameters
        )
        {
            _inputAction = inputAction;
            _cameraParameters = cameraParameters;
        }

        public void OnAwake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            _playerFilter = World.Filter
                .With<PlayerComponent>()
                .Build();
            
            _upgradeMenuFilter = World.Filter
                .With<UpgradeMenuComponent>()
                .Build();
            
            _moveDirectionStash = World.GetStash<MoveDirectionComponent>();
            _lookDirectionStash = World.GetStash<LookDirectionComponent>();
            _upgradeMenuStash = World.GetStash<UpgradeMenuComponent>();
            _shootStash = World.GetStash<ShootComponent>();
            
            _inputAction.Keyboard.Shoot.StartedAsObservable().Subscribe(_ => HandleShoot()).AddTo(_disposables);
            _inputAction.Keyboard.UpgradeMenu.StartedAsObservable().Subscribe(_ => HandleOpenUpgradeMenu()).AddTo(_disposables);
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

        private void HandleShoot()
        {
            foreach (var playerEntity in _playerFilter)
            {
                _shootStash.Set(playerEntity);
            }
        }

        private void HandleOpenUpgradeMenu()
        {
            foreach (var entity in _upgradeMenuFilter)
            {
                var upgradeMenuView = _upgradeMenuStash.Get(entity).Value;
                upgradeMenuView.ShowView();
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}