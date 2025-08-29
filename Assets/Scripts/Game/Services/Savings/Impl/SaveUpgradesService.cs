using Core.Dao;
using Game.Utils.UpgradeData;

namespace Game.Services.Savings.Impl
{
    public class SaveUpgradesService : ISaveUpgradesService
    {
        private readonly IDao<UpgradeSaveData> _upgradeData;

        public SaveUpgradesService(
            IDao<UpgradeSaveData> upgradeData
        )
        {
            _upgradeData = upgradeData;
        }
        
        public void Save(UpgradeSaveData data)
        {
            _upgradeData.Save(data);
        }

        public UpgradeSaveData Load()
        {
            return _upgradeData.Load();
        }
    }
}