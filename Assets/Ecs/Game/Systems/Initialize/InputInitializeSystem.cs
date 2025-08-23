using Scellecs.Morpeh;

namespace Ecs.Game.Systems.Initialize
{
    public class InputInitializeSystem : IInitializer
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