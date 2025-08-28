using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Utils.UI;
using Utils.Upgrade;

namespace Config.UpgradeData.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(UpgradeDataParameters), fileName = nameof(UpgradeDataParameters))]
    public class UpgradeDataParameters : SerializedScriptableObject, IUpgradeDataParameters
    {
        [OdinSerialize]
        [DictionaryDrawerSettings(KeyLabel = "Upgrade", ValueLabel = "Max Level")]
        private Dictionary<EUpgradeType, UpgradeVO> _maxLevels;

        public IReadOnlyDictionary<EUpgradeType, UpgradeVO> MaxLevels => _maxLevels;
    }
}