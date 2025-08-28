using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Utils.UI;

namespace Config.UpgradeData.Impl
{
    public class UpgradeDataParameters : ScriptableObject, IUpgradeDataParameters
    {
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Upgrade", ValueLabel = "Max Level")]
        private Dictionary<EUpgradeType, int> _maxLevels;

        public IReadOnlyDictionary<EUpgradeType, int> MaxLevels => _maxLevels;
    }
}