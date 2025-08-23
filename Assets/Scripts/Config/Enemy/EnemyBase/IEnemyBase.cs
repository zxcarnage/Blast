using System.Collections.Generic;
using Game.Utils;
using Game.Utils.Enemy;
using Game.Views.Enemy;

namespace Config.Enemy.EnemyBase
{
    public interface IEnemyBase
    {
        IReadOnlyDictionary<EEnemyType, IReadOnlyList<EnemyView>> EnemiesVariations { get; }
    }
}