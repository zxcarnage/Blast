using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Utils.UI;

namespace Ecs.Game.Components.UI
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ApplyUpgradeComponent : IComponent
    {
        public EUpgradeType Value;
    }
}