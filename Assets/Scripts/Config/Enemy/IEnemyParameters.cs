using System.Collections.Generic;
using Config.Utils;
using Game.Utils.Enemy;

namespace Config.Enemy
{
    public interface IEnemyParameters
    {
        IReadOnlyDictionary<EEnemyType, EnemyData> EnemyData { get; }
    }
}