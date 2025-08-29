using Config.Player;
using Core.Dao;
using Ecs.Game.Components.Upgrade;
using Ecs.Utils;
using Game.Utils.UpgradeData;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Utils.Providers.GameField;
using Utils.UI;

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
        private readonly IDao<UpgradeSaveData> _upgradeSaveData;
        private readonly IGameFieldProvider _gameFieldProvider;
        
        private Stash<ApplyHealthUpgradeComponent> _applyHealthUpgradeStash;
        private Stash<ApplyDamageUpgradeComponent> _applyDamageUpgradeStash;
        private Stash<ApplySpeedUpgradeComponent> _applySpeedUpgradeStash;

        public World World { get; set; }

        public PlayerInitializeSystem(
            IGameFieldProvider gameFieldProvider,
            IPlayerBasicParameters playerBasicParameters,
            IPlayerMovementParameters playerMovementParameters,
            IPlayerShootingParameters playerShootingParameters,
            IDao<UpgradeSaveData> upgradeSaveData
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _playerBasicParameters = playerBasicParameters;
            _playerMovementParameters = playerMovementParameters;
            _playerShootingParameters = playerShootingParameters;
            _upgradeSaveData = upgradeSaveData;
        }

        public void OnAwake()
        {
            var playerView = _gameFieldProvider.GameField.Player;
            
            _applyHealthUpgradeStash = World.GetStash<ApplyHealthUpgradeComponent>();
            _applyDamageUpgradeStash = World.GetStash<ApplyDamageUpgradeComponent>();
            _applySpeedUpgradeStash = World.GetStash<ApplySpeedUpgradeComponent>();
            
            var player = World.CreatePlayer(playerView, _playerBasicParameters.Health, _playerShootingParameters.Damage, _playerMovementParameters.Speed);
            InitializePlayerSavings(player);
            World.CreatePlayerHead(playerView);
        }

        private void InitializePlayerSavings(Entity player)
        {
            var saveData = _upgradeSaveData.Load();

            if (saveData == null)
                return;

            foreach (var data in saveData.UpgradeDatas)
            {
                switch (data.UpgradeType)
                {
                    case EUpgradeType.Health:
                        _applyHealthUpgradeStash.Set(player, new ApplyHealthUpgradeComponent() { Value = data.Level });
                        break;
                    case EUpgradeType.Damage:
                        _applyDamageUpgradeStash.Set(player, new ApplyDamageUpgradeComponent() { Value = data.Level });
                        break;
                    case EUpgradeType.Speed:
                        _applySpeedUpgradeStash.Set(player, new ApplySpeedUpgradeComponent() { Value = data.Level });
                        break;
                }
            }
        }

        public void Dispose()
        {
            
        }
    }
}