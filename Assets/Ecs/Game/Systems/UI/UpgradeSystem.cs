using Ecs.Game.Components.Player;
using Ecs.Game.Components.UI;
using Scellecs.Morpeh;
using Utils.DebugUtil;

namespace Ecs.Game.Systems.UI
{
    public sealed class UpgradeSystem : ISystem
    {
        private Filter _upgradeItemFilter;
        private Filter _playerReadyToUpgradeFilter;
        private Stash<ApplyUpgradeComponent> _applyUpgradeStash;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _playerReadyToUpgradeFilter = World.Filter
                .With<PlayerComponent>()
                .With<ApplyUpgradeComponent>()
                .Build();
            
            _applyUpgradeStash = World.GetStash<ApplyUpgradeComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var playerEntity in _playerReadyToUpgradeFilter)
            {
                DebugUtility.Log($"Upgrade", UtilsColors.NotificationColor);
                
                _applyUpgradeStash.Remove(playerEntity);
            }
        }

        public void Dispose()
        {
        }
    }
}