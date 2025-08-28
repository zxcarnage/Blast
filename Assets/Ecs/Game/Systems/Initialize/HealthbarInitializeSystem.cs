using Config.Player;
using Ecs.Game.Components.UI;
using Scellecs.Morpeh;
using VContainer;

namespace Ecs.Game.Systems.Initialize
{
    public class HealthbarInitializeSystem : IInitializer
    {
        private readonly IPlayerBasicParameters _playerBasicParameters;
        private readonly IObjectResolver _objectResolver;
        
        private Filter _playerHealthbarFilter;
        private Stash<PlayerHealthbarComponent> _healthbarStash;
        
        public World World { set; get; }

        public HealthbarInitializeSystem(
            IPlayerBasicParameters playerBasicParameters,
            IObjectResolver objectResolver    
        )
        {
            _playerBasicParameters = playerBasicParameters;
            _objectResolver = objectResolver;
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