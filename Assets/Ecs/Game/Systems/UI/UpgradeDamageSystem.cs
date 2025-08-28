using Config.UpgradeData;
using Ecs.Game.Components.Player;
using Ecs.Game.Components.Upgrade;
using Scellecs.Morpeh;
using Utils.DebugUtil;
using Utils.UI;

namespace Ecs.Game.Systems.UI
{
    public sealed class UpgradeDamageSystem : ISystem
    {
        private readonly IUpgradeDataParameters _upgradeDataParameters;
        
        private Filter _playerReadyToUpgradeFilter;
        
        private Stash<ApplyDamageUpgradeComponent> _applyUpgradeStash;
        private Stash<DamageComponent> _damageStash;
        
        public World World { get; set; }

        public UpgradeDamageSystem(
            IUpgradeDataParameters upgradeDataParameters
        )
        {
            _upgradeDataParameters = upgradeDataParameters;
        }

        public void OnAwake()
        {
            _playerReadyToUpgradeFilter = World.Filter
                .With<PlayerComponent>()
                .With<ApplyDamageUpgradeComponent>()
                .Build();
            
            
            _applyUpgradeStash = World.GetStash<ApplyDamageUpgradeComponent>();
            _damageStash = World.GetStash<DamageComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var playerEntity in _playerReadyToUpgradeFilter)
            {
                var skillLevel = _applyUpgradeStash.Get(playerEntity).Value;
                var damage = _damageStash.Get(playerEntity).Value;
                var increaseValue = _upgradeDataParameters.MaxLevels[EUpgradeType.Damage].IncreaseDelta * skillLevel;
                _damageStash.Set(playerEntity, new DamageComponent(){ Value = increaseValue + damage });
                DebugUtility.Log($"Upgrade damage", UtilsColors.NotificationColor);
                _applyUpgradeStash.Remove(playerEntity);
            }
        }

        public void Dispose()
        {
        }
    }
}