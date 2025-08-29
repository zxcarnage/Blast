using Game.Utils.UpgradeData;

namespace Game.Services.Savings
{
    public interface ISaveUpgradesService
    {
        void Save(UpgradeSaveData data);
        UpgradeSaveData Load();
    }
}