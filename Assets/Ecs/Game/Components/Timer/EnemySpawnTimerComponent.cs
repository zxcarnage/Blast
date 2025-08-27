using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Game.Components.Timer
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct EnemySpawnTimerComponent : IComponent
    {
        public float Value;
    }
}