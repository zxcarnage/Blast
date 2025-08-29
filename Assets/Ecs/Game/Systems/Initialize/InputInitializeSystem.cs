using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Game.Systems.Initialize
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class InputInitializeSystem : IInitializer
    {
        private readonly PlayerInputAction _playerInputAction;
            
        public World World { get; set; }

        public InputInitializeSystem(PlayerInputAction playerInputAction)
        {
            _playerInputAction = playerInputAction;
        }

        public void OnAwake()
        {
            _playerInputAction.Enable();
        }

        public void Dispose()
        {
            _playerInputAction.Disable();
        }
    }
}