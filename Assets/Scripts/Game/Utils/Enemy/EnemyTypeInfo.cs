using System;
using System.Collections;
using System.Collections.Generic;
using Game.Views.Enemy;

namespace Game.Utils.Enemy
{
    [Serializable]
    public sealed class EnemyTypeInfo : IReadOnlyList<EnemyView>
    {
        public EnemyView[] Enemies;

        public int Count => Enemies.Length;

        public EnemyView this[int index] => Enemies[index];
        
        public IEnumerator<EnemyView> GetEnumerator()
        {
            return (Enemies as IReadOnlyList<EnemyView>).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enemies.GetEnumerator();
        }
    }
}