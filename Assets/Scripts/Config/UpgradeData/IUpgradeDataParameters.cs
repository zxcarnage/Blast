using System.Collections.Generic;
using Utils.UI;
using Utils.Upgrade;

namespace Config.UpgradeData
{
    public interface IUpgradeDataParameters
    {
        IReadOnlyDictionary<EUpgradeType, UpgradeVO> MaxLevels { get; }
    }
}