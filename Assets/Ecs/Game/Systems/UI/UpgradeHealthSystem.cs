using Config.UpgradeData;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Ecs.Game.Components.UI;
using Ecs.Game.Components.Upgrade;
using Scellecs.Morpeh;
using Utils.DebugUtil;
using Utils.UI;

namespace Ecs.Game.Systems.UI
{
    public sealed class UpgradeHealthSystem : ISystem
    {
        private readonly IUpgradeDataParameters _upgradeDataParameters;
        
        private Filter _playerReadyToUpgradeFilter;
        private Filter _healthbarFilter;
        
        private Stash<ApplyHealthUpgradeComponent> _applyUpgradeStash;
        private Stash<MaxHealthComponent> _maxHealthStash;
        private Stash<HealthComponent> _healthStash;
        private Stash<PlayerHealthbarComponent> _healthbarStash;
        
        public World World { get; set; }

        public UpgradeHealthSystem(
            IUpgradeDataParameters upgradeDataParameters
        )
        {
            _upgradeDataParameters = upgradeDataParameters;
        }

        public void OnAwake()
        {
            _playerReadyToUpgradeFilter = World.Filter
                .With<PlayerComponent>()
                .With<ApplyHealthUpgradeComponent>()
                .Build();
            
            _healthbarFilter = World.Filter
                .With<PlayerHealthbarComponent>()
                .Build();
            
            _applyUpgradeStash = World.GetStash<ApplyHealthUpgradeComponent>();
            _maxHealthStash = World.GetStash<MaxHealthComponent>();
            _healthbarStash = World.GetStash<PlayerHealthbarComponent>();
            _healthStash = World.GetStash<HealthComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var playerEntity in _playerReadyToUpgradeFilter)
            {
                var skillLevel = _applyUpgradeStash.Get(playerEntity).Value;
                foreach (var healthbarEntity in _healthbarFilter)
                {
                    var healthbarView = _healthbarStash.Get(healthbarEntity).Value;
                    var health = _healthStash.Get(playerEntity).Value;
                    var currentMaxHealth = _maxHealthStash.Get(playerEntity).Value;
                    var increaseValue = _upgradeDataParameters.MaxLevels[EUpgradeType.Health].IncreaseDelta * skillLevel;
                    _maxHealthStash.Set(playerEntity, new MaxHealthComponent(){ Value = increaseValue + currentMaxHealth });
                    healthbarView.UpdateView(health);
                    DebugUtility.Log($"Upgrade health", UtilsColors.NotificationColor);
                }
                _applyUpgradeStash.Remove(playerEntity);
            }
        }

        public void Dispose()
        {
        }
    }
}