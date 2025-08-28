using Config.Player;
using Ecs.Utils;
using Scellecs.Morpeh;
using Utils.Providers.GameField;
using VContainer;

namespace Ecs.Game.Systems.Initialize
{
    public class PlayerInitializeSystem : IInitializer
    {
        private readonly IPlayerBasicParameters _playerBasicParameters;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IObjectResolver _resolver;

        public World World { get; set; }

        public PlayerInitializeSystem(
            IGameFieldProvider gameFieldProvider,
            IPlayerBasicParameters playerBasicParameters,
            IObjectResolver resolver
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _playerBasicParameters = playerBasicParameters;
            _resolver = resolver;
        }

        public void OnAwake()
        {
            var playerView = _gameFieldProvider.GameField.Player;
            
            World.CreatePlayer(playerView, _playerBasicParameters.Health);
            World.CreatePlayerHead(playerView);
            
            _resolver.Inject(playerView);
        }

        public void Dispose()
        {
            
        }
    }
}