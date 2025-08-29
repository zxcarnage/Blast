using Config.Player;
using Core.Dao;
using Ecs.Game.Components.Player;
using Ecs.Game.Components.Upgrade;
using Ecs.Utils;
using Game.Utils.Dao;
using Game.Utils.Dao.UpgradeData;
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
        private readonly IDao<LevelSaveData> _levelSaveData;
        private readonly IGameFieldProvider _gameFieldProvider;
        
        private Stash<ApplyHealthUpgradeComponent> _applyHealthUpgradeStash;
        private Stash<ApplyDamageUpgradeComponent> _applyDamageUpgradeStash;
        private Stash<ApplySpeedUpgradeComponent> _applySpeedUpgradeStash;
        private Stash<PlayerSkillpointComponent> _playerSkillpointStash;

        public World World { get; set; }

        public PlayerInitializeSystem(
            IGameFieldProvider gameFieldProvider,
            IPlayerBasicParameters playerBasicParameters,
            IPlayerMovementParameters playerMovementParameters,
            IPlayerShootingParameters playerShootingParameters,
            IDao<UpgradeSaveData> upgradeSaveData,
            IDao<LevelSaveData> levelSaveData
        )
        {
            _gameFieldProvider = gameFieldProvider;
            _playerBasicParameters = playerBasicParameters;
            _playerMovementParameters = playerMovementParameters;
            _playerShootingParameters = playerShootingParameters;
            _upgradeSaveData = upgradeSaveData;
            _levelSaveData = levelSaveData;
        }

        public void OnAwake()
        {
            var playerView = _gameFieldProvider.GameField.Player;
            
            _applyHealthUpgradeStash = World.GetStash<ApplyHealthUpgradeComponent>();
            _applyDamageUpgradeStash = World.GetStash<ApplyDamageUpgradeComponent>();
            _applySpeedUpgradeStash = World.GetStash<ApplySpeedUpgradeComponent>();
            _playerSkillpointStash = World.GetStash<PlayerSkillpointComponent>();
            
            var player = World.CreatePlayer(playerView, _playerBasicParameters.Health, _playerShootingParameters.Damage, _playerMovementParameters.Speed);
            InitializePlayerSavings(player);
            World.CreatePlayerHead(playerView);
        }

        private void InitializePlayerSavings(Entity player)
        {

            TryInitializeUpgradeSavings();
            TryInitializeLevelSavings();
            
            return;

            void TryInitializeUpgradeSavings()
            {
                var saveData = _upgradeSaveData.Load();

                if (saveData?.UpgradeDatas == null)
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

            void TryInitializeLevelSavings()
            {
                var levelSaveData = _levelSaveData.Load();

                if (levelSaveData == null)
                    return;
                
                _playerSkillpointStash.Set(player, new PlayerSkillpointComponent() { Value = levelSaveData.Level });
            }
        }

        public void Dispose()
        {
            
        }
    }
}