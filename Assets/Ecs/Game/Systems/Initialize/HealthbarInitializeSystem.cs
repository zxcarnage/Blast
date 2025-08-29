using Config.Player;
using Ecs.Game.Components.UI;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Game.Systems.Initialize
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class HealthbarInitializeSystem : IInitializer
    {
        private readonly IPlayerBasicParameters _playerBasicParameters;
        
        private Filter _playerHealthbarFilter;
        private Stash<PlayerHealthbarComponent> _healthbarStash;
        
        public World World { set; get; }

        public HealthbarInitializeSystem(
            IPlayerBasicParameters playerBasicParameters
        )
        {
            _playerBasicParameters = playerBasicParameters;
        }

        public void OnAwake()
        {
            _playerHealthbarFilter = World.Filter
                .With<PlayerHealthbarComponent>()
                .Build();

            _healthbarStash = World.GetStash<PlayerHealthbarComponent>();

            foreach (var playerHealthbarEntity in _playerHealthbarFilter)
            {
                var healthbarView = _healthbarStash.Get(playerHealthbarEntity).Value;
                healthbarView.UpdateView(_playerBasicParameters.Health);
            }
        }

        public void Dispose()
        {
        }
    }
}