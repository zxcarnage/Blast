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
    public sealed class UpgradeSpeedSystem : ISystem
    {
        private readonly IUpgradeDataParameters _upgradeDataParameters;
        
        private Filter _playerReadyToUpgradeFilter;
        
        private Stash<ApplySpeedUpgradeComponent> _applyUpgradeStash;
        private Stash<SpeedComponent> _speedStash;
        
        public World World { get; set; }

        public UpgradeSpeedSystem(
            IUpgradeDataParameters upgradeDataParameters
        )
        {
            _upgradeDataParameters = upgradeDataParameters;
        }

        public void OnAwake()
        {
            _playerReadyToUpgradeFilter = World.Filter
                .With<PlayerComponent>()
                .With<ApplySpeedUpgradeComponent>()
                .Build();
            
            
            _applyUpgradeStash = World.GetStash<ApplySpeedUpgradeComponent>();
            _speedStash = World.GetStash<SpeedComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var playerEntity in _playerReadyToUpgradeFilter)
            {
                var skillLevel = _applyUpgradeStash.Get(playerEntity).Value;
                var speed = _speedStash.Get(playerEntity).Value;
                var increaseValue = _upgradeDataParameters.MaxLevels[EUpgradeType.Speed].IncreaseDelta * skillLevel;
                _speedStash.Set(playerEntity, new SpeedComponent(){ Value = increaseValue + speed });
                _applyUpgradeStash.Remove(playerEntity);
            }
        }

        public void Dispose()
        {
        }
    }
}