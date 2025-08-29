#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;

namespace Game.Utils.Enemy
{
    [Serializable]
    [HideReferenceObjectPicker]
    public sealed class SerializedEnemyTypeInfos
    {
        [HideLabel] public EnemyMatrix Enemy = new ();

        public static implicit operator EnemyTypeInfo(SerializedEnemyTypeInfos serializedEnemyTypeInfo)
        {
            return new EnemyTypeInfo()
            {
                Enemies = serializedEnemyTypeInfo.Enemy.ToArray(),
            };
        }
    }
}
#endif