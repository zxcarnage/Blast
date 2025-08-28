using System.Collections.Generic;
using Utils.UI;

namespace Config.UpgradeData
{
    public interface IUpgradeDataParameters
    {
        IReadOnlyDictionary<EUpgradeType, int> MaxLevels { get; }
    }
}