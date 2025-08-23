using Game.Utils;
using Game.Utils.Enemy;
using Game.Views.Enemy;

namespace Game.Services.Pool.Enemy
{
    public interface IEnemyPool
    {
        EnemyView SpawnEnemy(EEnemyType enemyType);
        void DespawnEnemy(EEnemyType enemyType, EnemyView enemy);
    }
}