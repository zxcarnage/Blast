using Config.Player;
using Ecs.Utils;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Utils.Providers.GameField;

namespace Ecs.Game.Systems.Initialize
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerInitializeSystem : IInitializer
    {
        private readonly IPlayerBasicParameters _playerBasicParameters;
        private readonly IPlayerMovementParameters _playerMovementParameters;
        private readonly IPlayerShootingParameters _playerShootingParameters;
        private readonly IGameFieldProvider _gameFieldProvider;

        public World World { get; set; }

        public PlayerInitializeSystem(
            IGameFieldProvider gameFieldProvider,
            IPlayerBasicParameters playerBasicParameters,
            IPlayerMovementParameters playerMovementParameters,
            IPlayerShootingParameters playerShootingParameters
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _playerBasicParameters = playerBasicParameters;
            _playerMovementParameters = playerMovementParameters;
            _playerShootingParameters = playerShootingParameters;
        }

        public void OnAwake()
        {
            var playerView = _gameFieldProvider.GameField.Player;
            
            World.CreatePlayer(playerView, _playerBasicParameters.Health, _playerShootingParameters.Damage, _playerMovementParameters.Speed);
            World.CreatePlayerHead(playerView);
        }

        public void Dispose()
        {
            
        }
    }
}