using Config.UpgradeData;
using Ecs.Game.Components.Player;
using Ecs.Game.Components.Upgrade;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Utils.UI;

namespace Ecs.Game.Systems.UI
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
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
                _applyUpgradeStash.Remove(playerEntity);
            }
        }

        public void Dispose()
        {
        }
    }
}