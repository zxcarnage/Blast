using Game.Utils.Enemy;
using Game.Views.Enemy;
using UnityEngine;

namespace Game.Services.Factory.Enemy
{
    public interface IEnemyFactory
    {
        EnemyView CreateEnemy(EEnemyType enemyType, Vector3 position);
    }
}